using System.Threading.Tasks;
using HousingRepairsOnlineApi.Domain;

namespace HousingRepairsOnlineApi.Gateways;

public class PostgresGateway : IRepairStorageGateway
{
    private readonly RepairContext context;

    public PostgresGateway(RepairContext context)
    {
        this.context = context;
    }

    public async Task<Repair> AddRepair(Repair repair)
    {
        context.Repairs.Add(repair);
        var result = await context.SaveChangesAsync();

        return repair;
    }
}
