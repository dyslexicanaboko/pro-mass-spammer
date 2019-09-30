using ProMassSpammer.Core.Configuration;
using Twilio;
using Twilio.Rest.Api.V2010.Account;

namespace ProMassSpammer.Core.Transmission.Sms
{
    public class SmsTwilioClient
        : ISmsClient
    {
        private readonly ISmsConfiguration _configuration;

        public SmsTwilioClient(ISmsConfiguration configuration)
        {
            _configuration = configuration;
        }

        public void Send(SmsMessage sms)
        {
            TwilioClient.Init(_configuration.AccountSid, _configuration.AuthenticationToken);

            var pFrom = new Twilio.Types.PhoneNumber(sms.From);
            var pTo = new Twilio.Types.PhoneNumber(sms.To);

            var message = MessageResource.Create(
                body: sms.Body,
                from: pFrom,
                to: pTo
            );
        }
    }
}
