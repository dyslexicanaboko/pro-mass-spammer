using ProMassSpammer.Core.Diagnostic;
using ProMassSpammer.Core.Exceptions;
using ProMassSpammer.Core.Transmission.Sms;
using ProMassSpammer.Data.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ProMassSpammer.Core.Transmission
{
    public class TransmissionBySms
        : TransmissionBase
    {
        public TransmissionBySms(MassCommunication massCommunicaton)
            : base(massCommunicaton)
        {

        }

        private SendResult SendMessage(RecipientMessage message)
        {
            var result = new SendResult(message.RecipientReference);
            
            try
            {
                var text = CreateTextMessage(message);

                var sms = SpamEngine.Resolver.GetInstance<ISmsClient>();

                sms.Send(text);

                result.IsSuccess = true;
                result.Message = "SMS successfully sent";
            }
            catch (InvalidEmailException iee)
            {
                result.IsSuccess = false;
                result.Message = "Invalid phone number. 0x201909292330";
                result.Exception = iee;

                Logging.LogToJournalOnly(iee, result.Message);
            }
            catch (Exception ex)
            {
                result.IsSuccess = false;
                result.Message = "Unexpected error while sending SMS. 0x201909292331";
                result.Exception = ex;

                Logging.LogError(ex, result.Message);
            }

            return result;
        }

        public override List<SendResult> Send()
        {
            var lst = new List<Task<SendResult>>(Messages.Count);

            foreach (var message in Messages)
            {
                var t = Task<SendResult>.Factory.StartNew(() => SendMessage(message));

                lst.Add(t);
            }

            //TODO: Figure out what to fix here, not sure what the problem is really
            Task.WaitAll(lst.ToArray());

            var lstResult = lst.Select(x => x.Result).ToList();

            return lstResult;
        }

        private SmsMessage CreateTextMessage(RecipientMessage message)
        {
            //if (!IsValidPhoneNumber(message.To))
            //{
            //    throw new InvalidEmailException(InvalidEmailException.AddressField.To, message.To);
            //}

            var text = new SmsMessage
            {
                Body = MassCommunication.Body,
                To = message.To,
                From = message.From
            };

            return text;
        }
    }
}
