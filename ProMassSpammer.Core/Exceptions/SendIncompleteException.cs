using ProMassSpammer.Data.Entities;
using System;

namespace ProMassSpammer.Core.Exceptions
{
    public class SendIncompleteException : Exception
    {
        public SendIncompleteException(MassCommunication massComm, int completed)
            : base(GetMessage(massComm, completed))
        {
        }

        private static string GetMessage(MassCommunication massComm, int completed)
        {
            var msg = $"Mass Communication ID {massComm.MassCommunicationId} - {completed} of {massComm.Recipients.Count} Mass Communication Detail records were generated. 0x201909011811";

            return msg;
        }
    }
}