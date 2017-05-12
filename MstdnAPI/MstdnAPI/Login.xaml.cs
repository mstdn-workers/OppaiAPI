using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;

namespace MstdnAPI {
    public partial class Login : MyContentPage {
        private bool moveFlg = false;
        private PrefViewModel _vm;


        public Login() {
            InitializeComponent();
            this.Submit.Clicked += submitButton;
            this.Cancel.Clicked += cancelButton;
            this.Reset.Clicked += resetButton;
        }

        // 起動時処理
        protected async override void OnAppearing() {
            base.OnAppearing();
            getUser();

            _vm = new PrefViewModel();
            this.BindingContext = _vm;
            _vm.USER_ID = pUser;
            _vm.PASSWORD = pPass;

            try {
            } catch (Exception exception) {
            }
        }

        private void UpdateDb() {
            try {
                AccessSQLite db = new AccessSQLite();
                UserData ud = new UserData();
                ud.UserId = this.User.Text;
                ud.Password = this.Password.Text;
                db.DeleteUserMaster();
                db.InsertUserMaster(ud);
            } catch (Exception e) {
                throw e;
            }
        }

        private async void submitButton(object sender, EventArgs e) {
            try {
                if (moveFlg) return;
                moveFlg = true;
                var rtn = await DisplayAlert("確認", "設定を保存します", DefaultValue.MSG_OK, DefaultValue.MSG_CANCEL);
                if (!rtn) {
                    moveFlg = false;
                    return;
                }
                UpdateDb();
                MoveToRootPage();
            } catch (Exception exception) {
                ErrorProc(exception);
                return;
            }
        }

        private async void cancelButton(object sender, EventArgs e) {
            try {
                if (moveFlg) return;
                moveFlg = true;
                var rtn = await DisplayAlert("確認", "編集中の設定を破棄して戻ります", DefaultValue.MSG_OK, DefaultValue.MSG_CANCEL);
                if (!rtn) {
                    moveFlg = false;
                    return;
                }

                MoveToRootPage();
            } catch (Exception exception) {
                ErrorProc(exception);
                return;
            }
        }
        private async void resetButton(object sender, EventArgs e) {
            try {
                if (moveFlg) return;
                moveFlg = true;
                var rtn = await DisplayAlert("確認", "編集中の設定を破棄します", DefaultValue.MSG_OK, DefaultValue.MSG_CANCEL);
                if (!rtn) {
                    moveFlg = false;
                    return;
                }

                this._vm.USER_ID = this.pUser;
                this._vm.PASSWORD = this.pPass;

                moveFlg = false;
            } catch (Exception exception) {
                ErrorProc(exception);
                return;
            }
        }

    }
}
