using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.InteropServices;

namespace WindowTracker
{
    class Program
    {
        [DllImport("user32.dll")]
        static extern IntPtr GetForegroundWindow();

        [DllImport("user32.dll")]
        static extern int GetWindowText(IntPtr hWnd, StringBuilder text, int count);

        public string GetActiveWindowTitle()
        {
            const int nChars = 256;
            StringBuilder Buff = new StringBuilder(nChars);
            IntPtr handle = GetForegroundWindow();

            if (GetWindowText(handle, Buff, nChars) > 0)
            {
                return Buff.ToString();
                //return handle.ToString();
            }
            return null;
        }

        static void Main(string[] args)
        {
            Program a = new Program();

            while (true)
            {
                Console.WriteLine(DateTime.Now + " " + a.GetActiveWindowTitle());
                System.Threading.Thread.Sleep(5000);
            }
            
        }
    }
}
