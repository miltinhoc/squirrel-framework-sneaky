using IWshRuntimeLibrary;
using System;
using System.IO;

namespace SFWeaponized
{
    internal class ShortcutUtils
    {
        public static readonly string DesktopPath = Environment.GetFolderPath(Environment.SpecialFolder.Desktop);

        public static void Create(string path, string targetPath, string arguments = "", string iconPath = "")
        {
            var shortcut = new IWshShell_Class().CreateShortcut(path) as IWshShortcut;

            if (!string.IsNullOrEmpty(arguments))
                shortcut.Arguments = arguments;

            if (!string.IsNullOrEmpty(iconPath))
                shortcut.IconLocation = iconPath;

            shortcut.TargetPath = targetPath;
            shortcut.Save();
        }

        public static void SetRunAsAdmin(string path)
        {
            byte[] data = System.IO.File.ReadAllBytes(path);
            data[21] = 34;

            System.IO.File.WriteAllBytes(path, data);
        }

        public static string FindShortcut(string fullPath)
        {
            string[] files = Directory.GetFiles(DesktopPath, "*.lnk", SearchOption.TopDirectoryOnly);

            foreach (string file in files)
            {
                var temp = new IWshShell_Class().CreateShortcut(file) as IWshShortcut;

                if (temp.TargetPath.Contains(fullPath))
                    return file;
            }

            return string.Empty;
        }

        public static void Edit(string path, string arguments)
        {
            var shortcut = new IWshShell_Class().CreateShortcut(path) as IWshShortcut;

            shortcut.Arguments = arguments;
            shortcut.Save();
        }
    }
}
