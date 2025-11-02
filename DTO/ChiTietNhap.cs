using System;
using System.Data;

namespace QuanlyquanCoffe.DTO
{
    public class ChiTietNhap
    {
        public ChiTietNhap() { }

        public ChiTietNhap(int id, int idPhieuNhap, int idNguyenLieu, decimal quantity, decimal price)
        {
            this.ID = id;
            this.IdPhieuNhap = idPhieuNhap;
            this.IdNguyenLieu = idNguyenLieu;
            this.Quantity = quantity;
            this.Price = price;
        }

        public ChiTietNhap(DataRow row)
        {
            this.ID = (int)row["id"];
            this.IdPhieuNhap = (int)row["idPhieuNhap"];
            this.IdNguyenLieu = (int)row["idNguyenLieu"];
            this.Quantity = Convert.ToDecimal(row["quantity"]);
            this.Price = Convert.ToDecimal(row["price"]);
            if (row.Table.Columns.Contains("TenNguyenLieu") && row["TenNguyenLieu"] != DBNull.Value)
            {
                this.TenNguyenLieu = row["TenNguyenLieu"].ToString();
            }
        }

        public int ID { get; set; }
        public int IdPhieuNhap { get; set; }
        public int IdNguyenLieu { get; set; }
        public decimal Quantity { get; set; }
        public decimal Price { get; set; }
        public string TenNguyenLieu { get; set; }
    }
}
