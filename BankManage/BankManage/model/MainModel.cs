using BankManage.vm;
using System;

namespace BankManage.model {
    internal class MainModel:NotifyProperty {
        private Uri _uri;

        public Uri uri {
            get => _uri;
            set {
                if (value == _uri)
                    return;
                _uri = value;
                OnPropertyChanged();
            }
        }
    }
}
