using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Media;
using ServiceHelper.CustomsElem;

namespace ServiceHelper.Exstension
{
    public class PlaceholderController
    {
        public void ShowPlaceholderText(object sender, EventArgs e)
        {
            (sender as CustomTextBox).Text = "";
            //(sender as CustomTextBox).Foreground = new SolidColorBrush(Color.FromArgb(255, 36, 36, 36));
        }

        public void HidePlaceholderText(object sender, EventArgs e)
        {
            var tb = (sender as CustomTextBox);

            //tb.Foreground = new SolidColorBrush(Color.FromArgb(255, 136, 136, 136));

            if (string.IsNullOrWhiteSpace(tb.Text))
                tb.Text = tb.Placeholder;
        }

        public void ReplaceText(object sender, EventArgs e)
        {
            var tb = (sender as CustomTextBox);


        }
    }
}
