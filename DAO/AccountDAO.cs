using QuanlyquanCoffe.DTO;
using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing.Text;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuanlyquanCoffe.DAO
{
    public class AccountDAO
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
            string query = "USP_Login @username , @password";
            DataTable result = Dataprovider.Instance.ExcuteQuery(query, new object[] { username, password });
            return result.Rows.Count>0;
        }
        public bool UpdateAccount(string username,string pass,string newpass,string displayname) 
        {
            int result = Dataprovider.Instance.ExcuteNonQuery("exec USP_UpdateAccount @UserName , @DisplayName , @PassWord , @newPassWord ",new object[] {username,displayname,pass,newpass});
            return result>0;
        }
        public Account GetAccountByUserName(string username)
        {
            DataTable data=Dataprovider.Instance.ExcuteQuery("select* from Account where username = N'"+username+"'");
            foreach (DataRow item in data.Rows) 
            {
                return new Account(item);
            }
            return null;
        }
    }
}
