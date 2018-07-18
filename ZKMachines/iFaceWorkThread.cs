using System;
using System.Collections.Generic;
using System.Data.OleDb;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using zkemkeeper;

namespace ACIS
{
    public class iFaceWorkThread : iFace 
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

        public zkemkeeper.CZKEMClass sdk = new CZKEMClass();//create Standalone SDK class dynamicly
        private static Object myObject = new Object();//create a new Object for the database operation

        //work thread
        public iFaceWorkThread(string IPAdreess, string  InOut)  : base(IPAdreess, InOut)
        {
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
            bool bResult = sdk.Connect_Net(IPAdreess, Port);

            if (!bResult)//Connecting device failed.
            {
                Library.WriteErrorLog("*********Connecting " + IPAdreess + " Failed......Current Time:" + DateTime.Now.ToLongTimeString());
                return;
            }
            iConnectedCount++;//count of connected devices

            Library.WriteErrorLog("*********IP:" + IPAdreess + " " + "ThreadID:" + iThreadID.ToString() + " ConnectedCount:" + iConnectedCount.ToString() + " ConnectedTime:" + DateTime.Now.ToLongTimeString());
            Library.WriteErrorLog("*********Successfully Connect " + IPAdreess);
            int iLogCount = 0;
            int idwErrorCode = 0;

            sdk.EnableDevice(iMachineNumber, false);//disable the device
            if (sdk.ReadAllGLogData(iMachineNumber))
            {
                string sdwEnrollNumber = "";
                int idwVerifyMode = 0;
                int idwInOutMode = 0;
                int idwYear = 0;
                int idwMonth = 0;
                int idwDay = 0;
                int idwHour = 0;
                int idwMinute = 0;
                int idwSecond = 0;
                int idwWorkCode = 0;

                String connString = @"Provider=Microsoft.Jet.OLEDB.4.0;Data Source=..\..\data\AttLogs.mdb";

                while(sdk.SSR_GetGeneralLogData(iMachineNumber,out sdwEnrollNumber,out idwVerifyMode,out idwInOutMode,out idwYear,out idwMonth,out idwDay,out idwHour,out idwMinute,out idwSecond,ref idwWorkCode))
                {
                    iLogCount++;//increase the number of attendance records
                    
                    lock (myObject)//make the object exclusive 
                    {
                        OleDbConnection conn = new OleDbConnection(connString);
                        string sTime = idwYear.ToString() + "-" + idwMonth.ToString() + "-" + idwDay.ToString() + " " + idwHour.ToString() + ":" + idwMinute.ToString()+":"+idwSecond.ToString();
                        string sql = "insert into TFTAttLogs([IP],[EnrollNumber],[VerifyMode],[InOutMode],[Time],[WorkCode]) values('" + IPAdreess  + "','" + sdwEnrollNumber + "','" + idwVerifyMode + "','" + idwInOutMode + "','" + sTime + "','"+idwWorkCode.ToString()+"')";//
                        OleDbCommand cmd = new OleDbCommand(sql, conn);
                        conn.Open();
                        try
                        {
                            cmd.ExecuteNonQuery();
                        }
                        catch(Exception e)
                        {
                            Library.WriteErrorLog("Error:"+e.Message);
                            break;
                        }
                        Library.WriteErrorLog("ThreadID:" + iThreadID.ToString() + " IP:" + IPAdreess  + "," + iLogCount.ToString() + " Log(s) has(have) been inserted into database.");
                    }
                }
            }
            else
            {
                sdk.GetLastError(ref idwErrorCode);
                Library.WriteErrorLog("ThreadID:" + iThreadID.ToString() + " General Log Data Count:0 ErrorCode=" + idwErrorCode.ToString());
            }
            sdk.EnableDevice(iMachineNumber, true);//enable the device
            sdk.Disconnect();
            Library.WriteErrorLog("*********Successfully DisConnect " + IPAdreess );
        }

        private int GetTimeInMinute()//return the time in mimutes
        {
            return((DateTime.Now.Hour*24)+DateTime.Now.Minute);
        }
    }
}
