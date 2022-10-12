using System;
using Microsoft.Extensions.Logging;

namespace HousingRepairsOnlineApi.Extensions;

public static class LoggerExtensions
{
    private static readonly Action<ILogger,Guid, Exception> SaveRepairMessageDefinition =
        LoggerMessage
            .Define<Guid>(
                LogLevel.Information,
                new EventId(1), "Added repair with id {Id}");

    public static void AfterAddRepair(this ILogger logger,  Guid repairId) =>
        SaveRepairMessageDefinition(logger, repairId, null);

    private static readonly Action<ILogger, Exception> ErrorSavingRepairMessageDefinition =
        LoggerMessage
            .Define(
                LogLevel.Error, 2, "Error saving repair");

    public static void ErrorSavingRepair(this ILogger logger, Exception ex) =>
        ErrorSavingRepairMessageDefinition(logger, ex);

    private static readonly Action<ILogger, string, Exception> ErrorRetrievingAddressesMessageDefinition =
        LoggerMessage
            .Define<string>(
                LogLevel.Error, 3, "Error retrieving addresses with postcode {Pc}");

    public static void ErrorRetrievingAddresses(this ILogger logger, string postcode, Exception ex) =>
        ErrorRetrievingAddresses(logger, postcode, ex);
}
