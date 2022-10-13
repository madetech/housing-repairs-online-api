using System.Threading.Tasks;
using HousingRepairsOnlineApi.Domain;
using HousingRepairsOnlineApi.Helpers;

namespace HousingRepairsOnlineApi.Gateways;

public class PostgresGateway : IRepairStorageGateway
{
    private readonly RepairContext context;
    private readonly IIdGenerator idGenerator;

    public PostgresGateway(IIdGenerator idGenerator, RepairContext context)
    {
        this.idGenerator = idGenerator;
        this.context = context;
    }

    public async Task<Repair> AddRepair(Repair repair)
    {
        context.Repairs.Add(repair);
        var result = await context.SaveChangesAsync();

        return repair;
    }
}
