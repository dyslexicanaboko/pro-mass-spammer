namespace ProMassSpammer.Core.Transmission.PushNotification
{
    public interface IPushNotificationClient
    {
        void Send(Notification notification);
    }
}
