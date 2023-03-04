using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace HotKeyAppSupportV1.Models
{
    public static class InputSimulator
    {
        // Declare the INPUT struct
        [StructLayout(LayoutKind.Sequential)]
        public struct INPUT
        {
            public int type;
            public InputUnion union;
        }

        // Declare the InputUnion struct
        [StructLayout(LayoutKind.Explicit)]
        public struct InputUnion
        {
            [FieldOffset(0)]
            public MOUSEINPUT mi;
            [FieldOffset(0)]
            public KEYBDINPUT ki;
        }

        // Declare the KEYBDINPUT struct
        [StructLayout(LayoutKind.Sequential)]
        public struct KEYBDINPUT
        {
            public ushort wVk;
            public ushort wScan;
            public uint dwFlags;
            public uint time;
            public IntPtr dwExtraInfo;
        }

        // Declare the MOUSEINPUT struct
        [StructLayout(LayoutKind.Sequential)]
        public struct MOUSEINPUT
        {
            public int dx;
            public int dy;
            public uint mouseData;
            public uint dwFlags;
            public uint time;
            public IntPtr dwExtraInfo;
        }

        // Declare the SendInput function
        [DllImport("user32.dll", SetLastError = true)]
        static extern uint SendInput(uint nInputs, INPUT[] pInputs, int cbSize);

        // Define the key codes
        const ushort KEY_UP = 0x0002;
        const uint KEYEVENTF_EXTENDEDKEY = 0x0001;
        const uint KEYEVENTF_KEYUP = 0x0002;

        // Simulate a key press
        public static void SimulateKeyPress(Key key)
        {
            ushort keyCode = (ushort)KeyInterop.VirtualKeyFromKey(key);

            // Set up the INPUT struct
            INPUT[] input = new INPUT[1];
            input[0].type = 1;
            input[0].union.ki.wVk = keyCode;
            input[0].union.ki.wScan = 0;
            input[0].union.ki.dwFlags = KEYEVENTF_EXTENDEDKEY;
            input[0].union.ki.time = 0;
            input[0].union.ki.dwExtraInfo = IntPtr.Zero;

            // Send the key press
            SendInput(1, input, Marshal.SizeOf(typeof(INPUT)));

            // Set up the INPUT struct for the key release
            input[0].union.ki.dwFlags = KEYEVENTF_EXTENDEDKEY | KEYEVENTF_KEYUP;

            // Send the key release
            SendInput(1, input, Marshal.SizeOf(typeof(INPUT)));
        }
    }
}
