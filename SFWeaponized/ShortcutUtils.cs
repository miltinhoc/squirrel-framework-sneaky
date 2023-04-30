using IWshRuntimeLibrary;

namespace SFWeaponized
{
    internal class ShortcutUtils
    {
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

        public static void Edit(string path, string arguments)
        {
            var shortcut = new IWshShell_Class().CreateShortcut(path) as IWshShortcut;

            shortcut.Arguments = arguments;
            shortcut.Save();
        }
    }
}
