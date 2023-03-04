using KeyRebinderTestV3.Models;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Forms;

namespace KeyRebinderTestV3
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : System.Windows.Application
    {
        private static Mutex _mutex = null;
        private readonly NotifyIcon _notifyIcon;
        private const string _relativePath = @"..\RobloxHotKey\ondat's HotKey - Roblox.exe";
        private ProcessStartInfo _processStartInfo;
        private Process _process;



        public App()
        {
            _notifyIcon = new NotifyIcon();
        }

        protected override void OnStartup(StartupEventArgs e)
        {
            string fullPath = Path.GetFullPath(Path.Combine(AppDomain.CurrentDomain.BaseDirectory, _relativePath));
            _processStartInfo = new ProcessStartInfo(fullPath);
            //_processStartInfo.Verb = "runas";
            _processStartInfo.UseShellExecute = true;
            //_processStartInfo.WindowStyle = ProcessWindowStyle.Hidden;





            const string appName = "ondatHotKey";
            bool createdNew;

            _mutex = new Mutex(true, appName, out createdNew);

            if (!createdNew)
            {
                //app is already running! Exiting the application  
                Current.Shutdown();
            }



            _notifyIcon.Icon = new Icon("Resources/hotkeyicon.ico");
            _notifyIcon.Text = "ondat's HotKey";
            _notifyIcon.Visible = true;


            _notifyIcon.ContextMenuStrip = new System.Windows.Forms.ContextMenuStrip();
            _notifyIcon.ContextMenuStrip.Items.Add("Shutdown HotKey", null, OnHotKeyShutdownClick);

            /*_notifyIcon.BalloonTipText = "test";
            _notifyIcon.ShowBalloonTip(5);*/





            var activeWindowManager = new ActiveProcessManager();
            activeWindowManager.ActiveWindowChanged += WindowChange;


            NotificationManager.ShowNotification("Activated", "");

            base.OnStartup(e);
        }

        private void OnHotKeyShutdownClick(object? sender, EventArgs e)
        {
            Current.Shutdown();
        }

        /*
        private void NotifyIcon_BalloonTipClicked(object? sender, EventArgs e)
        {
            System.Windows.MessageBox.Show("Application is running.", "Status", MessageBoxButton.OK, MessageBoxImage.Information);
        }

        private void NotifyIcon_Click(object? sender, EventArgs e)
        {
        
        }*/

        protected override void OnExit(ExitEventArgs e)
        {
            NotificationManager.ShowNotification("Shutdown", "");

            _notifyIcon?.Dispose();

            if (_process != null && _process.HasExited == false)
            {
                _process.Kill();
            }

            base.OnExit(e);
        }








        public void WindowChange(object? sender, ActiveWindowChangedEventArgs e)
        {
            if (e.ProcessName == "RobloxPlayerBeta")
            {
                //launch service
                Dispatcher.Invoke(() =>
                {
                    if (_process == null || _process.HasExited == true)
                    {
                        _process = Process.Start(_processStartInfo)!;
                    }
                });
            }
            else
            {
                //shutdown service
                Dispatcher.Invoke(() =>
                {
                    if (_process != null && _process.HasExited == false)
                    {
                        _process.Kill();
                    }
                });
            }

            //NotificationManager.ShowNotification(e.ProcessName, "");
        }
    }
}
