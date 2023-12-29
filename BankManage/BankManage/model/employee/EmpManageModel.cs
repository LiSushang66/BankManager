using BankManage.vm;
using System.Collections.Generic;

namespace BankManage.model.employee {
    internal class EmpManageModel : NotifyProperty {
        private IEnumerable<EmployeeInfo> _employee_DataGrid;
        private object _selectedItem;
        public IEnumerable<EmployeeInfo> employee_DataGrid {
            get => _employee_DataGrid;
            set => SetProperty(ref _employee_DataGrid, value);
        }
        public object selectedItem {
            get => _selectedItem;
            set => SetProperty(ref _selectedItem, value);
        }
    }
}
