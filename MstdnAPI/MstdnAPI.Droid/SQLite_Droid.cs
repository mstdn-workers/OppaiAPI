using System.IO;
using MstdnAPI;
using MstdnAPI.Droid;
using Xamarin.Forms;

[assembly: Dependency(typeof(SQLite_Droid))]

namespace MstdnAPI.Droid {
    public class SQLite_Droid : ISQLite {
        public SQLite.Net.SQLiteConnection GetConnection() {
            const string sqliteFileName = "MstdnAPI.db3";
            var documentPath
                = System.Environment.GetFolderPath(System.Environment.SpecialFolder.Personal);

            var path = Path.Combine(documentPath, sqliteFileName);
            var pf = new SQLite.Net.Platform.XamarinAndroid.SQLitePlatformAndroid();
            var conn = new SQLite.Net.SQLiteConnection(pf, path);

            return conn;
        }
    }
}