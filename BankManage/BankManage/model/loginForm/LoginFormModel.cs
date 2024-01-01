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
        private string _veriCode;
        private ImageSource _captcha;
        private List<string> _txtComboxItem = new List<string>();
        public string txtCombox {
            get => _txtCombox;
            set => SetProperty(ref _txtCombox, value);
        }
        public string password {
            get => _password;
            set => SetProperty(ref _password, value);
        }
        public string veriCode {
            get => _veriCode;
            set => SetProperty(ref _veriCode, value);
        }
        public ImageSource captcha {
            get => _captcha;
            set => SetProperty(ref _captcha, value);
        }
        public List<string> txtComboxItem {
            get => _txtComboxItem;
            set => SetProperty(ref _txtComboxItem, value);
        }
    }
}
