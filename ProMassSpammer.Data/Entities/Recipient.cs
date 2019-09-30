using System;

namespace ProMassSpammer.Data.Entities
{
    public class Recipient
        : EntityBase
    {
        public int RecipientId { get; set; }

        public int MassCommunicationId { get; set; }

        public MassCommunication MassCommunication { get; set; }

        public int TransmissionStatusId { get; set; }

        public TransmissionStatus.TransmissionStatusEnum TransmissionStatus
        {
            get => ConvertToEnum<TransmissionStatus.TransmissionStatusEnum>(TransmissionStatusId);
            set => TransmissionStatusId = Convert.ToInt32(value);
        }

        //TODO: Calling this TransmissionStatusEntity until I figure out how to make the Enum coexist with this object
        public TransmissionStatus TransmissionStatusEntity { get; set; }

        public string TransmissionStatusMessage { get; set; }

        /// <summary>
        /// Depending on what the Delivery Method is set to, this string
        /// can vary in data type. Whether it is an email message, phone
        /// number etc. 
        /// </summary>
        public string ContactString { get; set; }

        public DateTime CreatedOnUtc { get; set; }

        public DateTime? ModifiedOnUtc { get; set; }
    }
}