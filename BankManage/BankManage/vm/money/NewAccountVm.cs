using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Globalization;
using System.Windows.Controls;
using System.Windows;
using System.Windows.Navigation;
using System.Windows.Input;
using BankManage.domain;
using BankManage.view.money;

namespace BankManage.vm.money
{
    internal class NewAccountVm : NotifyProperty, IValidationExceptionHandler
    {
        private Page _curPage;

        private string _accountName;
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
        private string _accountNo;
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

        private string _ID;
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

        private double _accMoney;
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

        private string _Pass;
        public string Pass
        {
            get => _Pass;
            set
            {
                if (_Pass != value)
                {
                    _Pass = value;
                    OnPropertyChanged();
                }
            }
        }

        private bool _IsValid;
        public bool IsValid
        {
            get => _IsValid;
            set
            {
                if (_IsValid != value)
                {
                    _IsValid = value;
                }
            }
        }

        private object _accType;
        public object accType
        {
            get => _accType;
            set
            {
                if (_accType != value)
                {
                    _accType = value;
                }
            }
        }

        private object _rateType;
        public object rateType
        {
            get => _rateType;
            set
            {
                if (_rateType != value)
                {
                    _rateType = value;
                }
            }
        }


        public NewAccountVm(Page curPage)
        {
            _curPage = curPage;
            Confirm_Upload = new RelayCommand(ExecuteConfirm_Upload);
        }

        public ICommand Confirm_Upload { get; set; }
        private void ExecuteConfirm_Upload(object obj)
        {
            if (accType == null)
            {
                MessageBox.Show("请选择开户类型");
                return;
            }
            if (rateType == null)
            {
                MessageBox.Show("请选择利率");
                return;
            }
            if (IsValid==false)
            {
                MessageBox.Show("验证失败请检查输入" +
                    "\n     Tips:" +
                    "\n身份证密码需要手动输入");
                return;
            }

            Custom custom = new Custom();
            custom.AccountInfo.accountNo = accountNo;
            custom.AccountInfo.IdCard = ID;
            custom.AccountInfo.accountName = accountName;
            custom.AccountInfo.accountPass = Pass;
            custom.AccountInfo.rateType = rateType.ToString();
            custom.AccountInfo.accountType = accType.ToString();
            custom.AccountBalance = accMoney;

            _curPage.NavigationService.Navigate(new AccountChecking(custom));
        }
    }
}
