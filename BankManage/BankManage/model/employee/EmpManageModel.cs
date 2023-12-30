using BankManage.vm;
using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace BankManage.model.employee {
    internal class EmpManageModel : NotifyProperty {
        private ObservableCollection<EmployeeInfo> _employee_DataGrid;
        private object _selectedItem;
        public ObservableCollection<EmployeeInfo> employee_DataGrid {
            get => _employee_DataGrid;
            set => SetProperty(ref _employee_DataGrid, value);
        }
        public object selectedItem {
            get => _selectedItem;
            set => SetProperty(ref _selectedItem, value);
        }
    }
}