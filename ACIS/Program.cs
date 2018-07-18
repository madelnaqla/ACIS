using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceProcess;
using System.Text;
using System.Threading.Tasks;

namespace ACIS
{
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        
        /*static void Main()
        {

            ServiceBase.Run(new Service1());
        }*/
        public static void Main(string[] args)
        {
            Service1 service = new Service1();
            service.TestStartupAndStop(args);
            ServiceThraed serviceThread;
            if (Environment.UserInteractive)
            {
                ServiceBase[] ServicesToRun;
                ServicesToRun = new ServiceBase[] 
                { 
                  serviceThread = new ServiceThraed()
                };
                ServiceBase.Run(ServicesToRun);

                //ServiceThraed service1 = new ServiceThraed ();
                //service1.TestStartupAndStop(args);
               
                //service.TestStartupAndStop(args );

            }
            else
            {
                // Put the body of your old Main method here.  
            }
        }
    }
}
