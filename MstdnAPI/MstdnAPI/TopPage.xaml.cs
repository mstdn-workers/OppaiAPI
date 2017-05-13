using System;

using Xamarin.Forms;

namespace MstdnAPI {
    public partial class TopPage : MyContentPage {
        public TopPage() {
            InitializeComponent();
        }

        // 起動時処理
        protected async override void OnAppearing() {
            base.OnAppearing();
            try {
                var db = new AccessSQLite();
                getUser();
                if (!flg) {
                    MoveToLoginPage(DefaultValue.FST_SETTING_MSG);
                    return;
                }
                // 初回のみアプリＩＤの取得
                var access = new HttpRequest();
                getAppKey();
                if (!flg) {
                    try {
                        var client = await access.getClientAppJson(DefaultValue.MSTDN_HOST);
                        setAppKey(db, client);
                    } catch (Exception ex) {
                        MoveToLoginPage(DefaultValue.ERR_SETTING_MSG);
                        return;
                    }
                    getAppKey();
                }

                // 起動時にアクセストークンを取得する
                try {
                    var token = await access.getTokenJson(DefaultValue.MSTDN_HOST, pClientId, pClientSec, pUser, pPass);
                    setAccessToken(db, token);
                } catch (Exception ex) {
                    MoveToLoginPage(DefaultValue.ERR_SETTING_MSG);
                    return;
                }
            } catch (Exception exception) {
                ErrorProc(exception);
            }
            moveMain();
        }

        // ＤＢへ登録
        private void setAppKey(AccessSQLite db, ClientAppJson client) {
            var app = new AppKey();
            app.ClientId = client.client_id;
            app.ClientSec = client.client_secret;
            db.DeleteAppKeyMaster();
            db.InsertAppKeyMaster(app);
        }

        private void setAccessToken(AccessSQLite db, AccessTokenJson token) {
            var tok = new AccessToken();
            tok.Host = DefaultValue.MSTDN_HOST;
            tok.Token = token.access_token;
            db.DeleteAccessTokenMaster();
            db.InsertAccessTokenMaster(tok);
        }
    }
}
