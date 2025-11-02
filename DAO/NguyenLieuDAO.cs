using QuanlyquanCoffe.DTO;
using System;
using System.Collections.Generic;
using System.Data;

namespace QuanlyquanCoffe.DAO
{
    public class NguyenLieuDAO
    {
        private static NguyenLieuDAO instance;

        public static NguyenLieuDAO Instance
        {
            get
            {
                if (instance == null) instance = new NguyenLieuDAO();
                return instance;
            }
            private set => instance = value;
        }

        private NguyenLieuDAO() { }

        // Lấy danh sách nguyên liệu
        public List<NguyenLieu> GetListNguyenLieu()
        {
            List<NguyenLieu> list = new List<NguyenLieu>();
            string query = "SELECT * FROM NguyenLieu";

            DataTable data = Dataprovider.Instance.ExcuteQuery(query);

            foreach (DataRow row in data.Rows)
            {
                NguyenLieu nl = new NguyenLieu(row);
                list.Add(nl);
            }

            return list;
        }

        // Thêm nguyên liệu
        public bool InsertNguyenLieu(string name, string unit, decimal stock, string note)
        {
            string query = "INSERT INTO NguyenLieu (name, unit, stock, note) VALUES ( @name , @unit , @stock , @note )";

            int result = Dataprovider.Instance.ExcuteNonQuery(query,
                new object[] { name, unit, stock, note });

            return result > 0;
        }

        // Cập nhật nguyên liệu
        public bool UpdateNguyenLieu(int id, string name, string unit, decimal stock, string note)
        {
            string query =
                "UPDATE NguyenLieu SET name = @name , unit = @unit , stock = @stock , note = @note WHERE id = @id";

            int result = Dataprovider.Instance.ExcuteNonQuery(query,
                new object[] { name, unit, stock, note, id });

            return result > 0;
        }

        // Xóa nguyên liệu
        public bool DeleteNguyenLieu(int id)
        {
            string query = "DELETE FROM NguyenLieu WHERE id = @id";

            int result = Dataprovider.Instance.ExcuteNonQuery(query,
                new object[] { id });

            return result > 0;
        }

        // Lấy nguyên liệu theo ID
        public NguyenLieu GetNguyenLieuById(int id)
        {
            string query = "SELECT * FROM NguyenLieu WHERE id = @id";

            DataTable data = Dataprovider.Instance.ExcuteQuery(query, new object[] { id });

            if (data.Rows.Count > 0)
            {
                return new NguyenLieu(data.Rows[0]);
            }

            return null;
        }
        public int GetIdByName(string name)
        {
            string query = "SELECT id FROM NguyenLieu WHERE name = @name";

            DataTable data = Dataprovider.Instance.ExcuteQuery(query, new object[] { name });

            if (data.Rows.Count > 0)
            {
                return Convert.ToInt32(data.Rows[0]["id"]);
            }

            return -1; // không tìm thấy
        }

    }
}
