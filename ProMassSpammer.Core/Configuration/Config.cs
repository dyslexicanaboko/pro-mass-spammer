using System;
using System.Collections.Generic;
using Microsoft.Extensions.Configuration;

//using System.Configuration;
//using System.Linq;

//TODO: ConfigurationManager is not supported in dot net core. Need to use equivalent.
namespace ProMassSpammer.Core.Configuration
{
    public static class Config
    {
        public const string ConnectionStringNameMain = "ConnectionString";
        public const string ConnectionStringNameLogging = "Logging";

        private static string _applicationBaseDirectory;


        private static string _devSmsPhoneNumber;


        public static string ApplicationBaseDirectory =>
            _applicationBaseDirectory ?? (_applicationBaseDirectory = AppDomain.CurrentDomain.BaseDirectory);


        public static string ConnectionString => GetConnectionString(ConnectionStringNameMain);

        public static string ConnectionStringLogging => GetConnectionString(ConnectionStringNameLogging);

        public static string SmsRouterToken => GetRequiredConfigValue("SmsRouterToken");

        public static string SmsRouterBaseUrl => GetRequiredConfigValue("SmsRouterBaseUrl");

        public static string DevSmsPhoneNumber =>
            _devSmsPhoneNumber ?? (_devSmsPhoneNumber = GetOptionalConfigValue("DevSmsPhoneNumber"));


        private static string GetRawConfigValue(string keyName)
        {
            throw new NotImplementedException();

            //return ConfigurationManager.AppSettings[keyName];
        }


        public static string GetRequiredConfigValue(string keyName)
        {
            var strValue = GetRawConfigValue(keyName);

            if (string.IsNullOrWhiteSpace(strValue)) throw new ConfigurationException(keyName);

            return strValue;
        }


        private static bool IsKeyFound(string keyName)
        {
            throw new NotImplementedException();

            //var isFound = ConfigurationManager.AppSettings.AllKeys.Contains(keyName);

            //return isFound;
        }


        public static string GetOptionalConfigValue(string keyName)
        {
            if (!IsKeyFound(keyName)) throw new ConfigurationException(keyName, true);

            return GetRawConfigValue(keyName);
        }


        public static string GetConnectionString(string name)
        {
            throw new NotImplementedException();

            //var strCs = ConfigurationManager.ConnectionStrings[name].ConnectionString;

            //if (string.IsNullOrWhiteSpace(strCs)) throw new ConfigurationException(name);

            //return strCs;
        }


        public static List<object> ValidateConfiguration()
        {
            var lst = new List<object>();


            lst.Add(ConnectionString);
            lst.Add(ConnectionStringLogging);
            lst.Add(SmsRouterBaseUrl);
            lst.Add(SmsRouterToken);
            lst.Add(DevSmsPhoneNumber);

            return lst;
        }

        public static SmtpConfiguration GetSmtpConfig()
        {
            var svc = new Data.Config("SmtpConfiguration.json");

            var c = svc.BuildConfigs();

            var sc = c.GetSection("SmtpConfiguration").Get<SmtpConfiguration>();

            return sc;
        }

        public static SmsConfiguration GetSmsConfig()
        {
            var svc = new Data.Config("SmsConfiguration.json");

            var c = svc.BuildConfigs();

            var sc = c.GetSection("SmsConfiguration").Get<SmsConfiguration>();

            return sc;
        }
    }
}