using Microsoft.EntityFrameworkCore.Design;
using ProMassSpammer.Data.Entities;
using ProMassSpammer.Data.Interfaces;

namespace ProMassSpammer.Data.Repositories
{
    public class LogRepository
        : RepositoryBase, ILogRepository
    {
        public LogRepository(IDesignTimeDbContextFactory<ProMassSpammerModel> modelFactory)
            : base(modelFactory)
        {

        }

        public void Add(Log log)
        {
            using (var context = GetDbContext())
            {
                context.Add(log);
                context.SaveChanges();
            }
        }
    }
}
