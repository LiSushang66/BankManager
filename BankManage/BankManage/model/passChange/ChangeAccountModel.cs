using BankManage.vm;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BankManage.model.passChange {
    internal class ChangeAccountModel : NotifyProperty {
        private string _txtAccount;
        private string _txtNewPass;
        private string _txtPassConf;
        public string txtAccount {
            get => _txtAccount;
            set => SetProperty(ref _txtAccount, value);
        }
        public string txtNewPass {
            get => _txtNewPass;
            set => SetProperty(ref _txtNewPass, value);
        }
        public string txtPassConf {
            get => _txtPassConf;
            set => SetProperty(ref _txtPassConf, value);
        }
    }
}
