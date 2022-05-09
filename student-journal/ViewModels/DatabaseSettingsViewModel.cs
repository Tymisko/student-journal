using System;
using System.Windows;
using System.Windows.Input;
using Diary.Commands;
using Diary.Models;

namespace Diary.ViewModels
{
    internal class DatabaseSettingsViewModel : ViewModelBase
    {
        public ICommand SaveCommand { get; set; }
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
            SaveCommand = new RelayCommand(Save);
            CancelCommand = new RelayCommand(Cancel);

            DbSettings = new DatabaseSettings
            {
                ServerAddress = Properties.Settings.Default.DatabaseServerAddress,
                ServerName = Properties.Settings.Default.DatabaseServerName,
                DatabaseName = Properties.Settings.Default.DatabaseName,
                Username = Properties.Settings.Default.DatabaseUsername,
                Password = Properties.Settings.Default.DatabasePassword
            };
        }

        private void Cancel(object obj)
        {
            CloseWindow(obj as Window);
        }

        private void Save(object obj)
        {
            if (!DbSettings.IsValid) return;

            UpdateDbSettings();
            CloseWindow(obj as Window);
        }

        private static void CloseWindow(Window window)
        {
            window.Close();
        }
        private void UpdateDbSettings()
        {
            Properties.Settings.Default.DatabaseServerAddress = DbSettings.ServerAddress;
            Properties.Settings.Default.DatabaseServerName = DbSettings.ServerName;
            Properties.Settings.Default.DatabaseName = DbSettings.DatabaseName;
            Properties.Settings.Default.DatabaseUsername = DbSettings.Username;
            Properties.Settings.Default.DatabasePassword = DbSettings.Password;
            Properties.Settings.Default.Save();
        }
    }
}