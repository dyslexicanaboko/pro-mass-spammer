using MailKit.Net.Smtp;
using MimeKit;
using MimeKit.Text;
using ProMassSpammer.Core.Configuration;

namespace ProMassSpammer.Core.Transmission.Smtp
{
    //https://dotnetcoretutorials.com/2017/11/02/using-mailkit-send-receive-email-asp-net-core/
    public class SmtpClientMailKit
        : ISmtpClient
    {
        private readonly ISmtpConfiguration _configuration;

        public SmtpClientMailKit(ISmtpConfiguration configuration)
        {
            _configuration = configuration;
        }

        public void Send(EmailMessage email)
        {
            var from = new MailboxAddress(email.From);
            var to = new MailboxAddress(email.To);

            var message = new MimeMessage();
            message.From.Add(from);
            message.To.Add(to);
 
            message.Subject = email.Subject;
            
            message.Body = new TextPart(TextFormat.Html)
            {
                Text = email.Body
            };
 
            using (var client = new SmtpClient())
            {
                client.Connect(_configuration.Server, _configuration.Port);

                if (_configuration.CredentialsRequired)
                {
                    client.Authenticate(_configuration.Username, _configuration.Password);
                }

                client.Send(message);
 
                client.Disconnect(true);
            }
        }
    }
}
