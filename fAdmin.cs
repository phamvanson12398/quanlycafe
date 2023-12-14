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
using QuanlyquanCoffe.DTO;

namespace QuanlyquanCoffe
{
    public partial class fAdmin : Form
    {
        BindingSource foodlist=new BindingSource();
        public fAdmin()
        {
            InitializeComponent();
            Load_info();
        }
        #region methods
       /* void LoadFoodList()
        {
            string query = "select * from Food";

            dtgvFood.DataSource = Dataprovider.Instance.ExcuteQuery(query);

        }*/

       void Load_info()
        {
            dtgvFood.DataSource = foodlist;
            LoadDateTimePickerBill();
            LoadAccoutList();
            LoadListBillByDate(dtpkfromDate.Value, dtpktoDate.Value);
            LoadListFood();
            AddFoodBinding();
            LoadCategoryIntoComboBox(cbFoodCategory);

        }
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
        void AddFoodBinding() {
            txbFoodName.DataBindings.Add(new Binding("Text", dtgvFood.DataSource, "Name",true,DataSourceUpdateMode.Never));
            txbFoodID.DataBindings.Add(new Binding("Text",dtgvFood.DataSource,"id", true, DataSourceUpdateMode.Never));
            nmFoodPrice.DataBindings.Add(new Binding("Value", dtgvFood.DataSource, "price", true, DataSourceUpdateMode.Never));
        }
        void LoadCategoryIntoComboBox(ComboBox cb)
        {
            cb.DataSource = CategoryDAO.Instance.GetListCategory();
            cb.DisplayMember = "Name";
        }
        void LoadListFood()
        {
            foodlist.DataSource=FoodDAO.Instance.GetListFood();
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

        private void btnAddFood1(object sender, EventArgs e)
        {
            string name = txbFoodName.Text;
            int categoryID=(cbFoodCategory.SelectedItem as Category).ID;
            float price = (float)nmFoodPrice.Value;
            if (FoodDAO.Instance.InsertFood(name, categoryID, price))
            {
                MessageBox.Show("Thêm món thành công");
                LoadListFood();
            }
            else
            {
                MessageBox.Show("Có lỗi khi thêm thức ăn");
            }
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

        private void txbFoodID_TextChanged(object sender, EventArgs e)
        {
            if (dtgvFood.SelectedCells.Count > 0)
            {
                int id = (int)dtgvFood.SelectedCells[0].OwningRow.Cells["CategoryID"].Value;
             
                    Category category = CategoryDAO.Instance.GetCategoryByID(id);
                    cbFoodCategory.SelectedItem = category;
                int index = -1;
                int i = 0;
                foreach(Category item  in cbFoodCategory.Items)
                {
                    if (item.ID == category.ID)
                    {
                        index = i;
                        break;
                    }
                    i++;
                }
                cbFoodCategory.SelectedIndex = index;
            }
        }

        private void btnEditFood_Click(object sender, EventArgs e)
        {
            string name = txbFoodName.Text;
            int categoryID = (cbFoodCategory.SelectedItem as Category).ID;
            float price = (float)nmFoodPrice.Value;
            int id=Convert.ToInt32(txbFoodID.Text);
            if (FoodDAO.Instance.UpdateFood(id,name, categoryID, price))
            {
                MessageBox.Show("Chỉnh sửa món thành công");
                LoadListFood();
            }
            else
            {
                MessageBox.Show("Có lỗi khi chỉnh sửa thức ăn");
            }
        }
    }
}
