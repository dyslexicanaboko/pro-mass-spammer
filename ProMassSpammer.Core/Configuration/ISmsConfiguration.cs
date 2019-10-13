namespace ProMassSpammer.Core.Configuration
{
    public interface ISmsConfiguration
    {
        string AccountSid { get; set; }

        string AuthenticationToken { get; set; }
    }
}
