using SQLite;
using System;
using System.Collections.Generic;
using System.Text;

namespace DumitracheDanLab7.Models
{
    public class ShopList
    {
        [PrimaryKey, AutoIncrement]
        public int ID { get; set; }
        [MaxLength(250), Unique]
        public string Description { get; set; }
        public DateTime Date { get; set; }

        public int ShopID { get; set; }
    }
}
