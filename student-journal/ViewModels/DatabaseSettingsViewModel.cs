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

            var savedDbSettings = StudentJournal.Properties.Settings.Default;
            DbSettings = new DatabaseSettings
            {
                ServerAddress = savedDbSettings.DatabaseServerAddress,
                ServerName = savedDbSettings.DatabaseServerName,
                DatabaseName = savedDbSettings.DatabaseName,
                Username = savedDbSettings.DatabaseUsername,
                Password = savedDbSettings.DatabasePassword
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
            var savedDbSettings = StudentJournal.Properties.Settings.Default;
            savedDbSettings.DatabaseServerAddress = DbSettings.ServerAddress;
            savedDbSettings.DatabaseServerName = DbSettings.ServerName;
            savedDbSettings.DatabaseName = DbSettings.DatabaseName;
            savedDbSettings.DatabaseUsername = DbSettings.Username;
            savedDbSettings.DatabasePassword = DbSettings.Password;
            savedDbSettings.Save();
        }
    }
}