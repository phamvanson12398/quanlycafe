using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuanlyquanCoffe.DAO
{
    internal class PhieuXuatDAO
    {
        private static PhieuXuatDAO instance;
        public static PhieuXuatDAO Instance
        {
            get
            {
                if (instance == null) instance = new PhieuXuatDAO();
                return instance;
            }
        }

        private PhieuXuatDAO() { }

        // Tạo phiếu xuất -> trả về id phiếu xuất
        public int InsertPhieuXuat(string reason, int idAcc)
        {
            reason = (reason ?? "").Trim();
            if (reason.Length == 0) reason = "Xuất kho"; // mặc định nếu bỏ trống

            // escape dấu ' để không lỗi SQL
            reason = reason.Replace("'", "''");

            string query = $@"
                INSERT INTO PhieuXuat ([date], reason, idAcc)
                VALUES (GETDATE(), N'{reason}', {idAcc});
                SELECT SCOPE_IDENTITY();
            ";

            object result = Dataprovider.Instance.ExcuteScalar(query);
            return Convert.ToInt32(result);
        }

        // Load danh sách phiếu xuất (tuỳ bạn có cần)
        public DataTable LoadDanhSachPhieuXuat()
        {
            string query = @"
                SELECT 
                    px.id AS MaPhieuXuat,
                    px.[date] AS NgayXuat,
                    px.reason AS LyDo,
                    acc.DisplayName AS NguoiXuat
                FROM PhieuXuat px
                JOIN Account acc ON acc.id = px.idAcc
                ORDER BY px.[date] DESC, px.id DESC;
            ";

            return Dataprovider.Instance.ExcuteQuery(query);
        }

        public DataTable LoadPhieuXuatTheoKhoangNgay(DateTime dateFrom, DateTime dateTo)
        {
            string from = dateFrom.Date.ToString("yyyy-MM-dd 00:00:00");
            string to = dateTo.Date.ToString("yyyy-MM-dd 23:59:59");

            // ✅ dùng đúng: PhieuXuat(id, date, reason, idAcc)
            // ✅ join Account để lấy tên (đổi cột acc cho đúng nếu bạn khác)
            string query = $@"
                SELECT 
                    px.id,
                    px.date,
                    px.reason,
                    acc.DisplayName AS NguoiXuat
                FROM PhieuXuat px
                LEFT JOIN Account acc ON acc.id = px.idAcc
                WHERE px.date >= '{from}' AND px.date <= '{to}'
                ORDER BY px.date DESC
            ";

            return Dataprovider.Instance.ExcuteQuery(query);
        }

        // PhieuXuatDAO
        public DataTable GetPhieuXuatById(int maPX)
        {
            string query = @"
        SELECT 
            px.id AS MaPhieuXuat,
            px.date AS NgayXuat,
            a.UserName AS NguoiXuat,
            px.reason AS Reason
        FROM PhieuXuat px
        JOIN Account a ON a.id = px.idAcc
        WHERE px.id = @id";
            return Dataprovider.Instance.ExcuteQuery(query, new object[] { maPX });
        }

        // ChiTietPhieuXuatDAO
        public DataTable GetChiTietXuatByPhieuId(int maPX)
        {
            string query = @"
        SELECT 
            nl.Name AS TenNguyenLieu,
            ctx.quantity AS SoLuong
        FROM ChiTietXuat ctx
        JOIN NguyenLieu nl ON nl.id = ctx.idNguyenLieu
        WHERE ctx.idPhieuXuat = @id";
            return Dataprovider.Instance.ExcuteQuery(query, new object[] { maPX });
        }

    }
}
