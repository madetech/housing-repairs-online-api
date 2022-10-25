namespace HousingRepairsOnlineApi.Dtos;

public record SaveRepairResponse
{
    public SaveRepairResponse(string reference, bool govNotifySuccess)
    {
        Reference = reference;
        GovNotifyStatus = govNotifySuccess ? "success" : "failure";
    }
    public string Reference { get; }
    public string GovNotifyStatus { get; } // "success" or "failure"
}

