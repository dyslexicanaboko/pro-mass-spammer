using ProMassSpammer.Data.Entities;
using System;

namespace ProMassSpammer.Core.Exceptions
{
    public class NoRecipientsFoundException 
        : Exception
    {
        public NoRecipientsFoundException(MassCommunication massComm)
            : base(GetMessage(massComm))
        {
        }

        private static string GetMessage(MassCommunication massComm)
        {
            var msg = $"Mass Communication ID {massComm.MassCommunicationId} - Does not have any recipients to process! 0x201909011427";

            return msg;
        }
    }
}