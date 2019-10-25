using NUnit.Framework;
using ProMassSpammer.Core.Transmission.PushNotification;

namespace ProMassSpammer.Tests
{
    [TestFixture]
    public class PushNotificationTests
    {
        [Test]
        public void Can_join_signalR_hub()
        {
            var n = new Notification { Message = "What up yo!" };

            var svc = new SignalRClient();

            svc.Send(n);
        }
    }
}
