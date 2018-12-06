using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

using SQLite;

using ServiceHelper.Exstension;
using ServiceHelper.Models;

namespace ServiceHelper.Forms
{
    /// <summary>
    /// Логика взаимодействия для OrderIssue.xaml
    /// </summary>
    public partial class OrderIssue : Window
    {
        private List<Button> tableObjectList;

        //private List<Table> tableList;
        //private List<Dish> dishList;

        private Order order;

        private bool isTableSelected;

        public OrderIssue()
        {
            InitializeComponent();
        }

        //public void InitializeDishOrder(List<Dish> dishList)
        //{
        //    this.dishList = dishList;
        //}

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            //Initialization new Order
            order = new Order();

            //Get link to tables on View
            Button[] buttons = new Button[8];
            tableInteractiveGrid.Children.CopyTo(buttons, 0);
            tableObjectList = buttons.ToList();

            foreach (var table in tableObjectList)
                table.Click += tableButton_Click;

            //Set current time
            reservationDatePicker.DisplayDateStart = DateTime.Now;
            reservationDatePicker.DisplayDate = DateTime.Now;
            reservationDatePicker.SelectedDate = DateTime.Now;

            UpdateTables();
        }

        private void CheckOrderedTable(object sender)
        {
            using(SQLiteConnection con = new SQLiteConnection(App.databasePath))
            {
                var orderedTableList = con.Table<Order>().ToList();
            }
        }

        private void backToMainMenuButton_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void tableButton_Click(object sender, RoutedEventArgs e)
        {
            for (int i = 0; i < tableObjectList.Count; i++)
            {
                if(tableObjectList[i].IsEnabled)
                    tableObjectList[i].Background = new SolidColorBrush(Color.FromArgb(255, 153, 177, 198));

                if (tableObjectList[i] == (sender as Button))
                {
                    order.TableNumber = i + 1;
                }
            }

            (sender as Button).Background = new SolidColorBrush(Color.FromArgb(255, 155, 250, 150));
            
            if (!isTableSelected)
                isTableSelected = true;
        }

        private void reservationButton_Click(object sender, RoutedEventArgs e)
        {
            if (!isTableSelected)
            {
                MessageBox.Show("Выберите столик!");
                return;
            }

            using (SQLiteConnection con = new SQLiteConnection(App.databasePath))
            {
                order.Date = reservationDatePicker.DisplayDate.ToString("dd/MM/yyyy");
                con.Insert(order, typeof(Order));

                orderIssueMainGridBlurEffect.Radius = 10;
                MessageBox.Show("Дата бронирования: " + order.Date + "\n" + "Столик № " + order.TableNumber);
                orderIssueMainGridBlurEffect.Radius = 0;
            }

            this.Close();
        }

        private void reservationDatePicker_CalendarClosed(object sender, RoutedEventArgs e)
        {
            UpdateTables();
        }

        private void UpdateTables()
        {
            string date = reservationDatePicker.SelectedDate.Value.ToString("dd/MM/yyyy");

            using (SQLiteConnection con = new SQLiteConnection(App.databasePath))
            {
                foreach (var table in tableObjectList)
                {
                    table.IsEnabled = true;
                    table.Background = new SolidColorBrush(Color.FromArgb(255, 153, 177, 198));
                }

                var tableNumberList = con.Table<Order>()
                                     .Where(x => x.Date.Equals(date))
                                     .Select(x => x.TableNumber).ToList();

                if (tableNumberList == null || tableNumberList.Count == 0)
                    return;

                foreach (var index in tableNumberList)
                {
                    tableObjectList[index - 1].IsEnabled = false;
                    tableObjectList[index - 1].Background = new SolidColorBrush(Color.FromArgb(255, 228, 100, 70));
                }

            }
        }
    }
}
