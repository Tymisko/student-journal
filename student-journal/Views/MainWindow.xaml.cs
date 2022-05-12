using System.Windows;
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
        public static MainWindow MainAppWindow;
        public MainWindow()
        {
            InitializeComponent();
            DataContext = new MainViewModel();
            MainAppWindow = this;

            SplashScreen.ApplicationSplashScreen.SplashScreenClosed += CheckDatabaseConnection;
        }

        public static void CloseApplication() => Application.Current.Shutdown();

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
                    CloseApplication();
                }
            }

            DbConnectionManager.OnValidDatabaseConnection?.Invoke();
        }
    }
}
