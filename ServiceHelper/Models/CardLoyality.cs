using SQLite;

namespace ServiceHelper.Models
{
    public class CardLoyality
    {
        [PrimaryKey, AutoIncrement]
        public int CardLoyalityID { get; set; }

        private int _score;

        public int Score
        {
            get
            {
                return _score;
            }
            set
            {
                _score = value;
            }
        }
    }
}
