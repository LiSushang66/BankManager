using BankManage.vm;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BankManage.model.employee {
    internal class ChangeSalaryModel : NotifyProperty {
        private decimal _txtSalary;
        private string _txtSalaryError;

        public decimal txtSalary {
            get => _txtSalary;
            set => SetProperty(ref _txtSalary, value);
        }
        public string txtSalaryError {
            get => _txtSalaryError;
            set => SetProperty(ref _txtSalaryError, value);
        }
    }
}