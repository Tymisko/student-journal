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
            RefreshStudentsCommand = new RelayCommand(RefreshStudents, CanRefreshStudents);
            Students = new ObservableCollection<Student>()
            {
                new Student
                {
                    FirstName = "Patryk", 
                    LastName = "Matczak", 
                    Group = new Group { Id = 0 }
                },

                new Student
                {
                    FirstName = "Kamil",
                    LastName = "Nowak",
                    Group = new Group { Id = 0 }
                },

                new Student
                {
                    FirstName = "Janusz",
                    LastName = "Kaczyński",
                    Group = new Group { Id = 0 }
                },
            };
        }
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


        private bool CanRefreshStudents(object obj)
        {
            return true;
        }

        private void RefreshStudents(object obj)
        {
            throw new NotImplementedException();
        }
    }
}
