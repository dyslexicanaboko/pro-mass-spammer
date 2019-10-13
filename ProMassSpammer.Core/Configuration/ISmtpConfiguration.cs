namespace ProMassSpammer.Core.Configuration
{
    public interface ISmtpConfiguration
    {
        string Server { get; }
        int Port { get; }
        bool CredentialsRequired { get; set; }
        string Username { get; set; }
        string Password { get; set; }       
    }   
}
