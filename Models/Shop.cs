using System;
using System.Collections.Generic;
using System.Text;
using SQLite;

namespace DumitracheDanLab7.Models
{
    public class Shop
    {
        [PrimaryKey, AutoIncrement]
        public int ID { get; set; }
        public string ShopName { get; set; }
        public string Adress { get; set; }
        public string ShopDetails
        {
            get
            {
                return ShopName + " " + Adress;
            }
        }

        [Ignore]
        public List<ShopList> ShopLists { get; set; } = new List<ShopList>();
    }
}
