using System;
using System.Collections.Generic;

namespace ProMassSpammer.Data.Entities
{
    public class MassCommunication
        : EntityBase
    {
        public int MassCommunicationId { get; set; }

        public string Title { get; set; }

        public int MassCommunicationStatusId { get; set; }

        public MassCommunicationStatus.MassCommunicationStatusEnum MassCommunicationStatus
        {
            get => ConvertToEnum<MassCommunicationStatus.MassCommunicationStatusEnum>(MassCommunicationStatusId);
            set => MassCommunicationStatusId = Convert.ToInt32(value);
        }

        //TODO: Calling this MassCommunicationStatusEntity until I figure out how to make the Enum coexist with this object
        public MassCommunicationStatus MassCommunicationStatusEntity { get; set; }

        public string StatusMessage { get; set; }

        public string Catalyst { get; set; }

        public int DeliveryMethodId { get; set; }

        public DeliveryMethod.DeliveryMethodEnum DeliveryMethod
        {
            get => ConvertToEnum<DeliveryMethod.DeliveryMethodEnum>(DeliveryMethodId);
            set => DeliveryMethodId = Convert.ToInt32(value);
        }

        //TODO: Calling this DeliveryMethodEntity until I figure out how to make the Enum coexist with this object
        public DeliveryMethod DeliveryMethodEntity { get; set; }

        public string Subject { get; set; }

        public string Body { get; set; }

        public string From { get; set; }

        public DateTime CreatedOnUtc { get; set; }

        public DateTime? ModifiedOnUtc { get; set; }
        
        public List<Recipient> Recipients { get; set; }
    }
}