using BankManage.vm;
using System;

namespace BankManage.model {
    internal class MainModel:NotifyProperty {
        private Uri _uri;
        private string _employeeName;
        private byte[] _imgPhoto;

        public Uri uri {
            get => _uri;
            set {
                _uri = value;
                OnPropertyChanged();
            }
        }
        public string employeeName {
            get => _employeeName;
            set => SetProperty(ref _employeeName, value);
        }
        public byte[] imgPhoto {
            get => _imgPhoto;
            set => SetProperty(ref _imgPhoto, value);
        }
    }
}
