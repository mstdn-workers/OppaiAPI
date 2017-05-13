using SQLite.Net;


namespace MstdnAPI {
    public interface ISQLite {
        SQLiteConnection GetConnection();
    }

    public interface IToast {
        void Show(string message);
    }
}
