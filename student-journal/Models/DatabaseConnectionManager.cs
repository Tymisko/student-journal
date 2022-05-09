using System;
using System.Configuration;
using System.Data.SqlClient;
using System.Threading.Tasks;
using System.Windows;
using Diary.Views;
using MahApps.Metro.Controls;
using MahApps.Metro.Controls.Dialogs;

namespace Diary.Models
{
    public static class DbConnectionManager
    {
        public static Action OnValidDatabaseConnection;

        private const string ConnectionStrings = "connectionStrings";
        private const string ApplicationDbContext = "ApplicationDbContext";

        public static bool UserRefusedChangeSettings { get; private set; }
        public static bool AreDatabaseSettingsEmpty { get; private set; }

        public static bool IsConnectionValid()
        {
            UserRefusedChangeSettings = false;
            AreDatabaseSettingsEmpty = false;

            if (!AreDatabaseSettingsSet())
            {
                AreDatabaseSettingsEmpty = true;
                return false;
            }

            var connectionString = GetConnectionStringFromSettings();
            return IsItPossibleToConnectToDatabase(connectionString);
        }
        
        private static void SetConnectionString(string connectionString)
        {
            var config = ConfigurationManager.OpenExeConfiguration(ConfigurationUserLevel.None);
            if (config.GetSection(ConnectionStrings) is ConnectionStringsSection connectionStringSection)
                connectionStringSection.ConnectionStrings[ApplicationDbContext].ConnectionString = connectionString;

            config.Save(ConfigurationSaveMode.Modified);
            ConfigurationManager.RefreshSection(ConnectionStrings);
        }

        private static string GetConnectionStringFromSettings()
        {
            if (AreDatabaseSettingsSet())
            {
                var appSettings = Properties.Settings.Default;

                var server = $@"Server={appSettings.DatabaseServerAddress}\{appSettings.DatabaseServerName};";
                var database = $@"Database={appSettings.DatabaseName};";
                var userName = $@"User Id={appSettings.DatabaseUsername};";
                var userPassword = $@"Password={appSettings.DatabasePassword};";

                return string.Concat(server, database, userName, userPassword);
            }

            throw new InvalidOperationException("The database wasn't set.");
        }

        private static bool AreDatabaseSettingsSet()
        {
            return !string.IsNullOrWhiteSpace(Properties.Settings.Default.DatabaseServerAddress) &&
                   !string.IsNullOrWhiteSpace(Properties.Settings.Default.DatabaseServerName) &&
                   !string.IsNullOrWhiteSpace(Properties.Settings.Default.DatabaseName) &&
                   !string.IsNullOrWhiteSpace(Properties.Settings.Default.DatabaseUsername) &&
                   !string.IsNullOrWhiteSpace(Properties.Settings.Default.DatabasePassword);
        }

        private static bool IsItPossibleToConnectToDatabase(string connectionString)
        {
            if (!AreDatabaseSettingsSet()) return false;

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                try
                {
                    connection.Open();
                    return true;
                }
                catch (SqlException)
                {
                    return false;
                }
            }
        }

        public static async Task AskUserToChangeDatabaseSettingsAsync()
        {
            var metroWindow = Application.Current.MainWindow as MetroWindow;
            var dialog = await metroWindow.ShowMessageAsync("Couldn't connect to database","Do you want to change database settings??", MessageDialogStyle.AffirmativeAndNegative);

            if (dialog == MessageDialogResult.Affirmative)
            {
                UserRefusedChangeSettings = false;

                var dbSettingsView = new DatabaseSettingsView();
                dbSettingsView.ShowDialog();
                SetConnectionString(GetConnectionStringFromSettings());

                return;
            }

            UserRefusedChangeSettings = true;
        }
        public static void AskUserToFillDatabaseSettings()
        {
            var dbSettingsView = new DatabaseSettingsView();
            dbSettingsView.ShowDialog();
            SetConnectionString(GetConnectionStringFromSettings());
        }
    }
}
