
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

using SQLite;
using ServiceHelper.Models;

namespace ServiceHelper.Forms
{
    /// <summary>
    /// Логика взаимодействия для StaffList.xaml
    /// </summary>
    public partial class StaffList : Window
    {
        public StaffList()
        {
            InitializeComponent();
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            using (SQLiteConnection con = new SQLiteConnection(App.databasePath))
            {
                var userList = con.Table<User>().ToList();
                staffDataGrid.ItemsSource = userList;
            }
        }
    }
}
