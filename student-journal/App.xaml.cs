using System;
using System.Configuration;
using System.Data;
using System.Diagnostics;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Threading;
using Diary.Views;
using MahApps.Metro.Controls;
using MahApps.Metro.Controls.Dialogs;

namespace Diary
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        private void Application_DispatcherUnhandledException(object sender, DispatcherUnhandledExceptionEventArgs e)
        {
            var metroWindow = Current.MainWindow as MetroWindow;
            metroWindow.ShowMessageAsync("Unexpected exception!",
                $"{e.Exception.Message}");

            e.Handled = true;
        }

        private const int MinimumSplashScreenDuration = 3000;
        protected override void OnStartup(StartupEventArgs e)
        {
            SplashScreen splash = new();
            splash.Show();

            Stopwatch timer = new();
            timer.Start();

            base.OnStartup(e);
            MainWindow mainWindow = new();

            timer.Stop();
            var remainingTimeToShowSplashScreen = MinimumSplashScreenDuration - (int)timer.ElapsedMilliseconds;
            if (remainingTimeToShowSplashScreen > 0)
            {
                Thread.Sleep(remainingTimeToShowSplashScreen);
            }

            splash.Close();
            splash.OnSplashScreenClosed();
        }
    }
}
