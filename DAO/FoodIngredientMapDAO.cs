using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuanlyquanCoffe.DAO
{
    public class FoodIngredientMapDAO
    {
        private static FoodIngredientMapDAO instance;
        public static FoodIngredientMapDAO Instance
        {
            get { if (instance == null) instance = new FoodIngredientMapDAO(); return instance; }
            private set { instance = value; }
        }
        private FoodIngredientMapDAO() { }

        // Hàm lấy công thức của 1 món ăn
        public DataTable GetListIngredientByFoodID(int idFood)
        {
            string query =
                "SELECT i.name AS [Tên nguyên liệu], fim.amount AS [Định lượng], i.unit AS [Đơn vị], fim.idIngredient " +
                "FROM dbo.FoodIngredientMap AS fim " +
                "JOIN dbo.NguyenLieu AS i ON fim.idIngredient = i.id " +
                "WHERE fim.idFood = @idFood";

            // Dùng 'ExcuteQuery' (chữ c) như trong Dataprovider của bạn
            return Dataprovider.Instance.ExcuteQuery(query, new object[] { idFood });
        }

        // Hàm Thêm hoặc Cập nhật 1 nguyên liệu (cho nút +)
        public bool InsertOrUpdateIngredient(int idFood, int idIngredient, decimal amount)
        {
            // Chỉ cần gọi tên Thủ tục thay vì câu lệnh MERGE dài
            // Dataprovider của bạn sẽ thấy 3 tham số: @idFood, @idIngredient, @amount
            string query = "EXEC USP_InsertOrUpdateIngredient @idFood , @idIngredient , @amount";

            // Dataprovider sẽ gán 3 giá trị vào 3 tham số, không còn lỗi "Index outside"
            int result = Dataprovider.Instance.ExcuteNonQuery(query, new object[] { idFood, idIngredient, amount });

            return result > 0;
        }

        // Hàm Xóa 1 nguyên liệu (cho nút X)
        public bool DeleteIngredient(int idFood, int idIngredient)
        {
            string query = "DELETE dbo.FoodIngredientMap WHERE idFood = @idFood AND idIngredient = @idIngredient";
            int result = Dataprovider.Instance.ExcuteNonQuery(query, new object[] { idFood, idIngredient });
            return result > 0;
        }

        public bool DeleteByFoodID(int idFood)
        {
            // Lệnh SQL: Xóa tất cả các hàng trong FoodIngredientMap có idFood tương ứng
            string query = "DELETE FROM dbo.FoodIngredientMap WHERE idFood = @idFood";

            // Sử dụng Dataprovider để thực thi lệnh
            int result = Dataprovider.Instance.ExcuteNonQuery(query, new object[] { idFood });

            // Trả về true nếu có ít nhất 1 hàng bị ảnh hưởng, hoặc thành công (0 nếu không có)
            return result >= 0;
        }
    }
}
