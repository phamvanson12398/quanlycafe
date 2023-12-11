using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuanlyquanCoffe.DTO
{
    public class Bill
    {
        public Bill(int status,DateTime? dateCheckOut,DateTime? dateCheckIn,int id,int discount)
        {
            this.Status = status;
            this.DateCheckOut=dateCheckOut;
            this.DateCheckIn = dateCheckIn;
            this.Id = id;
            this.Discount = discount;
        }
        public Bill(DataRow row)
        {
            var dateCheckOutTemp = row["DateCheckOut"];
            if(dateCheckOutTemp.ToString() != "")
            {this.DateCheckOut=(DateTime?)dateCheckOutTemp;
            }
            this.DateCheckIn = (DateTime?)row["DateCheckIn"];
            this.Id = (int)row["id"];
            this.Status = (int)row["status"];
            if (row["discount"].ToString() != "")
            {
                this.Discount = (int)row["discount"];
            }
        }
        private int discount;
        private int status;
        private DateTime? dateCheckOut;
        private DateTime? dateCheckIn;
        private int id;

        public int Id { get => id; set => id = value; }
        public DateTime? DateCheckIn { get => dateCheckIn; set => dateCheckIn = value; }
        public DateTime? DateCheckOut { get => dateCheckOut; set => dateCheckOut = value; }
        public int Status { get => status; set => status = value; }
        public int Discount { get => discount; set => discount = value; }
    }
}
