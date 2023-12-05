using QuanlyquanCoffe.DTO;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
    }
}
