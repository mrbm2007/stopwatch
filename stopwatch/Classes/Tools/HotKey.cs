using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using System.Windows.Forms;
using System.Xml.Serialization;

namespace stopwatch
{
    [Serializable]
    public class HotKey
    {
        [DllImport("user32.dll")]
        public static extern bool RegisterHotKey(IntPtr hWnd, int id, int fsModifiers, int vlc);
        [DllImport("user32.dll")]
        public static extern bool UnregisterHotKey(IntPtr hWnd, int id);
        int id = -1;
        public bool ctrl, alt, shift;
        int modifier
        {
            get
            {
                // Modifier keys codes: Alt = 1, Ctrl = 2, Shift = 4, Win = 8
                // Compute the addition of each combination of the keys you want to be pressed
                // ALT+CTRL = 1 + 2 = 3 , CTRL+SHIFT = 2 + 4 = 6...
                var m = 0;
                if (alt) m += 1;
                if (ctrl) m += 2;
                if (shift) m += 4;
                return m;
            }
        }
        public char key;
        [NonSerialized]
        static Window window = new Window();
        private class Window : NativeWindow, IDisposable
        {
            private static int WM_HOTKEY = 0x0312;

            public Window()
            {
                // create the handle for the window.
                this.CreateHandle(new CreateParams());
            }

            /// <summary>
            /// Overridden to get the notifications.
            /// </summary>
            /// <param name="m"></param>
            protected override void WndProc(ref Message m)
            {
                base.WndProc(ref m);

                // check if we got a hot key pressed.
                if (m.Msg == WM_HOTKEY)
                {
                    // get the keys.
                    var key = (char)(((int)m.LParam >> 16) & 0xFFFF);
                    var modifier = ((int)m.LParam & 0xFFFF);

                    // invoke the event to notify the parent.
                    KeyPressed(key, modifier);
                }
            }
            //public event EventHandler<System.Windows.Forms. KeyPressEventArgs> KeyPressed;
            #region IDisposable Members

            public void Dispose()
            {
                this.DestroyHandle();
            }

            #endregion
        }
        static void KeyPressed(char key, int modifier)
        {
            foreach (var h in LIST)
                if (h.key == key && h.modifier == modifier)
                    if (h.pressed != null)
                    {
                        h.pressed(h);
                        return;
                    }
        }
       
        public delegate void Pressed(HotKey sender);
        [NonSerialized, XmlIgnore]
        public Pressed pressed;
        public static List<HotKey> LIST = new List<HotKey>();
        public void Register()
        {
            LIST.Add(this);
            if (id < 0)
                id = LIST.Count + 1;
            RegisterHotKey(window.Handle, id, modifier, (int)key);
        }
        public void UnRegister()
        {
            LIST.Remove(this);
            UnregisterHotKey(window.Handle, id);
        }
        public override string ToString()
        {
            return (ctrl ? "Ctrl+" : "") + (alt ? "Alt+" : "") + (shift ? "Shift+" : "") + "'" + (char)key + "'";
        }
    }
}
