using QuanlyquanCoffe.DAO;
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
            LoadIngredientComboBox();
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

            // --- PHẦN 2: CODE MỚI (ĐỂ TẢI CÔNG THỨC) ---
            try
            {
                // Kiểm tra xem txbFoodID có rỗng hay không
                if (!string.IsNullOrEmpty(txbFoodID.Text))
                {
                    int idFood = Convert.ToInt32(txbFoodID.Text);
                    LoadRecipe(idFood); // <-- GỌI HÀM MỚI Ở ĐÂY
                }
                else
                {
                    // Nếu txbFoodID bị rỗng (ví dụ: đang thêm món mới)
                    rdoRecipeNo.Checked = true;
                }
            }
            catch
            {
                // Trường hợp này xảy ra khi ID không phải là số (ít khi xảy ra)
                rdoRecipeNo.Checked = true;
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

            string connectionString = "Data Source=DESKTOP-2PIF1AG\\SQLEXPRESS01;Initial Catalog=QuanLyQuanCoffe2;Integrated Security=True;Encrypt=False";



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

        void LoadIngredientComboBox()
        {
            DataTable ingredientList = IngredientDAO.Instance.GetListIngredient();

            cbNguyenLieu.DataSource = ingredientList;
            cbNguyenLieu.DisplayMember = "name";
            cbNguyenLieu.ValueMember = "id";
            // Dùng dt này để cập nhật Label đơn vị
            cbNguyenLieu.Tag = ingredientList;
        }

        private void cbNguyenLieu_SelectedIndexChanged(object sender, EventArgs e)
        {
            // Cập nhật Label đơn vị
            if (cbNguyenLieu.SelectedItem != null)
            {
                DataRowView drv = cbNguyenLieu.SelectedItem as DataRowView;
                string unit = drv["unit"].ToString();
                 lblDonVi.Text = $"({unit})";
            }
        }

        void LoadRecipe(int idFood)
        {
            // 1. Gọi DAO để lấy công thức
            DataTable dtRecipe = FoodIngredientMapDAO.Instance.GetListIngredientByFoodID(idFood);
            dtgvCongThuc.DataSource = dtRecipe;
            // 2. Kiểm tra xem có công thức không
            if (dtRecipe.Rows.Count > 0)
            {
                // Nếu CÓ công thức:
                rdoRecipeYes.Checked = true; // Tự động chọn "Có"
                dtgvCongThuc.DataSource = dtRecipe; // Tải dữ liệu vào lưới công thức

                // =========================================================================
                // === PHẦN SỬA ĐỔI: ĐIỀU CHỈNH CÁC CỘT SAU KHI DATASOURCE ĐƯỢC GÁN ===
                // =========================================================================

                // 3. Ẩn cột idIngredient (người dùng không cần thấy)
                if (dtgvCongThuc.Columns.Contains("idIngredient"))
                {
                    dtgvCongThuc.Columns["idIngredient"].Visible = false;
                }

                // 4. Đảm bảo cột "Xóa" tồn tại (nếu chưa có thì tạo) và chỉnh sửa
                //    Đây là cột nút mà bạn đã thêm thủ công trong Designer,
                //    Chúng ta cần đảm bảo nó là cột cuối cùng và có kích thước phù hợp.
                DataGridViewButtonColumn btnDeleteRecipe = null;
                if (dtgvCongThuc.Columns.Contains("colDelete"))
                {
                    btnDeleteRecipe = dtgvCongThuc.Columns["colDelete"] as DataGridViewButtonColumn;
                }
                else // Nếu chưa có, chúng ta tạo mới nó
                {
                    btnDeleteRecipe = new DataGridViewButtonColumn();
                    btnDeleteRecipe.Name = "colDelete";
                    btnDeleteRecipe.HeaderText = "";
                    dtgvCongThuc.Columns.Add(btnDeleteRecipe);
                }

                // Luôn đảm bảo nút "X" có chữ "X" và nằm ở cuối
                btnDeleteRecipe.Text = "X";
                btnDeleteRecipe.UseColumnTextForButtonValue = true; // HIỂN THỊ CHỮ "X" TRÊN NÚT
                btnDeleteRecipe.Width = 30; // CHIỀU RỘNG CỦA CỘT NÚT, ĐỂ NÓ NHỎ HƠN
                btnDeleteRecipe.FlatStyle = FlatStyle.Flat; // LÀM CHO NÚT TRÔNG ĐẸP HƠN
                btnDeleteRecipe.DefaultCellStyle.Padding = new Padding(0); // Bỏ padding để nút nhỏ gọn hơn
                btnDeleteRecipe.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter; // Canh giữa nút

                // Di chuyển cột "Xóa" về cuối cùng
                dtgvCongThuc.Columns["colDelete"].DisplayIndex = dtgvCongThuc.ColumnCount - 1;


                // 5. Điều chỉnh kích thước các cột khác để đẹp hơn
                if (dtgvCongThuc.Columns.Contains("Tên nguyên liệu"))
                {
                    dtgvCongThuc.Columns["Tên nguyên liệu"].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill; 
                }
                if (dtgvCongThuc.Columns.Contains("Định lượng"))
                {
                    dtgvCongThuc.Columns["Định lượng"].Width = 80; // Chiều rộng cho cột số
                    dtgvCongThuc.Columns["Định lượng"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
                }
                if (dtgvCongThuc.Columns.Contains("Đơn vị"))
                {
                    dtgvCongThuc.Columns["Đơn vị"].Width = 50; // Chiều rộng cho cột đơn vị
                    dtgvCongThuc.Columns["Đơn vị"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
                }
                // =========================================================================

            }
            else
            {
                // Nếu KHÔNG có công thức:
                rdoRecipeNo.Checked = true; // Tự động chọn "Không"

            }

        }

        private void btnThemNguyenLieu_Click(object sender, EventArgs e)
        {
            try
            {
                int idFood = Convert.ToInt32(txbFoodID.Text);                                                           

                int idIngredient = (int)cbNguyenLieu.SelectedValue;
                decimal amount = nmDinhLuong.Value;

                if (amount <= 0)
                {
                    MessageBox.Show("Định lượng phải lớn hơn 0.");
                    return;
                }

                if (FoodIngredientMapDAO.Instance.InsertOrUpdateIngredient(idFood, idIngredient, amount))
                {
                    MessageBox.Show("Cập nhật công thức thành công!");
                    LoadRecipe(idFood); // Tải lại lưới công thức ngay lập tức
                }
                else
                {
                    MessageBox.Show("Có lỗi khi cập nhật công thức!");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi: " + ex.Message);
            }
        }


        private void dtgvCongThuc_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            // Bỏ qua nếu click vào header
            if (e.RowIndex < 0) return;

            // Kiểm tra xem có click vào cột "Xóa" (tên là 'colDelete') không
            // (Hãy đảm bảo tên 'colDelete' khớp với tên bạn đặt trong Designer)
            if (dtgvCongThuc.Columns[e.ColumnIndex].Name == "colDelete")
            {
                try
                {
                    // === DÙNG TÊN ĐÚNG Ở ĐÂY ===
                    int idFood = Convert.ToInt32(txbFoodID.Text); // Dùng "txbFoodID"
                                                                  // ==========================

                    // Lấy idIngredient từ cột đã bị ẩn của dòng được click
                    int idIngredient = (int)dtgvCongThuc.Rows[e.RowIndex].Cells["idIngredient"].Value;

                    if (MessageBox.Show("Bạn có chắc muốn xóa nguyên liệu này?", "Xác nhận", MessageBoxButtons.YesNo, MessageBoxIcon.Warning) == DialogResult.Yes)
                    {
                        if (FoodIngredientMapDAO.Instance.DeleteIngredient(idFood, idIngredient))
                        {
                            //MessageBox.Show("Xóa thành công!");
                            LoadRecipe(idFood); // Tải lại lưới công thức
                        }
                        else
                        {
                            MessageBox.Show("Có lỗi khi xóa!");
                        }
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Lỗi: " + ex.Message);
                }
            }
        }

        private void pictureBox16_Click_1(object sender, EventArgs e)
        {

        }

        private void panel2_Paint(object sender, PaintEventArgs e)
        {

        }

        private void btnExportExcel_Click_1(object sender, EventArgs e)
        {
            // 1. LẤY DỮ LIỆU TỪ DATAGRIDVIEW (CHO SHEET 1)
            DataTable dtDoanhThu = (DataTable)dtgvBill.DataSource;

            // *** LẤY DỮ LIỆU CHO SHEET 2 ***
            // (Giả sử bạn đã tạo IngredientDAO.cs và BillDAO.cs có hàm này)
            DataTable dtNguyenLieu = BillDAO.Instance.GetIngredientUsageByDate(dtpkfromDate.Value, dtpktoDate.Value);

            // 2. YÊU CẦU NGƯỜI DÙNG CHỌN NƠI LƯU FILE
            SaveFileDialog saveDialog = new SaveFileDialog();
            saveDialog.Filter = "Excel Files (*.xlsx)|*.xlsx|All Files (*.*)|*.*";
            saveDialog.FileName = $"ThongKeTongHop_Tu_{dtpkfromDate.Value:dd-MM-yyyy}_Den_{dtpktoDate.Value:dd-MM-yyyy}.xlsx";

            if (saveDialog.ShowDialog() == DialogResult.OK)
            {
                try
                {
                    ExcelPackage.License.SetNonCommercialPersonal("fAdmin");

                    using (var p = new ExcelPackage())
                    {
                        // =======================================================
                        // 4. TẠO SHEET 1: DOANH THU
                        // =======================================================
                        var ws = p.Workbook.Worksheets.Add("DoanhThu");
                        ws.Cells["A1"].LoadFromDataTable(dtDoanhThu, true);

                        // =======================================================
                        // 4.1. LÀM ĐẸP (STYLING) CHO FILE EXCEL
                        // =======================================================

                        // A. Format cho Header (Dòng 1)
                        int lastColumn = ws.Dimension.End.Column; // Sẽ là 6
                        using (var range = ws.Cells[1, 1, 1, lastColumn])
                        {
                            range.Style.Font.Bold = true;
                            range.Style.Fill.PatternType = ExcelFillStyle.Solid;
                            range.Style.Fill.BackgroundColor.SetColor(Color.LightGray);
                            range.Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
                        }

                        // --- BẮT ĐẦU SỬA LỖI (CODE MỚI) ---

                        // *** SỬA 1: Ẩn cột 'id' (Cột A - cột 1) ***
                        ws.Column(1).Hidden = true;

                        // *** SỬA 2: Dịch chuyển các cột format sang phải 1 ***

                        // B. Format cột "Tổng tiền" (BÂY GIỜ LÀ CỘT C = 3)
                        ws.Column(3).Style.Numberformat.Format = "#,##0";
                        ws.Column(3).Style.HorizontalAlignment = ExcelHorizontalAlignment.Right;

                        // C. Format cột "Ngày vào" (BÂY GIỜ LÀ CỘT D = 4)
                        ws.Column(4).Style.Numberformat.Format = "dd/MM/yyyy HH:mm";

                        // D. Format cột "Ngày ra" (BÂY GIỜ LÀ CỘT E = 5)
                        ws.Column(5).Style.Numberformat.Format = "dd/MM/yyyy HH:mm";

                        // (Cột F - Giảm giá - Cột 6 - không cần format)

                        // =======================================================
                        // 4.2. THÊM TỔNG DOANH SỐ VÀO CUỐI
                        // =======================================================

                        int totalRow = ws.Dimension.End.Row + 2;

                        // *** SỬA 3: Dịch chuyển vị trí "Tổng Doanh số" ***

                        // Ghi chữ "Doanh số:" vào cột B (cột 2) (Vì cột A bị ẩn)
                        var labelCell = ws.Cells[totalRow, 2];
                        labelCell.Value = "Doanh số:";
                        labelCell.Style.Font.Bold = true;
                        labelCell.Style.HorizontalAlignment = ExcelHorizontalAlignment.Right;

                        // Ghi giá trị Doanh số (lấy từ TextBox) vào cột C (cột 3)
                        var valueCell = ws.Cells[totalRow, 3];
                        valueCell.Value = txbTotalBillAll.Text;
                        valueCell.Style.Font.Bold = true;
                        valueCell.Style.HorizontalAlignment = ExcelHorizontalAlignment.Right;
                        valueCell.Style.Numberformat.Format = "#,##0";


                        // E. Tự động dãn cột
                        ws.Cells[ws.Dimension.Address].AutoFitColumns();

                        // *** SỬA 4: Điều chỉnh lại cột B và C ***
                        ws.Column(2).Width = ws.Column(2).Width + 5; // Cột Tên Bàn
                        ws.Column(3).Width = ws.Column(3).Width + 5; // Cột Tổng tiền

                        // --- KẾT THÚC SỬA LỖI ---


                        // =======================================================
                        // 5. TẠO SHEET 2: NGUYÊN LIỆU 
                        // =======================================================
                        var wsNguyenLieu = p.Workbook.Worksheets.Add("NguyenLieuSuDung");
                        wsNguyenLieu.Cells["A1"].LoadFromDataTable(dtNguyenLieu, true);

                        // (Toàn bộ code format cho Sheet 2 giữ nguyên...)
                        int lastColumnNL = wsNguyenLieu.Dimension.End.Column;
                        using (var range = wsNguyenLieu.Cells[1, 1, 1, lastColumnNL])
                        {
                            range.Style.Font.Bold = true;
                            range.Style.Fill.PatternType = ExcelFillStyle.Solid;
                            range.Style.Fill.BackgroundColor.SetColor(Color.LightSkyBlue);
                            range.Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
                        }
                        wsNguyenLieu.Column(2).Style.Numberformat.Format = "#,##0";
                        wsNguyenLieu.Column(2).Style.HorizontalAlignment = ExcelHorizontalAlignment.Right;
                        wsNguyenLieu.Column(3).Style.HorizontalAlignment = ExcelHorizontalAlignment.Left;
                        wsNguyenLieu.Cells[wsNguyenLieu.Dimension.Address].AutoFitColumns();
                        wsNguyenLieu.Column(1).Width = wsNguyenLieu.Column(1).Width + 5;

                        // =======================================================
                        // 6. LƯU FILE
                        // =======================================================
                        FileInfo file = new FileInfo(saveDialog.FileName);
                        p.SaveAs(file);
                    }

                    MessageBox.Show("Xuất file Excel thống kê thành công!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
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

        private void rdoRecipe_CheckedChanged(object sender, EventArgs e)
        {
            panelCongThuc.Visible = rdoRecipeYes.Checked;
        }


        private void panel4_Paint(object sender, PaintEventArgs e)
        {

        }
    }
}
