using QuanlyquanCoffe.DTO;
using System;
using System.Collections.Generic;
using System.Data;

namespace QuanlyquanCoffe.DAO
{
    internal class ChiTietNhapDAO
    {
        private static ChiTietNhapDAO instance;

        public static ChiTietNhapDAO Instance
        {
            get
            {
                if (instance == null) instance = new ChiTietNhapDAO();
                return instance;
            }
            private set => instance = value;
        }

        private ChiTietNhapDAO() { }

        // Thêm chi tiết nhập
        public int InsertChiTietNhap(int idPhieuNhap, int idNguyenLieu, decimal quantity, decimal price)
        {
            string query = "EXEC USP_InsertChiTietNhap @idPhieuNhap , @idNguyenLieu , @quantity , @price";

            object result = Dataprovider.Instance.ExcuteScalar(
                query,
                new object[] { idPhieuNhap, idNguyenLieu, quantity, price }
            );

            return Convert.ToInt32(result); 
        }
        public List<ChiTietNhap> GetListByPhieuNhap(int idPhieuNhap)
        {
            List<ChiTietNhap> list = new List<ChiTietNhap>();

            string query = @"
                SELECT ctn.*, nl.name AS TenNguyenLieu
                FROM ChiTietNhap ctn
                INNER JOIN NguyenLieu nl ON nl.id = ctn.idNguyenLieu
                WHERE ctn.idPhieuNhap = " + idPhieuNhap;

            DataTable data = Dataprovider.Instance.ExcuteQuery(query);

            foreach (DataRow row in data.Rows)
            {
                ChiTietNhap item = new ChiTietNhap(row);
                item.TenNguyenLieu = row["TenNguyenLieu"].ToString();
                list.Add(item);
            }

            return list;
        }

    }
}
