using QuanlyquanCoffe.DTO;
using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing.Text;
using System.Linq;
using System.Security.Cryptography;
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
          /* Byte[] temp=ASCIIEncoding.ASCII.GetBytes(password);
            Byte[] hasData= new MD5CryptoServiceProvider().ComputeHash(temp);
            string hasPass = "";
            foreach (byte item in hasData)
            {
                hasPass+= item;
            }*/
         //  var list = hasData.ToString();
         // list.Reverse();
            
            string query = "USP_Login @username , @password";
            DataTable result = Dataprovider.Instance.ExcuteQuery(query, new object[] { username, password });
            return result.Rows.Count>0;
        }
        public bool UpdateAccount(string username,string pass,string newpass,string displayname) 
        {
            int result = Dataprovider.Instance.ExcuteNonQuery("exec USP_UpdateAccount @UserName , @DisplayName , @PassWord , @newPassWord ",new object[] {username,displayname,pass,newpass});
            return result>0;
        }
        public DataTable GetListAccount()
        {
            return Dataprovider.Instance.ExcuteQuery("select DisplayName as N'Tên hiển thị' ,UserName as N'Tên TK' ,Type as N'Loại TK' from Account");
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
        public bool InsertAccount(string username,string displayname,int type, string password)
        {
            string query = string.Format("INSERT Account ( UserName, DisplayName,PassWord, Type ) VALUES  ( N'{0}', N'{1}',N'{2}', {3})", username, displayname, password, type);
            int result = Dataprovider.Instance.ExcuteNonQuery(query);

            return result > 0;
        }
        public bool UpdateAccount(string username, string displayname, int type)
        {
            string query = string.Format("UPDATE Account  SET  DisplayName=N'{1}',Type={2} where UserName=N'{0}' ", username, displayname, type);
            int result = Dataprovider.Instance.ExcuteNonQuery(query);

            return result > 0;
        }
          public bool DeleteAccount(string name)
        {
            
            string query = string.Format("Delete Account where UserName=N'{0}'",name );
            int result = Dataprovider.Instance.ExcuteNonQuery(query);
            return result > 0;
        }
        public bool ResetPassword(string name)
        {
            string query = string.Format("update Account set PassWord=N'0' where UserName=N'{0}'", name);
            int result = Dataprovider.Instance.ExcuteNonQuery(query);
            return result > 0;
        }
    }
}
