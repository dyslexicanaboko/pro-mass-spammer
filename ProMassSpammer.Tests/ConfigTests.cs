using Microsoft.Extensions.Configuration;
using NUnit.Framework;
using ProMassSpammer.Core.Configuration;
using Config = ProMassSpammer.Data.Config;

namespace ProMassSpammer.Tests
{
    [TestFixture]
    public class ConfigTests
    {
        [Test]
        public void Connection_string_is_found()
        {
            var svc = new Config();

            var c = svc.BuildConfigs();

            var str = c.GetConnectionString("ProMassSpammer");

            Assert.AreEqual("data source=.;initial catalog=ProMassSpammer;integrated security=True;multipleactiveresultsets=True;application name=EntityFramework", str);
        }

        [Test]
        public void Section_with_array_is_found()
        {
            var svc = new Config();

            var c = svc.BuildConfigs();

            var arr = c.GetSection("exclude").Get<string[]>();

            //Expected values
            var arrExpected = new[]
            {
                "**/bin",
                "**/bower_components",
                "**/jspm_packages",
                "**/node_modules",
                "**/obj",
                "**/platforms"
            };

            Assert.AreEqual(arrExpected.Length, arr.Length);

            for (var i = 0; i < arr.Length; i++)
            {
                Assert.AreEqual(arrExpected[i], arr[i]);
            }
        }

        [Test]
        public void Section_in_config_file_and_project()
        {
            var svc = new Config("SmtpConfiguration.json");

            var c = svc.BuildConfigs();

            var sc = c.GetSection("SmtpConfiguration").Get<SmtpConfiguration>();

            Assert.IsNotNull(sc);
        }
    }
}
