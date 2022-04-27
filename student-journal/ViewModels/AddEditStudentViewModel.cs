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
    internal class AddEditStudentViewModel : ViewModelBase
    {
        public AddEditStudentViewModel(Student student = null)
        {
            ConfirmCommand = new RelayCommand(Confirm);
            CloseCommand = new RelayCommand(Close);

            if (student == null)
            {
                Student = new Student();
            }
            else
            {
                Student = student;
                IsUpdate = true;
            }

            InitGroups();
        }
        
        public ICommand ConfirmCommand { get; set; }
        public ICommand CloseCommand { get; set; }

        private Student _student;

        public Student Student
        {
            get => _student;
            set
            {
                _student = value;
                OnPropertyChanged();
            }
        }

        private bool _isUpdate;

        public bool IsUpdate
        {
            get => _isUpdate;
            set
            {
                _isUpdate = value;
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



        private void Close(object obj)
        {
            CloseWindow(obj as Window);
        }

        private void CloseWindow(Window window)
        {
            window.Close();
        }

        private void Confirm(object obj)
        {
            if (IsUpdate)
            {
                UpdateStudent();
                CloseWindow(obj as Window);
                return;
            }

            AddStudent();
            CloseWindow(obj as Window);
        }

        private void UpdateStudent()
        {
            // Database
            throw new NotImplementedException();
        }

        private void AddStudent()
        {
            // Database
            throw new NotImplementedException();
        }


        private void InitGroups()
        {
            Groups = new ObservableCollection<Group>()
            {
                new Group { Id = 0, Name = "-- none --" },
                new Group { Id = 1, Name = "1A" },
                new Group { Id = 2, Name = "2A" }
            };

            Student.Group.Id = 0;
        }
    }
}
