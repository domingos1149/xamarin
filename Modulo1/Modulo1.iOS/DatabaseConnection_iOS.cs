﻿using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

using Foundation;
using Modulo1.Infraestructure;
using Modulo1.iOS;
using SQLite.Net;
using SQLite.Net.Platform.XamarinIOS;
using UIKit;

// Registro de dependência - projeto PCL requisitará esta classe com esta instrução
[assembly: Xamarin.Forms.Dependency(typeof(DatabaseConnection_iOS))]
namespace Modulo1.iOS
{
    public class DatabaseConnection_iOS : IDatabaseConnection
    {
        public SQLiteConnection DbConnection()
        {
            var dbName = "CCFoodsDb.db3";
            string personalFolder = Environment.GetFolderPath(Environment.SpecialFolder.Personal);
            string libraryFolder = Path.Combine(personalFolder, ".", "Library");
            var path = Path.Combine(libraryFolder, dbName);
            var platform = new SQLitePlatformIOS();
            return new SQLiteConnection(platform, path);
        }
    }
}