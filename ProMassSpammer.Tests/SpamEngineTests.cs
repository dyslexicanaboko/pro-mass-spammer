using Moq;
using NUnit.Framework;
using ProMassSpammer.Core;
using ProMassSpammer.Core.Transmission.Smtp;
using ProMassSpammer.Data;
using ProMassSpammer.Data.Entities;
using SimpleInjector;
using System;
using System.Collections.Generic;

namespace ProMassSpammer.Tests
{
    [TestFixture]
    public class SpamEngineTests
    {
        [Test]
        public void Debug_happy_path_for_dev_with_moq()
        {
            var lst = GetDummyMassCommunications();

            var m = new Mock<IRepository>();

            m.Setup(x => x.MassCommunicationRepository
                .GetUnprocessedMassCommunications())
                .Returns(lst);

            m.Setup(x => x.MassCommunicationRepository
                .UpdateStatuses(null));

            m.Setup(x => x.RecipientRepository
                .UpdateStatuses(new List<Recipient>()));

            m.Setup(x => x.LogRepository
                .Add(null));

            var c = GetTestContainer();

            var svc = new SpamEngine(m.Object, c);

            svc.ProcessAvailableMassComms();
        }

        private Container GetTestContainer()
        {
            var c = new Container();

            c.Register<ISmtpClient, SmtpClientDummy>(Lifestyle.Transient);

            c.Verify();

            return c;
        }

        private List<MassCommunication> GetDummyMassCommunications()
        {
            var mc = new MassCommunication
            {
                MassCommunicationId = 1,
                Title = "Test mass com",
                DeliveryMethod = DeliveryMethod.DeliveryMethodEnum.Email,
                DeliveryMethodId = 1,
                Subject = "Email subject",
                Body = @"Dummy email body",
                From = "Unit@test.com",
                Catalyst = "Unit testing",
                CreatedOnUtc = DateTime.UtcNow,
                MassCommunicationStatus = MassCommunicationStatus.MassCommunicationStatusEnum.Waiting,
            };

            var r = new Recipient
            {
                RecipientId = 1,
                MassCommunicationId = mc.MassCommunicationId,
                CreatedOnUtc = DateTime.UtcNow,
                ContactString = "fake@email.com",
                TransmissionStatus = TransmissionStatus.TransmissionStatusEnum.Waiting
            };

            mc.Recipients = new List<Recipient> {r};

            var lst = new List<MassCommunication>
            {
                mc
            };

            return lst;
        }
    }
}
