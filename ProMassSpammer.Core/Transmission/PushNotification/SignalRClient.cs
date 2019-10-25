using Microsoft.AspNetCore.SignalR.Client;
using ProMassSpammer.Core.Configuration;

namespace ProMassSpammer.Core.Transmission.PushNotification
{
    public class SignalRClient
        : IPushNotificationClient
    {
        private readonly IPushNotificationConfiguration _configuration;
        private HubConnection _connection;

        public SignalRClient(IPushNotificationConfiguration configuration)
        {
            _configuration = configuration;
        }

        public void ConnectToHub()
        {
            _connection = new HubConnectionBuilder()
                .WithUrl(_configuration.HubUrl)
                .WithAutomaticReconnect()
                .Build();

            var t = _connection.StartAsync();

            t.Wait();
        }

        public void Send(Notification notification)
        {
            SendMessageToHub(notification);
        }

        private async void SendMessageToHub(Notification notification)
        {
            await _connection.InvokeAsync("SendMessage", "ProMassSpammer!", notification.Message);
        }
    }
}
