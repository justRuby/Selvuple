using ServiceHelper.Models;
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

namespace ServiceHelper.Forms
{
    /// <summary>
    /// Логика взаимодействия для Menu.xaml
    /// </summary>
    public partial class Menu : Window
    {
        private bool GlobalInitialization;

        public Menu()
        {
            InitializeComponent();
        }

        private void menuButton_Click(object sender, RoutedEventArgs e)
        {
            if (GlobalInitialization)
            {
                DishesMenu dishesMenu = new DishesMenu();
                this.Hide();
                dishesMenu.ShowDialog();
                this.Show();
            }
        }

        private void reservationButton_Click(object sender, RoutedEventArgs e)
        {
            if(GlobalInitialization)
            {
                OrderIssue orderIssue = new OrderIssue();
                this.Hide();
                orderIssue.ShowDialog();
                this.Show();
            }
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            if(!GlobalInitialization)
            {
                Authorization auth = new Authorization();
                BlurEffectMainGrid.Radius = 5;
                auth.ShowDialog();
                BlurEffectMainGrid.Radius = 0;
            }

            UpdateView();
        }

        private void UpdateView()
        {
            switch (App.currentUser.Level)
            {
                case 1:
                    currentAccountNameTextBlock.Text = "Аккаунт: Посетитель";
                    orderButton.Visibility = Visibility.Collapsed;
                    adminGrid.Visibility = Visibility.Hidden;

                    break;

                case 2:
                    currentAccountNameTextBlock.Text = "Аккаунт: Официант - " + App.currentUser.FullName;

                    orderButton.Visibility = Visibility.Visible;
                    adminGrid.Visibility = Visibility.Visible;
                    AdminMenuItem.Visibility = Visibility.Hidden;

                    break;

                case 3:
                    currentAccountNameTextBlock.Text = "Аккаунт: Админ - " + App.currentUser.FullName;

                    orderButton.Visibility = Visibility.Visible;
                    adminGrid.Visibility = Visibility.Visible;
                    AdminMenuItem.Visibility = Visibility.Visible;

                    break;

                default:
                    break;
            }
        }

        private void autorizationMenuItem_Click(object sender, RoutedEventArgs e)
        {
            Authorization auth = new Authorization();
            BlurEffectMainGrid.Radius = 5;
            auth.ShowDialog();
            BlurEffectMainGrid.Radius = 0;
            UpdateView();
        }

        public void ProccessCallBack(User user)
        {
            App.currentUser = user;

            if (App.currentUser != null)
                GlobalInitialization = true;
        }

        private void orderButton_Click(object sender, RoutedEventArgs e)
        {
            IssueMenu issueMenu = new IssueMenu();
            this.Hide();
            issueMenu.ShowDialog();
            this.Show();
        }

        private void staffListMenuItem_Click(object sender, RoutedEventArgs e)
        {
            StaffList staffList = new StaffList();
            this.Hide();
            staffList.ShowDialog();
            this.Show();
        }

        private void helpButton_Click(object sender, RoutedEventArgs e)
        {
            MessageBox.Show("SMILE :)");
        }

        private void registrationClientMenuItem_Click(object sender, RoutedEventArgs e)
        {
            Registration registration = new Registration();
            BlurEffectMainGrid.Radius = 5;
            registration.ShowDialog();
            BlurEffectMainGrid.Radius = 0;
        }

        private void buyProductsMenuItem_Click(object sender, RoutedEventArgs e)
        {
            BuyProducts buyProducts = new BuyProducts();
            BlurEffectMainGrid.Radius = 5;
            buyProducts.ShowDialog();
            BlurEffectMainGrid.Radius = 0;
        }

        private void Window_Closed(object sender, EventArgs e)
        {
            Environment.Exit(0);
        }

        private void profitMenuItem_Click(object sender, RoutedEventArgs e)
        {
            Profit prof = new Profit();
            this.Hide();
            prof.ShowDialog();
            this.Show();
        }
    }
}
