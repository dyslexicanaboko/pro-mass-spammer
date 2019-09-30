using Microsoft.EntityFrameworkCore.Design;

namespace ProMassSpammer.Data.Repositories
{
    public abstract class RepositoryBase
    {
        private readonly IDesignTimeDbContextFactory<ProMassSpammerModel> _modelFactory;

        protected RepositoryBase(IDesignTimeDbContextFactory<ProMassSpammerModel> modelFactory)
        {
            _modelFactory = modelFactory;
        }

        public ProMassSpammerModel GetDbContext()
        {
            //args isn't actually used, so it's always null - ignore the warning
            var context = _modelFactory.CreateDbContext(null);

            return context;
        }
    }
}
