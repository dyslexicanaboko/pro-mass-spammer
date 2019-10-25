using System;

namespace ProMassSpammer.Core.Exceptions
{
    public class InvalidPhoneNumberException 
        : Exception
    {
        public InvalidPhoneNumberException(string phoneNumber)
            : base(GetMessage(phoneNumber))
        {
        }

        private static string GetMessage(string phoneNumber)
        {
            return "Invalid Phone Number\"" + phoneNumber + "\""
                   + (string.IsNullOrWhiteSpace(phoneNumber)
                       ? " Address was null, blank or whitespace."
                       : string.Empty);
        }
    }
}