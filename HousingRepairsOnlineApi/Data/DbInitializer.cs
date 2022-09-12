using HousingRepairsOnlineApi.Domain;
using Microsoft.EntityFrameworkCore;

namespace HousingRepairsOnlineApi.Data;

public static class DbInitializer
{
    public static void Initialize(RepairContext context)
    {
        context.Database.Migrate();
    }
}
