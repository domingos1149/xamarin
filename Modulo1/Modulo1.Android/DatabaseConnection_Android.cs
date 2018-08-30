using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Modulo1.Droid;
using Modulo1.Infraestructure;
using SQLite.Net;
using SQLite.Net.Platform.XamarinAndroid;


// Registro de dependência - projeto PCL requisitará esta classe com esta instrução
[assembly: Xamarin.Forms.Dependency(typeof(DatabaseConnection_Android))]
namespace Modulo1.Droid
{
    public class DatabaseConnection_Android : IDatabaseConnection
    {
        public SQLiteConnection DbConnection()
        {
            var dbName = "CCFoodsDb.db3";
            string documentsFolder = System.Environment.GetFolderPath(System.Environment.SpecialFolder.Personal);
            string path = Path.Combine(documentsFolder, dbName);
            var platform = new SQLitePlatformAndroid();
            return new SQLiteConnection(platform, path);            
        }
    }
}