using ProMassSpammer.Data.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;

namespace ProMassSpammer.Core.Diagnostic
{
    [DataContract]
    public class Statistic
    {
        public const int JournalEntriesMax = 500;
        public const int JournalEntriesGetSize = 100;
        public const int JournalPruneSize = 10;

        public Statistic()
        {
            Journal = new List<Log>(JournalEntriesMax);

            ServiceStartedOn = DateTime.Now;
        }

        [DataMember] public DateTime ServiceStartedOn { get; set; }

        [DataMember]
        public double ServiceTotalUpTimeInMinutes
        {
            get => GetTotalUpTime().TotalMinutes;


            set
            {
                var ignore = value;
            }
        }

        [DataMember]
        public string ServiceTotalUpTimeStamp
        {
            get => GetTotalUpTime().ToString("ddd' days, 'hh' hours 'mm' minutes 'ss' seconds'");


            set
            {
                var ignore = value;
            }
        }

        [DataMember] public int ServiceTotalLoggedErrors { get; set; }

        [DataMember] public int ServiceTotalCycles { get; set; }

        [DataMember] public int ServiceUninterruptedSleepCycles { get; set; }

        [DataMember] public double ServiceMcdPerMinute { get; set; }

        [DataMember] public int McTotalProcessed { get; set; }

        [DataMember] public int McTotalSentFlawlessly { get; set; }

        [DataMember] public int McTotalSentWithError { get; set; }

        [DataMember] public int McTotalHardFailures { get; set; }

        [DataMember] public int McdTotalSentFlawlessly { get; set; }

        [DataMember] public int McdTotalErrors { get; set; }

        [DataMember] public int McdTotalProcessed { get; set; }

        public bool IsStopWatchStarted { get; private set; }

        private List<Log> Journal { get; }

        internal void AddJournalEntry(Log entry)
        {
            try
            {
                if (Journal.Count >= JournalEntriesMax)
                    Journal.RemoveRange(0, JournalPruneSize);

                Journal.Add(entry);

#if DEBUG
                Console.WriteLine("Logged: " + entry.Message + "\n");
#endif
            }
            catch
            {
            }
        }

        public void UpdateMassCommCounters(MassCommunication massComm)
        {
            McTotalProcessed++;

            switch (massComm.MassCommunicationStatus)
            {
                case MassCommunicationStatus.MassCommunicationStatusEnum.Sent:
                    McTotalSentFlawlessly++;
                    break;
                case MassCommunicationStatus.MassCommunicationStatusEnum.Error:
                    McTotalSentWithError++;
                    break;
                case MassCommunicationStatus.MassCommunicationStatusEnum.Failure:
                    McTotalHardFailures++;
                    break;
                default:
                    break;
            }
        }


        public void AddJournalEntry(Exception exception)
        {
            AddJournalEntry("LogDAL Failure - Ex: " + exception.GetType().Name + " ExMsg: " + exception.Message);
        }

        public void AddJournalEntry(string entry)
        {
            try
            {
                AddJournalEntry(new Log
                {
                    CreatedOnUtc = DateTime.Now,
                    Message = entry
                });
            }
            catch
            {
            }
        }

        public List<string> GetJournalEntries(int entries = JournalEntriesGetSize)
        {
            if (entries <= 0 || entries > JournalEntriesMax)
                entries = JournalEntriesGetSize;


            return Journal.OrderByDescending(x => x.CreatedOnUtc)
                .Select(x => x.CreatedOnUtc.ToString() + " = " + x.Message)
                .ToList();
        }

        private TimeSpan GetTotalUpTime()
        {
            return DateTime.Now - ServiceStartedOn;
        }

        #region Stop Watch

        private DateTime _dtmStart;
        private DateTime _dtmStop;
        private double _mcdStart;
        private double _mcdStop;

        public void StopWatchBegin()
        {
            IsStopWatchStarted = true;
            _dtmStart = DateTime.Now;
            _mcdStart = McdTotalProcessed;
        }

        public void StopWatchEnd()
        {
            IsStopWatchStarted = false;

            _dtmStop = DateTime.Now;


            _mcdStop = McdTotalProcessed - _mcdStart;


            var ts = _dtmStop - _dtmStart;


            if (ts.TotalMinutes == 0)
                ServiceMcdPerMinute = 0;
            else
                ServiceMcdPerMinute = _mcdStop / ts.TotalMinutes;
        }

        #endregion
    }
}