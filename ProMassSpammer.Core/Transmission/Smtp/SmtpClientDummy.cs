namespace ProMassSpammer.Core.Transmission.Smtp
{
    public class SmtpClientDummy
        : ISmtpClient
    {
        public void Send(EmailMessage mail)
        {
            //Do nothing on purpose
        }
    }
}
