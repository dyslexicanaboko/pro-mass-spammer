using System;
using ProMassSpammer.Data.Entities;

namespace ProMassSpammer.Core.Exceptions
{
    public class TransmissionTypeNotSupportedException : Exception
    {
        public TransmissionTypeNotSupportedException(
            DeliveryMethod.DeliveryMethodEnum deliveryMethod)
            : base(GetMessage(deliveryMethod))
        {
        }

        private static string GetMessage(DeliveryMethod.DeliveryMethodEnum deliveryMethod)
        {
            var msg = $"Delivery method not supported \"{deliveryMethod}\" is not supported at this time. 0x201909011347";

            return msg;
        }
    }
}