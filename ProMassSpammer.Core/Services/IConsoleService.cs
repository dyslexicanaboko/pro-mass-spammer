using ProMassSpammer.Core.Diagnostic;
using System.Collections.Generic;
//using System.ServiceModel;

//TODO: Wcf dot net core required here
namespace ProMassSpammer.Core.Services
{
    //[ServiceContract]
    public interface IConsoleService
    {
        //[OperationContract]
        string Ping();

        //[OperationContract]
        Statistic RuntimeStatistics();

        //[OperationContract]
        List<string> GetJournalEntries(int entries);
    }
}