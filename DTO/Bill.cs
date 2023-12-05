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
        public Bill(int status,DateTime? dateCheckOut,DateTime? dateCheckIn,int id)
        {
            this.status = status;
            this.dateCheckOut=dateCheckOut;
            this.dateCheckIn = dateCheckIn;
            this.id = id;
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
        }
        private int status;
        private DateTime? dateCheckOut;
        private DateTime? dateCheckIn;
        private int id;

        public int Id { get => id; set => id = value; }
        public DateTime? DateCheckIn { get => dateCheckIn; set => dateCheckIn = value; }
        public DateTime? DateCheckOut { get => dateCheckOut; set => dateCheckOut = value; }
        public int Status { get => status; set => status = value; }
    }
}
