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
    public partial class AddFormFood : Form
    {
        public AddFormFood()
        {
            InitializeComponent();
            LoadCategoryIntoComboBox(cbFoodCategory);
        }
        private bool checksameFood(string name)
        {

            DataTable a = Dataprovider.Instance.ExcuteQuery(string.Format("select * from Food where name=N'{0}'", name));
            return a.Rows.Count > 0;
        }
        private void AddFormFood_Load(object sender, EventArgs e)
        {

        }
        void LoadCategoryIntoComboBox(ComboBox cb)
        {
            cb.DataSource = CategoryDAO.Instance.GetListCategory();
            cb.DisplayMember = "Name";
        }
        private void btnAddFood_Click(object sender, EventArgs e)
        {
            string nameFood = txbFoodName.Text;
            if (nameFood.Equals(""))
            {
                MessageBox.Show("Vui lòng nhập tên món ăn");
                return;
            }
            int id_category = (cbFoodCategory.SelectedItem as Category).ID;
            float price = (float)nmFoodPrice.Value;
            if(MessageBox.Show("Bạn có chắc chắn muốn thêm món ăn mới","Thông báo", MessageBoxButtons.OKCancel) == DialogResult.OK) {
                if (!checksameFood(nameFood))
                {
                    if(FoodDAO.Instance.InsertFood(nameFood,id_category,price))
                    {
                        MessageBox.Show("Thêm món ăn mới thành công");
                        if (insertFood != null)
                        {
                            insertFood(this,new EventArgs());
                        }
                        txbFoodName.Text = "";
                        LoadCategoryIntoComboBox(cbFoodCategory);
                        nmFoodPrice.Value = 0;
                    }
                    else
                    {
                        MessageBox.Show("Thêm món ăn bị lỗi");
                    }
                }
                else
                {
                    MessageBox.Show("Món ăn đã tồn tại, vui lòng chọn tên món ăn khác !!!");
                }

            }
          
        }
        private event EventHandler insertFood;
        public event EventHandler InsertFood
        {
            add { insertFood += value; }
            remove { insertFood -= value; }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
