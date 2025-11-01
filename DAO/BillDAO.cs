﻿using QuanlyquanCoffe.DTO;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

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
<<<<<<< HEAD
        public void DeleteBill(int idBill)
        {
            string query = "DELETE FROM Bill WHERE id = @idBill";
            Dataprovider.Instance.ExcuteNonQuery(query, new object[] { idBill });
        }

        public void CheckOut(int id, int discount, float totalprice)
        {
=======
        public void CheckOut(int id, int discount, float totalprice)
        {
>>>>>>> 295c355 (update: add fuction export file report)
            string query = "update Bill set dateCheckOut=GETDATE(),status=1" + ",discount=" + discount + ",totalPrice=" + totalprice + " where id =" + id;
            Dataprovider.Instance.ExcuteNonQuery(query);
        }

        public void InsertBill(int id)
        {
            Dataprovider.Instance.ExcuteNonQuery("exec USP_InsertBill @idTable", new object[] { id });
        }
        

        public DataTable GetBillListByDate(DateTime checkin,DateTime checkout)
        {
            if (checkin == checkout)
            {
                checkout = checkout.AddDays(1);
            }
            return Dataprovider.Instance.ExcuteQuery("exec USP_GetListBillByDate @checkin , @checkout",new object[] { checkin, checkout });
        }
        public DataTable GetBillListByDateAndPage(DateTime checkin, DateTime checkout,int pageNumber)
        {
            return Dataprovider.Instance.ExcuteQuery("exec USP_GetListBillByDateAndPage @checkin , @checkout , @page", new object[] { checkin, checkout,pageNumber });
        }
        public int GetNumBillListByDate(DateTime checkin, DateTime checkout)
        {
            return (int)Dataprovider.Instance.ExcuteScalar("exec USP_GetNumBillByDate @checkin , @checkout", new object[] { checkin, checkout });
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
