using System.Threading.Tasks;
using HousingRepairsOnlineApi.Domain;
using HousingRepairsOnlineApi.Gateways;

namespace HousingRepairsOnlineApi.UseCases;

public class BookAppointmentUseCase : IBookAppointmentUseCase
{
    private readonly IAppointmentsGateway appointmentsGateway;

    public BookAppointmentUseCase(IAppointmentsGateway appointmentsGateway)
    {
        this.appointmentsGateway = appointmentsGateway;
    }

    public async Task Execute(Repair repair)
    {
        await appointmentsGateway.BookAppointment(repair);
    }
}
