using QuanlyquanCoffe.DAO;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace QuanlyquanCoffe
{
    public partial class fLogin : Form
    {
        public fLogin()
        {
            InitializeComponent();
        }

        private void btnLogin(object sender, EventArgs e)
        {
            string username = txbUsername.Text;
            string password = txbPassword.Text;
             if (Login(username,password))
            {
                this.Hide();
                fTableManager f = new fTableManager();
                f.ShowDialog();
                this.Show();
                }
                else
                {
                MessageBox.Show("Sai tên tài khoản hoặc mật khẩu");
                }
            }
        
       bool Login(string username,string password)
        {
           return AccountDAO.Instance.Login(username,password); 
        }

        private void button2_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void fLogin_FormClosing(object sender, FormClosingEventArgs e)
        {
            if(MessageBox.Show("Bạn có muốn thoát chương trình ?","Thông báo", MessageBoxButtons.OKCancel) != System.Windows.Forms.DialogResult.OK)
            {
                e.Cancel = true;
            }
        }
    }
}
