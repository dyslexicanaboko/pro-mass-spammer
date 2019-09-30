using ProMassSpammer.Core.Diagnostic;
using System;
using System.Collections.Generic;
//using System.ServiceModel;

//TODO: Wcf dot net core required here
namespace ProMassSpammer.Core.Services
{
    //[ServiceBehavior(InstanceContextMode = InstanceContextMode.Single,
    //    ConcurrencyMode = ConcurrencyMode.Multiple)]
    public class ConsoleService : IConsoleService
    {
        public string Ping()
        {
            return "Pong @ " + DateTime.Now;
        }

        public Statistic RuntimeStatistics()
        {
            return SpamEngine.MassCommStatistics;
        }

        public List<string> GetJournalEntries(int entries)
        {
            return SpamEngine.MassCommStatistics.GetJournalEntries(entries);
        }
    }
}