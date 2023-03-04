using HotKeyAppSupportV1.Models;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Forms;
using System.Windows.Input;

namespace HotKeyAppSupportV1
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : System.Windows.Application
    {
        private static Mutex _mutex = null;
        private bool _toBeShutdown;

        protected override void OnStartup(StartupEventArgs e)
        {
            const string appName = "ondatCustomHotkeySupportApp";
            bool createdNew;

            _mutex = new Mutex(true, this.GetType().Namespace.ToString(), out createdNew);

            if (!createdNew)
            {
                //app is already running! Exiting the application
                Current.Shutdown();
            }


            foreach (var input in Binds.RebindedRobloxInputs)
            {
                HotKey hotkey = new HotKey(input.Key);
                hotkey.Register(KeyModifier.None, (e) => { InputSimulator.SimulateKeyPress(input.Value); });
            }

            (new HotKey(Key.M)).Register(KeyModifier.None, (e) =>
            {
                if (_toBeShutdown == false)
                {
                    NotificationManager.ShowNotification("Roblox HotKey force shutdown.", "Re-focus on the process instance to activate again.");

                    _toBeShutdown = true;
                }

                Current.Shutdown();
            });
        }
    }
}
