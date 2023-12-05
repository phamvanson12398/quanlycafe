using QuanlyquanCoffe.DTO;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuanlyquanCoffe.DAO
{
  public class BillDAO
    {
       
        private static BillDAO instance;
        public static BillDAO Instance {
        get { if (instance == null) instance=new BillDAO(); return BillDAO.instance; }
          private  set { BillDAO.instance = value; }
        }
        public BillDAO() { }
        public int getUnCheckBillIDbyTableID(int id) {
            DataTable data = Dataprovider.Instance.ExcuteQuery("select * from Bill where idTable="+id+" and status=0");
            if(data.Rows.Count > 0)
            {
                Bill bill = new Bill(data.Rows[0]);
                return bill.Id;
            }
            return -1;
        }

    }
}
