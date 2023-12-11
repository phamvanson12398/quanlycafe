using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuanlyquanCoffe.DTO
{
    public class Food
    {
        public Food(int id,string name,float price,int category ) { 
            this.Name = name; 
            this.Price = price;
            this.ID = id;
            this.CategoryID = category;
        }
        public Food(DataRow row)
        {
            Name = row["name"].ToString();
            Price = (float)Convert.ToDouble(row["price"].ToString());
            ID = (int)row["id"];
            CategoryID = (int)row["idCategory"];
        }
        private Food() { }
        private int categoryID;
        private float price;
        private string name;
        private int iD;
        public int ID { get =>iD; set=> iD=value; }
        public string Name { get => name; set => name = value; }
        public float Price { get => price; set => price = value; }
        public int CategoryID { get => categoryID; set => categoryID = value; }
    }
}
