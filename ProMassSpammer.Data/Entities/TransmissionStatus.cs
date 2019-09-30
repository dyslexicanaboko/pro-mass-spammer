using System;

namespace ProMassSpammer.Data.Entities
{
    public class TransmissionStatus
        : EntityBase
    {
        public enum TransmissionStatusEnum
        {
            Waiting = 0,
            Processing = 1,
            Sent = 2,
            Error = 3,
            FirstAttempt = 4,
            SecondAttempt = 5,
            ThirdAttempt = 6
        }

        public int TransmissionStatusId { get; set; }

        public string Name { get; set; }

        public string Description { get; set; }

        public DateTime CreatedOnUtc { get; set; }
    }
}
