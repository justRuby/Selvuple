using SQLite;

namespace ServiceHelper.Models
{
    public class Order 
    {
        [PrimaryKey, AutoIncrement]
        public int OrderID { get; set; }

        private int _tableNumber;
        private string _date;

        public int TableNumber
        {
            get
            {
                return _tableNumber;
            }
            set
            {
                _tableNumber = value;
               
            }
        }
        public string Date
        {
            get
            {
                return _date;
            }
            set
            {
                _date = value;
           
            }
        }

    }
}
