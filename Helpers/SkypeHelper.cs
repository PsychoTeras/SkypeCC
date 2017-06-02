using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;

namespace SkypeCC.Helpers
{
    static class SkypeHelper
    {
        private const int WM_CLOSE = 0x0010;
        private const uint WM_GETTEXT = 0x000D;

        private delegate bool EnumThreadDelegate(IntPtr hWnd, IntPtr lParam);

        [DllImport("user32.dll")]
        private static extern bool EnumThreadWindows(int dwThreadId, EnumThreadDelegate lpfn, IntPtr lParam);

        [DllImport("user32.dll", CharSet = CharSet.Auto)]
        private static extern IntPtr SendMessage(IntPtr hWnd, uint msg, int wParam, StringBuilder lParam);

        [DllImport("user32.dll", CharSet = CharSet.Auto)]
        private static extern IntPtr SendMessage(IntPtr hWnd, uint msg, IntPtr wParam, IntPtr lParam);

        private static IEnumerable<IntPtr> EnumerateProcessWindowHandles(Process p)
        {
            List<IntPtr> handles = new List<IntPtr>();
            foreach (ProcessThread thread in p.Threads)
            {
                EnumThreadWindows(thread.Id, (hWnd, lParam) =>
                {
                    handles.Add(hWnd);
                    return true;
                }, IntPtr.Zero);
                foreach (IntPtr handle in handles)
                {
                    yield return handle;
                }
                handles.Clear();
            }
            yield return IntPtr.Zero;
        }

        public static Process GetSkypeActiveProcess(string profileName)
        {
            foreach (Process p in Process.GetProcesses(Environment.MachineName).
                Where(p => p.ProcessName.Equals("Skype", StringComparison.InvariantCultureIgnoreCase)))
            {
                foreach (IntPtr hWnd in EnumerateProcessWindowHandles(p))
                {
                    StringBuilder winCaption = new StringBuilder(200);
                    SendMessage(hWnd, WM_GETTEXT, winCaption.Capacity, winCaption);
                    if (winCaption.ToString().EndsWith(profileName, StringComparison.InvariantCultureIgnoreCase))
                    {
                        return p;
                    }
                }
            }
            return null;
        }

        public static bool ShutdownSkypeProcess(Process p)
        {
            foreach (IntPtr hWnd in EnumerateProcessWindowHandles(p))
            {
                SendMessage(hWnd, WM_CLOSE, IntPtr.Zero, IntPtr.Zero);
            }
            return p.WaitForExit(5000);
        }
    }
}