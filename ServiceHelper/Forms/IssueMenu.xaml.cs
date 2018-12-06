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
using ServiceHelper.Blueprint;

namespace ServiceHelper.Forms
{
    /// <summary>
    /// Логика взаимодействия для IssueMenu.xaml
    /// </summary>
    public partial class IssueMenu : Window
    {

        private List<OrderAccounting> orderAccountingList;
        private OrderAccounting _selectedOrder;

        private bool isActiveOnly = true;
        private bool isInitialized;

        public IssueMenu()
        {
            InitializeComponent();
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            issueDatePicker.DisplayDateStart = DateTime.Now;
            issueDatePicker.DisplayDate = DateTime.Now;
            issueDatePicker.SelectedDate = DateTime.Now;

            LoadDataToTable(issueDatePicker.SelectedDate.Value.ToString("dd/MM/yyyy"));

            isInitialized = true;
        }

        private void LoadDataToTable(string date)
        {
            using (SQLiteConnection con = new SQLiteConnection(App.databasePath))
            {
                orderAccountingList = con.Table<OrderAccounting>().Where(x => x.IsActive == isActiveOnly).ToList();
                IssueDataGrid.ItemsSource = orderAccountingList;
            }
        }

        private void CloseIssue()
        {
            using(SQLiteConnection con = new SQLiteConnection(App.databasePath))
            {
                if (_selectedOrder != null)
                {
                    _selectedOrder.IsActive = false;
                    con.Update(_selectedOrder, typeof(OrderAccounting));
                }
                    
            }
        }

        private void IssueDataGrid_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            var selected = e.AddedItems;

            if (selected.Count > 0)
                _selectedOrder = selected[0] as OrderAccounting;
        }

        private void issueDatePicker_CalendarClosed(object sender, RoutedEventArgs e)
        {
            LoadDataToTable((sender as DatePicker).SelectedDate.Value.ToString("dd/MM/yyyy"));
        }

        private void closeIssueButton_Click(object sender, RoutedEventArgs e)
        {
            CloseIssue();

            DraftGenerator.GenerateDraft(App.currentUser.FullName, _selectedOrder);

            LoadDataToTable(issueDatePicker.DisplayDate.ToString("dd/MM/yyyy"));
        }

        private void closeButton_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void onlyActiveCheckBox_Checked(object sender, RoutedEventArgs e)
        {

        }

        private void CheckBox_Click(object sender, RoutedEventArgs e)
        {
            isActiveOnly = (sender as CheckBox).IsChecked.Value;

            if (isInitialized)
                LoadDataToTable(issueDatePicker.SelectedDate.Value.ToString("dd/MM/yyyy"));
        }
    }
}
