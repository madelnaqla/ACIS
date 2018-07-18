using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using ACIS;
using System.IO;
using System.Runtime.InteropServices;
using System.Data.SqlClient;
namespace ACIS_APP
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            
            InitializeComponent();
        }
        string[][] Devices;
        iFace[] iFaceMachine;
        MA500[] MA500Machine;
        private void Form1_Load(object sender, EventArgs e)
        {
 
            //this.timer1.Enabled = true;
            Library.SetText += Library_SetText;

            Devices = ACIS.Library.LoadCsv(System.IO.File.ReadAllText(@"Config.csv"), ';');
            for (int i = 1; i < Devices.Length; i++)
            {
                ListViewItem lvi = new ListViewItem(Devices[i]);
                lsdevices.Items.Add(lvi);

            }

            int numberOfiFace = 0;
            int numberOfMA500 = 0;
            for (int i = 0; i < Devices.GetLength(0); i++)
            {
                if (Devices[i][2] == "IFACE")
                    numberOfiFace++;
                if (Devices[i][2] == "MA500")
                    numberOfMA500++;
            }
            iFaceMachine = new iFace[numberOfiFace];
            MA500Machine = new MA500[numberOfMA500];
            backgroundWorker1.RunWorkerAsync();
        }

        void Library_SetText(ref string Text)
        {
            textBox1.Text += Text + "\r\n";
            textBox1.SelectionStart = textBox1.TextLength;
            textBox1.ScrollToCaret();
        }

        private void timer1_Tick(object sender, EventArgs e)
        {

            textBox1.Text = File.ReadAllText("LogFile.txt");
            textBox1.SelectionStart = textBox1.TextLength;
            textBox1.ScrollToCaret();
        }

        
        private void button1_Click(object sender, EventArgs e)
        {
            DateTime LastFromMA = MA500Machine[0].GetLastAction();
            DateTime LastFromDB = Library.GetLastFromDB("");
            if (LastFromDB < LastFromMA)
            {

                MA500Machine[0].MA500_ReadLog(LastFromDB);
            }
            //backgroundWorker1.RunWorkerAsync();
           
            
        }

        private void backgroundWorker1_DoWork(object sender, DoWorkEventArgs e)
        {
            //Library.WriteErrorLog("********************************start*************************************");
            //for (int i = 0; i < iFaceMachine.Length; i++)
            //    iFaceMachine[i].iFace_ReadLog();
            //for (int i = 0; i < MA500Machine.Length; i++)
            //    MA500Machine[i].MA500_ReadLog();
            //Library.WriteErrorLog("*******************************end***************************************");
            //MessageBox.Show("Done");

            Library.WriteErrorLog("Start Working");
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
                    MA500Machine[c].EnableLogTimer();
                    //MA500Machine[c].MA500_ReadLog();
                    c++;
                }
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            MA500 temp = new MA500("192.168.1.128", "");
            temp.MA500_Connect();
            //List<UserTemplate> data = temp.MA500_Templates();
            
        }

       
    }
}
