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
using Microsoft.Win32;

using SQLite;
using ServiceHelper.Exstension;
using ServiceHelper.Models;
using System.Drawing;
using System.Text.RegularExpressions;

namespace ServiceHelper.Forms
{
    /// <summary>
    /// Логика взаимодействия для AddDishToMenu.xaml
    /// </summary>
    public partial class AddDishToMenu : Window
    {

        private Dish dish;
        private PlaceholderController pController;

        public AddDishToMenu()
        {
            InitializeComponent();
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            dish = new Dish();
            pController = new PlaceholderController();

            proteinTextBox.GotFocus += pController.ShowPlaceholderText;
            proteinTextBox.LostFocus += pController.HidePlaceholderText;

            carbohydratesTextBox.GotFocus += pController.ShowPlaceholderText;
            carbohydratesTextBox.LostFocus += pController.HidePlaceholderText;

            fatTextBox.GotFocus += pController.ShowPlaceholderText;
            fatTextBox.LostFocus += pController.HidePlaceholderText;

            caloriesTextBox.GotFocus += pController.ShowPlaceholderText;
            caloriesTextBox.LostFocus += pController.HidePlaceholderText;

            costTextBox.textBox.PreviewTextInput += NumberValidationTextBox;
        }

        private void addDishImageButton_Click(object sender, RoutedEventArgs e)
        {
            OpenFileDialog op = new OpenFileDialog();

            op.Title = "Выберите изображение";
            op.Filter = "All supported graphics|*.jpg;*.jpeg;*.png|" +
              "JPEG (*.jpg;*.jpeg)|*.jpg;*.jpeg|" +
              "Portable Network Graphic (*.png)|*.png";
            if (op.ShowDialog() == true)
            {
                previewImageLoaded.Source = new BitmapImage(new Uri(op.FileName));
                var pic = new Bitmap(op.FileName).ScaleImage(100, 100);
                dish.Image = Exstension.ImageConverter.ImageToBase64(new Bitmap(pic));
            }
        }

        private void addDishButton_Click(object sender, RoutedEventArgs e)
        {

            dish.Composition = "Углеводы: " + carbohydratesTextBox.Text + "г.\n" +
                               "Белки: " + proteinTextBox.Text + "г.\n" +
                               "Жиры: " + fatTextBox.Text + "г.\n" +
                               "ККал: " + caloriesTextBox.Text + "";

            dish.Cost = double.Parse(costTextBox.Text);
            dish.Name = nameTextBox.Text;
            dish.Ingridients = ingridientsTextBox.Text;
            dish.Access = true;

            if (!dish.Composition.Equals(String.Empty) &&
                !dish.Image.Equals(String.Empty) &&
                !dish.Ingridients.Equals(String.Empty) &&
                !dish.Name.Equals(String.Empty) &&
                dish.Cost > 0)
            {
                using (SQLiteConnection con = new SQLiteConnection(App.databasePath))
                {
                    con.Insert(dish, typeof(Dish));
                }

                this.Close();
            }
            else
            {

            }
        }

        private void CancelButton_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void NumberValidationTextBox(object sender, TextCompositionEventArgs e)
        {
            Regex regex = new Regex("[^0-9]+");
            e.Handled = regex.IsMatch(e.Text);
        }
    }
}
