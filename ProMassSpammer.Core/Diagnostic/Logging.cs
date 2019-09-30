using System;
using ProMassSpammer.Data.Entities;
using ProMassSpammer.Data.Interfaces;

namespace ProMassSpammer.Core.Diagnostic
{
    internal static class Logging
    {
        //TODO: This is a really crappy way to do this, but I will keep it this way temporarily
        public static ILogRepository LoggerRepository { get; set; }

        private static void LogErrorPrivate(Exception exception, string message = "", bool addToJournalOnly = false)
        {
            var log = new Log();

            log.Message = "Ex: " + exception.GetType().Name + " ExMsg: " + exception.Message + " Msg: " +
                          (string.IsNullOrWhiteSpace(message) ? string.Empty : message);

            log.Exception = exception.ToString();

            SpamEngine.MassCommStatistics.ServiceTotalLoggedErrors++;
            SpamEngine.MassCommStatistics.AddJournalEntry(log);

            if (addToJournalOnly) return;

            log.Level = @"Error";

            //Need to use a service locator or something for this to work
            LoggerRepository.Add(log);
        }


        internal static void LogError(Exception exception, string message = "")
        {
            LogErrorPrivate(exception, message, false);
        }


        internal static void LogToJournalOnly(Exception exception, string message = "")
        {
            LogErrorPrivate(exception, message, true);
        }
    }
}