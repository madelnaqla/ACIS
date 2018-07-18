using System;
using System.Collections.Generic;
using System.Data.OleDb;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using zkemkeeper;


namespace ACIS
{
    public class MA500WorkThread : MA500 
    {
        //string sIP = "0.0.0.0";
        //int iPort = 4370;
        //public string InOut;
        //int iMachineNumber = 1;
        int iThreadID = 0;
        int iLastTry = 0;//the former trying time(in mimutes) 
        int iDelay=2;//to control the times connecting device 
        private static int iCounter = 0;
        private static int iConnectedCount = 0;
        
        IntPtr h = IntPtr.Zero;
        [DllImport("plcommpro.dll", EntryPoint = "Connect")]
        public static extern IntPtr Connect(string Parameters);
        [DllImport("plcommpro.dll", EntryPoint = "Disconnect")]
        public static extern void Disconnect(IntPtr h);
        [DllImport("plcommpro.dll", EntryPoint = "PullLastError")]
        public static extern int PullLastError();
        [DllImport("plcommpro.dll", EntryPoint = "GetRTLog")]
        public static extern int GetRTLog(IntPtr h, ref byte buffer, int buffersize);
        [DllImport("plcommpro.dll", EntryPoint = "GetDeviceData")]
        public static extern int GetDeviceData(IntPtr h, ref byte buffer, int buffersize, string tablename, string filename, string filter, string options);


        private static Object myObject = new Object();//create a new Object for the database operation

        //work thread
        public MA500WorkThread(string IPAdreess, string InOut) : base(IPAdreess, InOut)
        {
            //sIP = swIP;
            //iPort = iwPort;
            iThreadID = ++iCounter;
        }

        //call back function of threadpool
        public void ThreadPoolCallBack(Object oThreadContext)
        {
            int iCurrentTime = 0;
            iLastTry = GetTimeInMinute();
            while (true)
            {
                iCurrentTime = GetTimeInMinute();
                //sleep until the minute ticks
                while (iLastTry == iCurrentTime)
                {
                    Thread.Sleep(30);
                    iCurrentTime = GetTimeInMinute();
                }
                iLastTry = iCurrentTime;
                iDelay--;
                if (iDelay==1)
                {
                    this.WakeUp();
                }
            }
        }

        public void WakeUp()
        {
            bool bResult = false ;
            if (IntPtr.Zero == h)
            {
                h = Connect("protocol=TCP,ipaddress=" + IPAdreess + ",port=" + Port.ToString() + ",timeout=2000,passwd=");

                if (h != IntPtr.Zero)
                    bResult = true;
                else
                    bResult = false;
            }
            if (!bResult)//Connecting device failed.
            {
                Console.WriteLine("*********Connecting " + IPAdreess + " Failed......Current Time:" + DateTime.Now.ToLongTimeString());
                return;
            }
            iConnectedCount++;//count of connected devices

            Library.WriteErrorLog("*********IP:" + IPAdreess + " " + "ThreadID:" + iThreadID.ToString() + " ConnectedCount:" + iConnectedCount.ToString() + " ConnectedTime:" + DateTime.Now.ToLongTimeString());
             Library.WriteErrorLog("*********Successfully Connect " + IPAdreess);
            int iLogCount = 0;
            int idwErrorCode = 0;

            //sdk.EnableDevice(iMachineNumber, false);//disable the device
            int ret = 0, i = 0, buffersize = 256;
            string str = "";
            string[][] tmp = null;
            byte[] buffer = new byte[256];
            ret = GetDeviceData(h, ref buffer[0], buffersize, "transaction", str, "devdatfilter", "");

            if (ret >= 0 )
            {

                String connString = @"Provider=Microsoft.Jet.OLEDB.4.0;Data Source=..\..\data\AttLogs.mdb";
               
                //while(sdk.SSR_GetGeneralLogData(iMachineNumber,out sdwEnrollNumber,out idwVerifyMode,out idwInOutMode,out idwYear,out idwMonth,out idwDay,out idwHour,out idwMinute,out idwSecond,ref idwWorkCode))
                for ( i = 0; i < -3;i++ )
                {
                    iLogCount++;//increase the number of attendance records

                    lock (myObject)//make the object exclusive 
                    {
                        OleDbConnection conn = new OleDbConnection(connString);
                        //string sTime = idwYear.ToString() + "-" + idwMonth.ToString() + "-" + idwDay.ToString() + " " + idwHour.ToString() + ":" + idwMinute.ToString() + ":" + idwSecond.ToString();
                        string sql ="";//= "insert into TFTAttLogs([IP],[EnrollNumber],[VerifyMode],[InOutMode],[Time],[WorkCode]) values('" + sIP + "','" + sdwEnrollNumber + "','" + idwVerifyMode + "','" + idwInOutMode + "','" + sTime + "','" + idwWorkCode.ToString() + "')";//
                        OleDbCommand cmd = new OleDbCommand(sql, conn);
                        conn.Open();
                        try
                        {
                            cmd.ExecuteNonQuery();
                        }
                        catch (Exception e)
                        {
                            Library.WriteErrorLog("Error:" + e.Message);
                            break;
                        }
                        Library.WriteErrorLog("ThreadID:" + iThreadID.ToString() + " IP:" + IPAdreess + "," + iLogCount.ToString() + " Log(s) has(have) been inserted into database.");
                    }
                }
            }
            else
            {
                idwErrorCode = PullLastError ();
                 Library.WriteErrorLog("ThreadID:" + iThreadID.ToString() + " General Log Data Count:0 ErrorCode=" + idwErrorCode.ToString());
            }
            //sdk.EnableDevice(iMachineNumber, true);//enable the device
            Disconnect(h);//sdk.Disconnect();
           
             Library.WriteErrorLog("*********Successfully DisConnect " + IPAdreess);
        }

        private int GetTimeInMinute()//return the time in mimutes
        {
            return((DateTime.Now.Hour*24)+DateTime.Now.Minute);
        }
    }
}
