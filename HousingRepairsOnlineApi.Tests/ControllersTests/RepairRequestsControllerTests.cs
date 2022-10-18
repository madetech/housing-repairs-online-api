using System;
using System.Threading.Tasks;
using FluentAssertions;
using HashidsNet;
using HousingRepairsOnlineApi.Controllers;
using HousingRepairsOnlineApi.Domain;
using HousingRepairsOnlineApi.Helpers;
using HousingRepairsOnlineApi.UseCases;
using Microsoft.Extensions.Logging.Abstractions;
using Moq;
using Xunit;

namespace HousingRepairsOnlineApi.Tests.ControllersTests;

public class RepairRequestsControllerTests : ControllerTests
{
    private readonly Mock<IAppointmentConfirmationSender> appointmentConfirmationSender;
    private readonly Mock<IBookAppointmentUseCase> bookAppointmentUseCaseMock;
    private readonly Mock<IInternalEmailSender> internalEmailSender;
    private readonly RepairAddress repairAddress = new() { LocationId = "Location Id" };

    private readonly RepairAvailability repairAvailability = new()
    {
        Display = "Displayed Time",
        StartDateTime = new DateTime(2022, 01, 01, 8, 0, 0),
        EndDateTime = new DateTime(2022, 01, 01, 12, 0, 0)
    };

    private readonly Mock<ISaveRepairRequestUseCase> saveRepairRequestUseCaseMock;
    private readonly RepairController systemUnderTest;

    public RepairRequestsControllerTests()
    {
        saveRepairRequestUseCaseMock = new Mock<ISaveRepairRequestUseCase>();
        bookAppointmentUseCaseMock = new Mock<IBookAppointmentUseCase>();
        appointmentConfirmationSender = new Mock<IAppointmentConfirmationSender>();
        systemUnderTest = new RepairController(NullLogger<RepairController>.Instance,
            saveRepairRequestUseCaseMock.Object,
            appointmentConfirmationSender.Object, bookAppointmentUseCaseMock.Object, new Hashids("salt"));
    }

    [Fact]
    public async Task TestEndpoint()
    {
        var repairRequest = new RepairRequest
        {
            ContactDetails = new RepairContactDetails { Value = "07465087654" },
            Address = new RepairAddress { Display = "address", LocationId = "uprn" },
            Description = new RepairDescriptionRequest { Text = "repair description", Base64Img = "image" },
            Location = new RepairLocation { Value = "location" },
            Problem = new RepairProblem { Value = "problem" },
            Issue = new RepairIssue { Value = "issue" }
        };

        var repair = new Repair
        {
            Id = 1,
            ContactDetails = new RepairContactDetails { Value = "07465087654" },
            Address = new RepairAddress { Display = "address", LocationId = "uprn" },
            Description =
                new RepairDescription { Text = "repair description", Base64Image = "image", PhotoUrl = "x/Url.png" },
            Location = new RepairLocation { Value = "location" },
            Problem = new RepairProblem { Value = "problem" },
            Issue = new RepairIssue { Value = "issue" },
            SOR = "sor",
            Time = repairAvailability
        };

        saveRepairRequestUseCaseMock.Setup(x => x.Execute(It.IsAny<RepairRequest>())).ReturnsAsync(repair);

        var result = await systemUnderTest.SaveRepair(repairRequest);

        GetStatusCode(result).Should().Be(200);


        saveRepairRequestUseCaseMock.Verify(x => x.Execute(repairRequest), Times.Once);
    }

    [Fact]
    public async Task ReturnsErrorWhenFailsToSave()
    {
        var repairRequest = new RepairRequest();

        saveRepairRequestUseCaseMock.Setup(x => x.Execute(It.IsAny<RepairRequest>())).Throws<Exception>();

        var result = await systemUnderTest.SaveRepair(repairRequest);

        GetStatusCode(result).Should().Be(500);
        saveRepairRequestUseCaseMock.Verify(x => x.Execute(repairRequest), Times.Once);
    }

    [Fact]
    public async Task GivenEmailContact_WhenRepair_ThenSendAppointmentConfirmationEmailUseCaseIsCalled()
    {
        //Arrange
        RepairRequest repairRequest = new RepairRequest
        {
            ContactDetails = new RepairContactDetails
            {
                Type = "email",
                Value = "dr.who@tardis.com"
            },
            Time = new RepairAvailability
            {
                Display = "Displayed Time"
            }
        };
        var repair = new Repair()
        {
            Id = 1,
            ContactDetails = new RepairContactDetails
            {
                Type = "email",
                Value = "dr.who@tardis.com"
            },
            Time = repairAvailability,
            Address = repairAddress
        };
        saveRepairRequestUseCaseMock.Setup(x => x.Execute(repairRequest)).ReturnsAsync(repair);

        //Assert
        await systemUnderTest.SaveRepair(repairRequest);

        //Act
        appointmentConfirmationSender.Verify(x => x.Execute(repair), Times.Once);
    }

    [Fact]
    public async Task GivenSmsContact_WhenRepair_ThenSendAppointmentConfirmationSmsUseCaseIsCalled()
    {
        //Arrange
        var repairRequest = new RepairRequest()
        {
            ContactDetails = new RepairContactDetails
            {
                Type = "text",
                Value = "0765374057"
            },
            Time = new RepairAvailability
            {
                Display = "Displayed Time"
            }
        };
        var repair = new Repair
        {
            Id = 1,
            ContactDetails = new RepairContactDetails
            {
                Type = "text",
                Value = "0765374057"
            },
            Time = repairAvailability,
            Address = repairAddress
        };

        saveRepairRequestUseCaseMock.Setup(x => x.Execute(repairRequest)).ReturnsAsync(repair);

        //Act
        await systemUnderTest.SaveRepair(repairRequest);

        //Assert
        appointmentConfirmationSender.Verify(x => x.Execute(repair), Times.Once);
    }
}
