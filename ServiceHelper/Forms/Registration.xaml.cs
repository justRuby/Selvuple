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

namespace ServiceHelper.Forms
{
    /// <summary>
    /// Логика взаимодействия для Registration.xaml
    /// </summary>
    public partial class Registration : Window
    {
        public Registration()
        {
            InitializeComponent();
        }

        private void cancelButton_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void registrationButton_Click(object sender, RoutedEventArgs e)
        {
            CardLoyality cardLoyality = new CardLoyality();
            int ID;

            bool isAccountExist;

            using (SQLiteConnection con = new SQLiteConnection(App.databasePath))
            {
                isAccountExist = con.Table<User>().Where(x => x.Login == loginTextBox.Text).Any();
                con.Insert(cardLoyality, typeof(CardLoyality));
                ID = con.Table<CardLoyality>().Last().CardLoyalityID;
            }

            if (isAccountExist)
            {
                MessageBox.Show("Невозможна регистрация, такой аккаунт уже существует!");
                return;
            }

            User user = new User()
            {
                FullName = fullNameTextBox.Text,
                Adress = addressTextBox.Text,
                Level = int.Parse(levelTextBox.Text),
                Login = loginTextBox.Text,
                Passport = passportTextBox.Text,
                Password = passwordTextBox.Text,
                Phone = "+" + phoneTextBox.Text,
                CardLoyality = ID
            };

            using (SQLiteConnection con = new SQLiteConnection(App.databasePath))
            {
                con.Insert(user, typeof(User));
            }

            this.Close();
        }

        private void NumberValidationTextBox(object sender, TextCompositionEventArgs e)
        {
            Regex regex = new Regex("[^0-9]+");
            e.Handled = regex.IsMatch(e.Text);
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            phoneTextBox.textBox.PreviewTextInput += NumberValidationTextBox;
            levelTextBox.textBox.PreviewTextInput += NumberValidationTextBox;
        }
    }
}
