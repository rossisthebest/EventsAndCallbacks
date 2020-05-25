using System;

namespace EventsAndCallbacks
{
    class Program
    {
        // Method that must run when the alarm if raised
        static void AlarmListener1(object sender, AlarmEventArgs e)
        {
            Console.WriteLine("Alarm Listener 1 called");
            Console.WriteLine("Alarm in {0}", e.Location);
        }
        // Method that must run when the alarm if raised
        static void AlarmListener2(object sender, AlarmEventArgs e)
        {
            Console.WriteLine("Alarm Listener 2 called");
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
            alarm.OnAlarmRaised += AlarmListener2;

            alarm.RaiseAlarm("Figure1_66");
            Console.WriteLine("Alarm raised");

            alarm.OnAlarmRaised -= AlarmListener1;
            alarm.OnAlarmRaised -= AlarmListener2;
            alarm.RaiseAlarm("Figure1_66 2");
            Console.WriteLine("Alarm Raised again");

            Console.ReadKey();
        }

        static void Main(string[] args)
        {
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
            // Raises the alarm
            // The event handler receives a reference to the alarm that is raising this event
            OnAlarmRaised(this, new AlarmEventArgs(location));
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
