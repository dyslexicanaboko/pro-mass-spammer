using System.Collections.Generic;
using Microsoft.EntityFrameworkCore.Design;
using NUnit.Framework;
using ProMassSpammer.Core;
using ProMassSpammer.Data;
using ProMassSpammer.Data.Entities;
using ProMassSpammer.Data.Repositories;
using SimpleInjector;

namespace ProMassSpammer.Tests
{
    [TestFixture]
    public class IntegrationTests
    {
        [Test]
        public void Full_run()
        {
            var repository = GetRepository();

            var svc = new SpamEngine(repository);

            svc.ProcessAvailableMassComms();
        }

        [Test]
        public void Debugging_recipient_status_update()
        {
            var repository = GetRepository();

            var before = repository.RecipientRepository.GetById(1);

            before.TransmissionStatus = TransmissionStatus.TransmissionStatusEnum.Error;

            var lst = new List<Recipient>() {before};

            repository.RecipientRepository.UpdateStatuses(lst);

            var after = repository.RecipientRepository.GetById(1);

            Assert.AreEqual(TransmissionStatus.TransmissionStatusEnum.Error, before.TransmissionStatus);
            Assert.AreEqual(TransmissionStatus.TransmissionStatusEnum.Error, after.TransmissionStatus);
        }

        private IRepository GetRepository()
        {
            var c = new Container();

            c.Options.DefaultScopedLifestyle = ScopedLifestyle.Flowing;

            c.Register<IDesignTimeDbContextFactory<ProMassSpammerModel>, ProMassSpammerModelFactory>(Lifestyle.Singleton);

            c.Register<IRepository, Repository>();

            c.Verify();

            var repository = c.GetInstance<IRepository>();

            return repository;
        }
    }
}
