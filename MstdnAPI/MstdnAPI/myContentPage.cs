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
        protected string pToken;

        protected bool flg = false;

        public MyContentPage() : base() {
            NavigationPage.SetHasNavigationBar(this, false);
        }
        protected override void OnAppearing() {
            base.OnAppearing();

        }
        protected void moveLogin() {
            Navigation.PushAsync(new Login());
        }
        protected void moveMain() {
            Navigation.PushAsync(new MainPage());
        }

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
            IEnumerable<AppKey> userMaster = db.GetAppKeyMaster();
            foreach (AppKey data in userMaster) {
                this.pClientId = data.ClientId;
                this.pClientSec = data.ClientSec;
                this.pToken = data.Token;
                flg = true;
            }
        }

        protected async void ErrorProc(Exception e) {
            try {
            } catch (Exception ex) {

            } finally {
                await DisplayAlert("エラー", e.Message, "OK");
                MoveToRootPage();
            }
        }
        protected virtual void MoveToRootPage() {
            if (Navigation.NavigationStack.Count > 1) {
                Navigation.PopToRootAsync();
            } else {
                Navigation.PushAsync(new TopPage());
            }
        }
    }
}
