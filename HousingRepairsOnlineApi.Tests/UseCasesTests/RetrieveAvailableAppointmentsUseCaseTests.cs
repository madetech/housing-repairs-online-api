﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FluentAssertions;
using HACT.Dtos;
using HousingRepairsOnlineApi.Gateways;
using HousingRepairsOnlineApi.Helpers;
using HousingRepairsOnlineApi.UseCases;
using Moq;
using Xunit;

namespace HousingRepairsOnlineApi.Tests.UseCasesTests;

public class RetrieveAvailableAppointmentsUseCaseTests
{
    private const string kitchen = "kitchen";
    private const string cupboards = "cupboards";
    private const string doorHangingOff = "doorHangingOff";
    private readonly Mock<IAppointmentsGateway> appointmentsGatewayMock;
    private readonly Mock<ISoREngine> sorEngineMock;
    private readonly RetrieveAvailableAppointmentsUseCase systemUnderTest;

    public RetrieveAvailableAppointmentsUseCaseTests()
    {
        sorEngineMock = new Mock<ISoREngine>();
        appointmentsGatewayMock = new Mock<IAppointmentsGateway>();
        systemUnderTest =
            new RetrieveAvailableAppointmentsUseCase(appointmentsGatewayMock.Object, sorEngineMock.Object);
    }

    [Theory]
    [MemberData(nameof(InvalidArgumentTestData))]
#pragma warning disable xUnit1026
    public async void GivenAnInvalidRepairLocation_WhenExecute_ThenExceptionIsThrown<T>(T exception,
        string repairLocation) where T : Exception
#pragma warning restore xUnit1026
    {
        // Arrange
        var systemUnderTest =
            new RetrieveAvailableAppointmentsUseCase(appointmentsGatewayMock.Object, sorEngineMock.Object);

        // Act
        Func<Task> act = async () => await systemUnderTest.Execute(repairLocation, cupboards, doorHangingOff, "A UPRN");

        // Assert
        await act.Should().ThrowExactlyAsync<T>();
    }

    [Theory]
    [MemberData(nameof(InvalidArgumentTestData))]
#pragma warning disable xUnit1026
    public async void GivenAnInvalidRepairProblem_WhenExecute_ThenExceptionIsThrown<T>(T exception,
        string repairProblem) where T : Exception
#pragma warning restore xUnit1026
    {
        // Arrange
        var systemUnderTest =
            new RetrieveAvailableAppointmentsUseCase(appointmentsGatewayMock.Object, sorEngineMock.Object);

        // Act
        Func<Task> act = async () => await systemUnderTest.Execute(kitchen, repairProblem, doorHangingOff, "A UPRN");

        // Assert
        await act.Should().ThrowExactlyAsync<T>();
    }

    [Theory]
    [MemberData(nameof(InvalidArgumentTestData))]
#pragma warning disable xUnit1026
    public async void GivenValidRepairIssue_WhenExecute_ThenNoExceptionIsThrown<T>(T exception, string repairIssue)
        where T : Exception
#pragma warning restore xUnit1026
    {
        // Arrange
        var systemUnderTest =
            new RetrieveAvailableAppointmentsUseCase(appointmentsGatewayMock.Object, sorEngineMock.Object);

        // Act
        Func<Task> act = async () => await systemUnderTest.Execute(kitchen, cupboards, repairIssue, "A UPRN");

        // Assert
        await act.Should().NotThrowAsync<T>();
    }

    [Theory]
    [MemberData(nameof(InvalidArgumentTestData))]
#pragma warning disable xUnit1026
    public async void GivenAnInvalidUprn_WhenExecute_ThenExceptionIsThrown<T>(T exception, string uprn)
        where T : Exception
#pragma warning restore xUnit1026
    {
        // Arrange
        var systemUnderTest =
            new RetrieveAvailableAppointmentsUseCase(appointmentsGatewayMock.Object, sorEngineMock.Object);

        // Act
        Func<Task> act = async () => await systemUnderTest.Execute(kitchen, cupboards, doorHangingOff, uprn);

        // Assert
        await act.Should().ThrowExactlyAsync<T>();
    }

    public static IEnumerable<object[]> InvalidArgumentTestData()
    {
        yield return new object[] { new ArgumentNullException(), null };
        yield return new object[] { new ArgumentException(), "" };
        yield return new object[] { new ArgumentException(), " " };
    }

    [Fact]
#pragma warning disable xUnit1026
    public async void GivenANullFromDate_WhenExecute_ThenExceptionIsNotThrown()
#pragma warning restore xUnit1026
    {
        // Arrange
        var systemUnderTest =
            new RetrieveAvailableAppointmentsUseCase(appointmentsGatewayMock.Object, sorEngineMock.Object);

        // Act
        Func<Task> act = async () => await systemUnderTest.Execute(kitchen, cupboards, doorHangingOff, "Location ID");

        // Assert
        await act.Should().NotThrowAsync<ArgumentNullException>();
    }

    [Fact]
    public async void GivenRepairParameters_WhenExecute_ThenMapSorCodeIsCalled()
    {
        await systemUnderTest.Execute(kitchen, cupboards, doorHangingOff, "uprn");
        sorEngineMock.Verify(x => x.MapSorCode(kitchen, cupboards, doorHangingOff), Times.Once);
    }

    [Fact]
    public async void GivenRepairParameters_WhenExecute_ThenGetAvailableAppointmentsGatewayIsCalled()
    {
        var repairCode = "N373049";
        sorEngineMock.Setup(x => x.MapSorCode(kitchen, cupboards, doorHangingOff)).Returns(repairCode);
        await systemUnderTest.Execute(kitchen, cupboards, doorHangingOff, "uprn");
        appointmentsGatewayMock.Verify(x => x.GetAvailableAppointments(repairCode, "uprn", null), Times.Once);
    }

    [Fact]
    public async void GivenRepairParameters_WhenExecute_AppointmentTimeAreReturned()
    {
        var repairCode = "N373049";
        var startTime = DateTime.Today.AddHours(8);
        var endTime = DateTime.Today.AddHours(12);

        sorEngineMock.Setup(x => x.MapSorCode(kitchen, cupboards, doorHangingOff)).Returns(repairCode);

        appointmentsGatewayMock.Setup(x => x.GetAvailableAppointments(repairCode, "uprn", null))
            .ReturnsAsync(new List<Appointment>
            {
                new()
                {
                    Reference = new Reference { ID = "1" },
                    TimeOfDay = new TimeOfDay { EarliestArrivalTime = startTime, LatestArrivalTime = endTime }
                }
            });

        var actual = await systemUnderTest.Execute(kitchen, cupboards, doorHangingOff, "uprn");
        var actualAddress = actual.First();

        Assert.Equal(startTime, actualAddress.StartTime);
        Assert.Equal(endTime, actualAddress.EndTime);
    }
}
