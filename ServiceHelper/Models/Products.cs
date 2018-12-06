
using SQLite;

namespace ServiceHelper.Models
{
    public class Products
    {
        [PrimaryKey, AutoIncrement]
        public int ProductsID { get; set; }

        public string Name { get; set; }
        public double Cost { get; set; }
        public int Count { get; set; }
        public string Date { get; set; }
        public int UserID { get; set; }
    }
}
