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
    /// Логика взаимодействия для CustomMenuBlock.xaml
    /// </summary>
    public partial class CustomMenuBlock : UserControl
    {
        private Dish dish;
        private bool isAdmin;

        public CustomMenuBlock()
        {

        }

        public CustomMenuBlock(Dish dish, bool isAdmin, Thickness margin)
        {
            InitializeComponent();
            this.dish = dish;
            this.isAdmin = isAdmin;
            this.Margin = margin;

            dishImage.Source = ImageConverter.Base64ToImage(dish.Image);
            titleTextBlock.Text = dish.Name;
            ingridientsListTextBlock.Text = dish.Ingridients;
            costTextBlock.Text = dish.Cost + "руб.";
            CompositionTextBlock.Text = dish.Composition;

            this.Width = 250;
            this.Height = 300;
        }

        private void addDishToBasketButton_Click(object sender, RoutedEventArgs e)
        {
            if (!isAdmin)
                OnAddedDishEvent(dish);
        }

        public void OnAddedDish(AddDish aDish)
        {
            this.OnAddedDishEvent += aDish;
        }

        public delegate void AddDish(Dish dish);
        private event AddDish OnAddedDishEvent;
    }
}
