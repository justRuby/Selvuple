using SQLite;

namespace ServiceHelper.Models
{
    public class User
    {
        [PrimaryKey, AutoIncrement]
        public int UserID { get; set; }

        private string _login;
        private string _password;
        private int _level;
        private string _fullName;
        private string _adress;
        private string _passport;
        private string _phone;
        private int _cardLoyality;

        public string Login
        {
            get
            {
                return _login;
            }
            set
            {
                _login = value;
            }
        }
        public string Password
        {
            get
            {
                return _password;
            }
            set
            {
                _password = value;
            }
        }
        public int Level
        {
            get
            {
                return _level;
            }
            set
            {
                _level = value;
            }
        }
        public string FullName
        {
            get
            {
                return _fullName;
            }
            set
            {
                _fullName = value;
            }
        }
        public string Adress
        {
            get
            {
                return _adress;
            }
            set
            {
                _adress = value;
            }
        }
        public string Passport
        {
            get { return _passport; }
            set
            {
                _passport = value;
            }
        }
        public string Phone
        {
            get { return _phone; }
            set { _phone = value; }
        }
        public int CardLoyality
        {
            get { return _cardLoyality; }
            set { _cardLoyality = value; }
        }
    }
}
