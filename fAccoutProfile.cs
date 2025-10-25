using QuanlyquanCoffe.DAO;
using QuanlyquanCoffe.DTO;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace QuanlyquanCoffe
{
    public partial class fAccoutProfile : Form
    {
        private Account loginAccount;

        public Account LoginAccount
        {
            get { return loginAccount; }
            set
            {
                loginAccount = value;
                ChangeAccount(loginAccount);
            }
        }
        void ChangeAccount(Account acc) {
            txbUserName.Text = LoginAccount.Username;
            txbDisplayName.Text = LoginAccount.DisplayName;
        }
        public fAccoutProfile(Account acc)
        {
            InitializeComponent();
            LoginAccount = acc;
        }

        private void btExit_Click(object sender, EventArgs e)
        {
            this.Close();
        }
        void UpdateAccountInfo()
        {
            string displayName= txbDisplayName.Text;
            string passWord=txbPassWord.Text;
            string newpass=txbNewPassword.Text;
            string reenterpass=txbReenterPass.Text;
            string username =txbUserName.Text;
            if(!newpass.Equals(reenterpass))
            {
                MessageBox.Show("Vui lòng nhập lại mật khẩu đúng với mật khẩu mới!", "Thông báo", MessageBoxButtons.OK);
            }
            else
            {
                if(AccountDAO.Instance.UpdateAccount(username,passWord,newpass,displayName)) 
                {
                    MessageBox.Show("Cập nhật thành công");
                    if (updateAccount1 != null) {
                        updateAccount1(this, new AccountEvent(AccountDAO.Instance.GetAccountByUserName(username)));
                    }
                }
                else
                {
                    MessageBox.Show("Vui lòng điền đúng mật khẩu");
                }
            }
        }
        private event EventHandler<AccountEvent> updateAccount1;
        public event EventHandler<AccountEvent> UpdateAccount1
        {
            add { updateAccount1 += value; }
            remove { updateAccount1-=value; }
        }
        private void btUpdate_Click(object sender, EventArgs e)
        {
            UpdateAccountInfo();
        }

        private void fAccoutProfile_Load(object sender, EventArgs e)
        {

        }
    }
    public class AccountEvent : EventArgs
    {
        private Account acc;

        public Account Acc { get => acc; set => acc = value; }

        public AccountEvent(Account acc) {  this.acc = acc; }

    }
}
