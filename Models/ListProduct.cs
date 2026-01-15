using SQLite;

namespace DumitracheDanLab7.Models
{
    public class ListProduct
    {
        [PrimaryKey, AutoIncrement]
        public int ID { get; set; }
        public int ShopListID { get; set; }
        public int ProductID { get; set; }
    }
}
