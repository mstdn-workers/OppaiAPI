using System;
using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace MstdnAPI {
    public abstract class BindableBase : INotifyPropertyChanged {
        public event PropertyChangedEventHandler PropertyChanged;
        protected bool SetProperty<T>(ref T storage, T value
            , [CallerMemberName] String propertyName = null) {
            if (object.Equals(storage, value)) return false;

            storage = value;
            this.OnPropertyChanged(propertyName);
            return true;
        }
        protected void OnPropertyChanged([CallerMemberName] string propertyName = null) {
            var eventHandler = this.PropertyChanged;
            if (eventHandler != null) {
                eventHandler(this, new PropertyChangedEventArgs(propertyName));
            }
        }
    }

    /// <summary>
    /// 設定画面
    /// </summary>
    public class PrefViewModel : BindableBase {
        private string _userId;
        private string _password;

        #region Property
        public string USER_ID {
            get { return this._userId; }
            set { this.SetProperty(ref this._userId, value); }
        }
        public string PASSWORD {
            get { return this._password; }
            set { this.SetProperty(ref this._password, value); }
        }
        #endregion

        public PrefViewModel() {

        }
    }


}
