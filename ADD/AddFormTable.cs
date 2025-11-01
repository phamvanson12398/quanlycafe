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
using System.Xml.Linq;

namespace QuanlyquanCoffe
{
    public partial class AddFormTable : Form
    {
        public AddFormTable()
        {
            InitializeComponent();
        }

        private void AddFormTable_Load(object sender, EventArgs e)
        {
        }
        private event EventHandler insertTable;
        public event EventHandler InsertTable
        {
            add { insertTable += value; }
            remove { insertTable -= value; }
        }

        private void btnAddTable_Click(object sender, EventArgs e)
        {

            if (txbTableName.Text.Equals(""))
            {
                MessageBox.Show("Vui lòng nhập tên bàn !!!");
                return;
            }
            string nametable = txbTableName.Text;
            if (MessageBox.Show("Bạn có chắc chắn muốn thêm bàn mới", "Thông báo", MessageBoxButtons.OKCancel) == System.Windows.Forms.DialogResult.OK)
            {

                if (TableDAO.Instance.InsertTable(nametable))
                {
                    MessageBox.Show("Thêm bàn mới thành công");
                    //LoadTableList();
                    if (insertTable != null)
                    {
                        insertTable(this, new EventArgs());
                    }
                    this.Close();

                }
                else
                {
                    MessageBox.Show("Thêm bàn mới thất bại");
                }
            }
        }

        private void btnExit_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
