using System;

namespace ProMassSpammer.Data.Entities
{
    public class MassCommunicationStatus
        : EntityBase
    {
        public enum MassCommunicationStatusEnum
        {
            Unsent = 0,
            Waiting = 1,
            Processing = 2,
            Sent = 3,
            Error = 4,
            Failure = 5
        }

        public int MassCommunicationStatusId { get; set; }

        public string Name { get; set; }

        public string Description { get; set; }

        public DateTime CreatedOnUtc { get; set; }
    }
}
