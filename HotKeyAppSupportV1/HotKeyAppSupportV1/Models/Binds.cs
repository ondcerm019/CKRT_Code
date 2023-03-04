using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace HotKeyAppSupportV1.Models
{
    public static class Binds
    {
        public static Dictionary<Key, Key> RebindedRobloxInputs = new Dictionary<Key, Key>
        {
            { Key.Y, Key.D7 },
            { Key.X, Key.D8 },
            { Key.C, Key.D9 },
            { Key.V, Key.H }
        };
    }
}
