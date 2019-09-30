using System;
using ProMassSpammer.Data.Entities;

namespace ProMassSpammer.Core
{
    public class SendResult
    {
        public SendResult(Recipient recipient)
        {
            RecipientReference = recipient;

            IsSuccess = false;
            Exception = null;
            Message = string.Empty;
        }

        public Recipient RecipientReference { get; set; }

        public bool IsSuccess { get; set; }

        public string Message { get; set; }

        public Exception Exception { get; set; }

        public override string ToString()
        {
            if (IsSuccess)
            {
                return $"OK - {Message}";
            }

            var msg = $"FAIL - Ex: {Exception.GetType().Name} ExMsg: {Exception.Message} Msg: {Message}";

            return msg;
        }
    }
}