﻿using System.Runtime.InteropServices;
using System.Threading;
using System.Windows.Forms;

namespace MineDetonator
{
    public static class Keyboard
    {
        private const int KEYEVENTF_EXTENDEDKEY = 0x0001;
        private const int KEYEVENTF_KEYUP = 0x0002;
        private const int ACTION_DELAY = 50;

        public const int VK_MBUTTON = 0x04;//middle mouse button
        public const int VK_LBUTTON = 0x01;//left mouse button
        public const int VK_RBUTTON = 0x02;//right mouse button


        [DllImport("user32.dll")]
        private static extern uint keybd_event(byte bVk, byte bScan, int dwFlags, int dwExtraInfo);

        public static void KeyDown(Keys key)
        {
            keybd_event((byte) key, 0, KEYEVENTF_EXTENDEDKEY | 0, 0);
        }

        public static void KeyUp(Keys key)
        {
            keybd_event((byte) key, 0, KEYEVENTF_EXTENDEDKEY | KEYEVENTF_KEYUP, 0); //0x7F
        }

        public static void KeyPress(Keys key)
        {
            KeyDown(key);
            Thread.Sleep(ACTION_DELAY);
            KeyUp(key);
        }

        [DllImport("user32.dll")]
        private static extern short GetKeyState(int nVirtKey);

        public static bool IsKeyDown(int nVirtKey) => GetKeyState(nVirtKey) < 0;

    }
}