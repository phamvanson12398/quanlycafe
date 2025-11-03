using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using QuanlyquanCoffe.DAO;
using System.Data;

namespace QuanlyquanCoffe.DAO
{
    public class IngredientDAO
    {
        private static IngredientDAO instance;
        public static IngredientDAO Instance
        {
            get { if (instance == null) instance = new IngredientDAO(); return instance; }
            private set { instance = value; }
        }
        private IngredientDAO() { }

        public DataTable GetListIngredient()
        {
            string query = "SELECT id, name, unit FROM dbo.Ingredient";
            return Dataprovider.Instance.ExcuteQuery(query);
        }
    }
}
