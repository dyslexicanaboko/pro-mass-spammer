using Microsoft.EntityFrameworkCore;
using ProMassSpammer.Data.Entities;
using ProMassSpammer.Data.Mappings;

namespace ProMassSpammer.Data
{
    public class ProMassSpammerModel
        : DbContext
    {
        //"name=ProMassSpammer"
        public ProMassSpammerModel(DbContextOptions<ProMassSpammerModel> options)
            : base(options)
        {
            //https://docs.microsoft.com/en-us/ef/core/miscellaneous/configuring-dbcontext
        }

        public virtual DbSet<DeliveryMethod> DeliveryMethods { get; set; }
        public virtual DbSet<Log> Logs { get; set; }
        public virtual DbSet<MassCommunication> MassCommunications { get; set; }
        public virtual DbSet<MassCommunicationStatus> MassCommunicationStatuses { get; set; }
        public virtual DbSet<Recipient> Recipients { get; set; }
        public virtual DbSet<TransmissionStatus> TransmissionStatuses { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder
                .UseQueryTrackingBehavior(QueryTrackingBehavior.NoTracking);
        }

        //This link has the methodology I am using for putting together my Fluent API mapping layer
        //https://www.learnentityframeworkcore.com/configuration/fluent-api
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            var m = modelBuilder;

            m.ApplyConfiguration(new DeliveryMethodMap());
            m.ApplyConfiguration(new LogMap());
            m.ApplyConfiguration(new MassCommunicationMap());
            m.ApplyConfiguration(new MassCommunicationStatusMap());
            m.ApplyConfiguration(new RecipientMap());
            m.ApplyConfiguration(new TransmissionStatusMap());
        }
    }
}
