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

namespace QuanlyquanCoffe.ADD
{
    public partial class AddFormIngredient : Form
    {
        public AddFormIngredient()
        {
            InitializeComponent();
            this.StartPosition = FormStartPosition.CenterParent;
        }

        // 1. TẠO SỰ KIỆN (EVENT) ĐỂ GỬI TÍN HIỆU VỀ fAdmin
        private event EventHandler insertNguyenLieu;
        public event EventHandler InsertNguyenLieu
        {
            add { insertNguyenLieu += value; }
            remove { insertNguyenLieu -= value; }
        }

        // 2. CODE NÚT THOÁT
        private void btnExit_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        // 3. CODE NÚT THÊM
        private void btnAdd_Click(object sender, EventArgs e)
        {
            string name = txtTenNguyenLieu.Text;
            string unit = txtDonVi.Text;

            if (string.IsNullOrWhiteSpace(name))
            {
                MessageBox.Show("Tên nguyên liệu không được để trống!");
                return;
            }

            if (MessageBox.Show($"Bạn có chắc chắn muốn thêm nguyên liệu: {name}?", "Xác nhận thêm", MessageBoxButtons.OKCancel) == DialogResult.OK)
            {
                if (NguyenLieuDAO.Instance.InsertNguyenLieu(name, unit, 0, ""))
                {
                    MessageBox.Show("Thêm nguyên liệu mới thành công!");
                    if (insertNguyenLieu != null)
                    {
                        insertNguyenLieu(this, new EventArgs());
                    }

                    this.Close(); // Đóng form popup này
                }
                else
                {
                    MessageBox.Show("Thêm nguyên liệu thất bại!");
                }
            }
        }
    }
}
