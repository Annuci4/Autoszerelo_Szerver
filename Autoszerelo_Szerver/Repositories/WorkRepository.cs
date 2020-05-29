using Autoszerelo_Szerver.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text.Json;
using System.Linq;
using System.Threading.Tasks;
using JsonSerializer = System.Text.Json.JsonSerializer;

//Ez az osztály fog azért felelni, hogy a work objektumainkat ki tudjuk menteni egy json fájlba,
//és vissza tudjuk onnan olvasni őket.
namespace Autoszerelo_Szerver.Repositories
{

    public static class WorkRepository
    {
        public static IList<Work> GetWorks()
        {
            var appDataPath = GetAppDataPath();
            if (File.Exists(appDataPath))
            {
                var rawContent = File.ReadAllText(appDataPath);
                //deszerializációs mádiával lesz a beolvasott json-ből work-ök egy listája
                var works = JsonSerializer.Deserialize<IList<Work>>(rawContent);
                return works;
            }
            else
            {
                return new List<Work>();
            }
        }

        public static void StoreWorks(IList<Work> works)
        {
            var appDataPath = GetAppDataPath();

            //Ez lesz a string amibe belegeneráljuk a json-t
            var rawContent = JsonSerializer.Serialize(works);
            File.WriteAllText(appDataPath, rawContent);

        }

        //Visszaadja az adatfájl nevét, amelyben a work objektumokat fogjuk tárolni
        public static string GetAppDataPath()
        {
            var localAppFolder = GetLocalFolder();
            var appDataPath = Path.Combine(localAppFolder, "car_mechanic_works.json");
            return appDataPath;
        }


        public static string GetLocalFolder()
        {
            var localAppDataFolder = Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData);
            //Combine a megadott paramétereket összekombinálja egy útvonallá.
            var localAppFolder = Path.Combine(localAppDataFolder, "Car_mechanic");

            if (!Directory.Exists(localAppFolder))
            {
                Directory.CreateDirectory(localAppFolder);
            }
            return localAppFolder;
        }
    }
}
