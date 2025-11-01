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
using static System.Windows.Forms.LinkLabel;

namespace QuanlyquanCoffe
{
    public partial class FormCheckOut : Form
    {
        //  public List<QuanlyquanCoffe.DTO.Menu> list_checkout = new List<QuanlyquanCoffe.DTO.Menu>();
        public int current_id_bill;
        public string table_name;
        private double discount;
        private double bill_total;
        public int table_id;
        private fTableManager parentForm;
        public FormCheckOut(List<QuanlyquanCoffe.DTO.Menu> listBillInfo,double num_discount,fTableManager f)
        {
            InitializeComponent();
            discount = num_discount;
            parentForm= f;
            Begin_Component(listBillInfo,num_discount);
        }

        private void richTextBox1_TextChanged(object sender, EventArgs e)
        {

        }
        private void Begin_Component(List<QuanlyquanCoffe.DTO.Menu> list_checkout, double num_discount)
        {
            txbCheckOut.ReadOnly = true;

            // Tiêu đề hóa đơn
            string header = "Tên món         Số lượng    Giá\n";
            header += "-------------------------------------\n";
            txbCheckOut.Text = header;

            double totalPrice = 0;

            // Hiển thị từng món ăn
            foreach (QuanlyquanCoffe.DTO.Menu item in list_checkout)
            {
                addToBill(item.FoodName, item.Count, item.Price);
                totalPrice += item.TotalPrice;
            }

            // Hiển thị tổng tiền
            addTotal(totalPrice, num_discount);
        }

        public void addToBill(string itemName, int quantity, double price)
        {
            // Định dạng mỗi dòng theo cột
            string line = string.Format("{0,-15} {1,-8} {2,-10:F2}đ\n", itemName, quantity, price);
            txbCheckOut.AppendText(line);
        }

        public void addTotal(double total,double discount)
        {
            // Hiển thị dòng tổng tiền
            string totalLine = string.Format("-------------------------------------\nTổng tiền: {0,30:F2}đ\n", total);
            txbCheckOut.AppendText(totalLine);
        string discount_line = string.Format("                                     Giảm giá: {0,30:F2}%\n", discount);
            txbCheckOut.AppendText(discount_line);
            bill_total = (total * (1 - discount / 100));
            string total_final = string.Format("                                     Tổng tiền(Cuối): {0,30:F2}đ\n", bill_total);
            txbCheckOut.AppendText(total_final);
        }

        private void btnCheckOut_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show(string.Format("Bạn có chắc muốn thanh toán hóa đơn cho {0} không ?", table_name), "Thông báo", MessageBoxButtons.OKCancel) == System.Windows.Forms.DialogResult.OK)
            {
                BillDAO.Instance.CheckOut(current_id_bill, (int)discount, (float)bill_total);
                if (MessageBox.Show("Thanh toán thành công")== System.Windows.Forms.DialogResult.OK) {
                    parentForm.ShowBill(table_id);
                    parentForm.LoadTable();
                    this.Close();
                }
               
            }
        }
    }
}
