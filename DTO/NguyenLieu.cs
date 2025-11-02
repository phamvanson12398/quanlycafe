using System;
using System.Data;

namespace QuanlyquanCoffe.DTO
{
    public class NguyenLieu
    {
        public NguyenLieu() { }

        public NguyenLieu(int id, string name, string unit, decimal stock, string note)
        {
            this.ID = id;
            this.Name = name;
            this.Unit = unit;
            this.Stock = stock;
            this.Note = note;
        }

        public NguyenLieu(DataRow row)
        {
            this.ID = (int)row["id"];
            this.Name = row["name"].ToString();
            this.Unit = row["unit"].ToString();
            this.Stock = Convert.ToDecimal(row["stock"]);
            this.Note = row["note"].ToString();
        }

        private int iD;
        private string name;
        private string unit;
        private decimal stock;
        private string note;

        public int ID { get => iD; set => iD = value; }
        public string Name { get => name; set => name = value; }
        public string Unit { get => unit; set => unit = value; }
        public decimal Stock { get => stock; set => stock = value; }
        public string Note { get => note; set => note = value; }
    }
}
