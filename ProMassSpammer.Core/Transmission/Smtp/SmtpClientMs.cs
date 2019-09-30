using MailKit.Net.Smtp;
using MimeKit;
using MimeKit.Text;
using ProMassSpammer.Core.Configuration;

namespace ProMassSpammer.Core.Transmission.Smtp
{
    //https://dotnetcoretutorials.com/2017/11/02/using-mailkit-send-receive-email-asp-net-core/
    public class SmtpClientMs
        : ISmtpClient
    {
        private readonly ISmtpConfiguration _configuration;

        public SmtpClientMs(ISmtpConfiguration configuration)
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
 
                //client.Authenticate(_emailConfiguration.SmtpUsername, _emailConfiguration.SmtpPassword);
 
                client.Send(message);
 
                client.Disconnect(true);
            }
        }
    }
}
