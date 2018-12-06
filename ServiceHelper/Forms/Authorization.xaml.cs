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
using ServiceHelper.Exstension;
using System.Windows.Threading;

namespace ServiceHelper.Forms
{
    /// <summary>
    /// Логика взаимодействия для Authorization.xaml
    /// </summary>
    public partial class Authorization : Window
    {

        private PlaceholderController pController;
        private DispatcherTimer timer;
        private bool isTextChanged;

        private bool isInitialized;

        public Authorization()
        {
            InitializeComponent();
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            pController = new PlaceholderController();

            timer = new DispatcherTimer();
            timer.Tick += new EventHandler(TurnStateTextChanged);
            timer.Interval = new TimeSpan(0, 0, 0, 0, 10);

            loginTextBox.GotFocus += pController.ShowPlaceholderText;
            loginTextBox.LostFocus += pController.HidePlaceholderText;

            passwordTextBox.GotFocus += pController.ShowPlaceholderText;
            passwordTextBox.LostFocus += pController.HidePlaceholderText;

            isInitialized = true;
        }

        private void authorizationButton_Click(object sender, RoutedEventArgs e)
        {
            bool isAuth = false;
            User user = new User();

            using (SQLiteConnection con = new SQLiteConnection(App.databasePath))
            {
                var table = con.Table<User>().ToList<User>()
                    .Where(x => x.Login == loginTextBox.Text && x.Password == passwordTextBox.Password);

                foreach (var item in table)
                {
                    isAuth = true;
                    user = item;
                }
            }

            if (isAuth)
            {

                Menu menu = Application.Current.Windows.OfType<Menu>().FirstOrDefault();
                if (menu != null)
                {
                    menu.ProccessCallBack(user);
                }

                this.DialogResult = true;
                this.Close();
            }
            else
            {
                MessageBox.Show("Логин или пароль, введены не верно!");
            }
        }

        private void cancelButton_Click(object sender, RoutedEventArgs e)
        {
            this.DialogResult = false;
            this.Close();
        }

        private void passwordTextBox_TextChanged(object sender, TextChangedEventArgs e)
        {

            if (!isInitialized)
                return;

            var tb = (sender as CustomsElem.CustomTextBox);

            if(tb.Placeholder != tb.Text && !isTextChanged)
            {
                char tempChar;

                if (tb.Text.Length > 0)
                {
                    isTextChanged = true;
                    tempChar = tb.Text[tb.Text.Length - 1];
                }
                else
                {
                    tb.Password = string.Empty;
                    return;
                }
                    
                if (tempChar != '*')
                {
                    tb.Password += tempChar;
                }
                else
                {
                    tb.Password = tb.Password.Substring(0, tb.Password.Length - 1);
                    isTextChanged = false;
                    return;
                }

                int lenght = tb.Text.Length;

                tb.Text = string.Empty;

                for (int i = 0; i < lenght; i++)
                {
                    tb.Text += tb.PasswordChar;
                }

                tb.SelectionStart = lenght;

                timer.Start();
            }
        }

        private void TurnStateTextChanged(object sender, EventArgs e)
        {
            isTextChanged = false;
            timer.Stop();
        }

    }
}
