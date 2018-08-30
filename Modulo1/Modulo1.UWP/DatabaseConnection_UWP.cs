using Modulo1.Infraestructure;
using Modulo1.UWP;
using SQLite.Net;
using SQLite.Net.Platform.WinRT;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.Storage;

// Registro de dependência - projeto PCL requisitará esta classe com esta instrução
[assembly: Xamarin.Forms.Dependency(typeof(DatabaseConnection_UWP))]
namespace Modulo1.UWP
{
    class DatabaseConnection_UWP : IDatabaseConnection
    {
        public SQLiteConnection DbConnection()
        {
            var dbName = "CCFoodsDb.db3";
            string path = Path.Combine(ApplicationData.Current.LocalFolder.Path, dbName);
            var platform = new SQLitePlatformWinRT();
            return new SQLiteConnection(platform, path);
        }
    }
}
