using ProMassSpammer.Data.Entities;
using ProMassSpammer.Data.Interfaces;
using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using System;

//https://docs.microsoft.com/en-us/ef/core/miscellaneous/cli/dbcontext-creation
namespace ProMassSpammer.Data.Repositories
{
    public class RecipientRepository
        : RepositoryBase, IRecipientRepository
    {
        public RecipientRepository(IDesignTimeDbContextFactory<ProMassSpammerModel> modelFactory)
            : base(modelFactory)
        {

        }

        public Recipient GetById(int recipientId)
        {
            using (var context = GetDbContext())
            {
                var e = context
                    .Recipients
                    .Include(x => x.MassCommunication)
                    .SingleOrDefault(x => x.RecipientId == recipientId);

                return e;
            }
        }

        public void UpdateStatuses(IList<Recipient> results)
        {
            using (var context = GetDbContext())
            {
                var dtm = DateTime.UtcNow;

                foreach (var r in results)
                {
                    r.ModifiedOnUtc = dtm;

                    context.AttachRange(r);

                    context.Entry(r).State = EntityState.Modified;
                }
                
                context.SaveChanges();
            }
        }
    }
}
