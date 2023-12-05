using QuanlyquanCoffe.DAO;
using QuanlyquanCoffe.DTO;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using Menu = QuanlyquanCoffe.DTO.Menu;

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
                btn.Click += btn_Click;
              btn.Tag = item;
                switch (item.Status)
                {
                    case "Trống": btn.BackColor = Color.Aqua;
                        break;
                    default: btn.BackColor = Color.LightPink; 
                        break;
                }
                flpTable.Controls.Add(btn);
            }
        }
        void ShowBill(int id)
        {
            lsvBill.Items.Clear();
            List<QuanlyquanCoffe.DTO.Menu> listBillInfo=MenuDAO.Instance.GetListMenuByTable(id);
            float totalprice = 0;
            foreach (QuanlyquanCoffe.DTO.Menu item in listBillInfo)
            {
                ListViewItem lvsItem=new ListViewItem(item.FoodName.ToString());
                lvsItem.SubItems.Add(item.Count.ToString());
                lvsItem.SubItems.Add(item.Price.ToString());
                lvsItem.SubItems.Add(item.TotalPrice.ToString());
                totalprice+= item.TotalPrice;
                lsvBill.Items.Add(lvsItem);
            }
            CultureInfo culture=new CultureInfo("vi-VN");
            Thread.CurrentThread.CurrentCulture = culture;
            txbTotalPrice.Text = totalprice.ToString("c",culture);
        }
        #endregion
        #region Events
        void btn_Click(object sender, EventArgs e)
        {
            int TableId=((sender as Button).Tag as Table).ID;
            ShowBill(TableId);
        }
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

        private void fTableManager_Load(object sender, EventArgs e)
        {

        }
    }
}
