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
    }
}
