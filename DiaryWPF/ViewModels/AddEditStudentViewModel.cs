using DiaryWPF.Commands;
using DiaryWPF.Models.Domains;
using DiaryWPF.Models.Wrappers;
using System.Collections.ObjectModel;
using System.Windows;
using System.Windows.Input;

namespace DiaryWPF.ViewModels
{
    public class AddEditStudentViewModel : ViewModelBase
    {
        private Repository _repository = new Repository();

        public AddEditStudentViewModel(StudentWrapper student =null)
        {
            CloseCommand = new RelayCommand(Close);
            AcceptCommand = new RelayCommand(Accept);


            if (student == null)
            {
                Student= new StudentWrapper();
            }
            else
            {
                Student=student;
                IsUpdate = true;
            }

            InitGroups();
        }

        private StudentWrapper _student;

        public StudentWrapper Student
        {
            get { return _student; }
            set {
                _student = value;
                OnPropertyChanged();
            }
        }

        private bool _isUpdate;

        public bool IsUpdate
        {
            get { return _isUpdate; }
            set
            {
                _isUpdate = value;
                OnPropertyChanged();
            }
        }

        private int _selectedGroupId;

        public int SelectedGroupId
        {
            get { return _selectedGroupId; }
            set
            {
                _selectedGroupId = value;
                OnPropertyChanged();
            }
        }


        private StudentWrapper _selectedStudent;
        public StudentWrapper SelectedStudent
        {
            get { return _selectedStudent; }
            set
            {
                _selectedStudent = value;
                OnPropertyChanged();
            }
        }


        private ObservableCollection<Group> _groups;

        public ObservableCollection<Group> Groups
        {
            get { return _groups; }
            set
            {
                _groups = value;
                OnPropertyChanged();
            }
        }


        public ICommand CloseCommand { get; set; }

        public ICommand AcceptCommand { get; set; }

        private void Accept(object obj)
        {
            if (!Student.isValid)
                return;

            if (!IsUpdate)
                AddStudent();
            else UpdateStudent();

            CloseWindow(obj as Window);
        }

        private void AddStudent()
        {
            // czemu przekazujemy obiekt student przy add ?
            _repository.AddStudent(Student);
        }

        private void UpdateStudent()
        {
            _repository.UpdateStudent(Student);
        }

        private void Close(object obj )
        {
            CloseWindow(obj as Window);
        }

        private void CloseWindow(Window window)
        {
            window.Close();
        }

        private void InitGroups()
        {
            var groups = _repository.GetGroups();
            groups.Insert(0, new Group { Id = 0, Name = "-- brak --" });

            Groups = new ObservableCollection<Group>(groups);

            SelectedGroupId = Student.Group.Id;
        }
    }
}
