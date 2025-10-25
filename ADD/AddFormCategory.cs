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
    public partial class AddFormCategory : Form
    {
        public AddFormCategory()
        {
            InitializeComponent();
        }
        private bool checksameFoodCategory(string name)
        {

            DataTable a = Dataprovider.Instance.ExcuteQuery(string.Format("select * from FoodCategory where name=N'{0}'", name));
            return a.Rows.Count > 0;
        }
        private event EventHandler insertCategory;
        public event EventHandler InsertCategory
        {
            add { insertCategory += value; }
            remove { insertCategory -= value; }
        }
        private void AddFormCategory_Load(object sender, EventArgs e)
        {
           
        }

        private void btnAddCategory_Click(object sender, EventArgs e)
        {
            if (txbCategoryName.Text.Equals(""))
            {
                MessageBox.Show("Vui lòng nhập tên loại thức ăn");
                return;
            }
            string name = txbCategoryName.Text;
            if (MessageBox.Show("Bạn có chắc chắn muốn thêm loại thức ăn mới", "Thông báo", MessageBoxButtons.OKCancel) == System.Windows.Forms.DialogResult.OK)
            {
                if (checksameFoodCategory(name))
                {
                    MessageBox.Show("Đã tồn tại loại thức ăn này");
                }
                else
                {
                    if (CategoryDAO.Instance.InsertCategory(name))
                    {
                        MessageBox.Show("Thêm loại thức ăn mới thành công");

                        if (insertCategory != null)
                        {
                            insertCategory(this, new EventArgs());
                        }
                        this.Close();
                    }
                    else
                    {
                        MessageBox.Show("Có lỗi khi thêm loại thức ăn mới");
                    }
                }
            }
        }
    }
}
