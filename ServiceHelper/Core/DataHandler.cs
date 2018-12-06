using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


using SQLite;
using ServiceHelper.Models;

namespace ServiceHelper.Core
{
    public static class DataHandler
    {
        private static Profit profit;

        public static void AddIncome(double cost)
        {
            using (SQLiteConnection con = new SQLiteConnection(App.databasePath))
            {
                if (CheckProfit(con))
                {
                    profit.Income += cost;
                    con.Update(profit, typeof(Profit));
                }
                else
                {
                    profit = new Profit();
                    profit.Date = DateTime.Now.ToString("dd/MM/yyyy");

                    profit.Income += cost;
                    con.Insert(profit, typeof(Profit));
                }
            }
        }

        public static void AddBenefit(double cost)
        {
            using (SQLiteConnection con = new SQLiteConnection(App.databasePath))
            {
                if (CheckProfit(con))
                {
                    profit.Expense += cost;
                    con.Update(profit, typeof(Profit));
                }
                else
                {
                    profit = new Profit();
                    profit.Date = DateTime.Now.ToString("dd/MM/yyyy");

                    profit.Expense += cost;
                    con.Insert(profit, typeof(Profit));
                }
            }
        }

        private static bool CheckProfit(SQLiteConnection con)
        {
            string date = DateTime.Now.ToString("dd/MM/yyyy");
            var search = con.Table<Profit>().Where(x => x.Date == date);
            bool isFinded = false;

            foreach (var item in search)
            {
                profit = item;
                isFinded = true;
                break;
            }

            return isFinded;
        }

    }
}
