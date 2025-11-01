using QuanlyquanCoffe.DTO;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace QuanlyquanCoffe.DAO
{
    public class BillInfoDAO
    {
        private static BillInfoDAO instance;
        public static BillInfoDAO Instance {  
            get { if (instance == null) instance = new BillInfoDAO(); return instance; } 
          private  set { instance = value; }
        }
        private BillInfoDAO() { }
        public void DeleteBillInfoByFoodID(int id)
        {
            Dataprovider.Instance.ExcuteQuery("delete BillInfo where idBill="+id);
        }
        public List<BillInfo> GetListBillInfos(int id) {
        List<BillInfo> listbillInfos = new List<BillInfo>();
            DataTable data = Dataprovider.Instance.ExcuteQuery("select * from BillInfo where idBill = "+id);
            foreach (DataRow item in data.Rows)
            {
                BillInfo billinfo = new BillInfo(item);
                listbillInfos.Add(billinfo);
            }
            return listbillInfos;
        
        }
        public void DeleteFoodFromBill(int idBill, int idFood)
        {
            string query = @"DELETE FROM BillInfo WHERE idBill = @idBill AND idFood = @idFood";

            Dataprovider.Instance.ExcuteNonQuery(query, new object[] { idBill, idFood });
        }

        public int GetCountItemInBill(int idBill)
        {
            string query = "SELECT COUNT(*) FROM BillInfo WHERE idBill = @idBill";
            object result = Dataprovider.Instance.ExcuteScalar(query, new object[] { idBill });

            return Convert.ToInt32(result);
        }
        public void DeleteBillInfoByBillId(int idBill)
        {
            string query = "DELETE FROM BillInfo WHERE idBill = @idBill";
            Dataprovider.Instance.ExcuteNonQuery(query, new object[] { idBill });
        }


        public void InsertBillInfo(int idBill,int idFood,int count)
        {
            Dataprovider.Instance.ExcuteNonQuery("exec USP_InsertBillInfo @idBill , @idFood , @count", new object[] { idBill,idFood,count });
        }
     
    }
}
