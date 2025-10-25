using QuanlyquanCoffe.DTO;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuanlyquanCoffe.DAO
{
    public class CategoryDAO
    {
        private CategoryDAO() { }
        private static CategoryDAO instance;
        public static CategoryDAO Instance {
            get { if (instance == null) { instance = new CategoryDAO(); }; return instance; }
            private set { instance = value; }
         }
        public List<Category> GetListCategory()
        {
            List<Category> list = new List<Category>();
            string query="select * from FoodCategory";
            DataTable data = Dataprovider.Instance.ExcuteQuery(query);
            foreach (DataRow item in data.Rows)
            {
                Category category=new Category(item);
                list.Add(category);
            }
            return list;
        }
        public DataTable GetListCategoryFood()
        {
            return Dataprovider.Instance.ExcuteQuery("select id as N'Mã số',name as N'Tên danh mục' from FoodCategory");
        }
        public Category GetCategoryByID(int id)
        {
            Category category = new Category();
            string query = "select * from FoodCategory where id="+id;
            DataTable data = Dataprovider.Instance.ExcuteQuery(query);
            foreach (DataRow item in data.Rows)
            {
                category = new Category(item);
                return category;
            }
            return category;
        }
        public bool InsertCategory(string name)
        {
            string query = string.Format("INSERT FoodCategory(name) VALUES  ( N'{0}')", name);
            int result = Dataprovider.Instance.ExcuteNonQuery(query);

            return result > 0;
        }
        public bool UpdateCategory(string name, int id)
        {
            string query = string.Format("update FoodCategory  set name=N'{0}' where id={1}", name, id);
            int result = Dataprovider.Instance.ExcuteNonQuery(query);

            return result > 0;
        }
        public bool DeleteCategory(int idCategory)
        {

            FoodDAO.Instance.DeleteFoodByIDCategory(idCategory);
            string query = "Delete from FoodCategory where id=" + idCategory;
            int result = Dataprovider.Instance.ExcuteNonQuery(query);

            return result > 0;
        }
    }
}
