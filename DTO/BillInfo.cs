using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuanlyquanCoffe.DTO
{
    public class BillInfo
    {
        public BillInfo(int id,int idbill,int idfood,int count) {
            this.Count = count;
            this.Id = id;
            this.IdBill = idbill;
            this.IdFood = idfood;
        }
        public BillInfo(DataRow row)
        {
            this.Count = (int)row["count"];
            this.Id = (int)row["id"];
            this.IdBill = (int)row["idBill"];
            this.IdFood = (int)row["idFood"];
        }
        public BillInfo() { }
        private int id;
        private int idBill;
        private int idFood;
        private int count;

        public int Id { get => id; set => id = value; }
        public int IdBill { get => idBill; set => idBill = value; }
        public int IdFood { get => idFood; set => idFood = value; }
        public int Count { get => count; set => count = value; }
    }
}
