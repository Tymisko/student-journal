using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using Diary.Commands;

namespace Diary.ViewModels
{
    internal class MainViewModel : ViewModelBase
    {
        public MainViewModel()
        {
            RefreshStudentsCommand = new RelayCommand(RefreshStudents, CanRefreshStudents);
        }
        public ICommand RefreshStudentsCommand { get; set; }

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
