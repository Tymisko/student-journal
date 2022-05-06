using Diary.Models;
using Diary.ViewModels;
using System.Windows;
using MahApps.Metro.Controls;

namespace Diary.Views
{
    /// <summary>
    /// Interaction logic for DatabaseSettingsView.xaml
    /// </summary>
    public partial class DatabaseSettingsView : MetroWindow
    {
        public DatabaseSettingsView()
        {
            InitializeComponent();
            DataContext = new DatabaseSettingsViewModel();
        }
    }
}