namespace ProMassSpammer.Core.Configuration
{
    public class SmtpConfiguration 
        : ISmtpConfiguration
    {
        public string Server { get; set; }
        public int Port  { get; set; }
        public bool CredentialsRequired { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }
    }
}
