using DiaryWPF.Commands;
using DiaryWPF.Properties;
using System;
using System.Windows;
using System.Windows.Input;

namespace DiaryWPF.ViewModels
{
    public class DbConfigurationViewModel : ViewModelBase
    {

        private string _dbServerAdress ;

        public string DbServerAdress
        {
            get { return Settings.Default.DbServerAdress; }
            set { _dbServerAdress = value;
                Settings.Default.DbServerAdress = value;
                OnPropertyChanged();
            }
        }

        private string _dbServerName ;

        public string DbServerName
        {
            get { return Settings.Default.DbServerName; }
            set
            {
                _dbServerName = value;
                Settings.Default.DbServerName = value;
                OnPropertyChanged();
            }
        }

        private string _dbName;

        public string DbName
        {
            get { return Settings.Default.DbName; }
            set
            {
                _dbName = value;
                Settings.Default.DbName = value;
                OnPropertyChanged();
            }
        }

        private string _dbUserLogin;

        public string DbUserLogin
        {
            get { return Settings.Default.DbUserLogin; }
            set
            {
                _dbUserLogin = value;
                Settings.Default.DbUserLogin = value;
                OnPropertyChanged();
            }
        }

        private string _dbUserPassword;

        public string DbUserPassword
        {
            get { return Settings.Default.DbUserPassword; }
            set
            {
                _dbUserPassword = value;
                Settings.Default.DbUserPassword = value;
                OnPropertyChanged();
            }
        }

        public DbConfigurationViewModel()
        {
            CloseSettingsCommand = new RelayCommand(CloseSettings);
            AcceptSettingsCommand = new RelayCommand(AcceptSettings);
        }

        public ICommand CloseSettingsCommand { get; set; }
        public ICommand AcceptSettingsCommand { get; set; }

        private void CloseSettings(object obj)
        {
            var window = (obj as Window);
            window.Close();
        }


        private void AcceptSettings(object obj)
        {
            //_dbServerAdress = Settings.Default.DbServerAdress;
            //_dbServerName = Settings.Default.DbServerName;
            //_dbName = Settings.Default.DbServerName;
            //_dbUserLogin = Settings.Default.DbUserLogin;
            //_dbUserPassword=Settings.Default.DbUserPassword;

            Settings.Default.DbServerAdress = DbServerAdress;
            Settings.Default.DbServerName = DbServerName;
            Settings.Default.DbName = DbName;
            Settings.Default.DbUserLogin = DbUserLogin;
            Settings.Default.DbUserPassword = DbUserPassword;
            Settings.Default.Save();

            var window = (obj as Window);
            window.Close();
            System.Diagnostics.Process.Start(Application.ResourceAssembly.Location);
            Application.Current.Shutdown();
        }

    }
}
