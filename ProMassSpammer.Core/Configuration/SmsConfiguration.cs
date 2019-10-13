namespace ProMassSpammer.Core.Configuration
{
    public class SmsConfiguration
        : ISmsConfiguration
    {
        public string AccountSid { get; set; }

        public string AuthenticationToken { get; set; }
    }
}
