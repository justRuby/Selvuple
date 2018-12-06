﻿using System;
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

namespace ServiceHelper.Forms
{
    /// <summary>
    /// Логика взаимодействия для Profit.xaml
    /// </summary>
    public partial class Profit : Window
    {
        public Profit()
        {
            InitializeComponent();
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            using (SQLiteConnection con = new SQLiteConnection(App.databasePath))
            {
                var profitList = con.Table<Models.Profit>().ToList();
                profitDataGrid.ItemsSource = profitList;
            }
        }
    }
}
