using System;
using System.Net.Http;
using HashidsNet;
using HousingRepairsOnlineApi.Domain;
using HousingRepairsOnlineApi.Gateways;
using HousingRepairsOnlineApi.Helpers;
using HousingRepairsOnlineApi.UseCases;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.OpenApi.Models;
using Notify.Client;

namespace HousingRepairsOnlineApi;

public class Startup
{
    public Startup(IConfiguration configuration)
    {
        Configuration = configuration;
    }

    public IConfiguration Configuration { get; }

    // This method gets called by the runtime. Use this method to add services to the container.
    public void ConfigureServices(IServiceCollection services)
    {
        services.AddSoREngine("SoRConfig.json");
        services.AddHasher(GetEnvironmentVariable("HASHIDS_SALT"));

        services.AddTransient<IRetrieveAddressesUseCase, RetrieveAddressesUseCase>();
        services.AddTransient<IRetrieveAvailableAppointmentsUseCase, RetrieveAvailableAppointmentsUseCase>();
        services.AddTransient<IBookAppointmentUseCase, BookAppointmentUseCase>();

        var addressesApiUrl = GetEnvironmentVariable("ADDRESSES_API_URL");
        var addressesOrganisationId = GetEnvironmentVariable("ADDRESSES_ORGANISATION_ID");
        var schedulingApiUrl = GetEnvironmentVariable("SCHEDULING_API_URL");
        services.AddHttpClient();

        services.AddDbContext<RepairContext>(options =>
        {
            var dbHost = GetEnvironmentVariable("DB_HOST");
            var dbUsername = GetEnvironmentVariable("DB_USERNAME");
            var dbPassword = GetEnvironmentVariable("DB_PASSWORD");
            var dbName = GetEnvironmentVariable("DB_NAME");

            var dbConnectionString = $"Host={dbHost};Username={dbUsername};Password={dbPassword};Database={dbName}";

            options.UseNpgsql(dbConnectionString).UseSnakeCaseNamingConvention();
        });

        services.AddDatabaseDeveloperPageExceptionFilter();

        services.AddTransient<IAddressGateway, AddressGateway>(s =>
            new AddressGateway(addressesApiUrl, addressesOrganisationId));

        services.AddTransient<IAppointmentsGateway, AppointmentsGateway>(s =>
        {
            var httpClient = s.GetService<HttpClient>();
            httpClient.BaseAddress = new Uri(schedulingApiUrl);
            var hasher = s.GetService<IHashids>();
            return new AppointmentsGateway(httpClient, hasher);
        });

        var notifyApiKey = GetEnvironmentVariable("GOV_NOTIFY_KEY");

        services.AddTransient<INotifyGateway, NotifyGateway>(s =>
            {
                var notifyClient = new NotificationClient(notifyApiKey);
                return new NotifyGateway(notifyClient);
            }
        );
        var smsConfirmationTemplateId = GetEnvironmentVariable("CONFIRMATION_SMS_NOTIFY_TEMPLATE_ID");
        var emailConfirmationTemplateId = GetEnvironmentVariable("CONFIRMATION_EMAIL_NOTIFY_TEMPLATE_ID");

        services.AddTransient<ISendAppointmentConfirmationSmsUseCase, SendAppointmentConfirmationSmsUseCase>(s =>
        {
            var notifyGateway = s.GetService<INotifyGateway>();
            return new SendAppointmentConfirmationSmsUseCase(notifyGateway, smsConfirmationTemplateId);
        });

        services.AddTransient<ISendAppointmentConfirmationEmailUseCase, SendAppointmentConfirmationEmailUseCase>(
            s =>
            {
                var notifyGateway = s.GetService<INotifyGateway>();
                return new SendAppointmentConfirmationEmailUseCase(notifyGateway, emailConfirmationTemplateId);
            });

        services.AddTransient<IAppointmentConfirmationSender, AppointmentConfirmationSender>();
        services.AddTransient<ISaveRepairRequestUseCase, SaveRepairRequestUseCase>();
        services.AddTransient<IRepairStorageGateway, PostgresGateway>();

        services.AddControllers();
        services.AddSwaggerGen(c =>
        {
            c.SwaggerDoc("v1", new OpenApiInfo { Title = "HousingRepairsOnlineApi", Version = "v1" });
        });

        services.AddHealthChecks();
    }

    // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
    public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
    {
        if (env.IsDevelopment())
        {
            app.UseDeveloperExceptionPage();
            app.UseSwagger();
            app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "HousingRepairsOnlineApi v1"));
        }

        app.UseRouting();

        app.UseEndpoints(endpoints =>
        {
            endpoints.MapHealthChecks("/health");
            endpoints.MapControllers();
        });
    }

    private static string GetEnvironmentVariable(string name)
    {
        return Environment.GetEnvironmentVariable(name) ??
               throw new InvalidOperationException(
                   $"Incorrect configuration: '{name}' environment variable must be set");
    }
}
