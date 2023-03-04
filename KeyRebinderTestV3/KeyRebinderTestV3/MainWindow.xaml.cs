using KeyRebinderTestV3.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Forms;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace KeyRebinderTestV3
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }


            /*
            if (HotKey.DictHotKeyToCalBackProc != null)
            {
                foreach (var hotkey in HotKey.DictHotKeyToCalBackProc.Values.ToList())
                {
                    hotkey.Unregister();

                    if (e.ProcessName == "chrome")
                    {
                        //hotkey.Register(KeyModifier.None, (e) => { SendKeys.SendWait("c"); });
                    }
                    else
                    {
                        hotkey.Register(KeyModifier.None, (e) => { SendKeys.SendWait("l"); });
                    }
                }

            }*/




            /*
            if (_hotKey != null)
            {
                _hotKey.Unregister();
                //_hotKey.Register();
            }*/

            /*
                foreach (var hotkey in HotKey.ActiveRebindedHotkeys)
                {
                    hotkey.Unregister();
                    _ = new HotKey(hotkey.KeyString, KeyModifier.None, (e) => { SendKeys.SendWait(hotkey.KeyString); });
                }



                if (e.ProcessName == "chrome")
                {
                    foreach (var rebind in Binds.RebindedRobloxInputs)
                    {
                        _ = new HotKey(rebind.Key, KeyModifier.None, (e) => { SendKeys.SendWait(rebind.Value); });
                    }
                }*/
    }
}
