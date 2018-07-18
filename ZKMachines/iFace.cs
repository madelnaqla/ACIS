using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ACIS
{
    public class iFace
    {
        public string IPAdreess;
        public string InOut;
        public string SerialNumber;
        public int Port = 4370; 
        public iFace(string IPAdreess, string InOut)
        {
            this.IPAdreess = IPAdreess;
            this.InOut = InOut;
        }
        /******************************************************************************************************************************************
        * Before you refer to this demo,we strongly suggest you read the development manual deeply first. 
        * This part is for demonstrating the communication with your device.The main commnication ways of Iface series are "TCP/IP","Serial Port" 
        * The communication way which you can use duing to the model of the device you have.
        * ****************************************************************************************************************************************/
       
        public  zkemkeeper.CZKEMClass  axCZKEM1 = new zkemkeeper.CZKEMClass();
        public bool bIsConnected = false;//the boolean value identifies whether the device is connected
        protected int iMachineNumber = 1;//the serial number of the device.After connecting the device ,this value will be changed.
        //If your device supports the TCP/IP communications, you can refer to this.
        //when you are using the tcp/ip communication,you can distinguish different devices by their IP address.
        public  void iFace_Connect()
        {
            int idwErrorCode = 0;
            axCZKEM1.Disconnect();

            this.axCZKEM1.OnFinger -= new zkemkeeper._IZKEMEvents_OnFingerEventHandler(axCZKEM1_OnFinger);
            this.axCZKEM1.OnVerify -= new zkemkeeper._IZKEMEvents_OnVerifyEventHandler(axCZKEM1_OnVerify);
            this.axCZKEM1.OnAttTransactionEx -= new zkemkeeper._IZKEMEvents_OnAttTransactionExEventHandler(axCZKEM1_OnAttTransactionEx);
            this.axCZKEM1.OnFingerFeature -= new zkemkeeper._IZKEMEvents_OnFingerFeatureEventHandler(axCZKEM1_OnFingerFeature);
            this.axCZKEM1.OnEnrollFingerEx -= new zkemkeeper._IZKEMEvents_OnEnrollFingerExEventHandler(axCZKEM1_OnEnrollFingerEx);
            this.axCZKEM1.OnDeleteTemplate -= new zkemkeeper._IZKEMEvents_OnDeleteTemplateEventHandler(axCZKEM1_OnDeleteTemplate);
            this.axCZKEM1.OnNewUser -= new zkemkeeper._IZKEMEvents_OnNewUserEventHandler(axCZKEM1_OnNewUser);
            this.axCZKEM1.OnHIDNum -= new zkemkeeper._IZKEMEvents_OnHIDNumEventHandler(axCZKEM1_OnHIDNum);
            this.axCZKEM1.OnAlarm -= new zkemkeeper._IZKEMEvents_OnAlarmEventHandler(axCZKEM1_OnAlarm);
            this.axCZKEM1.OnDoor -= new zkemkeeper._IZKEMEvents_OnDoorEventHandler(axCZKEM1_OnDoor);
            this.axCZKEM1.OnWriteCard -= new zkemkeeper._IZKEMEvents_OnWriteCardEventHandler(axCZKEM1_OnWriteCard);
            this.axCZKEM1.OnEmptyCard -= new zkemkeeper._IZKEMEvents_OnEmptyCardEventHandler(axCZKEM1_OnEmptyCard);
            

            bIsConnected = false;
            //Library.WriteErrorLog("Current State:DisConnected");

            bIsConnected = axCZKEM1.Connect_Net(IPAdreess, 4370);
            if (bIsConnected == true)
            {
               
                
                iMachineNumber = 1;//In fact,when you are using the tcp/ip communication,this parameter will be ignored,that is any integer will all right.Here we use 1.
                if (axCZKEM1.GetSerialNumber(iMachineNumber, out SerialNumber))
                {
                    //txtShow.Text = sdwSerialNumber;
                    Library.WriteErrorLog(IPAdreess + " - " + SerialNumber + " - Current State:Connected");
                }
                else
                {
                    axCZKEM1.GetLastError(ref idwErrorCode);
                    Library.WriteErrorLog("Operation failed,ErrorCode=" + idwErrorCode.ToString());
                }
                if (axCZKEM1.RegEvent(iMachineNumber, 65535))//Here you can register the realtime events that you want to be triggered(the parameters 65535 means registering all)
                {
                    this.axCZKEM1.OnFinger += new zkemkeeper._IZKEMEvents_OnFingerEventHandler(axCZKEM1_OnFinger);
                    this.axCZKEM1.OnVerify += new zkemkeeper._IZKEMEvents_OnVerifyEventHandler(axCZKEM1_OnVerify);
                    this.axCZKEM1.OnAttTransactionEx += new zkemkeeper._IZKEMEvents_OnAttTransactionExEventHandler(axCZKEM1_OnAttTransactionEx);
                    this.axCZKEM1.OnFingerFeature += new zkemkeeper._IZKEMEvents_OnFingerFeatureEventHandler(axCZKEM1_OnFingerFeature);
                    this.axCZKEM1.OnEnrollFingerEx += new zkemkeeper._IZKEMEvents_OnEnrollFingerExEventHandler(axCZKEM1_OnEnrollFingerEx);
                    this.axCZKEM1.OnDeleteTemplate += new zkemkeeper._IZKEMEvents_OnDeleteTemplateEventHandler(axCZKEM1_OnDeleteTemplate);
                    this.axCZKEM1.OnNewUser += new zkemkeeper._IZKEMEvents_OnNewUserEventHandler(axCZKEM1_OnNewUser);
                    this.axCZKEM1.OnHIDNum += new zkemkeeper._IZKEMEvents_OnHIDNumEventHandler(axCZKEM1_OnHIDNum);
                    this.axCZKEM1.OnAlarm += new zkemkeeper._IZKEMEvents_OnAlarmEventHandler(axCZKEM1_OnAlarm);
                    this.axCZKEM1.OnDoor += new zkemkeeper._IZKEMEvents_OnDoorEventHandler(axCZKEM1_OnDoor);
                    this.axCZKEM1.OnWriteCard += new zkemkeeper._IZKEMEvents_OnWriteCardEventHandler(axCZKEM1_OnWriteCard);
                    this.axCZKEM1.OnEmptyCard += new zkemkeeper._IZKEMEvents_OnEmptyCardEventHandler(axCZKEM1_OnEmptyCard);
                }
            }
            else
            {
                axCZKEM1.GetLastError(ref idwErrorCode);
                Library.WriteErrorLog( IPAdreess + " Error: Unable to connect the device,ErrorCode=" + idwErrorCode.ToString());
            }
           
        }
        public void iFace_Discoonect()
        {
            axCZKEM1.Disconnect();
            bIsConnected = false;
        }
        public void iFace_ReadLog()
        {
            if (bIsConnected == false)
            {
                Library.WriteErrorLog("Please connect the device first");
                return;
            }
            int idwErrorCode = 0;

            string sdwEnrollNumber = "";
            int idwVerifyMode = 0;
            int idwInOutMode = 0;
            int idwYear = 0;
            int idwMonth = 0;
            int idwDay = 0;
            int idwHour = 0;
            int idwMinute = 0;
            int idwSecond = 0;
            int idwWorkcode = 0;

            List<string[]> logData = new List<string[]>();
            int index = 0;
            axCZKEM1.EnableDevice(iMachineNumber, false);//disable the device
            if (axCZKEM1.ReadGeneralLogData(iMachineNumber))//read all the attendance records to the memory
            {
                while (axCZKEM1.SSR_GetGeneralLogData(iMachineNumber, out sdwEnrollNumber, out idwVerifyMode,
                            out idwInOutMode, out idwYear, out idwMonth, out idwDay, out idwHour, out idwMinute, out idwSecond, ref idwWorkcode))//get records from the memory
                {
                    string[] tmp = new string[10];
                    tmp[0] = sdwEnrollNumber;
                    tmp[1] = idwVerifyMode.ToString ();
                    tmp[2] = idwInOutMode.ToString();
                    tmp[3] = idwYear.ToString();
                    tmp[4] = idwMonth.ToString();
                    tmp[5] = idwDay.ToString();
                    tmp[6] = idwHour.ToString();
                    tmp[7] = idwMinute.ToString();
                    tmp[8] = idwSecond.ToString();
                    tmp[9] = idwWorkcode.ToString();
                    logData.Add(tmp);
                    //Library.insertByParameter(int.Parse (sdwEnrollNumber), new DateTime(idwYear, idwMonth,idwDay, idwHour, idwMinute, idwSecond), InOut, idwVerifyMode.ToString(), iMachineNumber, "Meminfo", idwWorkcode, SerialNumber, "UserExtFmt",6);
                    
                }
            }
            else
            {
                axCZKEM1.GetLastError(ref idwErrorCode);

                if (idwErrorCode != 0)
                {
                    Library.WriteErrorLog("Reading data from terminal failed,ErrorCode: " + idwErrorCode.ToString());
                }
                else
                {
                    Library.WriteErrorLog("No data from terminal returns!");
                }
            }
            axCZKEM1.EnableDevice(iMachineNumber, true);//enable the device
            for (int i = 0; i < logData.Count; i++)
            {
                string[] tmp = logData[i];
                sdwEnrollNumber = tmp[0];
                idwVerifyMode = int.Parse(tmp[1]);
                idwInOutMode = int.Parse(tmp[2]);
                idwYear = int.Parse(tmp[3]);
                idwMonth = int.Parse(tmp[4]);
                idwDay = int.Parse(tmp[5]);
                idwHour = int.Parse(tmp[6]);
                idwMinute = int.Parse(tmp[7]);
                idwSecond = int.Parse(tmp[8]);
                idwWorkcode = int.Parse(tmp[9]);
                DateTime dt = new DateTime(idwYear, idwMonth, idwDay, idwHour, idwMonth, idwDay);
                Library.insertByParameter(int.Parse ( sdwEnrollNumber), dt, InOut, idwVerifyMode.ToString(), iMachineNumber, "Meminfo", idwWorkcode, SerialNumber, "1",0);
                    
            }
        }

        public void SaveTemplates()
        {
            if (bIsConnected == false)
            {
                Library.WriteErrorLog("Please connect the device first!");
                return;
            }


            string sdwEnrollNumber = "";
            string sName = "";
            string sPassword = "";
            int iPrivilege = 0;
            bool bEnabled = false;

            int idwFingerIndex;
            string sTmpData = "";
            int iTmpLength = 0;
            int iFlag = 0;

            axCZKEM1.EnableDevice(iMachineNumber, false);

            axCZKEM1.ReadAllUserID(iMachineNumber);//read all the user information to the memory
            axCZKEM1.ReadAllTemplate(iMachineNumber);//read all the users' fingerprint templates to the memory
            while (axCZKEM1.SSR_GetAllUserInfo(iMachineNumber, out sdwEnrollNumber, out sName, out sPassword, out iPrivilege, out bEnabled))//get all the users' information from the memory
            {
                for (idwFingerIndex = 0; idwFingerIndex < 10; idwFingerIndex++)
                {
                    if (axCZKEM1.GetUserTmpExStr(iMachineNumber, sdwEnrollNumber, idwFingerIndex, out iFlag, out sTmpData, out iTmpLength))//get the corresponding templates string and length from the memory
                    {
                    }
                }
            }
            axCZKEM1.EnableDevice(iMachineNumber, true);
        }

        public void SaveFaces()
        {
            if (bIsConnected == false)
            {
                Library.WriteErrorLog("Please connect the device first!");
                return;
            }

            string sUserID = "";
            string sName = "";
            string sPassword = "";
            int iPrivilege = 0;
            bool bEnabled = false;

            int iFaceIndex = 50;//the only possible parameter value
            string sTmpData = "";
            int iLength = 0;

            axCZKEM1.EnableDevice(iMachineNumber, false);
            axCZKEM1.ReadAllUserID(iMachineNumber);//read all the user information to the memory

            while (axCZKEM1.SSR_GetAllUserInfo(iMachineNumber, out sUserID, out sName, out sPassword, out iPrivilege, out bEnabled))//get all the users' information from the memory
            {
                if (axCZKEM1.GetUserFaceStr(iMachineNumber, sUserID, iFaceIndex, ref sTmpData, ref iLength))//get the face templates from the memory
                {
           
                
                }
            }
            axCZKEM1.EnableDevice(iMachineNumber, true);
        }

        public void SaveCards()
        {
            if (bIsConnected == false)
            {
                Library.WriteErrorLog("Please connect the device first!");
                return;
            }
            string sdwEnrollNumber = "";
            string sName = "";
            string sPassword = "";
            int iPrivilege = 0;
            bool bEnabled = false;
            string sCardnumber = "";

          
            axCZKEM1.EnableDevice(iMachineNumber, false);//disable the device
            axCZKEM1.ReadAllUserID(iMachineNumber);//read all the user information to the memory
            while (axCZKEM1.SSR_GetAllUserInfo(iMachineNumber, out sdwEnrollNumber, out sName, out sPassword, out iPrivilege, out bEnabled))//get user information from memory
            {
                if (axCZKEM1.GetStrCardNumber(out sCardnumber))//get the card number from the memory
                {
                   
                }
            }
            axCZKEM1.EnableDevice(iMachineNumber, true);//enable the device
        }


      
        public bool RegisterCard(string sdwEnrollNumber,string sCardnumber)
        {
            string sName="", sPassword=""; bool bEnabled=true ; int iPrivilege=0; int EnrollNumber=0;
            bool Result;
            if (bIsConnected == false)
            {
               Library.WriteErrorLog("Please connect the device first!");
                return false ;
            }
            int idwErrorCode = 0;
            axCZKEM1.EnableDevice(iMachineNumber, false);
            axCZKEM1.GetAllUserInfo(iMachineNumber, EnrollNumber, sName, sPassword, iPrivilege, bEnabled);

            Result = axCZKEM1.SetStrCardNumber(sCardnumber);//Before you using function SetUserInfo,set the card number to make sure you can upload it to the device

            if (axCZKEM1.SSR_SetUserInfo(iMachineNumber, sdwEnrollNumber, sName, sPassword, iPrivilege, bEnabled))//upload the user's information(card number included)
            {
               Library.WriteErrorLog("(SSR_)SetUserInfo,UserID:" + sdwEnrollNumber + " Privilege:" + iPrivilege.ToString() + " Enabled:" + bEnabled.ToString());
               //Result = axCZKEM1.SetStrCardNumber(sCardnumber);//Before you using function SetUserInfo,set the card number to make sure you can upload it to the device
                
            }
            else
            {
                axCZKEM1.GetLastError(ref idwErrorCode);
                Library.WriteErrorLog("Operation failed,ErrorCode=" + idwErrorCode.ToString());
                Result =false;
            }
            axCZKEM1.RefreshData(iMachineNumber);//the data in the device should be refreshed
            axCZKEM1.EnableDevice(iMachineNumber, true);
            return Result;

        }
        public bool RegisterTemplate(string sdwEnrollNumber, int idwFingerIndex, string sTmpData, int TmpLength)
        {
            string Name = ""; int iPrivilege = 0; string sPassword = ""; int iFlag = 0; bool bEnabled = true;
            if (bIsConnected == false)
            {
                Library.WriteErrorLog("Please connect the device first!");
                return false;
            }
            int idwErrorCode = 0;
            axCZKEM1.EnableDevice(iMachineNumber, false);


            //

            //axCZKEM1.GetUserInfo(iMachineNumber, int.Parse(sdwEnrollNumber), ref Name, ref sPassword, ref iPrivilege, ref bEnabled);
            //if (axCZKEM1.SSR_SetUserInfo(iMachineNumber, sdwEnrollNumber, Name, sPassword, iPrivilege, bEnabled))
            bool result = axCZKEM1.SetUserTmpExStr(iMachineNumber, sdwEnrollNumber, idwFingerIndex, iFlag, sTmpData);
            if (axCZKEM1.SetUserTmpStr(iMachineNumber, int.Parse(sdwEnrollNumber), idwFingerIndex, sTmpData))
            {
                //upload user information to the device
                Library.WriteErrorLog("Operation Finger success  " + sdwEnrollNumber);
                axCZKEM1.SetUserTmpExStr(iMachineNumber, sdwEnrollNumber, idwFingerIndex, iFlag, sTmpData);//upload templates information to the device
            }
            else
            {
                axCZKEM1.GetLastError(ref idwErrorCode);
                Library.WriteErrorLog("Operation Finger failed,ErrorCode=" + idwErrorCode.ToString());
            }

            axCZKEM1.RefreshData(iMachineNumber);//the data in the device should be refreshed
            axCZKEM1.EnableDevice(iMachineNumber, true);
            return result;
        }
        public bool RegisterFace(string sUserID,int iFaceIndex,string sTmpData,int iLength)
        {
            if (bIsConnected == false)
            {
                Library.WriteErrorLog("Please connect the device first!");
                return false ;
            }
            int idwErrorCode = 0;
            bool result;
            //if (axCZKEM1.SSR_SetUserInfo(iMachineNumber, sUserID, sName, sPassword, iPrivilege, bEnabled))//face templates are part of users' information
            if( axCZKEM1.SetUserFaceStr(iMachineNumber, sUserID, iFaceIndex, sTmpData, iLength))
            {
                result = true;
                Library.WriteErrorLog("Operation Face success  "+ sUserID );
                //axCZKEM1.SetUserFaceStr(iMachineNumber, sUserID, iFaceIndex, sTmpData, iLength);//upload face templates information to the device
            }
            else
            {
                result = false;
                axCZKEM1.GetLastError(ref idwErrorCode);
                Library.WriteErrorLog("Operation Face failed,ErrorCode=" + idwErrorCode.ToString());
                axCZKEM1.EnableDevice(iMachineNumber, true);
            }
            return result ;
        }
        public void DeleteTemp(int sdwEnrollNumber)
        {
            bool result;
            axCZKEM1.EnableDevice(iMachineNumber, false);
            for (int i = 0; i < 10; i++)
                result =  axCZKEM1.SSR_DelUserTmp(iMachineNumber, sdwEnrollNumber.ToString (), i);
            axCZKEM1.EnableDevice(iMachineNumber, true );
        }
        public void DeleteFace(int sdwEnrollNumber)
        {
            bool result;
            axCZKEM1.EnableDevice(iMachineNumber, false);
            for (int i = 0; i < 15; i++)
                result = axCZKEM1.DelUserFace(iMachineNumber, sdwEnrollNumber.ToString (), i);
            axCZKEM1.EnableDevice(iMachineNumber, true );
        }

        public void GetUserData(string UserID, out UserData UserData)
        {

            string Name, Password;
            int Privilege, FaceIndex = 0, TmpLength = 128 * 1024, idwErrorCode = 0;  // FaceIndex = 50 for alll fingers 
            //byte[] byTmpData = new byte[TmpLength];
            string byTmpData = "";
            bool Enable;
            axCZKEM1.EnableDevice(iMachineNumber, false);
            axCZKEM1.RefreshData(iMachineNumber);
            //Templates = new List<UserTemplate>();
            //Faces = new List<UserFace>();
            //UserData data = new UserData();
            UserData = new UserData();
            UserData.UserID = UserID;
            axCZKEM1.SSR_GetUserInfo(iMachineNumber, UserID, out UserData.Name, out UserData.Password, out  UserData.Privilege, out UserData.Enable);
            axCZKEM1.GetStrCardNumber(out UserData.CardNo);


            for (FaceIndex = 0; FaceIndex <= 15; FaceIndex++)
            {
                if (axCZKEM1.GetUserFaceStr(iMachineNumber, UserID, FaceIndex, ref byTmpData, ref TmpLength))
                {
                    UserFace face = new UserFace();
                    face.FaceID = FaceIndex;
                    face.FaceSize = TmpLength;
                    face.Face = byTmpData;
                    UserData.Faces.Add(face);

                    //Here you can manage the information of the face templates according to your request.(for example,you can sava them to the database)
                    //MessageBox.Show("GetUserFace,the  length of the bytes array byTmpData is " + iLength.ToString(), "Success");
                }
                else
                {
                    axCZKEM1.GetLastError(ref idwErrorCode);
                    //MessageBox.Show("Operation failed,ErrorCode=" + idwErrorCode.ToString(), "Error");
                }

            }


            int idwFingerIndex;
            string sTmpData = "";
            int iTmpLength = 0;
            int iFlag = 0;
            //for (idwFingerIndex = 0; idwFingerIndex < 10; idwFingerIndex++)
            //{
            //    bool test = axCZKEM1.GetUserTmpExStr(iMachineNumber, UserID, idwFingerIndex, out iFlag, out  sTmpData, out iTmpLength);
            //    if (axCZKEM1.GetUserTmpStr(iMachineNumber, int.Parse(UserID), idwFingerIndex, ref sTmpData, ref iTmpLength))//get the corresponding templates string and length from the memory
            //    {
            //        UserTemplate template = new UserTemplate();
            //        template.FingerID = idwFingerIndex;
            //        template.TemplateSize = TmpLength;
            //        template.Template = sTmpData;
            //        //template.TempBin = TempBin;
            //        UserData.Templates.Add(template);
            //    }
            //    else
            //    {
            //        axCZKEM1.GetLastError(ref idwErrorCode);
            //        //MessageBox.Show("Operation failed,ErrorCode=" + idwErrorCode.ToString(), "Error");
            //    }
            //}
            axCZKEM1.ReadAllUserID(iMachineNumber);//read all the user information to the memory
            axCZKEM1.ReadAllTemplate(iMachineNumber);//read all the users' fingerprint templates to the memory
            if (axCZKEM1.SSR_GetUserInfo (iMachineNumber,  UserID, out Name, out  Password, out  Privilege, out Enable))//get all the users' information from the memory
            //{
                for (idwFingerIndex = 0; idwFingerIndex < 10; idwFingerIndex++)
                {
                    if (axCZKEM1.GetUserTmpExStr(iMachineNumber, UserID, idwFingerIndex, out iFlag, out sTmpData, out iTmpLength))//get the corresponding templates string and length from the memory
                    {
                        UserTemplate template = new UserTemplate();
                        template.FingerID = idwFingerIndex;
                        template.TemplateSize = TmpLength;
                        template.Template = sTmpData;
                        //template.TempBin = TempBin;
                        UserData.Templates.Add(template);
                    }
                }
           // }

            axCZKEM1.EnableDevice(iMachineNumber, true);
        }

        public bool EnableUser(string UserID, bool Enable)
        {
            int idwErrorCode = 0;
            axCZKEM1.EnableDevice(iMachineNumber, false);
            bool Result = axCZKEM1.SSR_EnableUser(iMachineNumber, UserID, Enable);
            if(Result == false )
                axCZKEM1.GetLastError(ref idwErrorCode);
            axCZKEM1.EnableDevice(iMachineNumber, true );
            return Result;

        }
        public void SaveLogs()
        {
            int idwErrorCode = 0;
            axCZKEM1.EnableDevice(iMachineNumber, false);
        }
        public List<string> UserIDs()
        {
            List<string> Users = new List<string>();

            if (bIsConnected == false)
            {
                return Users;
            }
            else
            {
                string sEnrollNumber = "";
                string sName = "";
                string sPassword = "";
                int iPrivilege = 0;
                bool bEnabled = false;

                axCZKEM1.EnableDevice(iMachineNumber, false);
                axCZKEM1.ReadAllUserID(iMachineNumber);//read all the user information to the memory
                while (axCZKEM1.SSR_GetAllUserInfo(iMachineNumber, out sEnrollNumber, out sName, out sPassword, out iPrivilege, out bEnabled))
                {
                    Users.Add(sEnrollNumber);
                }
                axCZKEM1.EnableDevice(iMachineNumber, true);
            }
            return Users;
        }
        #region RealTime Events

        //When you place your finger on sensor of the device,this event will be triggered
        private void axCZKEM1_OnFinger()
        {
            Library.WriteErrorLog("RTEvent OnFinger Has been Triggered");
        }

        //After you have placed your finger on the sensor(or swipe your card to the device),this event will be triggered.
        //If you passes the verification,the returned value userid will be the user enrollnumber,or else the value will be -1;
        private void axCZKEM1_OnVerify(int iUserID)
        {
            //Library.WriteErrorLog("RTEvent OnVerify Has been Triggered,Verifying...");
            if (iUserID != -1)
            {
                //Library.WriteErrorLog("Verified OK,the UserID is " + iUserID.ToString());
            }
            else
            {
                //Library.WriteErrorLog("Verified Failed... ");
            }
        }

        //If your fingerprint(or your card) passes the verification,this event will be triggered
        private void axCZKEM1_OnAttTransactionEx(string sEnrollNumber, int iIsInValid, int iAttState, int iVerifyMethod, int iYear, int iMonth, int iDay, int iHour, int iMinute, int iSecond, int iWorkCode)
        {
            
            //Library.WriteErrorLog("RTEvent OnAttTrasactionEx Has been Triggered,Verified OK");
            //Library.WriteErrorLog("...UserID:" + sEnrollNumber);
            //Library.WriteErrorLog("...isInvalid:" + iIsInValid.ToString());
            //Library.WriteErrorLog("...attState:" + iAttState.ToString());
            //Library.WriteErrorLog("...VerifyMethod:" + iVerifyMethod.ToString());
            //Library.WriteErrorLog("...Workcode:" + iWorkCode.ToString());//the difference between the event OnAttTransaction and OnAttTransactionEx
            //Library.WriteErrorLog("...Time:" + iYear.ToString() + "-" + iMonth.ToString() + "-" + iDay.ToString() + " " + iHour.ToString() + ":" + iMinute.ToString() + ":" + iSecond.ToString());
            Library.WriteErrorLog(IPAdreess + "   " + sEnrollNumber + "," + iIsInValid.ToString() + "," + iAttState.ToString() + "," + iVerifyMethod.ToString() + "," + iWorkCode.ToString() + "," + iYear.ToString() + "-" + iMonth.ToString() + "-" + iDay.ToString() + " " + iHour.ToString() + ":" + iMinute.ToString() + ":" + iSecond.ToString());
            Library.insertByParameter(int.Parse ( sEnrollNumber), new DateTime ( iYear, iMonth, iDay, iHour,iMinute, iSecond), InOut, iVerifyMethod.ToString(), iMachineNumber, "Meminfo", iWorkCode, SerialNumber, "1",0);
        }

        //When you have enrolled your finger,this event will be triggered and return the quality of the fingerprint you have enrolled
        private void axCZKEM1_OnFingerFeature(int iScore)
        {
            if (iScore < 0)
            {
                Library.WriteErrorLog("The quality of your fingerprint is poor");
            }
            else
            {
                Library.WriteErrorLog("RTEvent OnFingerFeature Has been Triggered...Score:　" + iScore.ToString());
            }
        }

        //When you are enrolling your finger,this event will be triggered.(The event can only be triggered by TFT screen devices)
        private void axCZKEM1_OnEnrollFingerEx(string sEnrollNumber, int iFingerIndex, int iActionResult, int iTemplateLength)
        {
            if (iActionResult == 0)
            {
                Library.WriteErrorLog("RTEvent OnEnrollFigerEx Has been Triggered....");
                Library.WriteErrorLog(".....UserID: " + sEnrollNumber + " Index: " + iFingerIndex.ToString() + " tmpLen: " + iTemplateLength.ToString());
            }
            else
            {
                Library.WriteErrorLog("RTEvent OnEnrollFigerEx Has been Triggered Error,actionResult=" + iActionResult.ToString());
            }
        }

        //When you have deleted one one fingerprint template,this event will be triggered.
        private void axCZKEM1_OnDeleteTemplate(int iEnrollNumber, int iFingerIndex)
        {
            Library.WriteErrorLog("RTEvent OnDeleteTemplate Has been Triggered...");
            Library.WriteErrorLog("...UserID=" + iEnrollNumber.ToString() + " FingerIndex=" + iFingerIndex.ToString());
        }

        //When you have enrolled a new user,this event will be triggered.
        private void axCZKEM1_OnNewUser(int iEnrollNumber)
        {
            Library.WriteErrorLog("RTEvent OnNewUser Has been Triggered...");
            Library.WriteErrorLog("...NewUserID=" + iEnrollNumber.ToString());
        }

        //When you swipe a card to the device, this event will be triggered to show you the card number.
        private void axCZKEM1_OnHIDNum(int iCardNumber)
        {
            Library.WriteErrorLog("RTEvent OnHIDNum Has been Triggered...");
            Library.WriteErrorLog("...Cardnumber=" + iCardNumber.ToString());
        }

        //When the dismantling machine or duress alarm occurs, trigger this event.
        private void axCZKEM1_OnAlarm(int iAlarmType, int iEnrollNumber, int iVerified)
        {
            Library.WriteErrorLog("RTEvnet OnAlarm Has been Triggered...");
            Library.WriteErrorLog("...AlarmType=" + iAlarmType.ToString());
            Library.WriteErrorLog("...EnrollNumber=" + iEnrollNumber.ToString());
            Library.WriteErrorLog("...Verified=" + iVerified.ToString());
        }

        //Door sensor event
        private void axCZKEM1_OnDoor(int iEventType)
        {
            Library.WriteErrorLog("RTEvent Ondoor Has been Triggered...");
            Library.WriteErrorLog("...EventType=" + iEventType.ToString());
        }

        //When you have emptyed the Mifare card,this event will be triggered.
        private void axCZKEM1_OnEmptyCard(int iActionResult)
        {
            Library.WriteErrorLog("RTEvent OnEmptyCard Has been Triggered...");
            if (iActionResult == 0)
            {
                Library.WriteErrorLog("...Empty Mifare Card OK");
            }
            else
            {
                Library.WriteErrorLog("...Empty Failed");
            }
        }

        //When you have written into the Mifare card ,this event will be triggered.
        private void axCZKEM1_OnWriteCard(int iEnrollNumber, int iActionResult, int iLength)
        {
            Library.WriteErrorLog("RTEvent OnWriteCard Has been Triggered...");
            if (iActionResult == 0)
            {
                Library.WriteErrorLog("...Write Mifare Card OK");
                Library.WriteErrorLog("...EnrollNumber=" + iEnrollNumber.ToString());
                Library.WriteErrorLog("...TmpLength=" + iLength.ToString());
            }
            else
            {
                Library.WriteErrorLog("...Write Failed");
            }
        }
       
        //After function GetRTLog() is called ,RealTime Events will be triggered. 
        //When you are using these two functions, it will request data from the device forwardly.
        private void rtTimer_Tick(object sender, EventArgs e)
        {
            if (axCZKEM1.ReadRTLog(iMachineNumber))
            {
                while (axCZKEM1.GetRTLog(iMachineNumber))
                {
                    ;
                }
            }
        }

        #endregion

    }
}
