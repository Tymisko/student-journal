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

namespace Diary.ViewModels
{
    internal class AddEditStudentViewModel : ViewModelBase
    {
        public AddEditStudentViewModel(StudentWrapper student = null)
        {
            ConfirmCommand = new RelayCommand(Confirm);
            CloseCommand = new RelayCommand(Close);

            if (student == null)
            {
                Student = new StudentWrapper();
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

        private StudentWrapper _student;

        public StudentWrapper Student
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
            Groups = new ObservableCollection<GroupWrapper>()
            {
                new GroupWrapper { Id = 0, Name = "-- none --" },
                new GroupWrapper { Id = 1, Name = "1A" },
                new GroupWrapper { Id = 2, Name = "2A" }
            };

            Student.Group.Id = 0;
        }
    }
}
