using System;

namespace ProMassSpammer.Core.Configuration
{
    public class ConfigurationException : Exception
    {
        public ConfigurationException(string keyName)
            : this(keyName, false, string.Empty, null)
        {
        }


        public ConfigurationException(string keyName, bool valueOptional)
            : this(keyName, valueOptional, string.Empty, null)
        {
        }


        public ConfigurationException(string keyName, bool valueOptional, string message)
            : this(keyName, valueOptional, message, null)
        {
        }


        public ConfigurationException(string keyName, bool valueOptional, string message, Exception innerException)
            : base(GetMessage(keyName, valueOptional, message), innerException)
        {
        }


        private static string GetMessage(string keyName, bool valueOptional, string additionalInformation)
        {
            var strCondition = valueOptional
                ? "This config is optional, it's value may remain blank, but the key must be present."
                : "The value cannot be blank!";

            var strMessage =
                string.Format(
                    "The key [{0}] was not found or its value was blank. Please add this entry --> <add key=\"{0}\" value=\"\" />\n{1}",
                    keyName, strCondition);

            if (!string.IsNullOrWhiteSpace(additionalInformation))
                strMessage += "\n" + additionalInformation;

            return strMessage;
        }
    }
}