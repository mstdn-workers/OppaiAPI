using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Emit;
using System.Text;
using SQLite.Net;
using Xamarin.Forms;

namespace MstdnAPI {
    public class AccessSQLite {
        static readonly object Locker = new object();
        readonly SQLiteConnection _db;

        public AccessSQLite() {
            try {
                _db = DependencyService.Get<ISQLite>().GetConnection();
                _db.CreateTable<UserData>();
            } catch (Exception exception) {
                throw exception;
            }
        }

        // ユーザデータの抽出
        public IEnumerable<UserData> GetUserMaster() {
            lock (Locker) {
                try {
                    return _db.Table<UserData>();
                } catch (Exception exception) {
                    throw exception;
                }
            }
        }

        // ユーザデータの挿入
        public int InsertUserMaster(UserData item) {
            lock (Locker) {
                try {
                    return _db.Insert(item);
                } catch (Exception exception) {
                    throw exception;
                }
            }
        }

        // ユーザデータの削除
        public int DeleteUserMaster() {
            lock (Locker) {
                try {
                    return _db.DeleteAll<UserData>();
                } catch (Exception exception) {
                    throw exception;
                }
            }
        }

        // APPKEYデータの抽出
        public IEnumerable<AppKey> GetAppKeyMaster() {
            lock (Locker) {
                try {
                    return _db.Table<AppKey>();
                } catch (Exception exception) {
                    throw exception;
                }
            }
        }

        // APPKEYデータの挿入
        public int InsertAppKeyMaster(AppKey item) {
            lock (Locker) {
                try {
                    return _db.Insert(item);
                } catch (Exception exception) {
                    throw exception;
                }
            }
        }

        // APPKEYデータの削除
        public int DeleteAppKeyMaster() {
            lock (Locker) {
                try {
                    return _db.DeleteAll<AppKey>();
                } catch (Exception exception) {
                    throw exception;
                }
            }
        }


    }
}
