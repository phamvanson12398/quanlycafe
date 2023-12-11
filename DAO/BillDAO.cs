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
        public void CheckOut(int id,int discount,float totalprice)
        {
            string query = "update Bill set dateCheckOut=GETDATE(),status=1"+",discount="+discount+",totalPrice="+totalprice+" where id ="+id;
            Dataprovider.Instance.ExcuteNonQuery(query);
        }

        public void InsertBill(int id)
        {
            Dataprovider.Instance.ExcuteNonQuery("exec USP_InsertBill @idTable", new object[] { id });
        }
        public DataTable GetBillListByDate(DateTime checkin,DateTime checkout)
        {
            return Dataprovider.Instance.ExcuteQuery("exec USP_GetListBillByDate @checkin , @checkout",new object[] { checkin, checkout });
        }
        public int GetMaxIDBill()
        {
            try
            {
                return (int)Dataprovider.Instance.ExcuteScalar("SELECT MAX(id) FROM Bill");
            }
            catch
            {
                return 1;
            }
        }
    }
}
