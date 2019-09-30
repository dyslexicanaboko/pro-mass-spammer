using System.Collections.Generic;
using ProMassSpammer.Data.Entities;

namespace ProMassSpammer.Core.Transmission
{
    public class TransmissionBySms
        : TransmissionBase
    {
        public TransmissionBySms(MassCommunication massCommunicaton)
            : base(massCommunicaton)
        {

        }

        public override List<SendResult> Send()
        {
            throw new System.NotImplementedException();
        }
    }
}
