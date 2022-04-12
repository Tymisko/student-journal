﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace Diary.ViewModels
{
    internal class ViewModelBase : INotifyPropertyChanged  
    {
        public event PropertyChangedEventHandler PropertyChanged;

        private int _myProperty;

        public int MyProperty
        {
            get { return _myProperty; }
            set 
            { 
                _myProperty = value;
                OnPropertyChanged();
            }
        }


        protected virtual void OnPropertyChanged(
            [CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
