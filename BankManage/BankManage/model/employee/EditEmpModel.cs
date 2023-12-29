using BankManage.vm;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BankManage.model.employee {
    internal class EditEmpModel:NotifyProperty {
        private string _txtEmployeeNo;
        private string _txtEmployeeName;
        private string _txtSex;
        private decimal _txtSalary;
        private string _txtTelphone;
        private string _txtIdCard;
        private byte[] _imgPhoto;


        private string _txtEmployeeNoError;
        private string _txtEmployeeNameError;
        private string _txtSexError;
        private string _txtSalaryError;
        private string _txtTelphoneError;
        private string _txtIdCardError;



        public string txtEmployeeNo {
            get => _txtEmployeeNo;
            set => SetProperty(ref _txtEmployeeNo, value);
        }
        public string txtEmployeeName {
            get => _txtEmployeeName;
            set => SetProperty(ref _txtEmployeeName, value);
        }
        public string txtSex {
            get => _txtSex;
            set => SetProperty(ref _txtSex, value);
        }
        public decimal txtSalary {
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
        public string txtEmployeeNoError {
            get => _txtEmployeeNoError;
            set => SetProperty(ref _txtEmployeeNoError, value);
        }
        public string txtEmployeeNameError {
            get => _txtEmployeeNameError;
            set => SetProperty(ref _txtEmployeeNameError, value);
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
