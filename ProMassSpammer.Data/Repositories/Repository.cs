using System;
using Microsoft.EntityFrameworkCore.Design;
using ProMassSpammer.Data.Interfaces;

namespace ProMassSpammer.Data.Repositories
{
    public class Repository
        : IRepository
    {
        public Repository(IDesignTimeDbContextFactory<ProMassSpammerModel> modelFactory)
        {
            _massCommunicationRepository = new Lazy<IMassCommunicationRepository>(() => new MassCommunicationRepository(modelFactory));

            _logRepository = new Lazy<ILogRepository>(() => new LogRepository(modelFactory));

            _recipientRepository = new Lazy<IRecipientRepository>(() => new RecipientRepository(modelFactory));
        }

        private Lazy<IMassCommunicationRepository> _massCommunicationRepository;

        public IMassCommunicationRepository MassCommunicationRepository
        {
            get => _massCommunicationRepository.Value;
            set => _massCommunicationRepository = new Lazy<IMassCommunicationRepository>(value);
        }

        private Lazy<ILogRepository> _logRepository;

        public ILogRepository LogRepository
        {
            get => _logRepository.Value;
            set => _logRepository = new Lazy<ILogRepository>(value);
        }

        private Lazy<IRecipientRepository> _recipientRepository;

        public IRecipientRepository RecipientRepository
        {
            get => _recipientRepository.Value;
            set => _recipientRepository = new Lazy<IRecipientRepository>(value);
        }
    }
}
