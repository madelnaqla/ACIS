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
using System.Reflection;
namespace Path
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
            AppDomain.CurrentDomain.AssemblyResolve += new ResolveEventHandler(CurrentDomain_AssemblyResolve);
   

   
        }
        public System.Reflection.Assembly CurrentDomain_AssemblyResolve(object sender, ResolveEventArgs args)
        {
            if (true)
                return Assembly.LoadFrom("DifferentDllFolder\\differentVersion.dll");
            else
                return Assembly.LoadFrom("");
        }
        private void Form1_Load(object sender, EventArgs e)
        {
            DateTime dt = Library.ConvertToDate("566616306");
            long d = Library.ConvertToLong(dt);
            ///////////////////////////////////////
            MA500 temp = new MA500("192.168.1.128", "");
            //temp.MA500_Connect();
            //temp.RegisterFinger("1313", "1", "1" , "--");
        }
    }
}
