using System.Diagnostics;
using System.Windows;
using System.Windows.Input;
using Diary.Commands;
using Diary.Models;
using Diary.ViewModels;

namespace StudentJournal.ViewModels
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

        private readonly bool _canCloseWindow;
        
        public DatabaseSettingsViewModel(bool canCloseWindow)
        {
            _canCloseWindow = canCloseWindow;
            
            SaveCommand = new RelayCommand(Save);
            CancelCommand = new RelayCommand(Cancel);

            DbSettings = new DatabaseSettings();
        }

        private void Cancel(object obj)
        {
            if (_canCloseWindow)
            {
                CloseWindow(obj as Window);
            }
            else
            {
                Application.Current.Shutdown();
            }
        }

        private void Save(object obj)
        {
            if (!DbSettings.IsValid) return;

            DbSettings.Save();
            RestartApplication();
        }

        private static void RestartApplication()
        {
            Process.Start(Application.ResourceAssembly.Location);
            Application.Current.Shutdown();
        }

        private static void CloseWindow(Window window)
        {
            window.Close();
        }
    }
}