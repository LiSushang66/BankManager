using BankManage.vm;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BankManage.model.employee {
    internal class PayManageModel:NotifyProperty {
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
