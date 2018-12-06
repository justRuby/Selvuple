using SQLite;

namespace ServiceHelper.Models
{
    public class Profit
    {
        [PrimaryKey, AutoIncrement]
        public int ProfitID { get; set; }

        private double _income;
        private double _expense;
        private double _benefit;
        private string _date;

        public double Income
        {
            get
            {
                return _income;
            }
            set
            {
                _income = value;
            }
        }
        public double Expense
        {
            get
            {
                return _expense;
            }
            set
            {
                _expense = value;
            }
        }
        public double Benefit
        {
            get
            {
                return _income + (-_expense);
            }
            set
            {
                _benefit = value;
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
