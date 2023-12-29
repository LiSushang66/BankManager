using BankManage.dao;
using BankManage.dao.impl;
using BankManage.model.employee;
using System;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace BankManage.vm.employee {
    internal class ChangeSalaryVm : NotifyProperty{

        private Page _curPage;
        private EmployeeInfo _curEmployee;

        private EmpMapper _empMapper = new EmpMapperImpl();

        private ChangeSalaryModel _changeSalary = new ChangeSalaryModel();

        public ChangeSalaryModel changeSalary {
            get =>_changeSalary; 
            set => SetProperty(ref _changeSalary ,value);
        }

        public string salary {
            get => changeSalary.txtSalary.ToString();
            set {
                changeSalary.txtSalary = decimal.TryParse(value, out _) ? decimal.Parse(value) : changeSalary.txtSalary;
                OnPropertyChanged();
            }
        }



        // 加载当前雇员信息
        public ICommand Page_Loaded { get;set; }
        private void ExecutePage_Loaded(object obj) {
            salary = _empMapper.GetEmp(_curEmployee.EmployeeNo).First().Salary.ToString();
        }

        //保存修改
        public ICommand SaveButton_Click { get; set; }
        private void ExecuteSaveButton_Click(object obj) {
            // 清除之前的错误提示
            changeSalary.txtSalaryError = string.Empty;

            // 验证用户输入
            if (!decimal.TryParse(salary, out _)) {
                changeSalary.txtSalaryError = "请输入有效的薪水信息";
                return;
            }

            // 保存修改后的薪水信息到数据库
            if (_empMapper.UpdateEmp(_curEmployee.EmployeeNo, changeSalary.txtSalary)) {
                // 提示保存成功
                MessageBox.Show("薪水信息保存成功！", "成功", MessageBoxButton.OK, MessageBoxImage.Information);
                // 返回上一页
                _curPage.NavigationService.Navigate(new Uri("view/employee/PayManage.xaml", UriKind.Relative));
            } else {
                // 提示保存失败
                MessageBox.Show("薪水信息保存失败！", "失败", MessageBoxButton.OK, MessageBoxImage.Information);
            }
        }

        //取消
        public ICommand CancelButton_Click { get; set; }
        private void ExecuteCancelButton_Click(object obj) {
            _curPage.NavigationService.GoBack();
        }

        public ChangeSalaryVm(Page curPage,EmployeeInfo employee) {
            _curPage = curPage;
            _curEmployee = employee;

            Page_Loaded = new RelayCommand(ExecutePage_Loaded);
            SaveButton_Click = new RelayCommand(ExecuteSaveButton_Click);
            CancelButton_Click = new RelayCommand(ExecuteCancelButton_Click);
        }
    }
}
