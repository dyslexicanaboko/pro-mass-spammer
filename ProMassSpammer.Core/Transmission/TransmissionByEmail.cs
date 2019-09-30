using ProMassSpammer.Core.Diagnostic;
using ProMassSpammer.Core.Exceptions;
using ProMassSpammer.Core.Transmission.Smtp;
using ProMassSpammer.Data.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mail;
using System.Threading.Tasks;

namespace ProMassSpammer.Core.Transmission
{
    public class TransmissionByEmail
        : TransmissionBase
    {
        public TransmissionByEmail(MassCommunication massCommunicaton)
            : base(massCommunicaton)
        {

        }

        private SendResult SendMessage(RecipientMessage message)
        {
            var result = new SendResult(message.RecipientReference);
            
            try
            {
                var mail = CreateMailMessage(message);

                var smtp = SpamEngine.Resolver.GetInstance<ISmtpClient>();

                smtp.Send(mail);

                result.IsSuccess = true;
                result.Message = "Email successfully sent";
            }
            catch (InvalidEmailException iee)
            {
                result.IsSuccess = false;
                result.Message = "Invalid email address. 0x201909011443";
                result.Exception = iee;

                Logging.LogToJournalOnly(iee, result.Message);
            }
            catch (Exception ex)
            {
                result.IsSuccess = false;
                result.Message = "Unexpected error while sending Email. 0x201909011444";
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

        private EmailMessage CreateMailMessage(RecipientMessage message)
        {
            if (!IsValidEmailAddress(message.To))
            {
                throw new InvalidEmailException(InvalidEmailException.AddressField.To, message.To);
            }

            var mail = new EmailMessage
            {
                Subject = MassCommunication.Subject,
                Body = MassCommunication.Body,
                To = message.To,
                From = message.From
            };

            return mail;
        }

        private static bool IsValidEmailAddress(string emailAddress)
        {
            //Should probably replace this with RegEx later, but for now here is a hack
            try
            {
                //This will blow up if the email address isn't valid
                var m = new MailAddress(emailAddress);

                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }
    }
}
