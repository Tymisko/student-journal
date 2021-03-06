using System;
using System.Collections.ObjectModel;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using Diary;
using Diary.Commands;
using Diary.Models.Domains;
using Diary.Models.Wrappers;
using Diary.ViewModels;
using Diary.Views;
using MahApps.Metro.Controls;
using MahApps.Metro.Controls.Dialogs;

namespace StudentJournal.ViewModels
{
    internal class MainViewModel : ViewModelBase
    {
        private readonly Repository _repository;

        public ICommand AddStudentCommand { get; set; }
        public ICommand EditStudentCommand { get; set; }
        public ICommand DeleteStudentCommand { get; set; }
        public ICommand RefreshStudentsCommand { get; set; }
        public ICommand DatabaseSettingsCommand { get; set; }
        public ICommand LoadedWindowCommand { get; set; }


        private StudentWrapper _selectedStudent;

        public StudentWrapper SelectedStudent
        {
            get => _selectedStudent;
            set
            {
                _selectedStudent = value;
                OnPropertyChanged();
            }
        }

        private ObservableCollection<StudentWrapper> _students;

        public ObservableCollection<StudentWrapper> Students
        {
            get => _students;
            set
            {
                _students = value;
                OnPropertyChanged();
            }
        }

        private int _selectedGroupId;

        public int SelectedGroupId
        {
            get => _selectedGroupId;
            set
            {
                _selectedGroupId = value;
                OnPropertyChanged();
            }
        }

        private ObservableCollection<Group> _groups;

        public ObservableCollection<Group> Groups
        {
            get => _groups;
            set
            {
                _groups = value;
                OnPropertyChanged();
            }
        }

        public MainViewModel()
        {
            _repository = new Repository();

            AddStudentCommand = new RelayCommand(AddEditStudent);
            EditStudentCommand = new RelayCommand(AddEditStudent, IsStudentSelected);
            DeleteStudentCommand = new AsyncRelayCommand(DeleteStudent, IsStudentSelected);
            RefreshStudentsCommand = new RelayCommand(RefreshStudents);
            DatabaseSettingsCommand = new RelayCommand(UpdateDatabaseSettingsAsync);
            LoadedWindowCommand = new RelayCommand(LoadedWindow);
        }
        
        private void RefreshStudents(object obj)
        {
            RefreshDiary();
        }

        private void UpdateDatabaseSettingsAsync(object obj)
        {
            var dbSettingsView = new Diary.Views.DatabaseSettingsView(true);
            dbSettingsView.ShowDialog();
        }

        private void InitGroups()
        {
            var groups = _repository.GetGroups();
            groups.Insert(0, new Group { Id = 0, Name = "All" });

            Groups = new ObservableCollection<Group>(groups);

            SelectedGroupId = 0;
        }

        private void RefreshDiary()
        {
            Students = new ObservableCollection<StudentWrapper>(
                _repository.GetStudents(SelectedGroupId));
        }

        private bool IsStudentSelected(object obj)
        {
            return SelectedStudent != null;
        }

        private async Task DeleteStudent(object obj)
        {
            var metroWindow = Application.Current.MainWindow as MetroWindow;
            var dialog = await metroWindow.ShowMessageAsync("Deleting a student",
                $"Are you sure you want to delete student {SelectedStudent.FirstName} {SelectedStudent.LastName}?",
                MessageDialogStyle.AffirmativeAndNegative);

            if (dialog != MessageDialogResult.Affirmative)
                return;

            _repository.DeleteStudent(SelectedStudent.Id);
            RefreshDiary();
        }

        private void AddEditStudent(object obj)
        {
            var addEditStudentWindow = new AddEditStudentView(obj as StudentWrapper);
            addEditStudentWindow.Closed += AddEditStudentWindow_Closed;
            addEditStudentWindow.ShowDialog();
        }

        private void AddEditStudentWindow_Closed(object sender, EventArgs e)
        {
            RefreshDiary();
        }

        private async void LoadedWindow(object obj)
        {
            if (!IsValidConnectionToDatabase())
            {
                var metroWindow = Application.Current.MainWindow as MetroWindow;
                var dialog = await metroWindow.ShowMessageAsync("Database connection problem.",
                    "Couldn't connect to database. Would you want to change connection settings?",
                    MessageDialogStyle.AffirmativeAndNegative);

                if (dialog == MessageDialogResult.Negative)
                {
                    Application.Current.Shutdown();
                }
                else
                {
                    var dbSettingsWindow = new DatabaseSettingsView(false);
                    dbSettingsWindow.Show();
                }
            }
            else
            {
                RefreshDiary();
                InitGroups();
            }
        }

        private static bool IsValidConnectionToDatabase()
        {
            try
            {
                using var context = new ApplicationDbContext();
                context.Database.Connection.Open();
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }
    }
}
