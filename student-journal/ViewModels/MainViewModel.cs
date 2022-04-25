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

namespace Diary.ViewModels
{
    internal class MainViewModel : ViewModelBase
    {
        public MainViewModel()
        {
            AddStudentCommand = new RelayCommand(AddStudent);
            EditStudentCommand = new RelayCommand(EditStudent);
            DeleteStudentCommand = new RelayCommand(DeleteStudent);
            RefreshStudentsCommand = new RelayCommand(RefreshStudents);

            Students = new ObservableCollection<Student>()
            {
                new Student
                {
                    FirstName = "Patryk",
                    LastName = "Matczak",
                    Group = new Group {Id = 0}
                },

                new Student
                {
                    FirstName = "Kamil",
                    LastName = "Nowak",
                    Group = new Group {Id = 0}
                },

                new Student
                {
                    FirstName = "Janusz",
                    LastName = "Kaczyński",
                    Group = new Group {Id = 0}
                },
            };

            InitGroups();
        }

        private void DeleteStudent(object obj)
        {
            throw new NotImplementedException();
        }

        private void EditStudent(object obj)
        {
            throw new NotImplementedException();
        }

        private void AddStudent(object obj)
        {
            throw new NotImplementedException();
        }

        public ICommand AddStudentCommand { get; set; }
        public ICommand EditStudentCommand { get; set; }
        public ICommand DeleteStudentCommand { get; set; }
        public ICommand RefreshStudentsCommand { get; set; }


        private Student _selectedStudent;

        public Student SelectedStudent
        {
            get => _selectedStudent;
            set
            {
                _selectedStudent = value;
                OnPropertyChanged();
            }
        }

        private ObservableCollection<Student> _students;

        public ObservableCollection<Student> Students
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

        private bool CanRefreshStudents(object obj)
        {
            return true;
        }

        private void RefreshStudents(object obj)
        {
            throw new NotImplementedException();
        }
        private void InitGroups()
        {
            Groups = new ObservableCollection<Group>()
            {
                new Group { Id = 0, Name = "All" },
                new Group { Id = 1, Name = "1A" },
                new Group { Id = 2, Name = "2A" }
            };

            SelectedGroupId = 0;
        }
    }
}
