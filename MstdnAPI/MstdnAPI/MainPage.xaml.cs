using System;

using Xamarin.Forms;

namespace MstdnAPI {
    public partial class MainPage : MyContentPage {
        private HttpRequest pAccess;

        public MainPage() {
            InitializeComponent();
            this.Oppai.Clicked += sendMsgOppai;
            this.Mer.Clicked += sendMsgMer;
            this.Toot.Clicked += sendMsgToot;
            this.Logiin.Clicked += moveLogin;
        }
        // 起動時処理
        protected async override void OnAppearing() {
            base.OnAppearing();
            try {
                getUser();

                if (!flg) {
                    MoveToLoginPage(DefaultValue.ERR_SETTING_MSG);
                    return;
                }
                getAppKey();
                if (!flg) {
                    MoveToLoginPage(DefaultValue.ERR_SETTING_MSG);
                    return;
                }
                getAccessToken();
                if (!flg) {
                    MoveToLoginPage(DefaultValue.ERR_SETTING_MSG);
                    return;
                }
            } catch (Exception exception) {
                ErrorProc(exception);
            }
        }

        // ボタン押下時処理
        private void sendMsgOppai(object send, EventArgs e) {
            sendMsg(DefaultValue.MSG_OPPAI);
        }
        private void sendMsgMer(object send, EventArgs e) {
            sendMsg(DefaultValue.MSG_MER);
        }
        private void sendMsgToot(object send, EventArgs e) {
            if (this.TootMsg.Text == "") {
                Toast(DefaultValue.STATUS_NULL_MSG);
            } else if(this.TootMsg.Text.Length > DefaultValue.MAX_CHARS) {
                Toast(DefaultValue.STATUS_NULL_MSG);
            } else {
                sendMsg(this.TootMsg.Text);
            }
        }
        private void moveLogin(object send, EventArgs e) {
            MoveToLoginPage();
        }

        // トゥート処理
        private async void sendMsg(string msg) {
            pAccess = new HttpRequest();
            var response = await pAccess.tootMsg(pHost, pToken, msg);
            if (response.IsSuccessStatusCode) {
                Toast(DefaultValue.STATUS_SUCCESS_MSG);
                this.TootMsg.Text = "";
            } else {
                Toast(DefaultValue.STATUS_ERROR_MSG + "\n" + response.RequestMessage);
            }
            pAccess = null;
            GC.Collect();
        }
    }
}
