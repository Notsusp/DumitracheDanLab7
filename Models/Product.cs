using SQLite;
using System.Collections.Generic;

namespace DumitracheDanLab7.Models
{
    public class Product
    {
        [PrimaryKey, AutoIncrement]
        public int ID { get; set; }
        public string Description { get; set; }
        // Navigation property for related ListProduct entries (ignored by sqlite-net)
        [Ignore]
        public List<ListProduct> ListProducts { get; set; }
    }
}
