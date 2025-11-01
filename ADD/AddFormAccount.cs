using QuanlyquanCoffe.DAO;
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
    public partial class AddFormAccount : Form
    {
        private fAdmin fAdmin;
        public AddFormAccount(fAdmin f)
        {
            InitializeComponent();
            fAdmin = f;
        }

        private void btnAddAccount_Click(object sender, EventArgs e)
        {
            string username = txbAccountName.Text;
            string display_name = txbDisplayName.Text;
            string password = txbPassword.Text;
            string confirmpassword = txbConfirmPassword.Text;
            int account_type = 0;
            if (cbTypeAccount.SelectedItem.ToString() == "Admin")
            {
                account_type = 1;
            }
            else
            {
                account_type = 0;
            }
            if (username.Equals("") || display_name.Equals(""))
            {
                MessageBox.Show("Vui lòng nhập đầy đủ thông tin !!!");
                return;
            }
            if(display_name.Length < 5||display_name.Contains(" "))
            {
                MessageBox.Show("Vui lòng không nhập tên tài khoản ít hơn 5 ký tự hoặc có ký tự trống !!!");
                return;
            }
            if (confirmpassword != password)
            {
                MessageBox.Show("Mật khẩu không khớp nhau !!!");
                return;
            }
            if (password.Length < 6)
            {
                MessageBox.Show("Mật khẩu phải có ít nhất 6 ký tự !!!");
                return;
            }
            AddAccount(username, display_name, account_type,password);
        }
        private bool checksameAccount(string name)
        {

            DataTable a = Dataprovider.Instance.ExcuteQuery(string.Format("select * from Account where UserName=N'{0}'", name));
            return a.Rows.Count > 0;
        }
        void AddAccount(string username, string displayname, int type, string password)
        {
            if (MessageBox.Show("Bạn có chắc chắn muốn thêm tài khoản mới", "Thông báo", MessageBoxButtons.OKCancel) == System.Windows.Forms.DialogResult.OK)
            {
                if (checksameAccount(username))
                {
                    MessageBox.Show("Đã tồn tại tên tài khoản");
                }
                else
                {
                    if (AccountDAO.Instance.InsertAccount(username, displayname, type, password))
                    {
                        MessageBox.Show("Thêm tài khoản thành công");
                        fAdmin.LoadAccoutList();
                        this.Close();
                    }
                    else
                    {
                        MessageBox.Show("Thêm tài khoản thất bại");
                    }
                }
            }
            
        }

        private void cbTypeAccount_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void AddFormAccount_Load(object sender, EventArgs e)
        {

        }

        private void btnExit_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void txbAccountName_TextChanged(object sender, EventArgs e)
        {

        }
    }
}
