using System.Windows.Controls;

namespace ServiceHelper.CustomsElem
{
    public class CustomTextBox : TextBox
    {
        public string Placeholder { get; set; }

        public char PasswordChar { get; set; }
        public string Password { get; set; }
    }
}
