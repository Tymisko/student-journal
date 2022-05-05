using System.Configuration;
using System.Windows;
using System.Windows.Input;
using Diary.Commands;
using Diary.Models;
using MahApps.Metro.Actions;

namespace Diary.ViewModels
{
    internal class DatabaseSettingsViewModel : ViewModelBase
    {
        public ICommand ConfirmCommand { get; set; }
        public ICommand CancelCommand { get; set; }

        private DatabaseSettings _dbSettings;

        public DatabaseSettings DbSettings
        {
            get => _dbSettings;
            set
            {
                _dbSettings = value;
                OnPropertyChanged();
            }
        }

        public DatabaseSettingsViewModel()
        {
            ConfirmCommand = new RelayCommand(Confirm);
            CancelCommand = new RelayCommand(Cancel);

            _dbSettings = new DatabaseSettings();
        }

        private void Cancel(object obj)
        {
            CloseWindow(obj as Window);
        }

        private void Confirm(object obj)
        {
            MessageBox.Show($"{DbSettings.DatabaseName}, " +
                            $"{DbSettings.ServerName}, " +
                            $"{DbSettings.ServerAddress}, " +
                            $"{DbSettings.Username}, " +
                            $"{DbSettings.Password}");
            CloseWindow(obj as Window);
        }

        private static void CloseWindow(Window window)
        {
            window.Close();
        }
    }
}