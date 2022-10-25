using System;
using Microsoft.Extensions.Logging;

namespace HousingRepairsOnlineApi.Extensions;

public static class LoggerExtensions
{
    private static readonly Action<ILogger, int, Exception> SaveRepairMessageDefinition =
        LoggerMessage
            .Define<int>(
                LogLevel.Information,
                new EventId(1), "Added repair with id {Id}");

    private static readonly Action<ILogger, Exception> ErrorSavingRepairMessageDefinition =
        LoggerMessage
            .Define(
                LogLevel.Error, 2, "Error saving repair");

    private static readonly Action<ILogger, string, Exception> ErrorRetrievingAddressesMessageDefinition =
        LoggerMessage
            .Define<string>(
                LogLevel.Error, 3, "Error retrieving addresses with postcode {Pc}");

    private static readonly Action<ILogger, Exception> ErrorRetrievingAppointmentsDefinition =
        LoggerMessage
            .Define(
                LogLevel.Error, 4, "Failed to fetch appointments");

    private static readonly Action<ILogger, string, Exception> ErrorSendingGovNotifyNotificationDefinition =
        LoggerMessage
            .Define<string>(
                LogLevel.Error, 5, "Failed to send GovNotify notification for reference {Ref}");

    public static void AfterAddRepair(this ILogger logger, int repairId)
    {
        SaveRepairMessageDefinition(logger, repairId, null);
    }

    public static void ErrorSavingRepair(this ILogger logger, Exception ex)
    {
        ErrorSavingRepairMessageDefinition(logger, ex);
    }

    public static void ErrorRetrievingAddresses(this ILogger logger, string postcode, Exception ex)
    {
        ErrorRetrievingAddressesMessageDefinition(logger, postcode, ex);
    }

    public static void ErrorRetrievingAppointments(this ILogger logger, Exception ex)
    {
        ErrorRetrievingAppointmentsDefinition(logger, ex);
    }

    public static void ErrorSendingGovNotifyNotification(this ILogger logger, string reference, Exception ex)
    {
        ErrorSendingGovNotifyNotificationDefinition(logger, reference, ex);
    }
}
