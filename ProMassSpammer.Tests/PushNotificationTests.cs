using NUnit.Framework;
using ProMassSpammer.Core.Configuration;
using ProMassSpammer.Core.Transmission.PushNotification;

namespace ProMassSpammer.Tests
{
    [TestFixture]
    public class PushNotificationTests
    {
        [Test]
        public void Can_join_signalR_hub()
        {
            var config = Config.GetPushNotificationConfig();

            var n = new Notification { Message = "What up yo!" };

            var svc = new SignalRClient(config);

            svc.Send(n);
        }
    }
}
