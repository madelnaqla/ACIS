using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ACIS
{
    public class UserData
    {
        public string UserID;
        public string Name;
        public int Privilege;
        public bool  Enable;
        public string Password;
        public string CardNo;
        public List<UserTemplate> Templates = new List<UserTemplate>();
        public List<UserFace> Faces=new List<UserFace>();
        public UserData()
        {
             UserID = "";
             Password = "";
        }

    }
}
