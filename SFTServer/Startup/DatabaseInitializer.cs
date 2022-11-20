using DAL;
using Microsoft.EntityFrameworkCore;

namespace SFTServer.Startup
{
    public static class DatabaseInitializer
    {
        private static readonly string appDataPath =
            Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData), "FileTagger");

        public static void Init()
        {
            if (!Directory.Exists(appDataPath))
            {
                Directory.CreateDirectory(appDataPath);
            }

            using var context = new TaggerContext();
            context.Database.Migrate();
        }
    }
}
