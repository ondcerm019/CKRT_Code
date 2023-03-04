using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Forms;

namespace KeyRebinderTestV3.Models
{
    public class ActiveProcessManager : IDisposable
    {
        private readonly TimeSpan _pollingInterval = TimeSpan.FromMilliseconds(200);
        private readonly Thread _pollingThread;
        private readonly CancellationTokenSource _cancellationTokenSource = new CancellationTokenSource();


        public event EventHandler<ActiveWindowChangedEventArgs> ActiveWindowChanged;





        private string? _lastProcessName;


        public ActiveProcessManager()
        {
            _pollingThread = new Thread(PollActiveWindow) { IsBackground = true };
            _pollingThread.Start();
        }

        private void PollActiveWindow()
        {
            while (!_cancellationTokenSource.IsCancellationRequested)
            {
                string lastActiveProcessName = GetProcessName(GetForegroundWindow());
                if (lastActiveProcessName != _lastProcessName)
                {
                    _lastProcessName = lastActiveProcessName;
                    ActiveWindowChanged?.Invoke(this, new ActiveWindowChangedEventArgs(_lastProcessName));
                }

                Thread.Sleep(_pollingInterval);
            }
        }

        public void Dispose()
        {
            _cancellationTokenSource.Cancel();
            _pollingThread.Join();
        }

        private static string GetProcessName(IntPtr hWnd)
        {
            uint processId;
            GetWindowThreadProcessId(hWnd, out processId);
            Process process = Process.GetProcessById((int)processId);
            return process.ProcessName;
        }

        #region Win32 Interop

        [DllImport("user32.dll", CharSet = CharSet.Auto)]
        private static extern IntPtr GetForegroundWindow();

        [DllImport("user32.dll")]
        static extern uint GetWindowThreadProcessId(IntPtr hWnd, out uint lpdwProcessId);

        #endregion
    }

    public class ActiveWindowChangedEventArgs : EventArgs
    {
        public string ProcessName { get; }

        public ActiveWindowChangedEventArgs(string processName)
        {
            ProcessName = processName;
        }
    }
}
        