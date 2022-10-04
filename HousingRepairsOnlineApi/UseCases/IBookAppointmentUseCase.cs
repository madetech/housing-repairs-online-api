using System.Threading.Tasks;
using HousingRepairsOnlineApi.Domain;

namespace HousingRepairsOnlineApi.UseCases;

public interface IBookAppointmentUseCase
{
    Task Execute(Repair repair);
}
