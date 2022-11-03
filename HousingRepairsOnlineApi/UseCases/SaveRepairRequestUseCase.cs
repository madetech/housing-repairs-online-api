using System.Threading.Tasks;
using HousingRepairsOnlineApi.Domain;
using HousingRepairsOnlineApi.Gateways;
using HousingRepairsOnlineApi.Helpers;

namespace HousingRepairsOnlineApi.UseCases;

public class SaveRepairRequestUseCase : ISaveRepairRequestUseCase
{
    private readonly IRepairStorageGateway repairStorageGateway;
    private readonly ISoREngine sorEngine;

    public SaveRepairRequestUseCase(IRepairStorageGateway repairStorageGateway, ISoREngine sorEngine)

    {
        this.repairStorageGateway = repairStorageGateway;
        this.sorEngine = sorEngine;
    }

    public async Task<Repair> Execute(RepairRequest repairRequest)
    {
        var repair = new Repair
        {
            Address = repairRequest.Address,
            Postcode = repairRequest.Postcode,
            Location = repairRequest.Location,
            ContactDetails = repairRequest.ContactDetails,
            Problem = repairRequest.Problem,
            Issue = repairRequest.Issue,
            ContactPersonNumber = repairRequest.ContactPersonNumber,
            Time = repairRequest.Time,
            Description = new RepairDescription { Text = repairRequest.Description.Text },
            SOR = sorEngine.MapSorCode(
                repairRequest.Location.Value,
                repairRequest.Problem.Value,
                repairRequest.Issue?.Value)
        };

        var savedRequest = await repairStorageGateway.AddRepair(repair);

        return savedRequest;
    }
}
