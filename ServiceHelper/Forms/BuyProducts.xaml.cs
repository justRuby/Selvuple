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
using System.Text.RegularExpressions;
using ServiceHelper.Exstension;

namespace ServiceHelper.Forms
{
    /// <summary>
    /// Логика взаимодействия для BuyProducts.xaml
    /// </summary>
    public partial class BuyProducts : Window
    {

        private PlaceholderController pController;

        public BuyProducts()
        {
            InitializeComponent();

            //Placeholder for textbox
            pController = new PlaceholderController();

            nameProductTextBox.GotFocus += pController.ShowPlaceholderText;
            nameProductTextBox.LostFocus += pController.HidePlaceholderText;

            countTextBox.GotFocus += pController.ShowPlaceholderText;
            countTextBox.LostFocus += pController.HidePlaceholderText;

            costTextBox.GotFocus += pController.ShowPlaceholderText;
            costTextBox.LostFocus += pController.HidePlaceholderText;
        }

        private void buyButton_Click(object sender, RoutedEventArgs e)
        {
            Products products = new Products();

            products.Name = nameProductTextBox.Text;
            products.Count = int.Parse(countTextBox.Text);
            products.Cost = int.Parse(costTextBox.Text);
            products.Date = DateTime.Now.ToString("dd/MM/yyyy");
            products.UserID = App.currentUser.UserID;

            using(SQLiteConnection con = new SQLiteConnection(App.databasePath))
            {
                ServiceHelper.Core.DataHandler.AddBenefit(products.Cost * products.Count);
                con.Insert(products, typeof(Products));
            }

            this.Close();
        }

        private void NumberValidationTextBox(object sender, TextCompositionEventArgs e)
        {
            Regex regex = new Regex("[^0-9]+");
            e.Handled = regex.IsMatch(e.Text);
        }

        private void cancelButton_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}
