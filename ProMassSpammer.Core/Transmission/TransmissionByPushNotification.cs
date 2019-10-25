using ProMassSpammer.Core.Diagnostic;
using ProMassSpammer.Core.Transmission.PushNotification;
using ProMassSpammer.Data.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ProMassSpammer.Core.Transmission
{
    public class TransmissionByPushNotification
        : TransmissionBase
    {
        private readonly IPushNotificationClient _client;

        public TransmissionByPushNotification(MassCommunication massCommunicaton)
            : base(massCommunicaton)
        {
            _client = SpamEngine.Resolver.GetInstance<IPushNotificationClient>();

            _client.ConnectToHub();
        }

        private SendResult SendMessage(RecipientMessage message)
        {
            var result = new SendResult(message.RecipientReference);
            
            try
            {
                var notification = CreateMessage(message);

                _client.Send(notification);

                result.IsSuccess = true;
                result.Message = "Push Notification successfully sent";
            }
            catch (Exception ex)
            {
                result.IsSuccess = false;
                result.Message = "Unexpected error while sending Push Notification. 0x201910250018";
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

        private Notification CreateMessage(RecipientMessage message)
        {
            var notification = new Notification
            {
                Message = message.Body
            };
            
            //For now sending the body of the message - this could change later
            return notification;
        }
    }
}
