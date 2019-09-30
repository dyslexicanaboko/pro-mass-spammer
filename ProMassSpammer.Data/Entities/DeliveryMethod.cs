using System;

namespace ProMassSpammer.Data.Entities
{
    public class DeliveryMethod
        : EntityBase
    {
        public enum DeliveryMethodEnum
        {
            Email = 0,
            Sms = 1,
            Im = 2,
            PushNotification = 3
        }
        
        public int DeliveryMethodId { get; set; }

        //No setter on purpose because this is the data driven class representing the enumeration
        public DeliveryMethodEnum DeliveryMethodType => ConvertToEnum<DeliveryMethodEnum>(DeliveryMethodId);

        public string Name { get; set; }

        public string Description { get; set; }

        public bool IsActive { get; set; }

        public DateTime CreatedOnUtc { get; set; }
    }
}