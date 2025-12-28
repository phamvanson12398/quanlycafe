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
        // Biến để lưu công thức tạm thời
        DataTable dtTempRecipe;
        public AddFormFood()
        {
            InitializeComponent();
            LoadCategoryIntoComboBox(cbFoodCategory);

            // 1. Khởi tạo DataTable cho công thức tạm
            dtTempRecipe = new DataTable();
            dtTempRecipe.Columns.Add("idIngredient", typeof(int));
            dtTempRecipe.Columns.Add("TenNguyenLieu", typeof(string));
            dtTempRecipe.Columns.Add("DinhLuong", typeof(decimal));
            dtTempRecipe.Columns.Add("DonVi", typeof(string));

            // 2. Gán dtgvCongThuc với DataTable này
            dtgvCongThuc.DataSource = dtTempRecipe;

            // (Tùy chỉnh cột cho đẹp - giống fAdmin)
            dtgvCongThuc.Columns["idIngredient"].Visible = false;
            dtgvCongThuc.Columns["TenNguyenLieu"].HeaderText = "Tên Nguyên Liệu";
            dtgvCongThuc.Columns["TenNguyenLieu"].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
            dtgvCongThuc.Columns["DinhLuong"].HeaderText = "Định Lượng";
            dtgvCongThuc.Columns["DinhLuong"].Width = 90;
            dtgvCongThuc.Columns["DonVi"].HeaderText = "Đơn Vị";
            dtgvCongThuc.Columns["DonVi"].Width = 50;

            // 3. Tải nguyên liệu vào ComboBox
            LoadIngredientComboBox(cbNguyenLieu);

            // 4. Mặc định chọn "Không"
            rdoRecipeNo.Checked = true;
            panelCongThuc.Visible = false;
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
                        try
                        {
                            // BƯỚC B: Kiểm tra xem có lưu công thức không
                            if (rdoRecipeYes.Checked && dtTempRecipe.Rows.Count > 0)
                            {
                                // BƯỚC C: Lấy ID của món ăn vừa tạo
                                // (Giả sử Dataprovider của bạn có ExcuteScalar và hỗ trợ tham số)
                                int newIdFood = (int)Dataprovider.Instance.ExcuteScalar("SELECT id FROM Food WHERE name = @name", new object[] { nameFood });

                                // BƯỚC D: Lặp qua công thức tạm và lưu
                                foreach (DataRow row in dtTempRecipe.Rows)
                                {
                                    int idIngredient = (int)row["idIngredient"];
                                    decimal amount = (decimal)row["DinhLuong"];

                                    // Gọi DAO để lưu vào FoodIngredientMap
                                    FoodIngredientMapDAO.Instance.InsertOrUpdateIngredient(newIdFood, idIngredient, amount);
                                }
                            }
                        }
                        catch (Exception ex)
                        {
                            MessageBox.Show("Thêm món ăn thành công, nhưng có lỗi khi lưu công thức: " + ex.Message);
                        }
                        if (insertFood != null)
                        {
                            insertFood(this,new EventArgs());
                        }
                        txbFoodName.Text = "";
                        LoadCategoryIntoComboBox(cbFoodCategory);
                        nmFoodPrice.Value = 0;
                        rdoRecipeNo.Checked = true;
                        panelCongThuc.Visible = false;
                        dtTempRecipe.Clear();
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


        private void label5_Click(object sender, EventArgs e)
        {

        }

        private void btnThemNguyenLieu_Click(object sender, EventArgs e)
        {
            if (cbNguyenLieu.SelectedItem == null)
            {
                MessageBox.Show("Vui lòng chọn một nguyên liệu.");
                return;
            }

            QuanlyquanCoffe.DTO.NguyenLieu selected = cbNguyenLieu.SelectedItem as QuanlyquanCoffe.DTO.NguyenLieu;
            decimal amount = nmDinhLuong.Value;

            if (amount <= 0)
            {
                MessageBox.Show("Định lượng phải lớn hơn 0.");
                return;
            }

            // (Tùy chọn: Kiểm tra xem nguyên liệu đã tồn tại trong dtTempRecipe chưa)

            // Thêm vào DataTable tạm
            dtTempRecipe.Rows.Add(selected.ID, selected.Name, amount, selected.Unit);

            // Reset
            nmDinhLuong.Value = 0;
        }

        private void dtgvCongThuc_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void rdoRecipe_CheckedChanged(object sender, EventArgs e)
        {
            panelCongThuc.Visible = rdoRecipeYes.Checked;
        }

        void LoadIngredientComboBox(ComboBox cb)
        {
            // Giả sử bạn đã có NguyenLieuDAO và DTO (từ các cuộc trò chuyện trước)
            List<QuanlyquanCoffe.DTO.NguyenLieu> ingredientList = NguyenLieuDAO.Instance.GetListNguyenLieu();
            cb.DataSource = ingredientList;
            cb.DisplayMember = "Name";
            cb.ValueMember = "ID";
        }

        private void cbNguyenLieu_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cbNguyenLieu.SelectedItem != null)
            {
                QuanlyquanCoffe.DTO.NguyenLieu selected = cbNguyenLieu.SelectedItem as QuanlyquanCoffe.DTO.NguyenLieu;
                lblDonVi.Text = $"({selected.Unit})";
            }
        }
    }
}
