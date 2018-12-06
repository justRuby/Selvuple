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

namespace ServiceHelper.CustomsElem
{
    /// <summary>
    /// Логика взаимодействия для CustomTabledButton.xaml
    /// </summary>
    public partial class CustomTabledButton : UserControl
    {

        public Button CButton { get; set; }

        public string CText { get; set; }

        public CustomTabledButton()
        {
            InitializeComponent();

            CButton = tableButton;
            CText = tableText.Text;
        }
    }
}
