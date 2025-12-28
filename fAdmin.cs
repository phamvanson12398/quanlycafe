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
        DataTable dtTempNhap = new DataTable();

        public Account loginAccount;

        private void fAdmin_Load(object sender, EventArgs e)
        {
            dtTempNhap = new DataTable();
            dtTempNhap.Columns.Add("TenNL", typeof(string));
            dtTempNhap.Columns.Add("SoLuong", typeof(decimal));
            dtTempNhap.Columns.Add("DonGia", typeof(decimal));
            dtTempNhap.Columns.Add("ThanhTien", typeof(decimal));
        }

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
            LoadNguyenLieu();
            Load_Phieu_Nhap();
            InitXuatKho();
            InitDgvPhieuXuat();
            InitDgvChiTietPhieuXuat();
            Load_Phieu_Xuat();
            LoadIngredientComboBox();
        }

        private void InitDgvPhieuXuat()
        {
            dtgvPhieuXuat.AutoGenerateColumns = true;
            dtgvPhieuXuat.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dtgvPhieuXuat.MultiSelect = false;
            dtgvPhieuXuat.ReadOnly = true;
            dtgvPhieuXuat.AllowUserToAddRows = false;
            dtgvPhieuXuat.AllowUserToDeleteRows = false;
            dtgvPhieuXuat.RowHeadersVisible = false;
            dtgvPhieuXuat.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            dtgvPhieuXuat.DataSource = null;

        }
        private void InitDgvChiTietPhieuXuat()
        {
            lvChiTietPhieuXuat.RowHeadersVisible = true;
            lvChiTietPhieuXuat.RowHeadersWidth = 30;
            lvChiTietPhieuXuat.AutoGenerateColumns = true;
            lvChiTietPhieuXuat.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            lvChiTietPhieuXuat.MultiSelect = false;
            lvChiTietPhieuXuat.ReadOnly = true;
            lvChiTietPhieuXuat.AllowUserToAddRows = false;
            lvChiTietPhieuXuat.AllowUserToDeleteRows = false;
            lvChiTietPhieuXuat.RowHeadersVisible = false;
            lvChiTietPhieuXuat.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;

            lvChiTietPhieuXuat.DataSource = null;
        }
        private void FormatMoneyGrid(DataGridView dgv, string[] cols)
        {
            foreach (var c in cols)
            {
                if (dgv.Columns.Contains(c))
                {
                    dgv.Columns[c].DefaultCellStyle.Format = "N0";
                    dgv.Columns[c].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
                }
            }
        }

        private void LoadChiTiet(int maPhieuNhap)
        {
            dgvChiTietNhap.DataSource = PhieuNhapDAO.Instance.GetChiTietNhapByMaPhieu(maPhieuNhap);
            FormatMoneyGrid(dgvChiTietNhap, new[] { "DonGia", "ThanhTien" });
        }


        void Load_Phieu_Nhap()
        {
            DateTime ngayDauThang = new DateTime(DateTime.Today.Year, DateTime.Today.Month, 1);
            DateTime ngayCuoiThang = ngayDauThang.AddMonths(1).AddDays(-1);

            dtpFrom.Value = ngayDauThang;
            dtpTo.Value = ngayCuoiThang;

            // 2. Load danh sách phiếu nhập (MASTER)
            dgvPhieuNhap.DataSource =
                PhieuNhapDAO.Instance.GetPhieuNhapTheoKhoangNgay(
                    dtpFrom.Value,
                    dtpTo.Value
                );

            FormatMoneyGrid(dgvPhieuNhap, new[] { "TongPhieu" });
            dgvPhieuNhap.Columns["MaPhieuNhap"].Visible = false;

            // 3. Auto load chi tiết của phiếu đầu tiên (DETAIL)
            if (dgvPhieuNhap.Rows.Count > 0 && !dgvPhieuNhap.Rows[0].IsNewRow)
            {
                int maPN = Convert.ToInt32(dgvPhieuNhap.Rows[0].Cells["MaPhieuNhap"].Value);
                LoadChiTiet(maPN);
            }
            else
            {
                dgvChiTietNhap.DataSource = null;
            }



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
                if (dtgvFood.SelectedCells.Count > 0 && dtgvFood.SelectedCells[0].OwningRow.Cells["Loại"].Value != null)
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
            }
            catch
            {
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

            string connectionString = "Data Source=DESKTOP-2PIF1AG\\SQLEXPRESS01;Initial Catalog=QuanLyQuanCoffe3;Integrated Security=True;Encrypt=False";



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
        void LoadNguyenLieu()
        {
            List<NguyenLieu> categories = PhieuNhapDAO.Instance.GetListNguyenLieu();
            cbNguyenlieu.DataSource = categories;
            cbNguyenlieu.DisplayMember = "Name";
        }
        public void ShowChiTietNhap(int idPhieuNhap)
        {
            dgvListNhapKho.Items.Clear();
            List<ChiTietNhap> list = ChiTietNhapDAO.Instance.GetListByPhieuNhap(idPhieuNhap);

            decimal totalPrice = 0;

            foreach (ChiTietNhap item in list)
            {
                // Tính thành tiền cho từng dòng
                decimal thanhTien = item.Quantity * item.Price;

                // Tạo dòng ListView
                ListViewItem lsvItem = new ListViewItem(item.TenNguyenLieu);
                lsvItem.SubItems.Add(item.Quantity.ToString("N0", new CultureInfo("vi-VN")));
                lsvItem.SubItems.Add(item.Price.ToString("N0", new CultureInfo("vi-VN")));
                lsvItem.SubItems.Add(thanhTien.ToString("N0", new CultureInfo("vi-VN")));
                // Cộng tổng
                totalPrice += thanhTien;

                dgvListNhapKho.Items.Add(lsvItem);
            }

            // Format tiền VNĐ
            CultureInfo culture = new CultureInfo("vi-VN");
            txbTotalImport.Text = totalPrice.ToString("c", culture); // hiển thị tổng tiền
        }

        private void UpdateTotal()
        {
            decimal total = 0;

            foreach (DataRow row in dtTempNhap.Rows)
            {
                total += (decimal)row["ThanhTien"];
            }

            txbTotalImport.Text = total.ToString();
        }

        private void ShowTempNhap()
        {
            dgvListNhapKho.Items.Clear();

            decimal tongTien = 0;

            foreach (DataRow row in dtTempNhap.Rows)
            {
                ListViewItem item = new ListViewItem(row["TenNL"].ToString());

                item.SubItems.Add(row["SoLuong"].ToString());
                item.SubItems.Add(row["DonGia"].ToString());
                item.SubItems.Add(row["ThanhTien"].ToString());

                dgvListNhapKho.Items.Add(item);

                tongTien += (decimal)row["ThanhTien"];
            }

            CultureInfo culture = new CultureInfo("vi-VN");
            txbTotalImport.Text = tongTien.ToString("c", culture);
        }


        private void btnAddNl_Click(object sender, EventArgs e)
        {
            if (cbNguyenlieu.SelectedItem == null)
            {
                MessageBox.Show("Vui lòng chọn nguyên liệu!");
                return;
            }

            NguyenLieu nl = cbNguyenlieu.SelectedItem as NguyenLieu;
            if (nl == null)
            {
                MessageBox.Show("Nguyên liệu không hợp lệ!");
                return;
            }

            decimal soLuongMoi = nmQuantity.Value;
            decimal donGiaMoi = nmPrice.Value;

            if (soLuongMoi <= 0 || donGiaMoi <= 0)
            {
                MessageBox.Show("Số lượng và đơn giá phải lớn hơn 0!");
                return;
            }

            bool daTonTai = false;

            foreach (DataRow row in dtTempNhap.Rows)
            {
                // So sánh tên nguyên liệu (có thể đổi sang ID nếu có)
                if (row["TenNL"].ToString() == nl.Name)
                {
                    decimal soLuongCu = Convert.ToDecimal(row["SoLuong"]);
                    decimal donGiaCu = Convert.ToDecimal(row["DonGia"]);

                    decimal soLuongMoiTong = soLuongCu + soLuongMoi;

                    // Cập nhật
                    row["SoLuong"] = soLuongMoiTong;

                    // Có thể giữ giá cũ hoặc lấy giá mới (tuỳ bạn)
                    row["DonGia"] = donGiaMoi;

                    row["ThanhTien"] = soLuongMoiTong * donGiaMoi;

                    daTonTai = true;
                    break;
                }
            }

            // Nếu chưa tồn tại → thêm mới
            if (!daTonTai)
            {
                decimal thanhTien = soLuongMoi * donGiaMoi;
                dtTempNhap.Rows.Add(
                    nl.Name,
                    soLuongMoi,
                    donGiaMoi,
                    thanhTien
                );
            }

            ShowTempNhap();
        }


        private void btnAdd_Click(object sender, EventArgs e)
        {
            string supplier = txbSupplier.Text;
            string raw = txbTotalImport.Text.Trim();

            // Xóa chữ "đ", "₫" và khoảng trắng
            raw = raw.Replace("đ", "")
                     .Replace("₫", "")
                     .Replace(" ", "");

            // Xóa dấu phẩy và dấu chấm nếu có
            raw = raw.Replace(",", "").Replace(".", "");

            decimal total;
            if (!decimal.TryParse(raw, out total))
            {
                MessageBox.Show("Giá trị tổng nhập không hợp lệ!");
                return;
            }
            int idAcc = AccountDAO.Instance.GetIDByUserName(loginAccount.Username);

            int idPhieu = PhieuNhapDAO.Instance.InsertImportReceipt(supplier, total, idAcc);

            foreach (DataRow row in dtTempNhap.Rows)
            {
                int idNL = NguyenLieuDAO.Instance.GetIdByName(row["TenNL"].ToString());
                decimal sl = (decimal)row["SoLuong"];
                decimal gia = (decimal)row["DonGia"];

                // lưu chi tiết nhập
                ChiTietNhapDAO.Instance.InsertChiTietNhap(idPhieu, idNL, sl, gia);

                // ✅ cộng tồn kho nguyên liệu
                NguyenLieuDAO.Instance.CongSoLuongTon(idNL, sl);
            }

            MessageBox.Show("Lưu phiếu nhập thành công!");
            dtTempNhap.Clear();           
            dgvListNhapKho.Items.Clear(); 

            // Reset TextBox
            txbSupplier.Text = "";
            txbTotalImport.Text = "0";    
            nmQuantity.Value = 1;         
            nmPrice.Value = 0;            

            cbNguyenlieu.SelectedIndex = -1; 
        }
        private void RecalculateTempTotal()
        {
            decimal total = 0;

            foreach (ListViewItem item in dgvListNhapKho.Items)
            {
                // Thành tiền nằm ở SubItem[3]
                decimal thanhTien = Decimal.Parse(item.SubItems[3].Text);
                total += thanhTien;
            }

            txbTotalImport.Text = total.ToString();
        }

        private void removeItemOfPhieu_Click(object sender, EventArgs e)
        {
            if (dgvListNhapKho.Items.Count == 0)
            {
                MessageBox.Show("Không có dữ liệu để xóa!");
                return;
            }

            if (dgvListNhapKho.SelectedItems.Count == 0)
            {
                MessageBox.Show("Vui lòng chọn 1 nguyên liệu để xóa!");
                return;
            }

            // Lấy item đang chọn
            ListViewItem selectedItem = dgvListNhapKho.SelectedItems[0];

            int index = dgvListNhapKho.SelectedItems[0].Index;

            dtTempNhap.Rows[index].Delete();

            dgvListNhapKho.Items.RemoveAt(index);

            RecalculateTempTotal();
        }

        private void thongke_Click(object sender, EventArgs e)
        {
            dgvPhieuNhap.DataSource =
             PhieuNhapDAO.Instance.GetPhieuNhapTheoKhoangNgay(dtpFrom.Value, dtpTo.Value);

            FormatMoneyGrid(dgvPhieuNhap, new[] { "TongPhieu" });

            // Auto load chi tiết của phiếu đầu tiên (nếu có)
            if (dgvPhieuNhap.Rows.Count > 0 && !dgvPhieuNhap.Rows[0].IsNewRow)
            {
                int maPN = Convert.ToInt32(dgvPhieuNhap.Rows[0].Cells["MaPhieuNhap"].Value);
                LoadChiTiet(maPN);
            }
            else
            {
                dgvChiTietNhap.DataSource = null;
            }
        }

        private void dgvPhieuNhap_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex < 0) return;

            int maPN = Convert.ToInt32(dgvPhieuNhap.Rows[e.RowIndex].Cells["MaPhieuNhap"].Value);
            LoadChiTiet(maPN);
        }

        private void xuatexcelSonPV_Click(object sender, EventArgs e)
        {
            if (dgvPhieuNhap.CurrentRow == null)
            {
                MessageBox.Show("Bạn hãy chọn 1 phiếu nhập để xuất!", "Thông báo",
                    MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            // Cột MaPhieuNhap có thể đang Visible=false nhưng vẫn lấy được
            int maPN = Convert.ToInt32(dgvPhieuNhap.CurrentRow.Cells["MaPhieuNhap"].Value);

            // 2) Lấy dữ liệu từ DB
            DataTable dtPhieu = PhieuNhapDAO.Instance.GetPhieuNhapById(maPN);
            DataTable dtCT = PhieuNhapDAO.Instance.GetChiTietNhapByPhieuId(maPN);

            if (dtPhieu.Rows.Count == 0)
            {
                MessageBox.Show("Không tìm thấy phiếu nhập!", "Lỗi",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            var r = dtPhieu.Rows[0];
            var ngayNhap = r["NgayNhap"];
            var nhaCC = r["NhaCungCap"]?.ToString();
            var nguoiNhap = r["NguoiNhap"]?.ToString();
            var tongPhieu = r["TongPhieu"];

            // 3) Chọn nơi lưu
            SaveFileDialog saveDialog = new SaveFileDialog();
            saveDialog.Filter = "Excel Files (*.xlsx)|*.xlsx|All Files (*.*)|*.*";
            saveDialog.FileName = $"PhieuNhap_{maPN}_{System.DateTime.Now:yyyyMMdd_HHmmss}.xlsx";

            if (saveDialog.ShowDialog() != DialogResult.OK) return;

            try
            {
                ExcelPackage.License.SetNonCommercialPersonal("fAdmin");

                using (var p = new ExcelPackage())
                {
                    var ws = p.Workbook.Worksheets.Add("PhieuNhap");

                    int row = 1;

                    // ===== TIÊU ĐỀ =====
                    ws.Cells[row, 1].Value = "PHIẾU NHẬP";
                    ws.Cells[row, 1, row, 4].Merge = true;
                    ws.Cells[row, 1].Style.Font.Bold = true;
                    ws.Cells[row, 1].Style.Font.Size = 16;
                    ws.Cells[row, 1].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
                    row += 2;

                    // ===== THÔNG TIN PHIẾU (KHÔNG HIỆN MÃ) =====
                    ws.Cells[row, 1].Value = "Ngày nhập:";
                    ws.Cells[row, 2].Value = ngayNhap;
                    ws.Cells[row, 2].Style.Numberformat.Format = "dd/MM/yyyy HH:mm";
                    row++;

                    ws.Cells[row, 1].Value = "Nhà cung cấp:";
                    ws.Cells[row, 2].Value = nhaCC;
                    row++;

                    ws.Cells[row, 1].Value = "Người nhập:";
                    ws.Cells[row, 2].Value = nguoiNhap;
                    row++;

                    ws.Cells[row, 1].Value = "Tổng phiếu:";
                    ws.Cells[row, 2].Value = Convert.ToDecimal(tongPhieu);
                    ws.Cells[row, 2].Style.Numberformat.Format = "#,##0";
                    ws.Cells[row, 2].Style.Font.Bold = true;
                    row += 2;

                    // Làm đậm label
                    ws.Cells[3, 1, row - 2, 1].Style.Font.Bold = true;

                    // ===== BẢNG CHI TIẾT =====
                    int startTableRow = row;

                    ws.Cells[row, 1].Value = "Tên nguyên liệu";
                    ws.Cells[row, 2].Value = "Số lượng";
                    ws.Cells[row, 3].Value = "Đơn giá";
                    ws.Cells[row, 4].Value = "Thành tiền";

                    using (var header = ws.Cells[row, 1, row, 4])
                    {
                        header.Style.Font.Bold = true;
                        header.Style.Fill.PatternType = ExcelFillStyle.Solid;
                        header.Style.Fill.BackgroundColor.SetColor(Color.LightGray);
                        header.Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
                    }

                    row++;

                    // Đổ chi tiết
                    foreach (DataRow dr in dtCT.Rows)
                    {
                        ws.Cells[row, 1].Value = dr["TenNguyenLieu"]?.ToString();
                        ws.Cells[row, 2].Value = Convert.ToDecimal(dr["SoLuong"]);
                        ws.Cells[row, 3].Value = Convert.ToDecimal(dr["DonGia"]);
                        ws.Cells[row, 4].Value = Convert.ToDecimal(dr["ThanhTien"]);

                        ws.Cells[row, 2].Style.Numberformat.Format = "#,##0";
                        ws.Cells[row, 3].Style.Numberformat.Format = "#,##0";
                        ws.Cells[row, 4].Style.Numberformat.Format = "#,##0";

                        ws.Cells[row, 2, row, 4].Style.HorizontalAlignment = ExcelHorizontalAlignment.Right;
                        row++;
                    }

                    // Tổng chi tiết (SUM)
                    ws.Cells[row + 1, 3].Value = "Tổng chi tiết:";
                    ws.Cells[row + 1, 3].Style.Font.Bold = true;
                    ws.Cells[row + 1, 3].Style.HorizontalAlignment = ExcelHorizontalAlignment.Right;

                    ws.Cells[row + 1, 4].Formula = $"SUM(D{startTableRow + 1}:D{row - 1})";
                    ws.Cells[row + 1, 4].Style.Numberformat.Format = "#,##0";
                    ws.Cells[row + 1, 4].Style.Font.Bold = true;
                    ws.Cells[row + 1, 4].Style.HorizontalAlignment = ExcelHorizontalAlignment.Right;

                    // AutoFit + Freeze header dòng bảng chi tiết
                    ws.Cells[ws.Dimension.Address].AutoFitColumns();
                    ws.View.FreezePanes(startTableRow + 1, 1);

                    p.SaveAs(new FileInfo(saveDialog.FileName));
                }

                MessageBox.Show("Xuất phiếu nhập thành công!", "Thông báo",
                    MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (System.Exception ex)
            {
                MessageBox.Show("Có lỗi khi xuất Excel: " + ex.Message, "Lỗi",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void dgvListNhapKho_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void listView1_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void button7_Click(object sender, EventArgs e)
        {
            LuuPhieuXuat();
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }
        private DataTable dtTempXuat;

        private void InitXuatKho()
        {
            dtTempXuat = new DataTable();
            dtTempXuat.Columns.Add("TenNguyenLieu", typeof(string));
            dtTempXuat.Columns.Add("DonVi", typeof(string));
            dtTempXuat.Columns.Add("SoLuong", typeof(decimal));

            // ====== ListView setup ======
            dgvXuatTam.View = View.Details;
            dgvXuatTam.FullRowSelect = true;
            dgvXuatTam.GridLines = true;
            dgvXuatTam.Columns.Clear();
            dgvXuatTam.Columns.Add("Tên nguyên liệu", 160);
            dgvXuatTam.Columns.Add("Đơn vị", 80);
            dgvXuatTam.Columns.Add("Số lượng", 80);
            dgvXuatTam.Items.Clear();

            // combobox nguyên liệu
            cbNguyenLieuXuat.DataSource = NguyenLieuDAO.Instance.GetListNguyenLieu();
            cbNguyenLieuXuat.DisplayMember = "Name";
            cbNguyenLieuXuat.ValueMember = "Id";
            cbNguyenLieuXuat.SelectedIndex = -1;

            nmSoLuongXuat.Value = 1;
            txbReasonXuat.Text = "";
        }
        private void RenderListViewXuatTam()
        {
            dgvXuatTam.Items.Clear();

            foreach (DataRow r in dtTempXuat.Rows)
            {
                var item = new ListViewItem(r["TenNguyenLieu"].ToString());
                item.SubItems.Add(r["DonVi"].ToString());
                item.SubItems.Add(Convert.ToDecimal(r["SoLuong"]).ToString("0.##"));
                dgvXuatTam.Items.Add(item);
            }
        }

        private void ThemXuatTam()
        {
            if (cbNguyenLieuXuat.SelectedIndex == -1)
            {
                MessageBox.Show("Bạn chưa chọn nguyên liệu!");
                return;
            }

            int idNL = Convert.ToInt32(cbNguyenLieuXuat.SelectedValue);
            string ten = cbNguyenLieuXuat.Text.Trim();

            decimal slXuat = nmSoLuongXuat.Value;
            if (slXuat <= 0)
            {
                MessageBox.Show("Số lượng phải > 0!");
                return;
            }

            // ✅ Lấy đơn vị từ DB
            string donVi = NguyenLieuDAO.Instance.GetDonViById(idNL);

            // check tồn kho
            decimal ton = NguyenLieuDAO.Instance.GetTonById(idNL);

            // tổng đã có trong dtTempXuat
            decimal dangCo = 0;
            foreach (DataRow r in dtTempXuat.Rows)
            {
                if (r["TenNguyenLieu"].ToString() == ten)
                    dangCo += Convert.ToDecimal(r["SoLuong"]);
            }

            if (dangCo + slXuat > ton)
            {
                MessageBox.Show($"Không đủ tồn kho!\nTồn: {ton}\nBạn muốn xuất: {dangCo + slXuat}");
                return;
            }

            // trùng thì cộng
            bool found = false;
            foreach (DataRow r in dtTempXuat.Rows)
            {
                if (r["TenNguyenLieu"].ToString() == ten)
                {
                    r["SoLuong"] = Convert.ToDecimal(r["SoLuong"]) + slXuat;
                    found = true;
                    break;
                }
            }

            if (!found)
                dtTempXuat.Rows.Add(ten, donVi, slXuat);

            RenderListViewXuatTam();
            nmSoLuongXuat.Value = 1;
        }


        private void btnThemNguyenLieu_Click(object sender, EventArgs e)
        {
            try
            {
                int idFood = Convert.ToInt32(txbFoodID.Text);

                int idIngredient = (int)cbNguyenLieu_ThucAn.SelectedValue;
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

        void LoadIngredientComboBox()
        {
            List<QuanlyquanCoffe.DTO.NguyenLieu> ingredientList = NguyenLieuDAO.Instance.GetListNguyenLieu();

            cbNguyenLieu_ThucAn.DataSource = ingredientList;

            // Bước 2: Dùng tên Thuộc tính (Property) của class DTO (viết hoa)
            cbNguyenLieu_ThucAn.DisplayMember = "Name"; // Thay vì "name"
            cbNguyenLieu_ThucAn.ValueMember = "ID";   // Thay vì "id"

            // Dùng dt này để cập nhật Label đơn vị
            cbNguyenLieu_ThucAn.Tag = ingredientList;
        }

        private void cbNguyenLieu_SelectedIndexChanged(object sender, EventArgs e)
        {
            // Cập nhật Label đơn vị
            if (cbNguyenLieu_ThucAn.SelectedItem != null)
            {
                QuanlyquanCoffe.DTO.NguyenLieu selectedIngredient = cbNguyenLieu_ThucAn.SelectedItem as QuanlyquanCoffe.DTO.NguyenLieu;
                string unit = selectedIngredient.Unit; // Đơn giản hơn code cũ rất nhiều!
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


        private void rdoRecipe_CheckedChanged(object sender, EventArgs e)
        {
            panelCongThuc.Visible = rdoRecipeYes.Checked;
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
    }

        private void XoaDongXuatTam()
        {
            if (dgvXuatTam.SelectedItems.Count == 0) return;

            int index = dgvXuatTam.SelectedItems[0].Index;
            if (index >= 0 && index < dtTempXuat.Rows.Count)
                dtTempXuat.Rows.RemoveAt(index);

            RenderListViewXuatTam();
        }

        private void btnThemXuat_Click(object sender, EventArgs e)
        {
            ThemXuatTam();
        }

        private void btnXoaXuat_Click(object sender, EventArgs e)
        {
            XoaDongXuatTam();
        }

        private void ResetXuatKhoTam()
        {
            // Xóa DataTable tạm
            dtTempXuat.Clear();

            // Xóa ListView
            dgvXuatTam.Items.Clear();

            // Reset control
            cbNguyenLieuXuat.SelectedIndex = -1;
            nmSoLuongXuat.Value = 1;
            txbReasonXuat.Text = "";
        }

        private void LuuPhieuXuat()
        {
            // 1. Kiểm tra bảng tạm
            if (dtTempXuat.Rows.Count == 0)
            {
                MessageBox.Show("Chưa có nguyên liệu nào để xuất!", "Thông báo");
                return;
            }

            // 2. Kiểm tra ghi chú (BẮT BUỘC)
            string reason = txbReasonXuat.Text.Trim();
            if (string.IsNullOrEmpty(reason))
            {
                MessageBox.Show("Vui lòng nhập ghi chú (lý do xuất kho)!", "Thiếu thông tin");
                txbReasonXuat.Focus();
                return;
            }

            int idAcc = AccountDAO.Instance.GetIDByUserName(loginAccount.Username);

            // 3. Tạo phiếu xuất
            int idPhieuXuat = PhieuXuatDAO.Instance.InsertPhieuXuat(reason, idAcc);

            // 4. Lưu chi tiết + trừ tồn kho
            foreach (DataRow r in dtTempXuat.Rows)
            {
                string ten = r["TenNguyenLieu"].ToString();
                decimal sl = Convert.ToDecimal(r["SoLuong"]);

                int idNL = NguyenLieuDAO.Instance.GetIdByName(ten);

                ChiTietXuatDAO.Instance.InsertChiTietXuat(idPhieuXuat, idNL, sl);
                NguyenLieuDAO.Instance.TruSoLuongTon(idNL, sl);
            }

            MessageBox.Show("Xuất kho thành công!", "Thông báo");


            ResetXuatKhoTam();
        }

        private void txbReasonXuat_TextChanged(object sender, EventArgs e)
        {
            txbReasonXuat.BackColor =
               string.IsNullOrWhiteSpace(txbReasonXuat.Text)
               ? Color.MistyRose
               : Color.White;
        }
        private void Load_Phieu_Xuat()
        {
            //dtgvPhieuXuat.DataSource = PhieuXuatDAO.Instance.LoadDanhSachPhieuXuat();
            //dtgvPhieuXuat.Columns["id"].Visible = false;
            //// 🔥 ĐỔI HEADER SAU KHI BIND DATA
            //dtgvPhieuXuat.Columns["NgayXuat"].HeaderText = "Ngày xuất";
            //dtgvPhieuXuat.Columns["LyDo"].HeaderText = "Lý do";
            //dtgvPhieuXuat.Columns["NguoiXuat"].HeaderText = "Người xuất";

            //// 🔥 ÉP THỨ TỰ CỘT
            //dtgvPhieuXuat.Columns["NgayXuat"].DisplayIndex = 0;
            //dtgvPhieuXuat.Columns["LyDo"].DisplayIndex = 1;
            //dtgvPhieuXuat.Columns["NguoiXuat"].DisplayIndex = 2;

            //dtgvPhieuXuat.RowHeadersVisible = true;
            //dtgvPhieuXuat.RowHeadersWidth = 30;

            var dt = PhieuXuatDAO.Instance.LoadDanhSachPhieuXuat();
            dtgvPhieuXuat.DataSource = dt;

            // bật row header
            dtgvPhieuXuat.RowHeadersVisible = true;
            dtgvPhieuXuat.RowHeadersWidth = 30;

            if (dtgvPhieuXuat.Columns == null || dtgvPhieuXuat.Columns.Count == 0)
                return;

            // Ẩn cột mã phiếu (nếu có)
            if (dtgvPhieuXuat.Columns["id"] != null)
                dtgvPhieuXuat.Columns["id"].Visible = false;

            // 🔥 LOAD CHI TIẾT PHIẾU ĐẦU TIÊN
            if (dtgvPhieuXuat.Rows.Count > 0)
            {
                dtgvPhieuXuat.Rows[0].Selected = true;
                int maPX = Convert.ToInt32(dtgvPhieuXuat.Rows[0].Cells[0].Value);

               


                LoadChiTietPhieuXuat(maPX);
            }
        }

        private void LoadChiTietPhieuXuat(int idPhieuXuat)
        {
            var dt = ChiTietXuatDAO.Instance.LoadChiTietTheoPhieuXuat(idPhieuXuat);

            // tránh lỗi khi dt null
            lvChiTietPhieuXuat.DataSource = dt;

            // nếu chưa có cột (dt null hoặc rỗng) thì vẫn bật row header rồi thoát
            lvChiTietPhieuXuat.RowHeadersVisible = true;
            lvChiTietPhieuXuat.RowHeadersWidth = 30;

            if (lvChiTietPhieuXuat.Columns == null || lvChiTietPhieuXuat.Columns.Count == 0)
                return;

            // 🔥 ĐỔI TÊN CỘT
            if (lvChiTietPhieuXuat.Columns["TenNguyenLieu"] != null)
                lvChiTietPhieuXuat.Columns["TenNguyenLieu"].HeaderText = "Nguyên liệu";

            if (lvChiTietPhieuXuat.Columns["DonVi"] != null)
                lvChiTietPhieuXuat.Columns["DonVi"].HeaderText = "Đơn vị";

            if (lvChiTietPhieuXuat.Columns["SoLuong"] != null)
                lvChiTietPhieuXuat.Columns["SoLuong"].HeaderText = "Số lượng";

            // 🔥 SẮP XẾP THỨ TỰ CỘT
            if (lvChiTietPhieuXuat.Columns["TenNguyenLieu"] != null)
                lvChiTietPhieuXuat.Columns["TenNguyenLieu"].DisplayIndex = 0;

            if (lvChiTietPhieuXuat.Columns["DonVi"] != null)
                lvChiTietPhieuXuat.Columns["DonVi"].DisplayIndex = 1;

            if (lvChiTietPhieuXuat.Columns["SoLuong"] != null)
                lvChiTietPhieuXuat.Columns["SoLuong"].DisplayIndex = 2;

            // 🔥 FORMAT SỐ LƯỢNG – BỎ .00
            if (lvChiTietPhieuXuat.Columns["SoLuong"] != null)
                lvChiTietPhieuXuat.Columns["SoLuong"].DefaultCellStyle.Format = "0.##";

            // (tuỳ chọn) canh giữa số lượng cho đẹp
            if (lvChiTietPhieuXuat.Columns["SoLuong"] != null)
                lvChiTietPhieuXuat.Columns["SoLuong"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
        }


        private void dataGridView2_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex < 0) return;
            int idPhieuXuat = Convert.ToInt32(dtgvPhieuXuat.Rows[e.RowIndex].Cells[0].Value);
            LoadChiTietPhieuXuat(idPhieuXuat);
            
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void label21_Click(object sender, EventArgs e)
        {

        }

        private void btnThongKeXuat_Click(object sender, EventArgs e)
        {
            DateTime from = dtpFromDateXuat.Value.Date;
            DateTime to = dtpToDateXuat.Value.Date;

            if (from > to)
            {
                MessageBox.Show("Ngày bắt đầu không được lớn hơn ngày kết thúc!");
                return;
            }

            // ✅ nếu bạn muốn lấy trọn ngày kết thúc (23:59:59)
            DateTime toEnd = to.AddDays(1).AddSeconds(-1);

            // 1) Load MASTER
            dtgvPhieuXuat.DataSource =
                PhieuXuatDAO.Instance.LoadPhieuXuatTheoKhoangNgay(from, toEnd);

            // 2) Ẩn cột mã phiếu (id)
            if (dtgvPhieuXuat.Columns["id"] != null)
                dtgvPhieuXuat.Columns["id"].Visible = false;

            // format cột date
            if (dtgvPhieuXuat.Columns["date"] != null)
                dtgvPhieuXuat.Columns["date"].DefaultCellStyle.Format = "dd/MM/yyyy HH:mm";

            // 3) Auto load DETAIL của dòng đầu tiên
            if (dtgvPhieuXuat.Rows.Count > 0 && !dtgvPhieuXuat.Rows[0].IsNewRow)
            {
                int maPX = Convert.ToInt32(dtgvPhieuXuat.Rows[0].Cells["id"].Value);
                LoadChiTietPhieuXuat(maPX);
            }
            else
            {
                lvChiTietPhieuXuat.DataSource = null;
            }
        }
    }
}
