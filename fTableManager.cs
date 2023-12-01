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
    public partial class fTableManager : Form
    {
        public fTableManager()
        {
            InitializeComponent();
            LoadTable();
        }
        #region Method
        void LoadTable()
        {
           List<Table> Tablelist=TableDAO.Instance.LoadTableList();
            foreach (Table item in Tablelist)
            {
                Button btn = new Button() { Width = TableDAO.tableWidth, Height = TableDAO.tableHeight };
                btn.Text = item.Name+Environment.NewLine+item.Status;
              flpTable.Controls.Add(btn);
                switch (item.Status)
                {
                    case "Trống": btn.BackColor = Color.Aqua;
                        break;
                    default: btn.BackColor = Color.LightPink; 
                        break;
                }
            }
        }
        #endregion
        #region Events
        private void ĐăngxuấtToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void thôngTinCáNhânToolStripMenuItem_Click(object sender, EventArgs e)
        {
            fAccoutProfile f = new fAccoutProfile();
            f.ShowDialog();
        }

        private void adminToolStripMenuItem_Click(object sender, EventArgs e)
        {
            fAdmin f=new fAdmin();
            f.ShowDialog();
        }
        #endregion

        private void flpTable_Paint(object sender, PaintEventArgs e)
        {

        }
    }
}
