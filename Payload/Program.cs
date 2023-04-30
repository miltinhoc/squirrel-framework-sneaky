using System.Diagnostics;
using System.Windows.Forms;

namespace Payload
{
    internal class Program
    {
        static void Main(string[] args)
        {
            if (args.Length == 1)
                LaunchProcess(args[0]);

            MessageBox.Show("hello");
        }

        static void LaunchProcess(string process)
        {
            string executablePath = process;
            ProcessStartInfo startInfo = new ProcessStartInfo
            {
                FileName = "cmd.exe",
                Arguments = $"/c start \"\" \"{executablePath}\"",
                UseShellExecute = false,
                CreateNoWindow = true,
            };

            try
            {
                Process.Start(startInfo);
            }
            catch { }
        }
    }
}
