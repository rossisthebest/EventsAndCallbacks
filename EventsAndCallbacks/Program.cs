using System;
using System.Collections.Generic;
using System.Reflection;

namespace EventsAndCallbacks
{
    class Program
    {
        // Method that must run when the alarm if raised
        static void AlarmListener1(object sender, AlarmEventArgs e)
        {
            Console.WriteLine("Alarm Listener 1 called");
            Console.WriteLine("Alarm in {0}", e.Location);
            throw new Exception("Bang");
        }
        // Method that must run when the alarm if raised
        static void AlarmListener2(object sender, AlarmEventArgs e)
        {
            Console.WriteLine("Alarm Listener 2 called");
            Console.WriteLine("Alarm in {0}", e.Location);
            throw new Exception("BOOM!!!");
        }

        static void Figure1_65()
        {
            // Create a new alarm
            Alarm alarm = new Alarm();

            // Connect the two listener methods
            alarm.OnAlarmRaised += AlarmListener1;
            alarm.OnAlarmRaised += AlarmListener2;

            alarm.RaiseAlarm("Figure1_65");
            Console.WriteLine("Alarm raised");

            Console.ReadKey();
        }

        static void Figure1_66()
        {
            // Create a new alarm
            Alarm alarm = new Alarm();

            // Connect the two listener methods
            alarm.OnAlarmRaised += AlarmListener1;
            alarm.OnAlarmRaised += AlarmListener2;

            try
            {
                alarm.RaiseAlarm("Kitchen");
            }
            catch (AggregateException agg)
            {
                foreach (Exception ex in agg.InnerExceptions)
                {
                    Console.WriteLine(ex.Message);
                }
            }

            Console.ReadKey();
        }

        static void Main(string[] args)
        {
            //Figure1_65();
            Figure1_66();
        }
    }

    class Alarm
    {
        // Delegate for the alarm event
        public event EventHandler<AlarmEventArgs> OnAlarmRaised = delegate { };

        // Called to raise an alarm
        public void RaiseAlarm(string location)
        {
            List<Exception> exceptionList = new List<Exception>();

            foreach (Delegate handler in OnAlarmRaised.GetInvocationList())
            {
                try
                {
                    handler.DynamicInvoke(this, new AlarmEventArgs(location));
                }
                catch (TargetInvocationException ex)
                {
                    exceptionList.Add(ex.InnerException);
                }
            }

            if (exceptionList.Count > 0)
            {
                throw new AggregateException(exceptionList);
            }
        }
    }

    class AlarmEventArgs : EventArgs
    {
        public string Location { get; set; }

        public AlarmEventArgs(string location)
        {
            this.Location = location;
        }
    }
}
