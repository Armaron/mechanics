//using System;
//using System.Collections.Generic;

//using System.Runtime.InteropServices;
//using System.Text;
//using RAGE;


//namespace cs_packages.client
//{
//    internal class Win32
//    {
//        [DllImport("user32.dll")]
//        internal static extern IntPtr GetForegroundWindow();

//        [DllImport("user32", SetLastError = true)]
//       internal static extern int GetWindowThreadProcessId([In]IntPtr hwnd, [Out]out int lProcessId);

//      internal static bool IsFocusWindow()
//        {
//            int processId =12964;

//            int lProcessId;
//            IntPtr foregroundWindow = GetForegroundWindow();
//            GetWindowThreadProcessId(foregroundWindow, out lProcessId);
//            return processId == lProcessId;
           
//        }
//    }
//}
