using System;

namespace ProMassSpammer.Core.Exceptions
{
    public class InvalidEmailException 
        : Exception
    {
        public enum AddressField
        {
            From,
            To,
            Cc,
            Bcc
        }

        public InvalidEmailException(AddressField addressField, string emailAddress)
            : base(GetMessage(addressField, emailAddress))
        {
        }

        private static string GetMessage(AddressField addressField, string emailAddress)
        {
            return "Invalid \"" + addressField +
                   "\" Email Address \"" + emailAddress + "\""
                   + (string.IsNullOrWhiteSpace(emailAddress)
                       ? " Address was null, blank or whitespace."
                       : string.Empty);
        }
    }
}