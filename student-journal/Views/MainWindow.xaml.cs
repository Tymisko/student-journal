using System.Windows;
using Diary.Models;
using Diary.ViewModels;
using MahApps.Metro.Controls;
using StudentJournal.ViewModels;

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
        }
    }
}
