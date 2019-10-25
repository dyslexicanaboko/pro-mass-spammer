using Microsoft.AspNetCore.SignalR.Client;
using System;

namespace ProMassSpammer.Core.Transmission.PushNotification
{
    public class SignalRClient
        : IPushNotificationClient
    {
        public void Send(Notification notification)
        {
            SendMessageToHub(notification);
        }

        private async void SendMessageToHub(Notification notification)
        {
            try
            {
                var url = "https://localhost:44302";
                //var url = "https://signalrchat20191019115531.azurewebsites.net";
            
                var connection = new HubConnectionBuilder()
                    .WithUrl($"{url}/chatHub")
                    .WithAutomaticReconnect()
                    .Build();

                var t = connection.StartAsync();

                t.Wait();

                await connection.InvokeAsync("SendMessage", "ProMassSpammer!", notification.Message);
            }
            catch (Exception ex)
            {
                if (true) ;
            }
        }
    }
}
