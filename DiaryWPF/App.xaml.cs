using DiaryWPF.Properties;
using DiaryWPF.Views;
using MahApps.Metro.Controls;
using MahApps.Metro.Controls.Dialogs;
using System.Data.Entity;
using System.Windows;

namespace DiaryWPF
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        private static string _dbServerAdress = Settings.Default.DbServerAdress;
        private static string _dbServerName = Settings.Default.DbServerName;
        private static string _dbName = Settings.Default.DbName;
        private static string _dbUserLogin = Settings.Default.DbUserLogin;
        private static string _dbUserPassword = Settings.Default.DbUserPassword;

        private void Application_DispatcherUnhandledException(object sender, System.Windows.Threading.DispatcherUnhandledExceptionEventArgs e)
        {
            var metroWindow = Current.MainWindow as MetroWindow;
            metroWindow.ShowMessageAsync("Błąd", "Wystąpił nieoczekinany wyjątek. \n" + e.Exception.Message);
            e.Handled = true;
        }

        public App()
        {
            if (CheckConnection())
            {
                MainWindow mainWindow = new MainWindow();
                mainWindow.Show();
            }
            else
            {
                MessageWindow messageWindow = new MessageWindow();
                messageWindow.Show();
            }
        }
        public static bool CheckConnection()
        {
            using (DbContext dbContext = new DbContext(GetUserConnectionString()))
            {
                if (dbContext.Database.Exists())
                    return true;
                else return false;
            }
        }
        public static string GetUserConnectionString()
        {
            return $@"Server = ({_dbServerAdress})\{_dbServerName}; Database = {_dbName}; User Id={_dbUserLogin};Password={_dbUserPassword};";
        }
    }
}
