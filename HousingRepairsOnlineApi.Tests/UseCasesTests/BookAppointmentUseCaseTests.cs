using HousingRepairsOnlineApi.Gateways;
using HousingRepairsOnlineApi.UseCases;
using Moq;

namespace HousingRepairsOnlineApi.Tests.UseCasesTests;

public class BookAppointmentUseCaseTests
{
    private const string BookingReference = "bookingReference";
    private const string SorCode = "sorCode";
    private const string LocationId = "locationId";

    private readonly Mock<IAppointmentsGateway> appointmentsGatewayMock;
    private readonly BookAppointmentUseCase systemUnderTest;

    public BookAppointmentUseCaseTests()
    {
        appointmentsGatewayMock = new Mock<IAppointmentsGateway>();
        systemUnderTest = new BookAppointmentUseCase(appointmentsGatewayMock.Object);
    }

    // TODO: Replace the tests?
}
