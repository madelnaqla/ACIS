using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using ACIS;
using System.IO;
namespace RegisterFinger
{
    public partial class fmRegister : Form
    {
        public fmRegister()
        {
            InitializeComponent();
        }
        //public zkemkeeper.CZKEMClass axCZKEM1 = new zkemkeeper.CZKEMClass();

   
        [DllImport("plcommpro.dll", EntryPoint = "SearchDevice")]
        public static extern int SearchDevice(string commtype, string address, ref byte buffer);
        [DllImport("plcommpro.dll", EntryPoint = "SetDeviceData")]
        public static extern int SetDeviceData(IntPtr h, string tablename, string data, string options);

        string[][] Devices;
        iFace iFaceDevice;
        MA500 MA500Device;
         //List<UserTemplate> Cards = new List<UserTemplate>();
        //List<UserTemplate> Templates = new List<UserTemplate>();
        //List<UserFace> Faces = new List<UserFace>();
        UserData UserDataTemp = new UserData();
        string CardNo;
        string ip, inout, type;
        private void fmRegister_Load(object sender, EventArgs e)
        {
            Devices = ACIS.Library.LoadCsv(System.IO.File.ReadAllText("Config.csv"), ';');
            for (int i = 1; i < Devices.Length; i++)
            {
                if (Devices[i][1] == "1")
                {
                    ListViewItem lvi = new ListViewItem(Devices[i]);
                    lvi.SubItems.Add("");
                    lsdevices.Items.Add(lvi);
                    lvi = new ListViewItem(Devices[i]);
                    lvi.SubItems.Add("");
                    lvDevicestb2.Items.Add(lvi);
                    lvi = new ListViewItem(Devices[i]);
                    lvi.SubItems.Add("");
                    lvDevicestb3.Items.Add(lvi);
                }
                else
                {
                    ComboboxItem item = new ComboboxItem();
                    item.Text = Devices[i][0];
                    item.Value = Devices[i][2];
                    cmbdevices.Items.Add(item);
                    cbDevicestb3.Items.Add(item);
                    cbDevicestb4.Items.Add(item);
                }
            }


            /////////////////////////////////////////////////////////////////////////////////////
            //List<string[]> Users = Library.GetUsers();
            //for (int i = 0; i < Users.Count; i++)
            //{
            //    cbUserTb2.Items.Add(Users[i][0]);
            //    cbUserTb3.Items.Add(Users[i][0]);

            //}
            /////////////////////////////////////////////////////////////////////////////////////
        }

        private void cmbdevices_SelectedIndexChanged(object sender, EventArgs e)
        {
            //foreach (ListViewItem lv in lsdevices.Items)
            //    lv.Checked = true;
            //this.lsdevices.Items[cmbdevices.SelectedIndex].Checked = false ;
            if (Devices[cmbdevices.SelectedIndex + 1][2] == "MA500")
            {
                cbFace.Visible = false;
                cbFace.Checked = false;
                txtFaceCount.Visible = false;
            }
            else
            {
                cbFace.Visible = true ;
                cbFace.Checked = true ;
                txtFaceCount.Visible = true;
            }
        }

        private void lsdevices_ItemCheck(object sender, ItemCheckEventArgs e)
        {
            //if (e.Index == cmbdevices.SelectedIndex)
            //    e.NewValue =   CheckState.Unchecked;//MessageBox.Show("Error");
        }

        private void btConnect_Click(object sender, EventArgs e)
        {
            if (cmbdevices.SelectedIndex < 0)
                return;
            //////////////////////////////////////////////////////DisConnect/////////////////////////////
            ComboboxItem cbitem = (ComboboxItem)cmbdevices.SelectedItem;
            //cbUser.Items.Clear();
            ip = cbitem.Text;
            type = cbitem.Value.ToString ();
            Cursor = Cursors.WaitCursor;
            if (btConnect.Text == "DisConnect")
            {
                
                if (iFaceDevice != null)
                {
                    if (iFaceDevice.bIsConnected)
                    {
                        iFaceDevice.iFace_Discoonect();
                        cmbdevices.Enabled = true ;
                        lbstatus.Text = iFaceDevice.IPAdreess + " DisConnected";
                    }
                }
                if (MA500Device != null)
                {
                    if (MA500Device.bIsConnected )
                    {
                        MA500Device.MA500_Disconnect();
                        cmbdevices.Enabled = true;
                        lbstatus.Text = MA500Device.IPAdreess  + " DisConnected";
                        cmbdevices.SelectedIndex = -1;
                    }
                }
                btConnect.Text = "Connect";
                cbUser.Items.Clear();
                txtCardNumber.Text = "";
                txtFaceCount.Text = "";
                txtFingerCount.Text = "";
                Cursor = Cursors.Default;
                return;
            }
            //////////////////////////////////////////////////////Connect/////////////////////////////
          
            List<string> Users = new List<string>();
            //lbstatus.Text = cmbdevices.SelectedItem.ToString() + " " + type ;
            if ( type == "IFACE")
            {
                iFaceDevice = new iFace(ip, "");
                iFaceDevice.axCZKEM1.OnHIDNum += new zkemkeeper._IZKEMEvents_OnHIDNumEventHandler(axCZKEM1_OnHIDNum); 
                Cursor = Cursors.WaitCursor;
                cbUser.Items.Clear();
                //cbUser.BeginUpdate();
                iFaceDevice.iFace_Connect();
                if (iFaceDevice.bIsConnected)
                {
                    btConnect.Text = "DisConnect";
                    lbstatus.Text = ip + " Connected ";
                    cmbdevices.Enabled = false;
                    Users = iFaceDevice.UserIDs();
                    if (Users.Count <= 0)
                    {
                        lbstatus.Text = "Error Reading Users";
                    }
                    cbUser.Items.Clear();
                    foreach (string user in Users)
                    {
                        cbUser.Items.Add(user);
                    }
                }
                else
                {
                    cmbdevices.Enabled = true;
                    lbstatus.Text = "Connection Faild";
                }
                cbUser.EndUpdate();
                Cursor = Cursors.Default;
            }
            if ( type == "MA500")
            {
                MA500Device = new MA500(ip, Devices[cmbdevices.SelectedIndex + 1][2]);
                cmbdevices.Enabled = false;
                Cursor = Cursors.WaitCursor;
                MA500Device.MA500_Connect();
                if (MA500Device.bIsConnected)
                {

                    MA500Device.EnableCradTimer();
                    MA500Device.ReadingCard += MA500Device_ReadingCard;

                    btConnect.Text = "DisConnect";
                    lbstatus.Text = ip + " Connected ";
                    Users = MA500Device.MA500_Users();
                   
                    if (Users.Count <= 0)
                    {
                        lbstatus.Text = "Error Reading Users";
                    }
                    cbUser.Items.Clear();
                    cbUser.Items.AddRange(Users.ToArray());
                }
                else
                {
                    cmbdevices.Enabled = true;
                    lbstatus.Text = "Connection Faild";
                }
                //MA500Device.MA500_Disconnect();
                cbUser.EndUpdate();
                Cursor = Cursors.Default;
            }
        }

        private void cbUser_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (type == "IFACE")
            {
                
            }
            if (type == "MA500")
            {
            }
        }

        private void btnCards_Click(object sender, EventArgs e)
        {
            Cursor = Cursors.WaitCursor;
          
            for (int i = 1; i < Devices.Length; i++)
            {
                ip = Devices[i][0];
                inout = Devices[i][1];
                type = Devices[i][2];

                bool Selected = this.lsdevices.Items[i-1].Checked;
                //lsdevices.BeginUpdate();
                if (Selected && Devices[i][2] == "IFACE")
                {
                    iFace ifacedevice = new iFace(ip, Devices[cmbdevices.SelectedIndex + 1][2]);
                    ifacedevice.iFace_Connect();
                    if (ifacedevice.bIsConnected)
                    {
                        lsdevices.Items[i-1].SubItems[3].Text  = "Connected";
                    }
                    else 
                    {
                        lsdevices.Items[i-1].SubItems[3].Text = "disconnect";
                    }
                    lsdevices.EndUpdate();
                    ifacedevice.iFace_Discoonect();
                }
                if (Selected && Devices[i][2] == "MA500")
                {
                    MA500 MA500device = new MA500(ip, Devices[cmbdevices.SelectedIndex + 1][2]);
                    MA500device.MA500_Connect();
                    
                    if (MA500device.bIsConnected)
                    {
                        lsdevices.Items[i-1].SubItems[3].Text = "Connected";
                        
                    }
                    else
                    {
                        lsdevices.Items[i-1].SubItems[3].Text = "disconnect";
                    }
                    lsdevices.EndUpdate();
                    MA500device.MA500_Disconnect();
                }
            }
            Cursor = Cursors.Default;
        }
        private void axCZKEM1_OnHIDNum(int iCardNumber)
        {
            txtCardNumbertb3.Text = iCardNumber.ToString();
            txtCardNumber.Text = iCardNumber.ToString(); 
        }
        private void cbUser_SelectedIndexChanged_1(object sender, EventArgs e)
        {
            ip = Devices[cmbdevices.SelectedIndex + 1][0];
            inout = Devices[cmbdevices.SelectedIndex + 1][1];
            type = Devices[cmbdevices.SelectedIndex + 1][2];
            string UserID = cbUser.SelectedItem.ToString ();
            CardNo = "";
            Cursor = Cursors.WaitCursor;
            cbUser.BeginUpdate();
            if (type == "IFACE")
            {
                if (iFaceDevice.bIsConnected)
                {
                    iFaceDevice.GetUserData(UserID, out UserDataTemp);
                    txtFingerCount.Text = UserDataTemp.Templates.Count.ToString();
                    txtFaceCount.Text = UserDataTemp.Faces.Count.ToString();
                    txtCardNumber.Text = UserDataTemp.CardNo;
                   /////////////////////////////////
                //    if (Faces.Count > 0)
                //    {
                //        using (System.IO.MemoryStream msCamera = new MemoryStream(Encoding.ASCII.GetBytes(Faces[0].Template), 0, int.Parse(Faces[0].TemplateSize)))
                //        {
                //            using (Bitmap bt = new Bitmap(msCamera))
                //            {
                //                if (bt != null)
                //                {

                //                    Bitmap Pic = new Bitmap(msCamera);
                //                    this.pictureBox1.Image = Pic;

                //                }

                //            }
                //        }
                //    }
                }
                else
                {

                }
            }
            if (type == "MA500")
            {
                if (MA500Device.bIsConnected)
                {
                    txtCardNumber.Text = MA500Device.GetCards(UserID);
                    MA500Device.GetTemplates(UserID,out UserDataTemp );
                    txtFingerCount.Text = UserDataTemp.Templates.Count.ToString();
                }
                else
                {
                }  
            }
            cbUser.EndUpdate();
            Cursor = Cursors.Default;
        }

        

        private void cbUser_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyValue == 13)
            {
                e.Handled = true;
                cbUser.Focus();

            }
        }

        private void btnConnecttb3_Click(object sender, EventArgs e)
        {
            if (cbDevicestb3.SelectedIndex < 0)
                return;
            //////////////////////////////////////////////////////DisConnect/////////////////////////////
            ComboboxItem cbitem = (ComboboxItem)cbDevicestb3.SelectedItem;
            ip = cbitem.Text;
            type = cbitem.Value.ToString();
            Cursor = Cursors.WaitCursor;
            if (btnConnecttb3.Text == "DisConnect")
            {
                if (iFaceDevice != null)
                {
                    if (iFaceDevice.bIsConnected)
                    {
                        iFaceDevice.iFace_Discoonect();
                        cbDevicestb3.Enabled = true;
                        cbDevicestb3.SelectedIndex = -1;
                    }
                }
                if (MA500Device != null)
                {
                    if (MA500Device.bIsConnected)
                    {
                        MA500Device.MA500_Disconnect();
                        cbDevicestb3.Enabled = true;
                        cbDevicestb3.SelectedIndex = -1;
                    }
                }
                btnConnecttb3.Text = "Connect";

                Cursor = Cursors.Default;
                return;
            }
            //////////////////////////////////////////////////////Connect/////////////////////////////

            List<string> Users = new List<string>();
            //lbstatus.Text = cmbdevices.SelectedItem.ToString() + " " + type ;
            if (type == "IFACE")
            {
                iFaceDevice = new iFace(ip, "");
                iFaceDevice.axCZKEM1.OnHIDNum += new zkemkeeper._IZKEMEvents_OnHIDNumEventHandler(axCZKEM1_OnHIDNum);
                Cursor = Cursors.WaitCursor;
                iFaceDevice.iFace_Connect();
                if (iFaceDevice.bIsConnected)
                {
                    btnConnecttb3.Text = "DisConnect";
                    lblStatustb3.Text = ip + " Connected ";
                    cbDevicestb3.Enabled = false;                    
                }
                else
                {
                    cbDevicestb3.Enabled = true;
                    lblStatustb3.Text = "Connection Faild";
                }
                Cursor = Cursors.Default;
            }
            if (type == "MA500")
            {
                MA500Device = new MA500(ip, Devices[cbDevicestb3.SelectedIndex + 1][2]);
                cbDevicestb3.Enabled = false;
                Cursor = Cursors.WaitCursor;
                MA500Device.MA500_Connect();
                MA500Device.ReadingCard += MA500Device_ReadingCard;      

                if (MA500Device.bIsConnected)
                {

                    btnConnecttb3.Text = "DisConnect";
                    lblStatustb3.Text = ip + " Connected ";
                    cbDevicestb3.Enabled = false;

               }
                else
                {
                    cbDevicestb3.Enabled = true ;
                    lblStatustb3.Text = "Connection Faild";
                }
                //MA500Device.MA500_Disconnect();
                cbUserTb3.EndUpdate();
                Cursor = Cursors.Default;
            }
        }

        void MA500Device_ReadingCard(ref string CardNo)
        {

            txtCardNumbertb3.Text = CardNo;
            txtCardNumber.Text = CardNo;
        }

        private void btUpload_Click(object sender, EventArgs e)
        {
            //if (cmbdevices.SelectedIndex == -1 )
            //{
            //    MessageBox.Show("Select Device First","Error");
            //    return;
            //}
            //if (iFaceDevice== null  || MA500Device == null  )
            //{
            //    MessageBox.Show("Device is not Connected", "Error");
            //    return;
            //}
            //if (iFaceDevice.bIsConnected  || MA500Device.bIsConnected )
            //{
            //    MessageBox.Show("Device is not Connected", "Error");
            //    return;
            //}
            //if (cbUser.SelectedIndex == -1)
            //{
            //    MessageBox.Show("Select User First", "Error");
            //    return;
            //}
            for (int i = 0; i < lsdevices.Items.Count; i++)
            {
                lsdevices.Items[i].SubItems[3].Text = "";
                lsdevices.Refresh();
            }
            string IP, InOut, Type;
            lsdevices.BeginUpdate();
            Cursor = Cursors.WaitCursor;
            for (int i = 0; i < lsdevices.Items.Count; i++)
            {
                if (lsdevices.Items[i].Checked)
                {
                    IP = lsdevices.Items[i].SubItems[0].Text;
                    InOut = lsdevices.Items[i].SubItems[1].Text;
                    Type = lsdevices.Items[i].SubItems[2].Text;
                    if (Type == "IFACE")
                    {
                        iFace faceTemp = new iFace(IP, InOut);
                        faceTemp.iFace_Connect();
                        if (faceTemp.bIsConnected)
                        {
                            lsdevices.Items[i].SubItems[3].Text = "Connected";
                            string UserID = cbUser.SelectedItem.ToString();
                            if (cbCard.Checked)
                            {
                                bool result = faceTemp.RegisterCard(UserID, txtCardNumber.Text );
                                lsdevices.Items[i].SubItems[3].Text += " - " + result;
                                Library.UpdateCard(int.Parse(UserID), txtCardNumber.Text);

                            }
                            if (cbFinger.Checked)
                            {
                                faceTemp.DeleteTemp(int.Parse (UserID));
                                for (int FingerIndex = 0; FingerIndex < UserDataTemp.Templates.Count; FingerIndex++)
                                {
                                    faceTemp.RegisterTemplate(UserID, UserDataTemp.Templates[FingerIndex].FingerID, UserDataTemp.Templates[FingerIndex].Template, UserDataTemp.Templates[FingerIndex].TemplateSize);
                                    Library.insertFinger(int.Parse(UserID), UserDataTemp.Templates[FingerIndex].FingerID, UserDataTemp.Templates[FingerIndex].Template, UserDataTemp.Templates[FingerIndex].TemplateSize);
                                }
                                lsdevices.Items[i].SubItems[3].Text += "- " + txtFingerCount.Text + "Finger Add ";
                            }
                            if (cbFace.Checked)
                            {
                                faceTemp.DeleteFace(int.Parse(UserID));
                                for (int FaceIndex = 0; FaceIndex < UserDataTemp.Faces.Count; FaceIndex++)
                                {
                                    faceTemp.RegisterFace(UserID, UserDataTemp.Faces[FaceIndex].FaceID, UserDataTemp.Faces[FaceIndex].Face, UserDataTemp.Faces[FaceIndex].FaceSize);
                                    Library.insertFace(int.Parse(UserID), UserDataTemp.Faces[FaceIndex].FaceID, UserDataTemp.Faces[FaceIndex].Face, UserDataTemp.Faces[FaceIndex].FaceSize);
                                }
                                lsdevices.Items[i].SubItems[3].Text += "- " + txtFingerCount.Text + "Face Add";
                            }
                        }
                        else
                        {
                            lsdevices.Items[i].SubItems[3].Text = "DisConnected";
                        }
                        faceTemp.iFace_Discoonect();
                    }
                    if (Type == "MA500")
                    {
                        MA500 ma500Temp = new MA500(IP, InOut);
                        ma500Temp.MA500_Connect();
                        if (ma500Temp.bIsConnected)
                        {
                            lsdevices.Items[i].SubItems[3].Text = "Connected";
                            string UserID = cbUser.SelectedItem.ToString();
                            if (cbFinger.Checked)
                            {
                                for (int FingerIndex = 0; FingerIndex < UserDataTemp.Templates.Count; FingerIndex++)
                                {
                                    ma500Temp.RegisterFinger(UserID, FingerIndex.ToString(), "1", UserDataTemp.Templates[FingerIndex].Template);
                                    Library.insertFinger(int.Parse(UserID), UserDataTemp.Templates[FingerIndex].FingerID, UserDataTemp.Templates[FingerIndex].Template, UserDataTemp.Templates[FingerIndex].TemplateSize);
                               
                                }
                                lsdevices.Items[i].SubItems[3].Text += "- " + txtFingerCount.Text + "Finger Add ";
                            }
                            if (cbCard.Checked)
                            {
                                ma500Temp.RegisterCard(UserID, txtCardNumber.Text, "", "0", "", "");
                                lsdevices.Items[i].SubItems[3].Text += "- " + txtFingerCount.Text + " Add";
                                Library.UpdateCard(int.Parse(UserID), txtCardNumber.Text);

                            }
                        }
                        else
                        {
                            lsdevices.Items[i].SubItems[3].Text = "DisConnected";
                        }
                        ma500Temp.MA500_Disconnect();
                    }
                }
            }
            lsdevices.EndUpdate();
            Cursor = Cursors.Default;
            MessageBox.Show("Done");
        }
        public class ComboboxItem
        {
            public string Text { get; set; }
            public object Value { get; set; }

            public override string ToString()
            {
                return Text;
            }
        }

        private void tabPage1_Click(object sender, EventArgs e)
        {

        }

        private void btDownload_Click(object sender, EventArgs e)
        {
            string  UserID = cbUser.SelectedItem.ToString ();
            for (int i = 0; i < UserDataTemp.Templates.Count; i++)
            {
                Library.insertFinger(int.Parse(UserID), UserDataTemp.Templates[i].FingerID, UserDataTemp.Templates[i].Template, UserDataTemp.Templates[i].TemplateSize);
            }
            for (int i = 0; i < UserDataTemp.Faces.Count; i++)
            {
                Library.insertFace(int.Parse(UserID), UserDataTemp.Faces[i].FaceID, UserDataTemp.Faces[i].Face, UserDataTemp.Faces[i].FaceSize);
            }
            Library.UpdateCard(int.Parse (UserID), txtCardNumber.Text);
            MessageBox.Show("Done");
        }

        private void button1_Click(object sender, EventArgs e)
        {
            string IP, InOut, Type;
            string UserID;
            bool Vaild;
            
            for (int i = 0; i < lvDevicestb2.Items.Count; i++)
            {
                //lvDevicestb2.BeginUpdate();
                lvDevicestb2.Items[i].SubItems[3].Text = "";
                //lvDevicestb2.EndUpdate();
                lvDevicestb2.Refresh();
            }
            if (cbUserTb2.SelectedIndex == -1)
            {
                MessageBox.Show("Please select user first","Error");
                return;
            }
            UserID = cbUserTb2.SelectedItem.ToString ();
            Vaild = cbVaildTb2.Checked;
            lvDevicestb2.BeginUpdate();
            Cursor = Cursors.WaitCursor;
            for (int i = 0; i < lvDevicestb2.Items.Count; i++)
            {
                if (lvDevicestb2.Items[i].Checked)
                {
                    IP = lvDevicestb2.Items[i].SubItems[0].Text;
                    InOut = lvDevicestb2.Items[i].SubItems[1].Text;
                    Type = lvDevicestb2.Items[i].SubItems[2].Text;
                    if (Type == "IFACE")
                    {
                        iFace faceTemp = new iFace(IP, InOut);
                        faceTemp.iFace_Connect();
                        if (faceTemp.bIsConnected)
                        {
                            lvDevicestb2.Items[i].SubItems[3].Text = "Connected ";
                            bool result = faceTemp.EnableUser(UserID, Vaild);
                            if (Vaild)
                                Library.EnableUser(int.Parse (UserID),1);
                            else
                                Library.EnableUser(int.Parse(UserID), -1);
                            lvDevicestb2.Items[i].SubItems[3].Text += result.ToString();
                        }
                        else
                        {
                            lvDevicestb2.Items[i].SubItems[3].Text = "DisConnected";
                        }
                        faceTemp.iFace_Discoonect();
                    }
                    if (Type == "MA500")
                    {
                        MA500 ma500Temp = new MA500(IP, InOut);
                        ma500Temp.MA500_Connect();
                        if (ma500Temp.bIsConnected)
                        {
                            lvDevicestb2.Items[i].SubItems[3].Text = "Connected";
                            bool result = ma500Temp.EnableUser(UserID, Vaild);
                            lvDevicestb2.Items[i].SubItems[3].Text += result.ToString();
                        }
                        else
                        {
                            lvDevicestb2.Items[i].SubItems[3].Text = "DisConnected";
                        }
                        ma500Temp.MA500_Disconnect();
                    }
                }
            }
            lvDevicestb2.EndUpdate();
            Cursor = Cursors.Default;
            MessageBox.Show("Done");
        }

        private void button3_Click(object sender, EventArgs e)
        {
            string IP, InOut, Type;
            string UserID;
            if (cbUserTb3.SelectedIndex == -1)
            {
                MessageBox.Show("Please select user first", "Error");
                return;
            }
            for (int i = 0; i < lvDevicestb3.Items.Count; i++)
            {
                lvDevicestb3.Items[i].SubItems[3].Text = "";
                lvDevicestb3.Refresh();
            }
            UserID = cbUserTb3.SelectedItem.ToString ();
            lvDevicestb3.BeginUpdate();
            Cursor = Cursors.WaitCursor;
            for (int i = 0; i < lvDevicestb3.Items.Count; i++)
            {
                if (lvDevicestb3.Items[i].Checked)
                {
                    IP = lvDevicestb3.Items[i].SubItems[0].Text;
                    InOut = lvDevicestb3.Items[i].SubItems[1].Text;
                    Type = lvDevicestb3.Items[i].SubItems[2].Text;
                    if (Type == "IFACE")
                    {
                        iFace faceTemp = new iFace(IP, InOut);
                        faceTemp.iFace_Connect();
                        if (faceTemp.bIsConnected)
                        {
                            lvDevicestb3.Items[i].SubItems[3].Text = "Connected ";
                            bool result =  faceTemp.RegisterCard(UserID, txtCardNumbertb3.Text);
                            Library.UpdateCard(int.Parse(UserID), txtCardNumber.Text);
                            lvDevicestb3.Items[i].SubItems[3].Text +=  result.ToString ();
                        }
                        else
                        {
                            lvDevicestb3.Items[i].SubItems[3].Text = "DisConnected";
                        }
                        //faceTemp.iFace_Discoonect();
                    }
                    if (Type == "MA500")
                    {
                        MA500 ma500Temp = new MA500(IP, InOut);
                        ma500Temp.MA500_Connect();
                        if (ma500Temp.bIsConnected)
                        {
                            lvDevicestb3.Items[i].SubItems[3].Text = "Connected ";
                            ma500Temp.RegisterCard(UserID, txtCardNumbertb3.Text, "", "", "", "");
                            lvDevicestb3.Items[i].SubItems[3].Text += " - Card Add";
                        }
                        else
                        {
                            lvDevicestb3.Items[i].SubItems[3].Text = "DisConnected";
                        }
                        //ma500Temp.MA500_Disconnect();
                    }
                }
            }
            lvDevicestb3.EndUpdate();
            Cursor = Cursors.Default;
            MessageBox.Show("Done");
        }

        private void lvDevicestb3_ItemCheck(object sender, ItemCheckEventArgs e)
        {
            if (cbDevicestb3.SelectedIndex == -1)
                return;
            string DeviceType_1 = lvDevicestb3.Items[e.Index].SubItems[2].Text;
            string DeviceType_2 = ((ComboboxItem)(cbDevicestb3.SelectedItem)).Value.ToString();
            if (DeviceType_1 != DeviceType_2)
            {
                e.NewValue = CheckState.Unchecked;
            }
        }

        private void cbDevicestb3_SelectedValueChanged(object sender, EventArgs e)
        {

            for (int i = 0; i < lvDevicestb3.Items.Count; i++)
            {
                lvDevicestb3.Items[i].Checked = false;
            }
            lvDevicestb3.Refresh();

        }

        private void btnReadLog_Click(object sender, EventArgs e)
        {
            string IP, InOut="1", Type;
            ComboboxItem cbitem = (ComboboxItem)cbDevicestb4.SelectedItem;
            ip = cbitem.Text;
            type = cbitem.Value.ToString();
            Cursor = Cursors.WaitCursor;
            if (type == "IFACE")
            {
                iFace faceTemp = new iFace(ip, InOut);
                faceTemp.iFace_Connect();
                if (faceTemp.bIsConnected)
                {
                    faceTemp.iFace_ReadLog();
                }
                else
                {
                }
                //faceTemp.iFace_Discoonect();
            }
            if (type == "MA500")
            {
                MA500 ma500Temp = new MA500(ip, InOut);
                ma500Temp.MA500_Connect();
                if (ma500Temp.bIsConnected)
                {
                    ma500Temp.MA500_ReadLog();
                }
                else
                {
                }
                //ma500Temp.MA500_Disconnect();
            }

            MessageBox.Show("Done");
        }

        private void btnConnecttb4_Click(object sender, EventArgs e)
        {
            if (cbDevicestb4.SelectedIndex < 0)
                return;
            //////////////////////////////////////////////////////DisConnect/////////////////////////////
            ComboboxItem cbitem = (ComboboxItem)cbDevicestb4.SelectedItem;
            ip = cbitem.Text;
            type = cbitem.Value.ToString();
            Cursor = Cursors.WaitCursor;
            if (btnConnecttb4.Text == "DisConnect")
            {
                if (iFaceDevice != null)
                {
                    if (iFaceDevice.bIsConnected)
                    {
                        iFaceDevice.iFace_Discoonect();
                        cbDevicestb4.Enabled = true;
                        cbDevicestb4.SelectedIndex = -1;
                    }
                }
                if (MA500Device != null)
                {
                    if (MA500Device.bIsConnected)
                    {
                        MA500Device.MA500_Disconnect();
                        cbDevicestb4.Enabled = true;
                        cbDevicestb4.SelectedIndex = -1;
                    }
                }
                btnConnecttb4.Text = "Connect";

                Cursor = Cursors.Default;
                return;
            }
            //////////////////////////////////////////////////////Connect/////////////////////////////

            List<string> Users = new List<string>();
            //lbstatus.Text = cmbdevices.SelectedItem.ToString() + " " + type ;
            if (type == "IFACE")
            {
                iFaceDevice = new iFace(ip, "");
                iFaceDevice.axCZKEM1.OnHIDNum += new zkemkeeper._IZKEMEvents_OnHIDNumEventHandler(axCZKEM1_OnHIDNum);
                Cursor = Cursors.WaitCursor;
                iFaceDevice.iFace_Connect();
                if (iFaceDevice.bIsConnected)
                {
                    btnConnecttb4.Text = "DisConnect";
                    lblStatustb4.Text = ip + " Connected ";
                    cbDevicestb4.Enabled = false;
                }
                else
                {
                    cbDevicestb4.Enabled = true;
                    lblStatustb4.Text = "Connection Faild";
                }
                Cursor = Cursors.Default;
            }
            if (type == "MA500")
            {
                MA500Device = new MA500(ip, Devices[cbDevicestb4.SelectedIndex + 1][2]);
                cbDevicestb3.Enabled = false;
                Cursor = Cursors.WaitCursor;
                MA500Device.MA500_Connect();
                if (MA500Device.bIsConnected)
                {

                    btnConnecttb4.Text = "DisConnect";
                    lblStatustb4.Text = ip + " Connected ";
                    cbDevicestb4.Enabled = false;

                    MA500Device.EnableCradTimer();
                    MA500Device.ReadingCard += MA500Device_ReadingCard;
                }
                else
                {
                    cbDevicestb4.Enabled = true;
                    lblStatustb4.Text = "Connection Faild";
                }
                //MA500Device.MA500_Disconnect();
               
                Cursor = Cursors.Default;
            }
        }

        
    }
}
