using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;

namespace ServiceHelper
{
    /// <summary>
    /// Логика взаимодействия для App.xaml
    /// </summary>
    public partial class App : Application
    {
        private static string databaseName = "shDB.db";
        private static string folderPath = AppDomain.CurrentDomain.BaseDirectory;
        public static string databasePath = Path.Combine(folderPath, databaseName);

        public static Models.User currentUser = new Models.User();
    }
}
