namespace ProMassSpammer.Core.Transmission.PushNotification
{
    public interface IPushNotificationClient
    {
        void ConnectToHub();

        void Send(Notification notification);
    }
}
