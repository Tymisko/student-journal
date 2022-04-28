using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using Diary.Commands;
using Diary.Models;
using Diary.Models.Wrappers;
using Diary.Views;
using MahApps.Metro.Controls;
using MahApps.Metro.Controls.Dialogs;

namespace Diary.ViewModels
{
    internal class MainViewModel : ViewModelBase
    {
        public MainViewModel()
        {
            AddStudentCommand = new RelayCommand(AddEditStudent);
            EditStudentCommand = new RelayCommand(AddEditStudent, IsStudentSelected);
            DeleteStudentCommand = new AsyncRelayCommand(DeleteStudent, IsStudentSelected);
            RefreshStudentsCommand = new RelayCommand(RefreshStudents);

            InitGroups();
        }

        public ICommand AddStudentCommand { get; set; }
        public ICommand EditStudentCommand { get; set; }
        public ICommand DeleteStudentCommand { get; set; }
        public ICommand RefreshStudentsCommand { get; set; }


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

        private ObservableCollection<GroupWrapper> _groups;

        public ObservableCollection<GroupWrapper> Groups
        {
            get => _groups;
            set
            {
                _groups = value;
                OnPropertyChanged();
            }
        }

        private bool CanRefreshStudents(object obj)
        {
            return true;
        }

        private void RefreshStudents(object obj)
        {
            RefreshDiary();
        }
        private void InitGroups()
        {
            Groups = new ObservableCollection<GroupWrapper>()
            {
                new GroupWrapper { Id = 0, Name = "All" },
                new GroupWrapper { Id = 1, Name = "1A" },
                new GroupWrapper { Id = 2, Name = "2A" }
            };

            SelectedGroupId = 0;
        }

        private void RefreshDiary()
        {
            Students = new ObservableCollection<StudentWrapper>()
            {
                new StudentWrapper
                {
                    FirstName = "Patryk",
                    LastName = "Matczak",
                    Group = new GroupWrapper {Id = 0}
                },

                new StudentWrapper
                {
                    FirstName = "Kamil",
                    LastName = "Nowak",
                    Group = new GroupWrapper {Id = 0}
                },

                new StudentWrapper
                {
                    FirstName = "Janusz",
                    LastName = "Kaczyński",
                    Group = new GroupWrapper {Id = 0}
                }
            };
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

            // deleting the student from database

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
    }
}
