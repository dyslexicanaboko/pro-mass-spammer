using System.Collections.Generic;
using ProMassSpammer.Core.Exceptions;
using ProMassSpammer.Data.Entities;

namespace ProMassSpammer.Core.Transmission
{
    public class TransmissionService
    {
        private MassCommunication _massCommunication;
        private readonly TransmissionBase _transmission;

        public TransmissionService(MassCommunication massCommunication)
        {
            _massCommunication = massCommunication;

            _transmission = LoadTransmissionMethod(_massCommunication);
        }

        public void PrepareForSend()
        {
            _transmission.PrepareRecipientMessages();
        }

        public List<SendResult> Send()
        {
            var lst = _transmission.Send();

            return lst;
        }

        private static TransmissionBase LoadTransmissionMethod(MassCommunication massComm)
        {
            var supported = true;

            TransmissionBase obj = null;

            switch (massComm.DeliveryMethod)
            {
                case DeliveryMethod.DeliveryMethodEnum.Email:
                    obj = new TransmissionByEmail(massComm);
                    break;
                case DeliveryMethod.DeliveryMethodEnum.Sms:
                    obj = new TransmissionBySms(massComm);
                    break;

                default:
                    supported = false;
                    break;
            }

            if (!supported)
            {
                throw new TransmissionTypeNotSupportedException(massComm.DeliveryMethod);
            }

            return obj;
        }
    }
}
