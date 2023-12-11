using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuanlyquanCoffe.DTO
{
    public class Account
    {
        public Account(string username,string displayname,int type,string password=null) {
            this.Username = username;
            this.Password = password;
            this.Type = type;
            this.DisplayName = displayname;
        }
        public Account(DataRow row) 
        {
            this.Username = row["UserName"].ToString();
            this.Password = row["Password"].ToString();
            this.DisplayName = row["DisplayName"].ToString() ;
            this.Type = (int)row["Type"];
        }

        private string username;
        private string password;
        private string displayName;
        private int type;
        public string Username { get => username; set => username = value; }
        public string Password { get => password; set => password = value; }
        public string DisplayName { get => displayName; set => displayName = value; }
        public int Type { get => type; set => type = value; }
    }
}
