using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Globalization;

namespace BankManage.vm.money {
    internal class NewAccountVm : NotifyProperty {

        public string _accountName;
        public string accountName
        {
            get => _accountName;
            set
            {
                if (_accountName != value)
                {
                    _accountName = value;
                    OnPropertyChanged();
                }
            }
        }
        public string _accountNo;
        public string accountNo
        {
            get => _accountNo;
            set
            {
                if (_accountNo != value)
                {
                    _accountNo = value;
                    OnPropertyChanged();
                }
            }
        }

        public string _ID;
        public string ID
        {
            get => _ID;
            set
            {
                if (_ID != value)
                {
                    _ID = value;
                    OnPropertyChanged();
                }
            }
        }

        public double _accMoney;
        public double accMoney
        {
            get => _accMoney;
            set
            {
                if (_accMoney != value)
                {
                    _accMoney = value;
                    OnPropertyChanged();
                }
            }
        }
    }
}
