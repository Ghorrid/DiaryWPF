using DiaryWPF.Commands;
using DiaryWPF.Models;
using DiaryWPF.Models.Domains;
using DiaryWPF.Models.Wrappers;
using DiaryWPF.Properties;
using DiaryWPF.Views;
using MahApps.Metro.Controls;
using MahApps.Metro.Controls.Dialogs;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;

namespace DiaryWPF.ViewModels
{
    public class MainViewModel : ViewModelBase
    {


        private Repository _repository = new Repository();
        public MainViewModel()
        {

            //DbServerAdress = "local";
            //DbServerName = "SQLEXPRESS";
            //DbName = "Diary";
            //DbUserLogin = "diarydb";
            //DbUserPassword = "12345";

            RefreshStudentsCommand = new RelayCommand(RefreshStudents, CanRefreshStudents);
            AddStudentCommand = new RelayCommand(AddEditStudent);
            EditStudentCommand = new RelayCommand(AddEditStudent, CanEditDeleteStudent);
            DeleteStudentCommand = new AsyncRelayCommand(DeleteStudent, CanEditDeleteStudent);
            DbConfigurationCommand = new RelayCommand(DbConfiguration);
            RefreshDiary();

            InitGroups();


//          var con =  GetConnectionString();

        }



        public ICommand RefreshStudentsCommand { get; set; }
        public ICommand DbConfigurationCommand { get; set; }
        public ICommand AddStudentCommand { get; set; }
        public ICommand EditStudentCommand { get; set; }
        public ICommand DeleteStudentCommand { get; set; }
        


        private StudentWrapper _selectedStudent;
        public StudentWrapper SelectedStudent
        {
            get { return _selectedStudent; }
            set { _selectedStudent = value;
                OnPropertyChanged();
            }
        }

        // W WPF zamiast list uzywamy ObservableCollection - lista z 2 dodatkowymi interfejsami
        private ObservableCollection<StudentWrapper> _students;
        public ObservableCollection<StudentWrapper> Students
        {
            get { return _students; }
            set
            {
                _students = value;
                OnPropertyChanged();
            }
        }

        private int _selectedGroupId;

        public int SelectedGroupId
        {
            get { return _selectedGroupId; }
            set { 
                _selectedGroupId = value;
                OnPropertyChanged();
                }
        }

        private ObservableCollection<Group> _groups;

        public ObservableCollection<Group> Groups
        {
            get { return _groups; }
            set { 
                _groups = value;
                OnPropertyChanged();
            }
        }

        private void RefreshDiary()
        {
            Students = new ObservableCollection<StudentWrapper>
                (_repository.GetStudents(SelectedGroupId));
            
        }

        private void InitGroups()
        {
             var groups =_repository.GetGroups();
            groups.Insert(0, new Group { Id = 0, Name = "Wszystkie" });

            Groups = new ObservableCollection<Group>(groups);
         
            SelectedGroupId= 0;
        }


        private void RefreshStudents(object obj)
        {
            RefreshDiary();
        }

        private bool CanRefreshStudents(object obj)
        {
            return true;
        }

        private bool CanEditDeleteStudent(object obj)
        {
            return SelectedStudent != null;
        }

        private async Task DeleteStudent(object obj)
        {
            var metroWindow = Application.Current.MainWindow as MetroWindow;
            var dialog = await metroWindow.ShowMessageAsync("Usuwanie ucznia", 
                $"Czy na pewno chcesz usunąc ucznia {SelectedStudent.FirstName} { SelectedStudent.LastName} ?"
                , MessageDialogStyle.AffirmativeAndNegative);
            if (dialog != MessageDialogResult.Affirmative) return;

            _repository.DeleteStudent(SelectedStudent.Id);

            RefreshDiary();

        }

        private void AddEditStudent(object obj)
        {
            var addEditStudentWindow = new AddEditStudentView(obj as StudentWrapper);
            addEditStudentWindow.Closed += AddEditStudentWindow_Closed;
            
            addEditStudentWindow.Show();


        }

        private void AddEditStudentWindow_Closed(object sender, EventArgs e)
        {
            RefreshDiary();
        }

        private void DbConfiguration(object obj)
        {

            var dbConfigurationWindow = new DbConfigurationView();
            //dbConfigurationWindow.Closed += AddEditStudentWindow_Closed;
            dbConfigurationWindow.Show();


        }

    }


}
