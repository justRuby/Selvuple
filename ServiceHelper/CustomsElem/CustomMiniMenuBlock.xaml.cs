
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
using System.Windows.Navigation;
using System.Windows.Shapes;
using ServiceHelper.Exstension;
using ServiceHelper.Models;

namespace ServiceHelper.CustomsElem
{
    /// <summary>
    /// Логика взаимодействия для CustomMiniMenuBlock.xaml
    /// </summary>
    public partial class CustomMiniMenuBlock : UserControl
    {
        private int count = 0;
        private Dish dish;

        public Dish Dish
        {
            get { return dish; }
        }

        public int Count
        {
            get { return count; }
        }

        public CustomMiniMenuBlock(Dish d)
        {
            InitializeComponent();
            dish = d;

            dishNameTextBlock.Text = dish.Name;
            dishImage.Source = ImageConverter.Base64ToImage(dish.Image);

            IncreaseDishesCount();
        }

        public void IncreaseDishesCount()
        {
            countDishesTextBlock.Text = "Кол-во: " + (++count).ToString();
        }

        public void ReduceDishesCount()
        {
            countDishesTextBlock.Text = "Кол-во: " + (--count).ToString();
        }

        private void CancelDishOrderButton_Click(object sender, RoutedEventArgs e)
        {
            OnCanceledDishEvent(dish);
        }

        public void OnCanceledDish(CancelDish cDish)
        {
            this.OnCanceledDishEvent += cDish;
        }

        public delegate void CancelDish(Dish dish);
        private event CancelDish OnCanceledDishEvent;
    }
}
