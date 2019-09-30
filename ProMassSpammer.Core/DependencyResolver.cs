using Microsoft.Extensions.Configuration;
using ProMassSpammer.Core.Configuration;
using ProMassSpammer.Core.Transmission.Sms;
using ProMassSpammer.Core.Transmission.Smtp;
using SimpleInjector;
using Config = ProMassSpammer.Data.Config;

namespace ProMassSpammer.Core
{
    public class DependencyResolver
    {
        private readonly Container _container;

        public DependencyResolver()
        {
            _container = new Container();
        }

        /// <summary>
        /// Use this constructor only for debug
        /// </summary>
        /// <param name="container">Container is only passed for debug.</param>
        public DependencyResolver(Container container)
        {
            _container = container;
        }

        public void Bootstrap()
        {
            _container.Register<ISmtpConfiguration>(GetSmtpConfig, Lifestyle.Singleton);
            _container.Register<ISmtpClient, SmtpClientMs>(Lifestyle.Transient);

            _container.Register<ISmsConfiguration>(GetSmsConfig, Lifestyle.Singleton);
            _container.Register<ISmsClient, SmsTwilioClient>(Lifestyle.Transient);

            _container.Verify();
        }

        private SmtpConfiguration GetSmtpConfig()
        {
            var svc = new Config("SmtpConfiguration.json");

            var c = svc.BuildConfigs();

            var sc = c.GetSection("SmtpConfiguration").Get<SmtpConfiguration>();

            return sc;
        }

        private SmsConfiguration GetSmsConfig()
        {
            var svc = new Config("SmsConfiguration.json");

            var c = svc.BuildConfigs();

            var sc = c.GetSection("SmsConfiguration").Get<SmsConfiguration>();

            return sc;
        }

        public T GetInstance<T>()
        {
            var t = typeof(T);

            var svc = (T)_container.GetInstance(t);

            return svc;
        }
    }
}
