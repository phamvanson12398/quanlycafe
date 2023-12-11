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
            LoadDateTimePickerBill();
            LoadAccoutList();
            LoadListBillByDate(dtpkfromDate.Value, dtpktoDate.Value);
        }
        #region methods
       /* void LoadFoodList()
        {
            string query = "select * from Food";

            dtgvFood.DataSource = Dataprovider.Instance.ExcuteQuery(query);

        }*/
        void LoadAccoutList()
        {
            //string query = "execute USP_GetAccountByUserName @username ";

            //dtgvAccount.DataSource = Dataprovider.Instance.ExcuteQuery(query,new object[] {"Hoàng Nguyễn"});
            string query = "select * from Account";
            dtgvAccount.DataSource = Dataprovider.Instance.ExcuteQuery(query);
        }
        void LoadListBillByDate(DateTime checkin,DateTime checkout)
        {
            
           dtgvBill.DataSource= BillDAO.Instance.GetBillListByDate(checkin, checkout);

        }
        void LoadDateTimePickerBill()
        {
            DateTime today=DateTime.Now;
            dtpkfromDate.Value = new DateTime(today.Year, today.Month, 1);
            dtpktoDate.Value=dtpkfromDate.Value.AddMonths(1).AddDays(-1);
        }
        void LoadListFood()
        {
            dtgvFood.DataSource=FoodDAO.Instance.GetListFood();
        }
        #endregion
        #region events

        private void dateTimePicker1_ValueChanged(object sender, EventArgs e)
        {

        }

        private void dtgvBill_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void btnViewBill_Click(object sender, EventArgs e)
        {
            LoadListBillByDate(dtpkfromDate.Value,dtpktoDate.Value);

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
        #endregion
        
        private void button3_Click(object sender, EventArgs e)
        {
            LoadListFood();
        }

        private void dtgvFood_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }
    }
}
