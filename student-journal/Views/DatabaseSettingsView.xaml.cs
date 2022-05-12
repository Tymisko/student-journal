using MahApps.Metro.Controls;
using StudentJournal.ViewModels;

namespace Diary.Views
{
    /// <summary>
    /// Interaction logic for DatabaseSettingsView.xaml
    /// </summary>
    public partial class DatabaseSettingsView : MetroWindow
    {
        public DatabaseSettingsView(bool canCloseWindow)
        {
            InitializeComponent();
            DataContext = new DatabaseSettingsViewModel(canCloseWindow);
        }
    }
}