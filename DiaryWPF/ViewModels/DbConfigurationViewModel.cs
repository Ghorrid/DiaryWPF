using DiaryWPF.Commands;
using DiaryWPF.Properties;
using System;
using System.Windows;
using System.Windows.Input;

namespace DiaryWPF.ViewModels
{
    public class DbConfigurationViewModel : ViewModelBase
    {

        public string DbServerAdress
        {
            get { return Settings.Default.DbServerAdress; }
            set { 
                Settings.Default.DbServerAdress = value;
                OnPropertyChanged();
            }
        }

 
        public string DbServerName
        {
            get { return Settings.Default.DbServerName; }
            set
            {
                Settings.Default.DbServerName = value;
                OnPropertyChanged();
            }
        }

        public string DbName
        {
            get { return Settings.Default.DbName; }
            set
            {
                Settings.Default.DbName = value;
                OnPropertyChanged();
            }
        }


        public string DbUserLogin
        {
            get { return Settings.Default.DbUserLogin; }
            set
            {
                Settings.Default.DbUserLogin = value;
                OnPropertyChanged();
            }
        }

        public string DbUserPassword
        {
            get { return Settings.Default.DbUserPassword; }
            set
            {
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
