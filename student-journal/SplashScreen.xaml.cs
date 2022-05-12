using Diary.Models;
using System;
using System.Windows;

namespace Diary
{
    /// <summary>
    /// Interaction logic for SplashScreen.xaml
    /// </summary>
    public partial class SplashScreen : Window
    {
        public static SplashScreen ApplicationSplashScreen;
        public event Action SplashScreenClosed;
        public static bool IsConnectionValidOnStartUp { get; private set; }
        public SplashScreen()
        {
            ApplicationSplashScreen = this;
            InitializeComponent();
            IsConnectionValidOnStartUp = DbConnectionManager.IsConnectionValid();
        }

        public virtual void OnSplashScreenClosed()
        {
            SplashScreenClosed?.Invoke();
        }
    }
}
