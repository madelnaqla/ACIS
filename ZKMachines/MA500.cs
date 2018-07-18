using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using System.Text;
using System.Timers;

namespace ACIS
{
    public class MA500
    {
      
        IntPtr h = IntPtr.Zero;
        [DllImport("plcommpro.dll", EntryPoint = "GetRTLog")]
        public static extern int GetRTLog(IntPtr h, ref byte buffer, int buffersize);
        [DllImport("plcommpro.dll", EntryPoint = "Connect")]
        public static extern IntPtr Connect(string Parameters);
        [DllImport("plcommpro.dll", EntryPoint = "PullLastError")]
        public static extern int PullLastError();
        [DllImport("plcommpro.dll", EntryPoint = "GetDeviceData")]
        public static extern int GetDeviceData(IntPtr h, ref byte buffer, int buffersize, string tablename, string filename, string filter, string options);
        [DllImport("plcommpro.dll", EntryPoint = "Disconnect")]
        public static extern void Disconnect(IntPtr h);
        [DllImport("plcommpro.dll", EntryPoint = "GetDeviceParam")]
        public static extern int GetDeviceParam(IntPtr h, ref byte buffer, int buffersize, string itemvalues);
        [DllImport("plcommpro.dll", EntryPoint = "SetDeviceData")]
        public static extern int SetDeviceData(IntPtr h, string tablename, string data, string options);
        [DllImport("plcommpro.dll", EntryPoint = "DeleteDeviceData")]
        public static extern int DeleteDeviceData(IntPtr h, string tablename, string data, string options);

        public delegate void dgEventRaiser(ref string CardNo);
        public event dgEventRaiser ReadingCard;

        private System.Timers.Timer LogTimer;
        private System.Timers.Timer CradTimer;
        public string IPAdreess;
        public int Port = 4370;  //Default Value 4370
        public string InOut;
        public string SerialNumber;
        public bool bIsConnected = false;

        public MA500(string  IPAdreess , string InOut)
        {
            this.IPAdreess = IPAdreess;
            this.InOut = InOut;           
        }

        public void EnableLogTimer()
        {
            this.LogTimer = new System.Timers.Timer();
            this.LogTimer.Interval = 3000;
            this.LogTimer.Elapsed += new System.Timers.ElapsedEventHandler(this.timer_Tick);
            this.LogTimer.Enabled = true;
        }
        public void EnableCradTimer()
        {
            this.CradTimer = new System.Timers.Timer();
            this.CradTimer.Interval = 1000;
            this.CradTimer.Elapsed += new System.Timers.ElapsedEventHandler(this.timer_ReadCard);
            this.CradTimer.Enabled = true;
        }
        private void timer_Tick(object sender, ElapsedEventArgs e)
        {
            //Write code here to do some job depends on your requirement
            //Library.WriteErrorLog("Timer ticked and some job has been done successfully");
            //////////////////////////////////////////////////////////////////////////////

            int ret = 0, buffersize = 256;
            string str = "";
            string[] tmp = null;
            byte[] buffer = new byte[256];

            if (IntPtr.Zero != h)
            {
                ret = GetRTLog(h, ref buffer[0], buffersize);
                if (ret >= 0)
                {
                    str = Encoding.Default.GetString(buffer);
                    tmp = str.Split(',');

                    StringBuilder strBuilder = new StringBuilder();
                    strBuilder.Append(tmp[0] + ',');
                    strBuilder.Append(tmp[1] + ',');
                    strBuilder.Append(tmp[2] + ',');
                    strBuilder.Append(tmp[3] + ',');
                    strBuilder.Append(tmp[4] + ',');
                    strBuilder.Append(tmp[5] + ',');
                    strBuilder.Append(tmp[6].Replace("\r", "").Replace("\0", "").Replace("\n", ""));
                    //Library.WriteErrorLog(int.Parse(tmp[1]).ToString());
                    //Library.WriteErrorLog(strBuilder.ToString());
                    try
                    {
                        //if (tmp[3] != "0" && tmp[1] != "0" )
                        //{
                            int sdwEnrollNumber = int.Parse(tmp[1]);
                            string idwVerifyMode = tmp[6].Replace("\r", "").Replace("\0", "").Replace("\n", "");
                            int idwWorkcode = 0;

                            DateTime dt = DateTime.Parse(tmp[0]);
                            Library.WriteErrorLog(strBuilder.ToString());
                        //    Library.insertByParameter(sdwEnrollNumber, dt, InOut, idwVerifyMode.ToString(), 1, "Meminfo", idwWorkcode, SerialNumber, "1",0);
                        //}

                    }
                    catch (Exception ex)
                    {
                        Library.WriteErrorLog(ex);

                    }
                }
                if (ret == -10053 || ret == -10054 || ret == -10055)
                {
                    h = IntPtr.Zero;
                    bIsConnected = false;

                    //MA500_Disconnect();
                }
                if (ret < 0)
                {
                    Library.WriteErrorLog("Unhandled Code :- " + ret );
                }
            }
            else
            {
            //    Library.WriteErrorLog(IPAdreess + " Reconnecting device!");
            //    MA500_Connect();
            //    return;
                if (IntPtr.Zero == h)
                {
                    h = Connect("protocol=TCP,ipaddress=" + IPAdreess + ",port=" + Port + ",timeout=2000,passwd=");
                    if (h != IntPtr.Zero)
                    {
                        Library.WriteErrorLog(IPAdreess + " - " + SerialNumber + " - Connect device Seccuss!");
                        bIsConnected = true;
                        ///////////////////////////////////////////////////////////////
                        DateTime LastFromMA = GetLastAction();
                        DateTime LastFromDB = Library.GetLastFromDB("");
                        if (LastFromDB < LastFromMA)
                        {

                            //MA500_ReadLog(LastFromDB);
                        }

                        ////////////////////////////////////////////////////////////////
                    }
                    else
                        Library.WriteErrorLog(IPAdreess + " Reconnecting device!");
                }      
            }
            ////////////////////////////////////////////////////////
        }

        private void timer_ReadCard(object sender, ElapsedEventArgs e)
        {
            int ret = 0, buffersize = 256;
            string str = "";
            string[] tmp = null;
            byte[] buffer = new byte[256];

            if (IntPtr.Zero != h)
            {
                ret = GetRTLog(h, ref buffer[0], buffersize);
                if (ret >= 0)
                {
                    str = Encoding.Default.GetString(buffer);
                    tmp = str.Split(',');


                    if (tmp[2] != "0")
                    {
                        string CardID = tmp[2];
                        ReadingCard(ref CardID);
                    }
                }
            }
        }

        public  void MA500_Connect()
        {
            int ret = 0;
            
            if (IntPtr.Zero == h)
            {
                h = Connect("protocol=TCP,ipaddress=" + IPAdreess + ",port="+ Port + ",timeout=2000,passwd=");
                if (h != IntPtr.Zero)
                {
                    //EnableTimer();
                    bIsConnected = true;
                    SerialNumber = MA500_Serial();
                    Library.WriteErrorLog(IPAdreess + " - " + SerialNumber +  " - Connect device Seccuss!");
                }
                else
                {
                    ret = PullLastError();
                    Library.WriteErrorLog(IPAdreess + " Error: Unable to connect the device,ErrorCode=" + ret);
                    bIsConnected = false;
                }
            }
        }
        public string MA500_Serial()
        {
            int ret = 0, i = 0;
            int BUFFERSIZE = 1 * 1024 ;
            byte[] buffer = new byte[BUFFERSIZE];
            string str = null;
            string tmp = null;
            string[] value = null;

            ret = GetDeviceParam(h, ref buffer[0], BUFFERSIZE, "~SerialNumber");       //obtain device's param value
            if (ret >= 0)
            {
                tmp = Encoding.Default.GetString(buffer);
            }
            else
            {
               Library.WriteErrorLog ("GetDeviceParam function failed.The error is " + PullLastError() + ".");
               return "-1";
                //PullLastError();
            }
            return tmp.TrimEnd('\0').Replace ("~SerialNumber=","");
        }
        public void MA500_Disconnect()
        {
            if (IntPtr.Zero != h)
            {
                Disconnect(h);
                h = IntPtr.Zero;
                bIsConnected = false;
                Library.WriteErrorLog(IPAdreess + " Disconnected!");
            }
        }
        public void MA500_ReadLog()
        {
            int ret = 0;
            string str = "Cardno\tPin\tVerified\tDoorID\tEventType\tInOutState\tTime_second";//this.txtdevdata.Text;
            int BUFFERSIZE = 1 * 1024 * 1024;
            byte[] buffer = new byte[BUFFERSIZE];
            string options = "";
            if (IntPtr.Zero != h)
            {
                //MessageBox.Show("str="+str);
                //MessageBox.Show("devdatfilter=" + devdatfilter);
                ret = GetDeviceData(h, ref buffer[0], BUFFERSIZE, "transaction", str, "devdatfilter", options);
                string[][] data = Library.LoadCsv(Encoding.Default.GetString(buffer).TrimEnd('\0'), ',');
                for (int i = 1; i < data.GetLength(0); i++)
                {
                    string sdwEnrollNumber = data[i][0];
                    string idwVerifyMode = data[i][2];
                    int idwWorkcode = 0;
                    DateTime dt = Library.ConvertToDate(data[i][6]);
                    Library.insertByParameter(int.Parse (sdwEnrollNumber), dt, InOut, idwVerifyMode, 1, "Meminfo", idwWorkcode, SerialNumber, "UserExtFmt",0);
                }
            }
            else
            {
                Library.WriteErrorLog("Connect device failed!");
                return;
            }

            if (ret >= 0)
            {
                //this.txtgetdata.Text = Encoding.Default.GetString(buffer);
                string strcount = Encoding.Default.GetString(buffer);

                Library.WriteErrorLog("Transaction : Get " + ret + " records");
            }
            else
            {
                Library.WriteErrorLog("Get data failed.The error is " + ret);
                return;
            }
        }
        //Read Pervious log from the specific date
        public void MA500_ReadLog(DateTime  Date)
        {
            int ret = 0;
            bool StartPoint = false ;
            string str = "Cardno\tPin\tVerified\tDoorID\tEventType\tInOutState\tTime_second";//this.txtdevdata.Text;
            int BUFFERSIZE = 1 * 1024 * 1024;
            byte[] buffer = new byte[BUFFERSIZE];
            string options = "";
            if (IntPtr.Zero != h)
            {
                ret = GetDeviceData(h, ref buffer[0], BUFFERSIZE, "transaction", str, "", options);

                if (ret >= 0)
                {
                    string[][] data = Library.LoadCsv(Encoding.Default.GetString(buffer).TrimEnd('\0'), ',');

                    for (int i = 1; i < data.GetLength(0); i++)
                    {
                        if (Library.ConvertToDate(data[i][6]) == Date)
                            StartPoint = true;
                        if (StartPoint)
                        {
                            string sdwEnrollNumber = data[i][0];
                            string idwVerifyMode = data[i][2];
                            int idwWorkcode = 0;
                            DateTime dt = Library.ConvertToDate(data[i][6]);
                            Library.insertByParameter(int.Parse(sdwEnrollNumber), dt, InOut, idwVerifyMode, 1, "Meminfo", idwWorkcode, SerialNumber, "UserExtFmt", 0);
                        }
                    }
                    Library.WriteErrorLog("Pervious Data :  " + data.GetLength(0) + " records");
                }
                else
                {
                    Library.WriteErrorLog("Get Pervious data failed.The error is " + ret);
                    return;
                }
                
            }
            else
            {
                Library.WriteErrorLog("Connect device failed for Pervious Data");
                return;
            }

            
        }
        public List<string> MA500_Users()
        {
            int ret = 0;
            string str = "CardNo\tPin\tPassword\tGroup\tStartTime\tEndTime";//this.txtdevdata.Text;
            int BUFFERSIZE = 2 * 1024 * 1024;
            byte[] buffer = new byte[BUFFERSIZE];
            string options = "";
            string[][] User;
            List<string> UserList = new List<string>();
            if (IntPtr.Zero != h)
            {
                ret = GetDeviceData(h, ref buffer[0], BUFFERSIZE, "user", str, "", options);
            }
            else
            {
                Library.WriteErrorLog("Connect device failed!");
            }

            if (ret >= 0)
            {
                //this.txtgetdata.Text = Encoding.Default.GetString(buffer);
                string strcount = Encoding.Default.GetString(buffer).TrimEnd('\0');
                User = Library.LoadCsv(strcount, ',');
                for (int i = 1; i < User.Length; i++)
                {
                    UserList.Add(User[i][1]);
                }
                Library.WriteErrorLog("Users : Get " + ret + " records");
            }
            else
            {
                Library.WriteErrorLog("Get data failed.The error is " + ret);
            }
            return UserList;
        }

        public DateTime GetLastAction()
        {
            int ret = 0;
            string str = "Cardno\tPin\tVerified\tDoorID\tEventType\tInOutState\tTime_second";//this.txtdevdata.Text;
            int BUFFERSIZE = 1 * 1024 * 1024;
            byte[] buffer = new byte[BUFFERSIZE];
            string options = "";
            if (IntPtr.Zero != h)
            {
                ret = GetDeviceData(h, ref buffer[0], BUFFERSIZE, "transaction", str, "devdatfilter", options);
                string[][] data = Library.LoadCsv(Encoding.Default.GetString(buffer).TrimEnd('\0'), ',');

                string time_second = data[data.Length-1][6];
                return Library.ConvertToDate(time_second);

            }
            else
            {
                Library.WriteErrorLog("Connect device failed!");
                return new DateTime (0,DateTimeKind.Local);
            }
        }
        //To get All Template set UserID To "" Empty 
        public void GetTemplates(string UserID, out UserData UserData)
        {
            int ret = 0;
            string str = "Size\tUID\tPin\tFingerID\tValid\tTemplate\tResverd\tEndTag";//this.txtdevdata.Text;
            int BUFFERSIZE = 1 * 1024 * 1024 * 2; // increas it if data is larg to handle -112
            byte[] buffer = new byte[BUFFERSIZE];
            string options = "",filter = "";
            if (UserID == "")
                filter = "";
            else
                filter = "Pin=" + UserID;
            UserData = new UserData();
            UserData.UserID = UserID;
            List<UserTemplate> Templates = new List<UserTemplate>();
            if (IntPtr.Zero != h)
            {
                ret = GetDeviceData(h, ref buffer[0], BUFFERSIZE, "templatev10", str,filter , options);
            }
            else
            {
                Library.WriteErrorLog("Connect device failed!");
            }
            string[][] dataArray = Library.LoadCsv(Encoding.Default.GetString(buffer).TrimEnd('\0'), ',');
            if (ret >= 0)
            {
                for (int i = 1; i < dataArray.Length; i++)
                {
                    UserTemplate temp = new UserTemplate();
                    temp.TemplateSize = int.Parse ( dataArray[i][0]);
                    //temp.Password = dataArray[i][1];
                    //temp.UserID = dataArray[i][2];
                    temp.FingerID = int.Parse (dataArray[i][3]);
                    //temp.Valid = dataArray[i][4];
                    temp.Template = dataArray [i][5];
                    UserData.Templates.Add(temp);
                }
                Library.WriteErrorLog("templatev10 : Get " + ret + " records");
            }
            else
            {
                Library.WriteErrorLog("Get data failed.The error is " + ret);
            }
            
        }
        //To get All Cards set UserID To "" Empty 
        public string GetCards(string UserID)
        {
            int ret = 0;
            string str = "CardNo\tPin\tPassword\tGroup\tStartTime\tEndTime";//this.txtdevdata.Text;
            int BUFFERSIZE = 1 * 1024 * 1024 ; // increas it if data is larg to handle -112
            byte[] buffer = new byte[BUFFERSIZE];
            string options = "",filter = "";
            if (UserID != "")
                filter = "Pin=" + UserID;
            List<UserData> UserList = new List<UserData>();
            if (IntPtr.Zero != h)
            {
                ret = GetDeviceData(h, ref buffer[0], BUFFERSIZE, "user", str, filter , options);
            }
            else
            {
                Library.WriteErrorLog("Connect device failed!");
            }
            string[][] dataArray = Library.LoadCsv(Encoding.Default.GetString(buffer).TrimEnd('\0'), ',');
            if (ret >= 0)
            {
                //this.txtgetdata.Text = Encoding.Default.GetString(buffer);
                //string strcount = Encoding.Default.GetString(buffer);
                //dataArray = Library.LoadCsv(str, ',');
                for (int i = 1; i < dataArray.Length; i++)
                {
                    UserData temp = new UserData();
                    temp.CardNo  = dataArray[i][0];
                    temp.UserID = UserID;
                    temp.Password = dataArray[i][2];
                    UserList.Add(temp);
                }
                Library.WriteErrorLog("user : Get " + ret + " records");
            }
            else
            {
                Library.WriteErrorLog("Get data failed.The error is " + ret);
            }
            if (UserList.Count == 0)
                return "0";
            return UserList[0].CardNo;
        }


        // Vaild 1 is false 3 is true 
        public void RegisterFinger(string UserID,string FingerID,string Vaild, string Template)
        {
            int ret1 = 0, ret2 = 0;
            //string FileName = "transaction.dat";
            string tableName = "templatev10";
            string datas = "";
            string options = "";

            //Byte valid = 1;
            //if (true == forceFPMake.Checked)
            //{
            //    valid = 3;
            //}

            datas = "Pin=" + UserID + "\tFingerID=" + FingerID +
                "\tValid=" + Vaild + "\tTemplate=" + Template;
            if (IntPtr.Zero != h)
            {
                ret1 = SetDeviceData(h, tableName, datas, options);
                if (0 == ret1)
                {
                    Library.WriteErrorLog("Upload templatev10 succeed");
                }
                else
                {
                    Library.WriteErrorLog("Upload templatev10 failed.The error is：　" + ret1.ToString());
                }

                //////////////////////////////////////////////////////////////////////////////////////////////////////////////
                ret2 = SetDeviceData(h, "userauthorize", "Pin="+UserID +"\tAuthorizeTimezoneId=1\tAuthorizeDoorId=1", options);

                if (0 == ret2)
                {
                    Library.WriteErrorLog("Upload userauthorize succeed");
                }
                else
                {
                    Library.WriteErrorLog("Upload userauthorize failed.The error is：　" + ret2.ToString());
                }
            }
            else
            {
                Library.WriteErrorLog("Please connect device");
            }
        }
        public void RegisterCard(string UserID, string CardNo, string Password, string Group, string StartDate, string EndDate)
        {
            int ret = 0;
            //string FileName = "transaction.dat";
            string tableName = "user";
            string datas = "";
            string options = "";


            datas = "CardNo=" + CardNo + "\tPin=" + UserID +
                "\tPassword=" + Password + "\tGroup=" + Group+
                 "\tStartTime=" + StartDate + "\tEndTime=" + EndDate;
            if (IntPtr.Zero != h)
            {
                ret = SetDeviceData(h, tableName, datas, options);

                if (0 == ret)
                {
                    Library.WriteErrorLog("Upload card succeed");
                }
                else
                {
                    Library.WriteErrorLog("Upload card failed.The error is：　" + ret.ToString());
                }
            }
            else
            {
                Library.WriteErrorLog("Please connect device");
            }
        }

        public bool EnableUser(string UserID, bool Enable)
        {
            int ret = 0 , ret1 = 0 ;
            //string FileName = "transaction.dat";
            string tableName = "userauthorize";
            string datas = "";
            string options = "";
            if (Enable)
                datas = "Pin=" + UserID + "\tAuthorizeTimezoneId=1\tAuthorizeDoorId=1";
            else
                datas = "Pin=" + UserID + "\tAuthorizeTimezoneId=0\tAuthorizeDoorId=0";
         
            if (IntPtr.Zero != h)
            {
                ret1 = DeleteDeviceData(h, "userauthorize", "Pin=" + UserID, options);
                ret = SetDeviceData(h, "userauthorize", datas, options);
             
                if (0 == ret)
                {
                    Library.WriteErrorLog("Enable User succeed");
                }
                else
                {
                    Library.WriteErrorLog("Enable User.The error is：　" + ret.ToString());
                }
            }
            else
            {
                Library.WriteErrorLog("Please connect device");
            }
            if (ret >= 0)
                return true;
            else
                return false;
        }
    }

}
