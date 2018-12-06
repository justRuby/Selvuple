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
    /// Логика взаимодействия для CustomTextBoxWithLabel.xaml
    /// </summary>
    public partial class CustomTextBoxWithLabel : UserControl
    {
        public string Text
        {
            get { return textBox.Text; }
            set { textBox.Text = value; }
        }

        public string Label
        {
            get { return textBlock.Text; }
            set { textBlock.Text = value; }
        }

        public CustomTextBoxWithLabel()
        {
            InitializeComponent();
        }
    }
}
