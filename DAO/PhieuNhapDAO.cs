using QuanlyquanCoffe.DTO;
using System;
using System.Collections.Generic;
using System.Data;

namespace QuanlyquanCoffe.DAO
{
    internal class PhieuNhapDAO
    {
        private static PhieuNhapDAO instance;

        public static PhieuNhapDAO Instance
        {
            get
            {
                if (instance == null) instance = new PhieuNhapDAO();
                return instance;
            }
            private set => instance = value;
        }

        private PhieuNhapDAO() { }

        // Thêm phiếu nhập
        public int InsertImportReceipt(string supplier, decimal total, int idAcc)
        {
            string query = "EXEC USP_InsertPhieuNhap @supplier , @total , @idAcc";

            object result = Dataprovider.Instance.ExcuteScalar(
                query,
                new object[] { supplier, total, idAcc }
            );

            return Convert.ToInt32(result); // trả về ID mới tạo
        }
        public List<NguyenLieu> GetListNguyenLieu()
        {
            List<NguyenLieu> list = new List<NguyenLieu>();

            string query = "SELECT * FROM NguyenLieu";
            DataTable data = Dataprovider.Instance.ExcuteQuery(query);

            foreach (DataRow item in data.Rows)
            {
                NguyenLieu nl = new NguyenLieu(item);
                list.Add(nl);
            }

            return list;
        }

        public DataTable LoadPhieuNhapChiTiet_Chung1Bang()
        {
            string query = @"
                SELECT
                    pn.id           AS MaPhieuNhap,
                    pn.[date]       AS NgayNhap,
                    pn.supplier     AS NhaCungCap,

                    acc.DisplayName AS NguoiNhap,

                    nl.Name         AS TenNguyenLieu,
                    ct.quantity     AS SoLuong,
                    ct.price        AS DonGia,
                    (ct.quantity * ct.price) AS ThanhTien,

                    pn.total        AS TongPhieu
                FROM PhieuNhap pn
                JOIN ChiTietNhap ct ON ct.idPhieuNhap = pn.id
                JOIN NguyenLieu nl ON nl.id = ct.idNguyenLieu
                JOIN Account acc ON acc.id = pn.idAcc
                ORDER BY pn.[date] DESC, pn.id DESC;
            ";

            return Dataprovider.Instance.ExcuteQuery(query);
        }

        public DataTable GetPhieuNhapTheoKhoangNgay(DateTime ngayBatDau, DateTime ngayKetThuc)
        {
            DateTime from = ngayBatDau.Date;
            DateTime to = ngayKetThuc.Date.AddDays(1).AddTicks(-1);

            string fromStr = from.ToString("yyyy-MM-dd HH:mm:ss");
            string toStr = to.ToString("yyyy-MM-dd HH:mm:ss");

            string query = $@"
                SELECT
                    pn.id           AS MaPhieuNhap,
                    pn.[date]       AS NgayNhap,
                    pn.supplier     AS NhaCungCap,
                    acc.DisplayName AS NguoiNhap,
                    pn.total        AS TongPhieu
                FROM PhieuNhap pn
                JOIN Account acc ON acc.id = pn.idAcc
                WHERE pn.[date] >= '{fromStr}' AND pn.[date] <= '{toStr}'
                ORDER BY pn.[date] DESC, pn.id DESC;
            ";

            return Dataprovider.Instance.ExcuteQuery(query);
        }

        public DataTable GetChiTietNhapByMaPhieu(int maPhieuNhap)
        {
            string query = $@"
                SELECT
                    nl.Name                      AS TenNguyenLieu,
                    ct.quantity                  AS SoLuong,
                    ct.price                     AS DonGia,
                    (ct.quantity * ct.price)     AS ThanhTien
                FROM ChiTietNhap ct
                JOIN NguyenLieu nl ON nl.id = ct.idNguyenLieu
                WHERE ct.idPhieuNhap = {maPhieuNhap}
                ORDER BY ct.id ASC;
            ";

            return Dataprovider.Instance.ExcuteQuery(query);
        }

        

        public DataTable GetPhieuNhapById(int maPhieuNhap)
        {
            string query = $@"
                SELECT
                    pn.id           AS MaPhieuNhap,
                    pn.[date]       AS NgayNhap,
                    pn.supplier     AS NhaCungCap,
                    acc.DisplayName AS NguoiNhap,
                    pn.total        AS TongPhieu
                FROM PhieuNhap pn
                JOIN Account acc ON acc.id = pn.idAcc
                WHERE pn.id = {maPhieuNhap};
            ";
            return Dataprovider.Instance.ExcuteQuery(query);
        }

        public DataTable GetChiTietNhapByPhieuId(int maPhieuNhap)
        {
            string query = $@"
                SELECT
                    nl.Name                  AS TenNguyenLieu,
                    ct.quantity              AS SoLuong,
                    ct.price                 AS DonGia,
                    (ct.quantity * ct.price) AS ThanhTien
                FROM ChiTietNhap ct
                JOIN NguyenLieu nl ON nl.id = ct.idNguyenLieu
                WHERE ct.idPhieuNhap = {maPhieuNhap}
                ORDER BY ct.id ASC;
            ";
            return Dataprovider.Instance.ExcuteQuery(query);
        }



    }
}
