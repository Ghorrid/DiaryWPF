using DiaryWPF.Commands;
using DiaryWPF.Views;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;

namespace DiaryWPF.ViewModels
{
    public class MessageWindowViewModel : ViewModelBase
    {

        public MessageWindowViewModel()
        {
            CloseSettingsCommand = new RelayCommand(CloseSettings);
            AcceptSettingsCommand = new RelayCommand(AcceptSettings);

        }

        private void AcceptSettings(object obj)
        {
            var dbConfigurationWindow = new DbConfigurationView();
            //dbConfigurationWindow.Closed += AddEditStudentWindow_Closed;
            dbConfigurationWindow.Show();

        }

        private void CloseSettings(object obj)
        {
            Application.Current.Shutdown();
        }

        public ICommand CloseSettingsCommand { get; set; }
        public ICommand AcceptSettingsCommand { get; set; }

    }


}
