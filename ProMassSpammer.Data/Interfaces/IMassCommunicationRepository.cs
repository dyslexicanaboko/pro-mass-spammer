using ProMassSpammer.Data.Entities;
using System.Collections.Generic;

namespace ProMassSpammer.Data.Interfaces
{
    public interface IMassCommunicationRepository
    {
        IList<MassCommunication> GetUnprocessedMassCommunications();

        void UpdateStatuses(MassCommunication massComm);
    }
}
