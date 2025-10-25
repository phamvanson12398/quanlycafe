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
        private Account loginAccount;

        public Account LoginAccount
        {
            get { return loginAccount; }
            set { loginAccount = value;
                ChangeAccount(loginAccount.Type);                      }
        }

        public fTableManager(Account acc)
        {
            InitializeComponent();
            this.LoginAccount = acc;
            LoadTable();
            LoadCategory();
            LoadComboBoxTable(cbSwitchTable);
        }
        #region Method
        void ChangeAccount(int type)
        {
            //adminToolStripMenuItem.Enabled = type == 1;
            adminToolStripMenuItem.Visible = type == 1;
            thôngtintkToolStripMenuItem.Text += "("+loginAccount.DisplayName+")";
        }
        void LoadCategory()
        {
            List<Category> categories = CategoryDAO.Instance.GetListCategory();
            cbCategory.DataSource = categories;
            cbCategory.DisplayMember = "Name";
        }
        void LoadFoodListCategoryID(int id)
        {
            List<Food> listfood = FoodDAO.Instance.GetListFoodByCategoryID(id);
            cbFood.DataSource = listfood;
            cbFood.DisplayMember = "Name";
        }

       public void LoadTable()
        {
            flpTable.Controls.Clear();
            List<Table> Tablelist = TableDAO.Instance.LoadTableList();
            foreach (Table item in Tablelist)
            {
                Button btn = new Button() { Width = TableDAO.tableWidth, Height = TableDAO.tableHeight };
                btn.Text = item.Name + Environment.NewLine + item.Status;
                btn.Click += btn_Click;
                btn.Tag = item;
                switch (item.Status)
                {
                    case "Trống":
                        btn.BackColor = Color.PeachPuff;
                        break;
                  /*  case "Đặt trước":
                        btn.BackColor= Color.Brown;
                        break;*/
                    default:
                        btn.BackColor = Color.SandyBrown;
                        break;
                }
                flpTable.Controls.Add(btn);
            }
        }
       public void ShowBill(int id)
        {
            lsvBill.Items.Clear();
            List<QuanlyquanCoffe.DTO.Menu> listBillInfo = MenuDAO.Instance.GetListMenuByTable(id);
            float totalprice = 0;
            foreach (QuanlyquanCoffe.DTO.Menu item in listBillInfo)
            {
                ListViewItem lvsItem = new ListViewItem(item.FoodName.ToString());
                lvsItem.SubItems.Add(item.Count.ToString());
                lvsItem.SubItems.Add(item.Price.ToString());
                lvsItem.SubItems.Add(item.TotalPrice.ToString());
                totalprice += item.TotalPrice;
                lsvBill.Items.Add(lvsItem);
            }
            CultureInfo culture = new CultureInfo("vi-VN");
            Thread.CurrentThread.CurrentCulture = culture;
            txbTotalPrice.Text = totalprice.ToString("c", culture);
         
        }
        void LoadComboBoxTable(ComboBox cb)
        {
            cb.DataSource = TableDAO.Instance.LoadTableList();
            cb.DisplayMember = "Name";
            cb.ValueMember = "ID";

        }
        #endregion

        #region Events
        private void thêmMónToolStripMenuItem_Click(object sender, EventArgs e)
        {
            btnAddFood_Click(this, new EventArgs());
        }

        private void thanhToánToolStripMenuItem_Click(object sender, EventArgs e)
        {
            btnCheck_Click(this, new EventArgs());
        }
        void btn_Click(object sender, EventArgs e)
        {
            int TableId = ((sender as Button).Tag as Table).ID;
            lsvBill.Tag = (sender as Button).Tag;
            ShowBill(TableId);
        }
        private void ĐăngxuấtToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void thôngTinCáNhânToolStripMenuItem_Click(object sender, EventArgs e)
        {
            fAccoutProfile f = new fAccoutProfile(LoginAccount);
            f.UpdateAccount1 += F_UpdateAccount1;
            f.ShowDialog();
        }

        private void F_UpdateAccount1(object sender, AccountEvent e)
        {
            thôngtintkToolStripMenuItem.Text = "Thông tin tài khoản (" + e.Acc.DisplayName + ")";
        }

        private void adminToolStripMenuItem_Click(object sender, EventArgs e)
        {
            fAdmin f = new fAdmin();
            f.loginAccount = LoginAccount;
            f.InsertFood+=F_InsertFood;
            f.DeleteFood += F_DeleteFood;
            f.UpdateFood += F_UpdateFood;
            f.InsertCategory += F_InsertCategory;
            f.UpdateCategory += F_UpdateCategory;
            f.DeleteCategory += F_DeleteCategory;
            f.UpdateTable += F_UpdateTable;
            f.InsertTable += F_InsertTable;
            f.DeleteTable += F_DeleteTable;

            f.ShowDialog();
        }

        private void F_DeleteCategory(object sender, EventArgs e)
        {
            LoadCategory();
            if (lsvBill.Tag != null)
                ShowBill((lsvBill.Tag as Table).ID);
            LoadTable();
        }

        private void F_DeleteTable(object sender, EventArgs e)
        {
            LoadTable();
            if (lsvBill.Tag != null)
                ShowBill((lsvBill.Tag as Table).ID);
        }

        private void F_InsertTable(object sender, EventArgs e)
        {
            LoadTable();
            if (lsvBill.Tag != null)
                ShowBill((lsvBill.Tag as Table).ID);
        }

        private void F_UpdateTable(object sender, EventArgs e)
        {
            LoadTable();
            if (lsvBill.Tag != null)
                ShowBill((lsvBill.Tag as Table).ID);
        }

        private void F_UpdateCategory(object sender, EventArgs e)
        {
            LoadCategory();
            if (lsvBill.Tag != null)
                ShowBill((lsvBill.Tag as Table).ID);
        }

        private void F_InsertCategory(object sender, EventArgs e)
        {
            LoadCategory();
            if (lsvBill.Tag != null)
                ShowBill((lsvBill.Tag as Table).ID);
        }

        private void F_UpdateFood(object sender, EventArgs e)
        {
            LoadFoodListCategoryID((cbCategory.SelectedItem as Category).ID);
            if(lsvBill.Tag != null)
            ShowBill((lsvBill.Tag as Table).ID);
        }

        private void F_DeleteFood(object sender, EventArgs e)
        {
            LoadFoodListCategoryID((cbCategory.SelectedItem as Category).ID);
            if (lsvBill.Tag != null)
            {
                ShowBill((lsvBill.Tag as Table).ID);
            }
            LoadTable();
        }

        private void F_InsertFood(object sender, EventArgs e)
        {
            LoadFoodListCategoryID((cbCategory.SelectedItem as Category).ID);
            if(lsvBill.Tag != null)
            ShowBill((lsvBill.Tag as Table).ID);
        }

        private void flpTable_Paint(object sender, PaintEventArgs e)
        {
        }
        private void fTableManager_Load(object sender, EventArgs e)
        {
        }




        private void cb_Category(object sender, EventArgs e)
        {
            int id = 0;
            ComboBox cb = sender as ComboBox;
            if (cb.SelectedItem == null) { return; }
            Category selected = cb.SelectedItem as Category;
            id = selected.ID;

            LoadFoodListCategoryID(id);
        }
        #endregion

        private void btnAddFood_Click(object sender, EventArgs e)
        {
            Table table = lsvBill.Tag as Table;
            if (table == null)
            {
                MessageBox.Show("Vui lòng chọn một bàn !!!", "Thông báo", MessageBoxButtons.OK);
                return;
            }
            else { 
                int idBill = BillDAO.Instance.getUnCheckBillIDbyTableID(table.ID);
            int idFood = (cbFood.SelectedItem as Food).ID;
            int count = (int)(nmFoodCount.Value);
            if (idBill == -1)
            {
                BillDAO.Instance.InsertBill(table.ID);
                BillInfoDAO.Instance.InsertBillInfo(BillDAO.Instance.GetMaxIDBill(), idFood, count);
            }
            else
            {
                BillInfoDAO.Instance.InsertBillInfo(idBill, idFood, count);
            }
            ShowBill(table.ID);
            LoadTable();
        }
        }

        private void btnCheck_Click(object sender, EventArgs e)
        {
            Table table = lsvBill.Tag as Table;
            if (table == null)
            {
                MessageBox.Show("Vui lòng chọn một bàn !!!", "Thông báo", MessageBoxButtons.OK);
                return;
            }
            List<QuanlyquanCoffe.DTO.Menu> listBillInfo = MenuDAO.Instance.GetListMenuByTable(table.ID);
            //double totalPrice = Convert.ToDouble(txbTotalPrice.Text.Split('.')[0]);
            //sonpv sửa
            string text = txbTotalPrice.Text;

            // Xóa đơn vị tiền và các dấu phẩy, khoảng trắng
            text = text.Replace("VNĐ", "")
                       .Replace("₫", "")
                       .Replace(",", "")
                       .Replace(".", "")
                       .Trim();

            double totalPrice = 0;
            if (!double.TryParse(text, out totalPrice))
            {
                totalPrice = 0; // nếu parse lỗi thì gán 0
            }

            // 3️⃣ Hiển thị lại tổng tiền có định dạng 3 số cách nhau bằng dấu chấm
            txbTotalPrice.Text = totalPrice.ToString("C0", new System.Globalization.CultureInfo("vi-VN")) + "đ";

            //end
            if (totalPrice == 0)
            {
                MessageBox.Show("Bàn này còn trống vui lòng chọn bàn đã ăn !!!", "Thông báo", MessageBoxButtons.OK);
                return;
            }
            FormCheckOut f = new FormCheckOut(listBillInfo, (double)nmDiscount.Value, this);
            f.table_name = table.Name;
            int idBill = BillDAO.Instance.getUnCheckBillIDbyTableID(table.ID);
            f.current_id_bill = idBill;
            f.table_id=table.ID;
            f.ShowDialog();
           
            /* int idBill = BillDAO.Instance.getUnCheckBillIDbyTableID(table.ID);
             int Discount=(int)nmDiscount.Value;
             double totalPrice = Convert.ToDouble(txbTotalPrice.Text.Split(',')[0]);
             double finalTotalPrice = totalPrice - (totalPrice / 100) * Discount;
             if (idBill != -1)
             {
                 if (MessageBox.Show(string.Format("Bạn có chắc muốn thanh toán hóa đơn cho {0}\n Tổng tiền - (Tổng tiền/100) x Giảm giá\n=> {1} - ({1} / 100) * {2} = {3} ", table.Name,totalPrice,Discount,finalTotalPrice , " không ?"), "Thông báo", MessageBoxButtons.OKCancel) == System.Windows.Forms.DialogResult.OK)
                 {
                     BillDAO.Instance.CheckOut(idBill,Discount,(float)finalTotalPrice);
                     ShowBill(table.ID);
                     LoadTable();
                 }

             }*/
        }

        private void btnSwitchTable_Click(object sender, EventArgs e)
        {
            
            int id1 = (lsvBill.Tag as Table).ID;
            int id2=(cbSwitchTable.SelectedItem as Table).ID;
            if (MessageBox.Show(String.Format("Bạn có thực sự muốn chuyển từ bàn {0} sang bàn {1}", (lsvBill.Tag as Table).Name, (cbSwitchTable.SelectedItem as Table).Name),"Thông báo",MessageBoxButtons.OKCancel)==System.Windows.Forms.DialogResult.OK)
            TableDAO.Instance.SwitchTable(id1, id2);
            LoadTable();
        }

        private void txbTotalPrice_TextChanged(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {

        }

        private void menuStrip1_ItemClicked(object sender, ToolStripItemClickedEventArgs e)
        {

        }

        private void thôngtintkToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {

        }

        private void lsvBill_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void cbSwitchTable_SelectedIndexChanged(object sender, EventArgs e)
        {

        }
    }
}
