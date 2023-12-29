using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows;
using BankManage.model.employee;
using BankManage.dao.impl;
using BankManage.dao;
using System.Windows.Input;
using BankManage.view.employee;

namespace BankManage.vm.employee {
    internal class PayManageVm:NotifyProperty {
        private Page _curPage;
        private EmpMapper _empMapper = new EmpMapperImpl();

        private PayManageModel _payManage = new PayManageModel();

        public PayManageModel payManage {
            get => _payManage;
            set => SetProperty(ref _payManage, value);
        }

        //页面初始化
        public ICommand Page_Loaded { get; set; }
        private void ExecutePage_Loaded(object obj) {
            payManage.employee_DataGrid = _empMapper.GetEmp();
        }


        //编辑按钮
        public ICommand EditSalaryButton_Click { get; set; }
        private void ExecuteEditSalaryButton_Click(object obj) {
            EmployeeInfo selectedEmployee = payManage.selectedItem as EmployeeInfo;

            if (selectedEmployee != null) {

                _curPage.NavigationService.Navigate(new ChangeSalary(selectedEmployee));

            } else {
                MessageBox.Show("请选择需要编辑薪水的职员", "提示", MessageBoxButton.OK, MessageBoxImage.Information);
            }
            payManage.employee_DataGrid= _empMapper.GetEmp();
        }

        public PayManageVm(Page curPage) {
            _curPage = curPage;


            Page_Loaded = new RelayCommand(ExecutePage_Loaded);
            EditSalaryButton_Click = new RelayCommand(ExecuteEditSalaryButton_Click);
        }
    }
}
