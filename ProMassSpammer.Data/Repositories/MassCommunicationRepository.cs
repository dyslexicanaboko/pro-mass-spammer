using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using ProMassSpammer.Data.Entities;
using ProMassSpammer.Data.Interfaces;
using Status = ProMassSpammer.Data.Entities.MassCommunicationStatus.MassCommunicationStatusEnum;

namespace ProMassSpammer.Data.Repositories
{
    public class MassCommunicationRepository
        : RepositoryBase, IMassCommunicationRepository
    {
        public MassCommunicationRepository(IDesignTimeDbContextFactory<ProMassSpammerModel> modelFactory)
            : base(modelFactory)
        {

        }

        public IList<MassCommunication> GetUnprocessedMassCommunications()
        {
            using (var context = GetDbContext())
            {
                var lst = context
                    .MassCommunications
                    .Include(x => x.Recipients)
                    .Where(x =>
                        x.MassCommunicationStatus == Status.Unsent || x.MassCommunicationStatus == Status.Waiting)
                    .OrderBy(x => x.CreatedOnUtc)
                    .ToList();

                return lst;
            }
        }

        public void UpdateStatuses(MassCommunication massComm)
        {
            using (var context = GetDbContext())
            {
                massComm.ModifiedOnUtc = DateTime.UtcNow;

                context.Attach(massComm);

                context.Entry(massComm).State = EntityState.Modified;

                context.SaveChanges();
            }
        }
    }
}
