using NUnit.Framework;
using ProMassSpammer.Core.Configuration;
using ProMassSpammer.Core.Transmission.Sms;

namespace ProMassSpammer.Tests
{
    [TestFixture]
    public class SmsTests
    {
        [Test]
        public void SendSmsViaTwilio()
        {
            var config = Config.GetSmsConfig();

            var svc = new SmsTwilioClient(config);

            var msg = new SmsMessage
            {
                From = "+10123456789", //Replace with Twilio phone number
                To = "+10123456789", //Replace with real phone number to test with
                Body = "I like bacon!"
            };

            svc.Send(msg);
        }
    }
}
