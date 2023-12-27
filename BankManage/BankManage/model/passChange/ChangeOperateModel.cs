using BankManage.vm;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BankManage.model.passChange {
    internal class ChangeOperateModel : NotifyProperty{
        private string _txtEmployee;
        private string _txtNewPass;
        private string _txtPassConf;
        public string txtEmployee {
            get => _txtEmployee;
            set => SetProperty(ref _txtEmployee, value);
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
