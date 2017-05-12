using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
                //getUser();
                //if (!flg) {
                //    await DisplayAlert("", "初期設定をしてください", DefaultValue.MSG_OK);
                //    moveLogin();
                //    return;
                //}
                //// 初回のみアクセストークンの取得
                //getAppKey();
                //if (!flg) {

                //}
            } catch (Exception exception) {

            }
            moveMain();
        }
    }
}
