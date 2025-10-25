﻿using QuanlyquanCoffe.DAO;
using QuanlyquanCoffe.DTO;
using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Diagnostics;
using System.Drawing;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml.Linq;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.StartPanel;
using OfficeOpenXml; // Thư viện EPPlus
using System.IO;    // Để làm việc với FileInfo
using OfficeOpenXml.Style; // <-- THÊM CÁI NÀY
using System.Drawing;

namespace QuanlyquanCoffe
{
    public partial class fAdmin : Form
    {
        BindingSource foodlist=new BindingSource();
        BindingSource accountlist=new BindingSource();
        BindingSource categorylist=new BindingSource();
      BindingSource tablelist=new BindingSource();
         
        public Account loginAccount;
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
            dtgvAccount.DataSource = accountlist;
            dtgvCategoryFood.DataSource = categorylist;
            dtgv_TableFood.DataSource = tablelist;
            
            LoadDateTimePickerBill();
           LoadAccoutList();
            LoadListBillByDate(dtpkfromDate.Value, dtpktoDate.Value);
            LoadListFood();
            LoadTableList();
            AddFoodBinding();
            LoadCategoryIntoComboBox(cbFoodCategory);
            AddAccountBinding();
            AddCategoryBinding();
            AddTableBinding();
            LoadCategoryFoodList();
            txbPageBill_TextChanged(this, new EventArgs());
            //ShowTotalBill();

        }
  
        //Table
        void AddTableBinding()
        {
            txbIDTable.DataBindings.Add(new Binding("Text", dtgv_TableFood.DataSource, "Mã số", true, DataSourceUpdateMode.Never));
            txbNameTable.DataBindings.Add(new Binding("Text", dtgv_TableFood.DataSource, "Tên bàn", true, DataSourceUpdateMode.Never));
            txbStatusTable.DataBindings.Add(new Binding("Text", dtgv_TableFood.DataSource, "Trạng thái", true, DataSourceUpdateMode.Never));
        }
        void LoadTableList()
        {

           tablelist.DataSource = TableDAO.Instance.GetListTable();
        }
        //Category
        void AddCategoryBinding()
        {
            txbIDCategory.DataBindings.Add(new Binding("Text", dtgvCategoryFood.DataSource, "Mã số", true, DataSourceUpdateMode.Never));
            txbNameCategory.DataBindings.Add(new Binding("Text", dtgvCategoryFood.DataSource, "Tên danh mục", true, DataSourceUpdateMode.Never));
        }
        void LoadCategoryFoodList()
        {
            categorylist.DataSource = CategoryDAO.Instance.GetListCategoryFood();
        }
        //Account
        void AddAccountBinding()
        {
            /* txbUserName.DataBindings.Add(new Binding("Text",dtgvAccount.DataSource,"UserName",true,DataSourceUpdateMode.Never));
             txbDisplayName.DataBindings.Add(new Binding("Text", dtgvAccount.DataSource, "DisplayName", true, DataSourceUpdateMode.Never));
             nmAccountType.DataBindings.Add(new Binding("Value", dtgvAccount.DataSource, "Type", true, DataSourceUpdateMode.Never));*/
            txbUserName.DataBindings.Add(new Binding("Text", dtgvAccount.DataSource, "Tên TK", true, DataSourceUpdateMode.Never));
            txbDisplayName.DataBindings.Add(new Binding("Text", dtgvAccount.DataSource, "Tên hiển thị", true, DataSourceUpdateMode.Never));
            nmAccountType.DataBindings.Add(new Binding("Value", dtgvAccount.DataSource, "Loại TK", true, DataSourceUpdateMode.Never));
        }
     
        public void LoadAccoutList()
        {
            accountlist.DataSource = AccountDAO.Instance.GetListAccount();
        }
        //Food
        List<Food> SearchFoodByName(string name)
        {
            List<Food> listFood =FoodDAO.Instance.SearchFoodbyName(name);
            return listFood;
        }
        void LoadListBillByDate(DateTime checkin,DateTime checkout)
        {
            
            dtgvBill.DataSource= BillDAO.Instance.GetBillListByDate(checkin, checkout);
            decimal total = 0;

            foreach (DataGridViewRow row in dtgvBill.Rows)
            {
                if (row.Cells["Tổng tiền"].Value != null && row.Cells["Tổng tiền"].Value.ToString() != "")
                {
                    total += Convert.ToDecimal(row.Cells["Tổng tiền"].Value);
                }
            }
            txbTotalBillAll.Text = total.ToString("N0");

            //MessageBox.Show("Tổng doanh thu: " + total.ToString("N0") + " VNĐ");


        }
        void LoadDateTimePickerBill()
        {
            DateTime today=DateTime.Now;
            dtpkfromDate.Value = new DateTime(today.Year, today.Month, 1);
            dtpktoDate.Value=dtpkfromDate.Value.AddMonths(1).AddDays(-1);
        }
        void AddFoodBinding() {
            /* txbFoodName.DataBindings.Add(new Binding("Text", dtgvFood.DataSource, "name",true,DataSourceUpdateMode.Never));
             txbFoodID.DataBindings.Add(new Binding("Text",dtgvFood.DataSource,"id", true, DataSourceUpdateMode.Never));
             nmFoodPrice.DataBindings.Add(new Binding("Value", dtgvFood.DataSource, "price", true, DataSourceUpdateMode.Never));*/
            txbFoodName.DataBindings.Add(new Binding("Text", dtgvFood.DataSource, "Tên", true, DataSourceUpdateMode.Never));
            txbFoodID.DataBindings.Add(new Binding("Text", dtgvFood.DataSource, "ID", true, DataSourceUpdateMode.Never));
            nmFoodPrice.DataBindings.Add(new Binding("Value", dtgvFood.DataSource, "Giá", true, DataSourceUpdateMode.Never));
        }
        void LoadCategoryIntoComboBox(ComboBox cb)
        {
            cb.DataSource = CategoryDAO.Instance.GetListCategory();
            cb.DisplayMember = "Name";
        }
        void LoadListFood()
        {
            foodlist.DataSource = FoodDAO.Instance.GetListFood1();
        }
        //Quản lý tài khoản
        private bool checksameAccount(string name)
        {

            DataTable a = Dataprovider.Instance.ExcuteQuery(string.Format("select * from Account where UserName=N'{0}'", name));
            return a.Rows.Count > 0;
        }
        void AddAccount(string username,string displayname,int type,string pasword)
        {
            if (MessageBox.Show("Bạn có chắc chắn muốn thêm tài khoản mới", "Thông báo", MessageBoxButtons.OKCancel) == System.Windows.Forms.DialogResult.OK)
            {
                if (checksameAccount(username)) {
                    MessageBox.Show("Đã tồn tại tên tài khoản");
                }
                else
                {
                    if (AccountDAO.Instance.InsertAccount(username, displayname, type, pasword ))
                    {
                        MessageBox.Show("Thêm tài khoản thành công");
                    }
                    else
                    {
                        MessageBox.Show("Thêm tài khoản thất bại");
                    }
                }
            }
           LoadAccoutList();
        }
        void EditAccount(string username, string displayname, int type)
        {
            if (MessageBox.Show("Bạn có chắc chắn muốn cập nhật lại thông tin tài khoản này", "Thông báo", MessageBoxButtons.OKCancel) == System.Windows.Forms.DialogResult.OK)
            {
                if (AccountDAO.Instance.UpdateAccount(username, displayname, type))
                {
                    MessageBox.Show("Cập nhật tài khoản thành công");
                }
                else
                {
                    MessageBox.Show("Cập nhật tài khoản thất bại");
                }
            }
            LoadAccoutList();
        }
        void Deleteaccount(string username)
        {
            if (MessageBox.Show("Bạn có chắc chắn muốn xóa tài khoản này", "Thông báo", MessageBoxButtons.OKCancel) == System.Windows.Forms.DialogResult.OK)
            {
                
                if (loginAccount.Username.Equals(username))
                {
                    MessageBox.Show("Không thể xóa tài khoản đang sử dụng");
                    return;
                }
                if (AccountDAO.Instance.DeleteAccount(username))
                {
                    MessageBox.Show("Xóa tài khoản thành công");
                }
                else
                {
                    MessageBox.Show("Xóa tài khoản thất bại");
                }
            }
            LoadAccoutList();
        }
        void ResetPass(string name)
        {
            if (MessageBox.Show("Bạn có chắc chắn muốn đặt lại mật khẩu tài khoản", "Thông báo", MessageBoxButtons.OKCancel) == System.Windows.Forms.DialogResult.OK)
            {
                if (AccountDAO.Instance.ResetPassword(name))
                {
                    MessageBox.Show("Đặt lại mật khẩu thành công");
                }
                else
                {
                    MessageBox.Show("Đặt lại mật khẩu thất bại");
                }
            }
        }
        #endregion
        #region events
        //BtnAccount
        private void button18_Click(object sender, EventArgs e)
        {
            string username = txbUserName.Text;
            ResetPass(username);
        }
        private void btnAddAccount_Click(object sender, EventArgs e)
        {
            /* string username=txbUserName.Text;
             string displayname=txbDisplayName.Text;
             int AccounType =(int)nmAccountType.Value;
             AddAccount(username,displayname,AccounType);*/
            AddFormAccount faddAccount = new AddFormAccount(this);
            faddAccount.ShowDialog();
        }

        private void btnEditAccount_Click(object sender, EventArgs e)
        {
            string username = txbUserName.Text;
            string displayname = txbDisplayName.Text;
            int AccounType = (int)nmAccountType.Value;
            EditAccount(username, displayname, AccounType);
        }

        private void btnDeleteAccount_Click(object sender, EventArgs e)
        {
            string username = txbUserName.Text;
            Deleteaccount(username);
        }
        private void dateTimePicker1_ValueChanged(object sender, EventArgs e)
        {
            // Sự kiện này chạy khi bạn thay đổi "Ngày bắt đầu"
            if (dtpkfromDate.Value > dtpktoDate.Value)
            {
                // Nếu "Ngày bắt đầu" bạn vừa chọn lớn hơn "Ngày kết thúc"
                // Tự động gán "Ngày kết thúc" = "Ngày bắt đầu"
                dtpktoDate.Value = dtpkfromDate.Value;

                // (Tùy chọn) Hiển thị thông báo cho người dùng
                 MessageBox.Show("Ngày bắt đầu không thể lớn hơn ngày kết thúc. Đã tự động điều chỉnh.", "Thông báo");
            }
        }

        private void dtgvBill_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void btnViewBill_Click(object sender, EventArgs e)
        {
            LoadListBillByDate(dtpkfromDate.Value,dtpktoDate.Value);

        }
        private bool checksameFood(string name)
        {

            DataTable a = Dataprovider.Instance.ExcuteQuery(string.Format("select * from Food where name=N'{0}'", name));
            return a.Rows.Count > 0;
        }
        private void btnAddFood1(object sender, EventArgs e)
        {
/*            string name = txbFoodName.Text;
            int categoryID=(cbFoodCategory.SelectedItem as Category).ID;
            float price = (float)nmFoodPrice.Value;
            if (MessageBox.Show("Bạn có chắc chắn muốn thêm món ăn mới", "Thông báo", MessageBoxButtons.OKCancel) == System.Windows.Forms.DialogResult.OK)
            {
                if (checksameFood(name)) {
                    MessageBox.Show("Đã tồn tại món ăn này");
                }
                else
                {
                    if (FoodDAO.Instance.InsertFood(name, categoryID, price))
                    {
                        MessageBox.Show("Thêm món thành công");
                        LoadListFood();
                        if (insertFood != null)
                        {
                            insertFood(this, new EventArgs());
                        }
                    }
                    else
                    {
                        MessageBox.Show("Có lỗi khi thêm thức ăn");
                    }
                }
            }
        */
            AddFormFood f = new AddFormFood();
            f.InsertFood += F_InsertFood;
            f.ShowDialog();
        }

        private void F_InsertFood(object sender, EventArgs e)
        {
            LoadListFood();
            if (insertFood != null)
            {
                insertFood(this, new EventArgs());
            }
        }

        private void btnDeleteFood_Click(object sender, EventArgs e)
        {
            int id = Convert.ToInt32(txbFoodID.Text);
            if (MessageBox.Show("Bạn có chắc chắn muốn xóa món ăn này", "Thông báo", MessageBoxButtons.OKCancel) == System.Windows.Forms.DialogResult.OK)
            {
                if (FoodDAO.Instance.DeleteFood(id))
                {
                    MessageBox.Show("Xoá món thành công");
                    LoadListFood();
                    if (deleteFood != null)
                    {
                        deleteFood(this, new EventArgs());
                    }
                }
                else
                {
                    MessageBox.Show("Có lỗi khi xóa thức ăn");
                }
            }
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
       
        
        private void button3_Click(object sender, EventArgs e)
        {
            LoadListFood();
        }

        private void dtgvFood_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void txbFoodID_TextChanged(object sender, EventArgs e)
        {
            try
            {
                if (dtgvFood.SelectedCells.Count > 0&& dtgvFood.SelectedCells[0].OwningRow.Cells["Loại"].Value!=null)
                {
                    int id = (int)dtgvFood.SelectedCells[0].OwningRow.Cells["Loại"].Value;

                    Category category = CategoryDAO.Instance.GetCategoryByID(id);
                    cbFoodCategory.SelectedItem = category;
                    int index = -1;
                    int i = 0;
                    foreach (Category item in cbFoodCategory.Items)
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
            }catch {
                MessageBox.Show("Vui lòng nhập đúng tên món ăn ");
            }
        }

        private void btnEditFood_Click(object sender, EventArgs e)
        {
            string name = txbFoodName.Text;
            int categoryID = (cbFoodCategory.SelectedItem as Category).ID;
            float price = (float)nmFoodPrice.Value;
            int id=Convert.ToInt32(txbFoodID.Text);
            if (MessageBox.Show("Bạn có chắc chắn muốn chỉnh sửa món ăn mới", "Thông báo", MessageBoxButtons.OKCancel) == System.Windows.Forms.DialogResult.OK)
            {
                if (FoodDAO.Instance.UpdateFood(id, name, categoryID, price))
                {
                    MessageBox.Show("Chỉnh sửa món thành công");
                    LoadListFood();
                    if (updateFood != null)
                    {
                        updateFood(this, new EventArgs());
                    }
                }
                else
                {
                    MessageBox.Show("Có lỗi khi chỉnh sửa thức ăn");
                }
            }
        }
        //event Food
        private event EventHandler insertFood;
        public event EventHandler InsertFood
        {
            add { insertFood += value; }
            remove { insertFood -= value; }
        }
        private event EventHandler deleteFood;
        public event EventHandler DeleteFood
        {
            add { deleteFood += value; }
            remove { deleteFood -= value; }
        }
        private event EventHandler updateFood;
        public event EventHandler UpdateFood
        {
            add { updateFood += value; }
            remove {updateFood -= value; }
        }
        //Event Category
        private event EventHandler insertCategory;
        public event EventHandler InsertCategory
        {
            add { insertCategory += value; }
            remove { insertCategory -= value; }
        }
        private event EventHandler updateCategory;
        public event EventHandler UpdateCategory
        {
            add { updateCategory += value; }
            remove { updateCategory -= value; }
        }
        private event EventHandler deleteCategory;
        public event EventHandler DeleteCategory
        {
            add { deleteCategory += value; }
            remove { deleteCategory -= value; }
        }
        //Event Table
        private event EventHandler insertTable;
        public event EventHandler InsertTable
        {
            add { insertTable += value; }
            remove { insertTable -= value; }
        }
        private event EventHandler updateTable;
        public event EventHandler UpdateTable
        {
            add { updateTable += value; }
            remove { updateTable -= value; }
        }
        public event EventHandler deleteTable;
        public event EventHandler DeleteTable
        {
            add { deleteTable += value; }
            remove { deleteTable -= value; }
        }

        private void btn_searchfood_Click(object sender, EventArgs e)
        {
            foodlist.DataSource= SearchFoodByName(txbSearchFoodName.Text);
        }

        private void btn_ShowAccount_Click(object sender, EventArgs e)
        {
            LoadAccoutList();
        }
        private void button6_Click(object sender, EventArgs e)
        {
            LoadCategoryFoodList();
        }
        private void dataGridView3_CellContentClick(object sender, EventArgs e) { }

        private void dtgvCategoryFood_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void button5_Click(object sender, EventArgs e)
        {
            LoadTableList();
        }

        private void txbStatusTable_TextChanged(object sender, EventArgs e)
        {

        }




        #endregion
        //Phân trang
        private void btnFirstBillPage_Click(object sender, EventArgs e)
        {
            txbPageBill.Text = "1";
        }

        private void btnLastBillPage_Click(object sender, EventArgs e)
        {
            int sumRecord = BillDAO.Instance.GetNumBillListByDate(dtpkfromDate.Value,dtpktoDate.Value);
            int LastPage = sumRecord / 10;
            if (sumRecord % 10 != 0)
            {
                LastPage++;
            }
            txbPageBill.Text=LastPage.ToString();
        }

        private void txbPageBill_TextChanged(object sender, EventArgs e)
        {
            dtgvBill.DataSource = BillDAO.Instance.GetBillListByDateAndPage(dtpkfromDate.Value, dtpktoDate.Value,Convert.ToInt32(txbPageBill.Text));
            if (dtgvBill.Columns["id"] != null) // Thêm kiểm tra để tránh lỗi
            {
                dtgvBill.Columns["id"].Visible = false;
            }
        }
        
        private void btnPreviousBillPage_Click(object sender, EventArgs e)
        {
            int page =Convert.ToInt32(txbPageBill.Text);
            if (page > 1)
            {
                page--;
            }
            txbPageBill.Text=page.ToString();
        }

        private void btnNextBillPage_Click(object sender, EventArgs e)
        {
            int page = Convert.ToInt32(txbPageBill.Text);
            int sumRecord = BillDAO.Instance.GetNumBillListByDate(dtpkfromDate.Value, dtpktoDate.Value);
            if (sumRecord % 10 == 0) { 
                if (page < sumRecord/10)
                {
                    page++;
                }
            }
            else
            {
                if (page < ((sumRecord / 10)+1))
                {
                    page++;
                }
            }
            txbPageBill.Text = page.ToString();
        }

        private void pictureBox5_Click(object sender, EventArgs e)
        {

        }
        //Show doanh số
        private void ShowTotalBill()
        {

            string connectionString = "Data Source=DESKTOP-2PIF1AG\\SQLEXPRESS01;Initial Catalog=QuanLyQuanCoffe;Integrated Security=True;Encrypt=False";



            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();

                
                string sql = "SELECT SUM(b.totalPrice) FROM Bill AS b WHERE b.status = 1";

                
                using (SqlCommand command = new SqlCommand(sql, connection))
                {
                  

                    
                    object result = command.ExecuteScalar();

                    
                    if (result != DBNull.Value && result != null)
                    {
                        double sum = Convert.ToDouble(result);
                      
                        CultureInfo culture = new CultureInfo("vi-VN");
                        Thread.CurrentThread.CurrentCulture = culture;
                        //txbTotalBillAll.Text = sum.ToString("c",culture);
                    }

                }
            }
        }

        private void pictureBox16_Click(object sender, EventArgs e)
        {

        }
        //Quản lý loại thức ăn
        private bool checksameFoodCategory(string name)
        {
            
            DataTable a= Dataprovider.Instance.ExcuteQuery(string.Format("select * from FoodCategory where name=N'{0}'",name));
            return a.Rows.Count > 0;
        }
        private void btnAddCategory_Click(object sender, EventArgs e)
        {
/*            string name=txbNameCategory.Text;
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
                        LoadCategoryFoodList();
                        LoadCategoryIntoComboBox(cbFoodCategory);

                        if (insertCategory != null)
                        {
                            insertCategory(this, new EventArgs());
                        }

                    }
                    else
                    {
                        MessageBox.Show("Có lỗi khi thêm loại thức ăn mới");
                    }
                }
            }*/
            AddFormCategory f = new AddFormCategory();
            f.InsertCategory += F_InsertCategory;
            f.ShowDialog();

        }

        private void F_InsertCategory(object sender, EventArgs e)
        {
            LoadCategoryFoodList();
            LoadCategoryIntoComboBox(cbFoodCategory);

            if (insertCategory != null)
            {
                insertCategory(this, new EventArgs());
            }
        }

        private void btnUpdateCategory_Click(object sender, EventArgs e)
        {
            string name = txbNameCategory.Text;
            int id = Convert.ToInt32(txbIDCategory.Text);
            if (MessageBox.Show("Bạn có chắc chắn muốn cập nhật loại thức ăn mới", "Thông báo", MessageBoxButtons.OKCancel) == System.Windows.Forms.DialogResult.OK)
            {
                if (CategoryDAO.Instance.UpdateCategory(name, id))
                {
                    MessageBox.Show("Cập nhật loại thức ăn thành công");
                    LoadCategoryFoodList();
                    LoadCategoryIntoComboBox(cbFoodCategory);

                    if (updateCategory != null)
                    {
                        updateCategory(this, new EventArgs());
                    }
                }
                else
                {
                    MessageBox.Show("Cập nhật loại thức ăn thất bại");
                }
            }
        }
        private void btnDeleteCategory_Click(object sender, EventArgs e)
        {
            int id = Convert.ToInt32(txbIDCategory.Text);
            if (MessageBox.Show("Bạn có chắc chắn muốn xóa loại thức ăn mới", "Thông báo", MessageBoxButtons.OKCancel) == System.Windows.Forms.DialogResult.OK)
            {
                if (CategoryDAO.Instance.DeleteCategory(id))
                {
                    MessageBox.Show("Xóa loại thức ăn thành công");
                    LoadCategoryFoodList();
                    LoadListFood();
                    LoadCategoryIntoComboBox(cbFoodCategory);
                    if (deleteCategory != null)
                    {
                        deleteCategory(this, new EventArgs());
                    }
                }
                else
                {
                    MessageBox.Show("Xóa loại thức ăn thất bại");
                }
            }
        }
      
        //Quản lý Bàn
        private void btnAddTable_Click(object sender, EventArgs e)
        {
/*            string name=txbNameTable.Text;
            if (MessageBox.Show("Bạn có chắc chắn muốn thêm bàn mới", "Thông báo", MessageBoxButtons.OKCancel) == System.Windows.Forms.DialogResult.OK)
            {
                
                if (TableDAO.Instance.InsertTable(name))
                {
                    MessageBox.Show("Thêm bàn mới thành công");
                    LoadTableList();
                    if (insertTable != null)
                    {
                        insertTable(this, new EventArgs());
                    }

                }
                else
                {
                    MessageBox.Show("Thêm bàn mới thất bại");
                }
            }*/
            AddFormTable addFormTable = new AddFormTable();
            addFormTable.InsertTable += AddFormTable_InsertTable;
            addFormTable.ShowDialog();


        }

        private void AddFormTable_InsertTable(object sender, EventArgs e)
        {
            LoadTableList();
            if (insertTable != null)
            {
                insertTable(this, new EventArgs());
            }
        }

        private void btnUpdateTable_Click(object sender, EventArgs e)
        {
            string name = txbNameTable.Text;
            int id= Convert.ToInt32(txbIDTable.Text);
            string status=txbStatusTable.Text;
            if (MessageBox.Show("Bạn có chắc chắn muốn cập nhật bàn mới", "Thông báo", MessageBoxButtons.OKCancel) == System.Windows.Forms.DialogResult.OK)
            {
                if (TableDAO.Instance.UpdateTable(name, id, status))
                {
                    MessageBox.Show("Cập nhật bàn thành công");
                    LoadTableList();
                    if (updateTable != null)
                    {
                        updateTable(this, new EventArgs());
                    }
                }
                else
                {
                    MessageBox.Show("Cập nhật bàn thất bại");
                }
            }
        }

        private void btnDeleteTable_Click(object sender, EventArgs e)
        {
            string name = txbNameTable.Text;
            int id = Convert.ToInt32(txbIDTable.Text);
            string status = txbStatusTable.Text;
            if (MessageBox.Show("Bạn có chắc chắn xóa bàn "+name, "Thông báo", MessageBoxButtons.OKCancel) == System.Windows.Forms.DialogResult.OK)
            {
                if (status == "Trống")
                {
                    if (TableDAO.Instance.DeleteTable(id, status))
                    {
                        MessageBox.Show("Xóa bàn thành công");
                        LoadTableList();
                        if (deleteTable != null)
                        {
                            deleteTable(this, new EventArgs());
                        }
                    }
                    else
                    {
                        MessageBox.Show("Xóa bàn thất bại");
                    }
                }
                else
                {
                    MessageBox.Show("Bàn đang có người không thể xóa");
                }
            }
        }

        private void fAdmin_Load(object sender, EventArgs e)
        {

        }

        private void cbFoodCategory_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void pictureBox16_Click_1(object sender, EventArgs e)
        {

        }

        private void panel2_Paint(object sender, PaintEventArgs e)
        {

        }

        private void btnExportExcel_Click_1(object sender, EventArgs e)
        {
            // 1. LẤY DỮ LIỆU TỪ DATAGRIDVIEW
            DataTable dt = (DataTable)dtgvBill.DataSource;

            // 2. YÊU CẦU NGƯỜI DÙNG CHỌN NƠI LƯU FILE
            SaveFileDialog saveDialog = new SaveFileDialog();
            saveDialog.Filter = "Excel Files (*.xlsx)|*.xlsx|All Files (*.*)|*.*";
            // Tự động tạo tên file dựa trên ngày bắt đầu và kết thúc
            saveDialog.FileName = $"ThongKeDoanhThu_Tu_{dtpkfromDate.Value:dd-MM-yyyy}_Den_{dtpktoDate.Value:dd-MM-yyyy}.xlsx";

            if (saveDialog.ShowDialog() == DialogResult.OK)
            {
                try
                {
                    // 3. SET LICENSE
                    ExcelPackage.License.SetNonCommercialPersonal("fAdmin");

                    using (var p = new ExcelPackage())
                    {
                        // TẠO MỘT SHEET MỚI
                        var ws = p.Workbook.Worksheets.Add("DoanhThu");

                        // 4. LOAD DỮ LIỆU TỪ DATATABLE VÀO SHEET
                        ws.Cells["A1"].LoadFromDataTable(dt, true);

                        // =======================================================
                        // 4.1. LÀM ĐẸP (STYLING) CHO FILE EXCEL
                        // =======================================================

                        // A. Format cho Header (Dòng 1)
                        int lastColumn = ws.Dimension.End.Column;
                        using (var range = ws.Cells[1, 1, 1, lastColumn])
                        {
                            range.Style.Font.Bold = true;
                            range.Style.Fill.PatternType = ExcelFillStyle.Solid;
                            range.Style.Fill.BackgroundColor.SetColor(Color.LightGray);
                            range.Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
                        }

                        // B. Format cột "Tổng tiền" (Cột B = 2)
                        ws.Column(2).Style.Numberformat.Format = "#,##0";
                        ws.Column(2).Style.HorizontalAlignment = ExcelHorizontalAlignment.Right;

                        // C. Format cột "Ngày vào" (Cột C = 3)
                        ws.Column(3).Style.Numberformat.Format = "dd/MM/yyyy HH:mm";

                        // D. Format cột "Ngày ra" (Cột D = 4)
                        ws.Column(4).Style.Numberformat.Format = "dd/MM/yyyy HH:mm";


                        // =======================================================
                        // 4.2. THÊM TỔNG DOANH SỐ VÀO CUỐI (PHẦN MỚI)
                        // =======================================================

                        // Lấy dòng cuối cùng mà dữ liệu chiếm + 2 (để cách ra 1 dòng)
                        int totalRow = ws.Dimension.End.Row + 2;

                        // Ghi chữ "Doanh số:" vào cột A (cột 1)
                        var labelCell = ws.Cells[totalRow, 1];
                        labelCell.Value = "Doanh số:";
                        labelCell.Style.Font.Bold = true;
                        labelCell.Style.HorizontalAlignment = ExcelHorizontalAlignment.Right; // Căn phải

                        // Ghi giá trị Doanh số (lấy từ TextBox) vào cột B (cột 2)
                        var valueCell = ws.Cells[totalRow, 2];
                        valueCell.Value = txbTotalBillAll.Text; // Lấy text từ ô Doanh số
                        valueCell.Style.Font.Bold = true;
                        valueCell.Style.HorizontalAlignment = ExcelHorizontalAlignment.Right; // Căn phải
                                                                                              // Đặt định dạng số cho ô này (dù nó là text)
                        valueCell.Style.Numberformat.Format = "#,##0";


                        // E. (Tùy chọn) Tự động dãn cột cho vừa dữ liệu
                        // Phải đặt ở cuối sau khi đã format và thêm tổng
                        ws.Cells[ws.Dimension.Address].AutoFitColumns();

                        // Điều chỉnh lại cột A và B một chút cho đẹp hơn
                        ws.Column(1).Width = ws.Column(1).Width + 5;
                        ws.Column(2).Width = ws.Column(2).Width + 5;

                        // 5. LƯU FILE
                        FileInfo file = new FileInfo(saveDialog.FileName);
                        p.SaveAs(file);
                    }

                    MessageBox.Show("Xuất file Excel thành công!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Có lỗi khi xuất file Excel: " + ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private void dtpktoDate_ValueChanged(object sender, EventArgs e)
        {
            // Sự kiện này chạy khi bạn thay đổi "Ngày kết thúc"
            if (dtpktoDate.Value < dtpkfromDate.Value)
            {
                // Nếu "Ngày kết thúc" bạn vừa chọn nhỏ hơn "Ngày bắt đầu"
                // Tự động gán "Ngày bắt đầu" = "Ngày kết thúc"
                dtpkfromDate.Value = dtpktoDate.Value;

                // (Tùy chọn) Hiển thị thông báo cho người dùng
                 MessageBox.Show("Ngày kết thúc không thể nhỏ hơn ngày bắt đầu. Đã tự động điều chỉnh.", "Thông báo");
            }
        }
    }
}
