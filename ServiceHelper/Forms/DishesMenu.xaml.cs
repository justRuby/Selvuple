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
using ServiceHelper.CustomsElem;
using ServiceHelper.Exstension;

namespace ServiceHelper.Forms
{
    /// <summary>
    /// Логика взаимодействия для DIshesMenu.xaml
    /// </summary>
    public partial class DishesMenu : Window
    {
        private List<Dish> dishList;
        private List<CustomMenuBlock> customMenuBlockList;

        private double _cost;
        private List<CustomMiniMenuBlock> customMiniMenuBlockList;

        public DishesMenu()
        {
            InitializeComponent();

            if (App.currentUser.Level == 3)
            {
                addDishToMenuButton.Visibility = Visibility.Visible;
            }
            else
            {
                addDishToMenuButton.Visibility = Visibility.Hidden;
            }

            dishList = new List<Dish>();
            customMenuBlockList = new List<CustomMenuBlock>();
            customMiniMenuBlockList = new List<CustomMiniMenuBlock>();
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            LoadAndUpdateDishes();
        }

        private async void LoadAndUpdateDishes()
        {
            dishList.Clear();
            customMenuBlockList.Clear();
            dishesMenuWrapPanel.Children.Clear();

            using (SQLiteConnection con = new SQLiteConnection(App.databasePath))
            {
                dishList = await Task.Run(() => con.Table<Dish>().Where(x => x.Access == true).ToList<Dish>());
            }

            if (dishList != null)
               await ViewDishesOnWindow();
        }

        private Task ViewDishesOnWindow()
        {
            foreach (var dish in dishList)
            {
                bool isAdmin = App.currentUser.Level == 3 ? true : false;
                var dishView = new CustomMenuBlock(dish, isAdmin, new Thickness(10));

                if (!isAdmin)
                    dishView.OnAddedDish(addToBasket);

                dishesMenuWrapPanel.Children.Add(dishView);
                customMenuBlockList.Add(dishView);
            }

            return Task.FromResult(0);
        }

        private void addDishToMenuButton_Click(object sender, RoutedEventArgs e)
        {
            AddDishToMenu dish = new AddDishToMenu();

            //this.Hide();
            DishMenuGridBlurEffect.Radius = 5;
            dish.ShowDialog();
            LoadAndUpdateDishes();
            DishMenuGridBlurEffect.Radius = 0;
            //this.Show();
        }

        private void addToBasket(Dish dish)
        {
            _cost += dish.Cost;
            FullCostOfIssue.Text = _cost + "руб.";

            var search = customMiniMenuBlockList.Where(x => x.Dish == dish);
            foreach (var obj in search)
            {
                obj.IncreaseDishesCount();
                return;
            }

            var dishBasketView = new CustomMiniMenuBlock(dish);
            dishBasketView.OnCanceledDish(removeFromBasket);

            dishesBasketWrapPanel.Children.Add(dishBasketView);
            customMiniMenuBlockList.Add(dishBasketView);
        }

        //Call when dishes </== 1
        private void removeFromBasket(Dish dish)
        {
            _cost -= dish.Cost;
            FullCostOfIssue.Text = _cost + "руб.";

            CustomMiniMenuBlock removeObj = null;
            var search = customMiniMenuBlockList.Where(x => x.Dish == dish);
            foreach (var obj in search)
            {
                if(obj.Count > 1)
                {
                    obj.ReduceDishesCount();
                    return;
                }

                removeObj = obj;
                break;
            }

            customMiniMenuBlockList.Remove(removeObj);
            dishesBasketWrapPanel.Children.Remove(removeObj);
        }

        private void checkoutButton_Click(object sender, RoutedEventArgs e)
        {
            if (customMiniMenuBlockList.Count != 0)
            {
                OrderAccounting orderAccounting = new OrderAccounting();

                CardLoyality card = new CardLoyality();

                using (SQLiteConnection con = new SQLiteConnection(App.databasePath))
                {
                    card = con.Table<CardLoyality>().Where(x => x.CardLoyalityID == App.currentUser.CardLoyality)
                                                    .FirstOrDefault();
                }

                double costWithDiscount = CostCalculation.GetCostWithDiscount(card.Score);

                orderAccounting.Cost = costWithDiscount;
                orderAccounting.TableNumber = 99; // Need to fix
                orderAccounting.IsActive = true;
                orderAccounting.Date = DateTime.Now.ToString("dd/MM/yyyy");

                string temp = String.Empty;
                string message = String.Empty;

                foreach (var dish in customMiniMenuBlockList)
                {
                    card.Score += dish.Count;
                    string buffer = dish.Dish.Name + "_" + dish.Count;
                    string mbuffer = dish.Dish.Name + " кол-во " + dish.Count;

                    if (temp == String.Empty)
                    {
                        temp = buffer;
                        message = mbuffer;
                    }
                    else
                    {
                        temp += ", " + buffer;
                        message += "\n" + mbuffer;
                    }
                        
                }

                orderAccounting.Dish = temp;

                message += "\n\n Цена cо скидкой: " + costWithDiscount + "руб.";

                var result = MessageBox.Show(message, "Заказ", MessageBoxButton.OKCancel);

                if(result == MessageBoxResult.OK)
                {
                    using (SQLiteConnection con = new SQLiteConnection(App.databasePath))
                    {
                        ServiceHelper.Core.DataHandler.AddIncome(costWithDiscount);
                        con.Insert(orderAccounting, typeof(OrderAccounting));
                        con.Update(card, typeof(CardLoyality));
                    }
                }
                
            }
        }

        private void backToMainMenuButton_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}
