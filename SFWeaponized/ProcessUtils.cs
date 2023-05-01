using System;
using System.Diagnostics;

namespace SFWeaponized
{
    public class ProcessUtils
    {
        public static void Run(string path, string arguments, bool asAdmin = true)
        {
            ProcessStartInfo startInfo = new ProcessStartInfo
            {
                FileName = path,
                Arguments = arguments,
                UseShellExecute = true,
                CreateNoWindow = true,
                WindowStyle = ProcessWindowStyle.Hidden
            };

            if (asAdmin)
                startInfo.Verb = "runas";

            try
            {
                Process.Start(startInfo).WaitForExit();
            }
            catch { }
        }

    }
}
