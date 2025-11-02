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

    }
}
