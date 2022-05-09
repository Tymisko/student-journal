using Diary.Models;
using Diary.ViewModels;
using MahApps.Metro.Controls;

namespace Diary.Views
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : MetroWindow
    {
        public MainWindow()
        {
            InitializeComponent();
            DataContext = new MainViewModel();
            CheckDatabaseConnection();
        }

        private static async void CheckDatabaseConnection()
        {
            while (!DbConnectionManager.IsConnectionValid())
            {
                if (DbConnectionManager.AreDatabaseSettingsEmpty)
                {
                    DbConnectionManager.AskUserToFillDatabaseSettings();
                }

                await DbConnectionManager.AskUserToChangeDatabaseSettingsAsync();
                if (DbConnectionManager.UserRefusedChangeSettings)
                {
                    App.Close();
                }
            }

            DbConnectionManager.OnValidDatabaseConnection?.Invoke();
        }
    }
}
