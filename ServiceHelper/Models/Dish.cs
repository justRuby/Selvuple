using SQLite;

namespace ServiceHelper.Models
{
    public class Dish 
    {
        [PrimaryKey, AutoIncrement]
        public int DishID { get; set; }

        private string _image;
        private string _name;
        private double _cost;
        private string _composition;
        private string _ingridients;
        private bool _access;

        public string Image
        {
            get
            {
                return _image;
            }
            set
            {
                _image = value;

            }
        }
        public string Name
        {
            get
            {
                return _name;
            }
            set
            {
                _name = value;

            }
        }
        public double Cost
        {
            get
            {
                return _cost;
            }
            set
            {
                _cost = value;

            }
        }
        public string Composition
        {
            get
            {
                return _composition;
            }
            set
            {
                _composition = value;

            }
        }
        public string Ingridients
        {
            get
            {
                return _ingridients;
            }
            set
            {
                _ingridients = value;

            }
        }
        public bool Access
        {
            get
            {
                return _access;
            }
            set
            {
                _access = value;
            }
        }

    }
}
