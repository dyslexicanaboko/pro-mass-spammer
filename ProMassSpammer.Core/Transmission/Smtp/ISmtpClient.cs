namespace ProMassSpammer.Core.Transmission.Smtp
{
    public interface ISmtpClient
    {
        void Send(EmailMessage email);
    }
}
