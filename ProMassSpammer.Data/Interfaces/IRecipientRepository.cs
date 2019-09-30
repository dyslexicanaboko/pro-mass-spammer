using ProMassSpammer.Data.Entities;
using System.Collections.Generic;

namespace ProMassSpammer.Data.Interfaces
{
    public interface IRecipientRepository
    {
        void UpdateStatuses(IList<Recipient> results);

        Recipient GetById(int recipientId);
    }
}
