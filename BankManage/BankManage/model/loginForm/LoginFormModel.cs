using BankManage.component.pagination.pagerControl;
using BankManage.vm;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media;

namespace BankManage.model.loginForm {
    internal class LoginFormModel : NotifyProperty {
        private string _txtCombox;
        private string _password;
        private ImageSource _captcha;
        private List<string> _txtComboxItem;
        public string txtCombox {
            get => _txtCombox;
            set {
                if (value == _txtCombox)
                    return;
                _txtCombox = value;
                OnPropertyChanged();
            }
        }
        public string password {
            get => _password;
            set {
                if (value == _password)
                    return;
                _password = value;
                OnPropertyChanged();
            }
        }
        public ImageSource captcha {
            get => _captcha;
            set {
                if (value == _captcha)
                    return;
                _captcha = value;
                OnPropertyChanged();
            }
        }
        public List<string> txtComboxItem {
            get => _txtComboxItem;
            set {
                if (value == _txtComboxItem)
                    return;
                _txtComboxItem = value;
                OnPropertyChanged();
            }
        }
    }
}
