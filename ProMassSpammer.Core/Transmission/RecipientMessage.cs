using ProMassSpammer.Data.Entities;

namespace ProMassSpammer.Core.Transmission
{
    public class RecipientMessage
    {
        public RecipientMessage(MassCommunication massCommunicaton, Recipient recipient)
        {
            RecipientReference = recipient;

            Subject = massCommunicaton.Subject;

            Body = massCommunicaton.Body;

            From = massCommunicaton.From;

            To = recipient.ContactString;
        }

        public Recipient RecipientReference { get; set; }

        public string Subject { get; set; }

        public string Body { get; set; }

        public string From { get; set; }

        public string To { get; set; }
    }
}
