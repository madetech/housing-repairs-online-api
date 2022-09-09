using HousingRepairsOnlineApi.Domain;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;

namespace HousingRepairsOnlineApi.Data;

public class RepairContextFactory : IDesignTimeDbContextFactory<RepairContext>
{
    public RepairContext CreateDbContext(string[] args)
    {
        var optionsBuilder = new DbContextOptionsBuilder<RepairContext>();
        optionsBuilder.UseNpgsql("Host=localhost;Username=housing_repairs;Password=password;Database=repairs")
            .UseSnakeCaseNamingConvention();

        return new RepairContext(optionsBuilder.Options);
    }
}
