using ProMassSpammer.Core.Diagnostic;
using ProMassSpammer.Core.Exceptions;
using ProMassSpammer.Core.Transmission;
using ProMassSpammer.Data;
using ProMassSpammer.Data.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using SimpleInjector;

namespace ProMassSpammer.Core
{
    public class SpamEngine
    {
        private readonly IRepository _repository;

        public SpamEngine(IRepository repository)
            : this(repository, null)
        {
            
        }

        /// <summary>
        /// Only use this constructor for unit testing
        /// </summary>
        /// <param name="repository">Moq-ed container</param>
        /// <param name="container">
        /// Container being passed for the purposes of unit testing only. Inject a container
        /// that is configured specifically for unit testing.
        /// </param>
        public SpamEngine(IRepository repository, Container container)
        {
            SetupDependencyResolver(container);

            _repository = repository;

            Logging.LoggerRepository = _repository.LogRepository;
            //Config.ValidateConfiguration();
        }

        public bool IsBusy { get; private set; }

        private static Statistic _stats;

        internal static Statistic MassCommStatistics => _stats ?? (_stats = new Statistic());

        internal static DependencyResolver Resolver { get; private set; }

        /// <summary>
        /// Setup dependencies that cannot be injected immediately. These are all part of the factory below.
        /// </summary>
        /// <param name="container">The container should only be passed for debug.</param>
        private static void SetupDependencyResolver(Container container)
        {
            if (container == null)
            {
                Resolver = new DependencyResolver();
                Resolver.Bootstrap();
            }
            else
            {
                Resolver = new DependencyResolver(container);
            }
        }

        public void ProcessAvailableMassComms()
        {
            try
            {
                IsBusy = true;

                MassCommStatistics.ServiceTotalCycles++;

                var lstMc = _repository.MassCommunicationRepository.GetUnprocessedMassCommunications();

                if (lstMc.Count == 0)
                {
                    MassCommStatistics.ServiceUninterruptedSleepCycles++;
                    MassCommStatistics.AddJournalEntry("No mass communications found to process?");

                    return;
                }

                MassCommStatistics.StopWatchBegin();

                var lstTasks = new List<Task>(lstMc.Count);

                //Process each mass communication in a separate thread because you can
                foreach (var massComm in lstMc)
                {
                    lstTasks.Add(Task.Factory.StartNew(() => ProcessIndividualMassCommEntity(massComm)));
                }

                Task.WaitAll(lstTasks.ToArray());
            }
            finally
            {
                IsBusy = false;

                if (MassCommStatistics.IsStopWatchStarted) MassCommStatistics.StopWatchEnd();
            }
        }

        private void ProcessIndividualMassCommEntity(MassCommunication massComm)
        {
            try
            {
                if (massComm.Recipients == null ||
                    massComm.Recipients.Count == 0)
                {
                    throw new NoRecipientsFoundException(massComm);
                }

                var svc = new TransmissionService(massComm);

                svc.PrepareForSend();

                var lstResults = svc.Send();

                var lstUpdatedRecipients = UpdateTransmissionStatus(lstResults);

                _repository.RecipientRepository.UpdateStatuses(lstUpdatedRecipients);

                UpdateStatus(massComm, lstResults);

                _repository.MassCommunicationRepository.UpdateStatuses(massComm);
            }
            catch (Exception ex)
            {
                Logging.LogError(ex);

                Fail(massComm, ex);
            }
        }

        private static void UpdateStatus(MassCommunication massComm, List<SendResult> results)
        {
            massComm.MassCommunicationStatus = MassCommunicationStatus.MassCommunicationStatusEnum.Sent;

            var count = massComm.Recipients.Count;
            var sent = results.Count(x => x.IsSuccess);
            var fail = count - sent;

            massComm.MassCommunicationStatus = fail > 0 ? MassCommunicationStatus.MassCommunicationStatusEnum.Error : MassCommunicationStatus.MassCommunicationStatusEnum.Sent;
            massComm.StatusMessage = $"Sent: {sent} Unsent: {fail} Total: {count}";

            MassCommStatistics.UpdateMassCommCounters(massComm);
            MassCommStatistics.McdTotalProcessed += count;
            MassCommStatistics.McdTotalSentFlawlessly += sent;
            MassCommStatistics.McdTotalErrors += fail;
        }

        private static void Fail(MassCommunication massComm, Exception reason)
        {
            try
            {
                massComm.MassCommunicationStatus = MassCommunicationStatus.MassCommunicationStatusEnum.Failure;
                massComm.StatusMessage = "Type: " + reason.GetType() + " - Message: " + reason.Message;
                
                MassCommStatistics.UpdateMassCommCounters(massComm);
            }
            catch (Exception ex)
            {
                Logging.LogError(ex);
            }
        }

        private static TransmissionStatus.TransmissionStatusEnum GetNextStatus(Recipient target, bool success)
        {
            var status = GetNextStatus(target.TransmissionStatus, success);

            return status;
        }

        private static TransmissionStatus.TransmissionStatusEnum GetNextStatus(
            TransmissionStatus.TransmissionStatusEnum currentTransmissionStatusEnum, 
            bool success)
        {
            if (success)
                return TransmissionStatus.TransmissionStatusEnum.Sent;

            TransmissionStatus.TransmissionStatusEnum se;

            switch (currentTransmissionStatusEnum)
            {
                case TransmissionStatus.TransmissionStatusEnum.Waiting:
                case TransmissionStatus.TransmissionStatusEnum.Processing:
                    se = TransmissionStatus.TransmissionStatusEnum.FirstAttempt;
                    break;

                case TransmissionStatus.TransmissionStatusEnum.FirstAttempt:
                    se = TransmissionStatus.TransmissionStatusEnum.SecondAttempt;
                    break;

                case TransmissionStatus.TransmissionStatusEnum.SecondAttempt:
                    se = TransmissionStatus.TransmissionStatusEnum.ThirdAttempt;
                    break;

                default:
                    se = TransmissionStatus.TransmissionStatusEnum.Error;
                    break;
            }

            return se;
        }

        private static List<Recipient> UpdateTransmissionStatus(List<SendResult> results)
        {
            var lst = new List<Recipient>();

            foreach (var sr in results)
            {
                var r = sr.RecipientReference;
                r.TransmissionStatus = GetNextStatus(r, sr.IsSuccess);
                r.TransmissionStatusMessage = sr.ToString();

                lst.Add(r);
            }

            return lst;
        }
    }
}