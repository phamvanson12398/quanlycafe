using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.SqlClient;
using QuanlyquanCoffe.DAO;
using System.Collections;

namespace QuanlyquanCoffe
{
    public partial class fAdmin : Form
    {
        public fAdmin()
        {
            InitializeComponent();
            LoadAccoutList();
            LoadFoodList();
        }
        void LoadFoodList()
        {
            string query = "select * from Food";

            dtgvFood.DataSource = Dataprovider.Instance.ExcuteQuery(query);

        }
        void LoadAccoutList()
        {
            //string query = "execute USP_GetAccountByUserName @username ";

            //dtgvAccount.DataSource = Dataprovider.Instance.ExcuteQuery(query,new object[] {"Hoàng Nguyễn"});
            string query = "select * from Account";
            dtgvAccount.DataSource = Dataprovider.Instance.ExcuteQuery(query);

        }
        private void dateTimePicker1_ValueChanged(object sender, EventArgs e)
        {

        }

        private void dtgvBill_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void btnViewBill_Click(object sender, EventArgs e)
        {

        }

        private void btlogin_Click(object sender, EventArgs e)
        {

        }

        private void button2_Click(object sender, EventArgs e)
        {

        }

        private void tcAdmin_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void panel12_Paint(object sender, PaintEventArgs e)
        {

        }

        private void panel13_Paint(object sender, PaintEventArgs e)
        {

        }

        private void panel18_Paint(object sender, PaintEventArgs e)
        {

        }

        private void tabPage1_Click(object sender, EventArgs e)
        {

        }

        private void tabPage1_Click_1(object sender, EventArgs e)
        {

        }
    }
}
