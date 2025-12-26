using System;
using System.Collections.Generic;
using System.Data;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuanlyquanCoffe.DAO
{
    internal class ChiTietXuatDAO
    {
        private static ChiTietXuatDAO instance;
        public static ChiTietXuatDAO Instance
        {
            get
            {
                if (instance == null)
                    instance = new ChiTietXuatDAO();
                return instance;
            }
        }

        private ChiTietXuatDAO() { }

        // Thêm chi tiết xuất
        public void InsertChiTietXuat(int idPhieuXuat, int idNguyenLieu, decimal quantity)
        {
            string query = $@"
                INSERT INTO ChiTietXuat (idPhieuXuat, idNguyenLieu, quantity)
                VALUES (
                    {idPhieuXuat},
                    {idNguyenLieu},
                    {quantity.ToString(CultureInfo.InvariantCulture)}
                )
            ";

            Dataprovider.Instance.ExcuteNonQuery(query);
        }

        // Load chi tiết theo phiếu xuất (dùng cho xem chi tiết)
        public DataTable LoadChiTietXuatByPhieu(int idPhieuXuat)
        {
            string query = $@"
                SELECT 
                    nl.Name AS TenNguyenLieu,
                    ctx.quantity AS SoLuong
                FROM ChiTietXuat ctx
                JOIN NguyenLieu nl ON nl.id = ctx.idNguyenLieu
                WHERE ctx.idPhieuXuat = {idPhieuXuat}
            ";

            return Dataprovider.Instance.ExcuteQuery(query);
        }

        public DataTable LoadChiTietTheoPhieuXuat(int idPhieuXuat)
        {
            // ChiTietXuat(id, idPhieuXuat, idNguyenLieu, quantity)
            // Lấy DonVi từ cột Unit của bảng NguyenLieu
            string query = $@"
                SELECT
                    nl.Name AS TenNguyenLieu,
                    nl.Unit AS DonVi,
                    ctx.quantity AS SoLuong
                FROM ChiTietXuat ctx
                INNER JOIN NguyenLieu nl ON nl.id = ctx.idNguyenLieu
                WHERE ctx.idPhieuXuat = {idPhieuXuat}
            ";

            return Dataprovider.Instance.ExcuteQuery(query);
        }
    }
}
