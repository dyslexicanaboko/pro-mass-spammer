namespace ProMassSpammer.Core.Transmission.Sms
{
    public interface ISmsClient
    {
        void Send(SmsMessage sms);
    }
}
