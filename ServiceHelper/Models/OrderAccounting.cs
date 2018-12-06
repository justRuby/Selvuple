using SQLite;

namespace ServiceHelper.Models
{
    public class OrderAccounting 
    {
        [PrimaryKey, AutoIncrement]
        public int OrderAccountingID { get; set; }

        private int _tableNumber;
        private string _dish;
        private string _date;
        private double _cost;
        private bool _isActive;

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
        public string Dish
        {
            get
            {
                return _dish;
            }
            set
            {
                _dish = value;
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
        public double Cost
        {
            get { return _cost; }
            set { _cost = value; }
        }
        public bool IsActive
        {
            get { return _isActive; }
            set { _isActive = value; }
        }

    }
}
