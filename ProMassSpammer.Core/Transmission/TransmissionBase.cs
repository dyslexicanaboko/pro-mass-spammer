using System.Collections.Generic;
using System.Linq;
using ProMassSpammer.Data.Entities;

namespace ProMassSpammer.Core.Transmission
{
    public abstract class TransmissionBase
    {
        protected MassCommunication MassCommunication { get; set; }

        protected List<RecipientMessage> Messages { get; set; }

        protected TransmissionBase(MassCommunication massCommunication)
        {
            MassCommunication = massCommunication;
        }

        public virtual void PrepareRecipientMessages()
        {
            var lst = MassCommunication
                .Recipients
                .Select(r => new RecipientMessage(MassCommunication, r))
                .ToList();

            Messages = lst;
        }

        public abstract List<SendResult> Send();
    }
}
