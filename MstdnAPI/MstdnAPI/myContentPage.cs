using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Emit;
using System.Text;

using Xamarin.Forms;

namespace MstdnAPI {
    public class MyContentPage : ContentPage {
        protected string pUser;
        protected string pPass;
        protected string pClientId;
        protected string pClientSec;
        protected string pHost;
        protected string pToken;

        protected bool flg = false;

        public MyContentPage() : base() {
            NavigationPage.SetHasNavigationBar(this, false);
        }
        protected override void OnAppearing() {
            base.OnAppearing();

        }

        // ページ移動処理
        protected void moveMain() {
            Navigation.PushAsync(new MainPage());
        }
        protected void MoveToLoginPage() {
            Navigation.PushAsync(new Login());
        }
        protected async void MoveToLoginPage(string msg) {
            await DisplayAlert("", msg, DefaultValue.MSG_OK);
            MoveToLoginPage();
        }
        protected virtual void MoveToRootPage() {
            if (Navigation.NavigationStack.Count > 1) {
                Navigation.PopToRootAsync();
            } else {
                Navigation.PushAsync(new TopPage());
            }
        }

        // SQLiteからデータを取得
        protected void getUser() {
            flg = false;
            AccessSQLite db = new AccessSQLite();
            IEnumerable<UserData> userMaster = db.GetUserMaster();
            foreach (UserData data in userMaster) {
                this.pUser = data.UserId;
                this.pPass = data.Password;
                flg = true;
            }
        }

        protected void getAppKey() {
            flg = false;
            AccessSQLite db = new AccessSQLite();
            IEnumerable<AppKey> appData = db.GetAppKeyMaster();
            foreach (AppKey data in appData) {
                this.pClientId = data.ClientId;
                this.pClientSec = data.ClientSec;
                flg = true;
            }
        }

        protected void getAccessToken() {
            flg = false;
            AccessSQLite db = new AccessSQLite();
            IEnumerable<AccessToken> token = db.GetAccessTokenMaster();
            foreach (AccessToken data in token) {
                this.pHost = data.Host;
                this.pToken = data.Token;
                flg = true;
            }
        }

        // エラー処理
        protected async void ErrorProc(Exception e) {
            Toast(e.Message);
            MoveToRootPage();
        }

        protected void Toast(string msg) {
            DependencyService.Get<IToast>().Show(msg);
        }
    }
}
