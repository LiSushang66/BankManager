using BankManage.vm;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media.Imaging;

namespace BankManage.model.employee {
    internal class AddEmpModel : NotifyProperty {

        private string _txtEmployeeId;
        private string _txtEmployeeName;
        private string _password;
        private string _passwordConfirm;
        private string _txtSex;
        private decimal _txtSalary;
        private string _txtTelphone;
        private string _txtIdCard;
        private byte[] _imgPhoto;

        private string _txtEmployeeIdError;
        private string _txtEmployeeNameError;
        private string _passwordError;
        private string _passwordConfirmError;
        private string _txtSexError;
        private string _txtSalaryError;
        private string _txtTelphoneError;
        private string _txtIdCardError;

        public string txtEmployeeId {
            get => _txtEmployeeId;
            set => SetProperty(ref _txtEmployeeId, value);
        }
        public string txtEmployeeName {
            get => _txtEmployeeName;
            set => SetProperty(ref _txtEmployeeName, value);
        }
        public string password {
            get => _password;
            set => SetProperty(ref _password, value);
        }
        public string passwordConfirm {
            get => _passwordConfirm;
            set => SetProperty(ref _passwordConfirm, value);
        }
        public string txtSex {
            get => _txtSex;
            set => SetProperty(ref _txtSex, value);
        }

        public decimal txtSalary{
            get => _txtSalary;
            set => SetProperty(ref _txtSalary, value);
        }

        public string txtTelphone {
            get => _txtTelphone;
            set => SetProperty(ref _txtTelphone, value);
        }
        public string txtIdCard {
            get => _txtIdCard;
            set => SetProperty(ref _txtIdCard, value);
        }
        public byte[] imgPhoto {
            get => _imgPhoto;
            set => SetProperty(ref _imgPhoto, value);
        }



        public string txtEmployeeIdError {
            get => _txtEmployeeIdError;
            set => SetProperty(ref _txtEmployeeIdError, value);
        }
        public string txtEmployeeNameError {
            get => _txtEmployeeNameError;
            set => SetProperty(ref _txtEmployeeNameError, value);
        }
        public string passwordError {
            get => _passwordError;
            set => SetProperty(ref _passwordError, value);
        }
        public string passwordConfirmError {
            get => _passwordConfirmError;
            set => SetProperty(ref _passwordConfirmError, value);
        }
        public string txtSexError {
            get => _txtSexError;
            set => SetProperty(ref _txtSexError, value);
        }

        public string txtSalaryError {
            get => _txtSalaryError;
            set => SetProperty(ref _txtSalaryError, value);
        }

        public string txtTelphoneError {
            get => _txtTelphoneError;
            set => SetProperty(ref _txtTelphoneError, value);
        }
        public string txtIdCardError {
            get => _txtIdCardError;
            set => SetProperty(ref _txtIdCardError, value);
        }

    }
}
