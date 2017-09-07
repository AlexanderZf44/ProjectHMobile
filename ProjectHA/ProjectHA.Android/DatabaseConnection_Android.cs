using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;

using SQLite;
using System.IO;
using ProjectHA.Droid;

[assembly: Xamarin.Forms.Dependency(typeof(
  DatabaseConnection_Android))]
namespace ProjectHA.Droid
{
    public class DatabaseConnection_Android : IDatabaseConnection    
    {
        public SQLiteConnection DbConnection()
        {
            var dbName = "cllCtrN.sqlit";
            var path = Path.Combine("/sdcard/Download", dbName);
            return new SQLiteConnection(path);
        }
    }
}