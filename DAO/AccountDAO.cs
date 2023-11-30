using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing.Text;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuanlyquanCoffe.DAO
{
    internal class AccountDAO
    {
        private static AccountDAO instance;
        public static AccountDAO Instance
        {
            get{ if (instance == null) instance = new AccountDAO(); return instance; }
            private set{  instance=value;}

        }
        private AccountDAO() { }
        public bool Login(string username, string password)
        {
            string query = "select * from Account where Username=N'"+username+"' and Password=N'"+password+"' ";
            DataTable result = Dataprovider.Instance.ExcuteQuery(query);
            return result.Rows.Count>0;
        }
    }
}
