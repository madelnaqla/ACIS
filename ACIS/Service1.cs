using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.OracleClient;
using System.Diagnostics;
using System.Linq;
using System.Runtime.InteropServices;
using System.ServiceProcess;
using System.Text;
using System.Threading.Tasks;
using System.Timers;
using System.IO;
using System.Threading;

namespace ACIS
{
    public partial class Service1 : ServiceBase
    {
        public Service1()
        {
            this.ServiceName = "Pharaonic Service";
            this.EventLog.Source = "Pharaonic Service";
            this.EventLog.Log = "Application";

            // These Flags set whether or not to handle that specific
            //  type of event. Set to true if you need it, false otherwise.
            this.CanHandlePowerEvent = true;
            this.CanHandleSessionChangeEvent = true;
            this.CanPauseAndContinue = true;
            this.CanShutdown = true;
            this.CanStop = true;

            if (!EventLog.SourceExists("Pharaonic Service"))
                EventLog.CreateEventSource("Pharaonic Service", "Application");
          
            read_config();

        }

        internal void TestStartupAndStop(string[] args)
        {
            this.OnStart(args);
            Console.ReadLine();
            this.OnStop();
        }
        /// <summary>
        /// Dispose of objects that need it here.
        /// </summary>
        /// <param name="disposing">Whether or not disposing is going on.</param>
        protected override void Dispose(bool disposing)
        {
            base.Dispose(disposing);
        }

        string[][] Devices;
        void read_config()
        {
            Devices = Library.LoadCsv(File.ReadAllText("Config.csv"), ';');
            
            int numberOfiFace=0;
            int numberOfMA500=0;
            for (int i = 0; i < Devices.GetLength(0); i++)
            {
                if (Devices[i][2] == "IFACE")
                    numberOfiFace++;
                if (Devices[i][2] == "MA500")
                    numberOfMA500++;
            }
            iFace[] iFaceMachine = new iFace[numberOfiFace];
            MA500[] MA500Machine = new MA500[numberOfMA500];
            int r = 0, c = 0;
            for (int i = 0; i < Devices.GetLength(0); i++)
            {
                if (Devices[i][2] == "IFACE")
                {
                    iFaceMachine[r] = new iFace(Devices[i][0], Devices[i][1]);
                    iFaceMachine[r].iFace_Connect();
                    //iFaceMachine[r].iFace_ReadLog();
                    r++;
                }
                if (Devices[i][2] == "MA500")
                {
                    MA500Machine[c] = new MA500(Devices[i][0], Devices[i][1]);
                    MA500Machine[c].MA500_Connect();
                    //MA500Machine[c].MA500_ReadLog();
                    c++;
                }
            }
            
        }
        #region Webservice events
        /// <summary>
        /// OnStart: Put startup code here
        ///  - Start threads, get inital data, etc.
        /// </summary>
        /// <param name="args"></param>
        protected override void OnStart(string[] args)
        {
            base.OnStart(args);
            ///////////////////////////////////////////////////
            
            Library.WriteErrorLog("Pharonic service started");
            
          

            //MA500_Connect("192.168.1.128");
            //iFace_Connect("192.168.1.129");
        }

        /// <summary>
        /// OnStop: Put your stop code here
        /// - Stop threads, set final data, etc.
        /// </summary>
        protected override void OnStop()
        {
            base.OnStop();
            ////////////////////////////////////////////////////
            //timer1.Enabled = false;
            Library.WriteErrorLog("Pharonic service stopped");
        }

        /// <summary>
        /// OnPause: Put your pause code here
        /// - Pause working threads, etc.
        /// </summary>
        protected override void OnPause()
        {
            base.OnPause();
            Library.WriteErrorLog("Pharonic service Paused");
        }

        /// <summary>
        /// OnContinue: Put your continue code here
        /// - Un-pause working threads, etc.
        /// </summary>
        protected override void OnContinue()
        {
            base.OnContinue();
            Library.WriteErrorLog("Pharonic service Continue");
        }

        /// <summary>
        /// OnShutdown(): Called when the System is shutting down
        /// - Put code here when you need special handling
        ///   of code that deals with a system shutdown, such
        ///   as saving special data before shutdown.
        /// </summary>
        protected override void OnShutdown()
        {
            base.OnShutdown();
            Library.WriteErrorLog("Pharonic service Shutdown");
        }

        /// <summary>
        /// OnCustomCommand(): If you need to send a command to your
        ///   service without the need for Remoting or Sockets, use
        ///   this method to do custom methods.
        /// </summary>
        /// <param name="command">Arbitrary Integer between 128 & 256</param>
        protected override void OnCustomCommand(int command)
        {
            //  A custom command can be sent to a service by using this method:
            //#  int command = 128; //Some Arbitrary number between 128 & 256
            //#  ServiceController sc = new ServiceController("NameOfService");
            //#  sc.ExecuteCommand(command);

            base.OnCustomCommand(command);
        }

        /// <summary>
        /// OnPowerEvent(): Useful for detecting power status changes,
        ///   such as going into Suspend mode or Low Battery for laptops.
        /// </summary>
        /// <param name="powerStatus">The Power Broadcase Status (BatteryLow, Suspend, etc.)</param>
        protected override bool OnPowerEvent(PowerBroadcastStatus powerStatus)
        {
            return base.OnPowerEvent(powerStatus);
        }

        /// <summary>
        /// OnSessionChange(): To handle a change event from a Terminal Server session.
        ///   Useful if you need to determine when a user logs in remotely or logs off,
        ///   or when someone logs into the console.
        /// </summary>
        /// <param name="changeDescription"></param>
        protected override void OnSessionChange(SessionChangeDescription changeDescription)
        {
            base.OnSessionChange(changeDescription);
        }

        #endregion
         

        
    }
}
