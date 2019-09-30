using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;
using System.IO;

namespace ProMassSpammer.Data
{
    public class ProMassSpammerModelFactory
        : IDesignTimeDbContextFactory<ProMassSpammerModel>
    {
        private static string _connectionString;

        public ProMassSpammerModelFactory()
        {
            if (_connectionString != null) return;

            _connectionString = LoadConnectionString();
        }

        /* dotnet ef migrations add InitialCreate
         * dotnet ef migrations add <Migration name here>
         *
         * dotnet ef database update  */
        public ProMassSpammerModel CreateDbContext(string[] args)
        {
            var optionsBuilder = new DbContextOptionsBuilder<ProMassSpammerModel>();

            optionsBuilder
                .UseSqlServer(_connectionString, providerOptions => providerOptions.CommandTimeout(60));

            return new ProMassSpammerModel(optionsBuilder.Options);
        }

        private static string LoadConnectionString()
        {
            var builder = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true);

            var configuration = builder.Build();

            string connectionString = null;

            try
            {
                connectionString = configuration.GetConnectionString("ProMassSpammer");
            }
            catch
            {
                //11/01/2018
                //I hate that I have to do this, but it's Microsoft's fault that this isn't working yet
                //An exception is thrown when running migrations or running scaffolding
                //Therefore just grab this hard coded connection string
                connectionString = "data source=.;initial catalog=ProMassSpammer;integrated security=True;multipleactiveresultsets=True;application name=EntityFramework";
            }

            return connectionString;
        }
    }
}
