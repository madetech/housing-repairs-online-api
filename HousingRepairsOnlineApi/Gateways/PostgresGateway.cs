using System.Threading.Tasks;
using HousingRepairsOnlineApi.Domain;
using HousingRepairsOnlineApi.Helpers;

namespace HousingRepairsOnlineApi.Gateways;

public class PostgresGateway : IRepairStorageGateway
{
    private readonly IIdGenerator idGenerator;
    private readonly RepairContext context;

    public PostgresGateway(IIdGenerator idGenerator, RepairContext context)
    {
        this.idGenerator = idGenerator;
        this.context = context;
    }

    public async Task<Repair> AddRepair(Repair repair)
    {
        repair.Id = idGenerator.Generate();
        context.Repairs.Add(repair);
        repair = await context.sa;

        return repair;
    }
}