using HousingRepairsOnlineApi.Domain;

namespace HousingRepairsOnlineApi.Data;

public static class DbInitializer
{
    public static void Initialize(RepairContext context)
    {
        context.Database.EnsureCreated();
    }
}