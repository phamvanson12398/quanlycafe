using QuanlyquanCoffe.DTO;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuanlyquanCoffe.DAO
{
    public class FoodDAO
    {
        private static FoodDAO instance;
        public static FoodDAO Instance {  
            get { if (instance == null) instance = new FoodDAO() ; return instance; } 
          private  set { instance = value; }
        }
        public List<Food> GetListFoodByCategoryID(int id)
        {
            List<Food> list = new List<Food>();
            string query = "select * from Food where idCategory = "+id;
            DataTable data=Dataprovider.Instance.ExcuteQuery(query);
            foreach (DataRow item in data.Rows)
            {
                Food food = new Food(item);
                list.Add(food);
            }
            return list;
        }
        public List<Food> GetListFood()
        {
            List<Food> list = new List<Food>();

            string query = "select * from Food";

            DataTable data = Dataprovider.Instance.ExcuteQuery(query);

            foreach (DataRow item in data.Rows)
            {
                Food food = new Food(item);
                list.Add(food);
            }

            return list;
        }
        public DataTable GetListFood1()
        {
            string query = "select id as N'ID' , name as N'Tên' , price as N'Giá' , idCategory as N'Loại' from Food";
            return Dataprovider.Instance.ExcuteQuery(query);
        }
        public List<Food> SearchFoodbyName(string name) {
            List<Food> list = new List<Food>();
            string query =string.Format("SELECT * FROM dbo.Food WHERE dbo.fuConvertToUnsign1(name) LIKE N'%' + dbo.fuConvertToUnsign1(N'{0}') + '%'", name);
            DataTable data = Dataprovider.Instance.ExcuteQuery(query);
            foreach (DataRow item in data.Rows)
            {
                Food food = new Food(item);
                list.Add(food);
            }
            return list;
        }
        public bool InsertFood(string name, int id, float price)
        {
            string query = string.Format("INSERT dbo.Food ( name, idCategory, price ) VALUES  ( N'{0}', {1}, {2})", name, id, price);
            int result = Dataprovider.Instance.ExcuteNonQuery(query);

            return result > 0;
        }
        public bool UpdateFood(int idFood,string name, int id, float price)
        {
            string query = string.Format("update dbo.Food  set name=N'{0}',idCategory={1},price={2} where id={3}", name, id, price,idFood);
            int result = Dataprovider.Instance.ExcuteNonQuery(query);

            return result > 0;
        }
        public bool DeleteFood(int idFood)
        {
            BillInfoDAO.Instance.DeleteBillInfoByFoodID(idFood);
            string query = "Delete Food where id="+idFood;
            int result = Dataprovider.Instance.ExcuteNonQuery(query);

            return result > 0;
        }
        public void DeleteFoodByIDCategory(int id)
        {
            Dataprovider.Instance.ExcuteQuery(string.Format("delete from BillInfo where idFood=(select id from Food where idCategory={0})",id));
            Dataprovider.Instance.ExcuteQuery("Delete from Food where idCategory="+id);
        }
    }
}
