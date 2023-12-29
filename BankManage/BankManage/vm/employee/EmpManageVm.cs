using BankManage.view.employee;
using System.Windows;
using System.Windows.Controls;
using BankManage.model.employee;
using BankManage.dao;
using BankManage.dao.impl;
using System.Windows.Input;

namespace BankManage.vm.employee {
    internal class EmpManageVm : NotifyProperty {

        private Page _curPage;
        private EmpMapper _empMapper = new EmpMapperImpl();

        private EmpManageModel _empManage = new EmpManageModel();

        public EmpManageModel empManage {
            get => _empManage;
            set => SetProperty(ref _empManage, value);
        }

        // 加载职员数据到 DataGrid
        public ICommand Page_Loaded { get; set; }
        private void ExecutePage_Loaded(object obj) {
                empManage.employee_DataGrid = _empMapper.GetEmp();
        }


        // 添加新职员按钮点击事件
        public ICommand AddEmployeeButton_Click { get; set; }
        private void ExecuteAddEmployeeButton_Click(object obj) {
            // 在 Frame 中导航到 AddPage
            _curPage.NavigationService.Navigate(new AddEmp());
            empManage.employee_DataGrid = _empMapper.GetEmp();

        }

        // 删除按钮点击事件
        public ICommand DeleteButton_Click { get; set; }
        private void ExecuteDeleteButton_Click(object obj) {
            // 检查是否选择了职员
            EmployeeInfo selectedEmployee = empManage.selectedItem as EmployeeInfo;

            if (selectedEmployee != null) {
                // 在执行删除前与用户确认
                MessageBoxResult result = MessageBox.Show($"确认删除职员 {selectedEmployee.EmployeeName} 的信息吗？",
                                                          "确认删除",
                                                          MessageBoxButton.YesNo,
                                                          MessageBoxImage.Question);

                if (result == MessageBoxResult.Yes) {
                    // 执行删除操作
                    if (_empMapper.DeleteEmp(selectedEmployee.EmployeeNo)) {
                        MessageBox.Show("删除成功", "提示", MessageBoxButton.OK, MessageBoxImage.Information);
                    } else {
                        MessageBox.Show("删除失败", "提示", MessageBoxButton.OK, MessageBoxImage.Information);
                    }
                }
            } else {
                MessageBox.Show("请选择需要删除信息的职员", "提示", MessageBoxButton.OK, MessageBoxImage.Information);
            }
            empManage.employee_DataGrid = _empMapper.GetEmp();
        }

        // 编辑按钮点击事件
        public ICommand EditButton_Click { get; set; }
        private void ExecuteEditButton_Click(object obj) {
            // 检查是否选择了职员
            EmployeeInfo selectedEmployee = empManage.selectedItem as EmployeeInfo;

            if (selectedEmployee != null) {
                // 打开 EditPage 页面以编辑选定的职员信息
                _curPage.NavigationService.Navigate(new EditEmp(selectedEmployee));
            } else {
                MessageBox.Show("请选择需要编辑信息的职员", "提示", MessageBoxButton.OK, MessageBoxImage.Information);
            }
            empManage.employee_DataGrid = _empMapper.GetEmp();
        }

        // 查看详细信息按钮点击事件
        public ICommand DetailButton_Click { get; set; }
        private void ExecuteDetailButton_Click(object obj) {
            // 检查是否选择了职员
            EmployeeInfo selectedEmployee = empManage.selectedItem as EmployeeInfo;

            if (selectedEmployee != null) {
                // 打开 ViewPage 页面以查看选定的职员详细信息
                _curPage.NavigationService.Navigate(new ViewEmp(selectedEmployee));
            } else {
                MessageBox.Show("请选择所要查看信息的职员", "提示", MessageBoxButton.OK, MessageBoxImage.Information);
            }
        }
        public EmpManageVm(Page curPage) {
            _curPage = curPage;

            Page_Loaded = new RelayCommand(ExecutePage_Loaded);
            AddEmployeeButton_Click = new RelayCommand(ExecuteAddEmployeeButton_Click);
            DeleteButton_Click = new RelayCommand(ExecuteDeleteButton_Click);
            EditButton_Click = new RelayCommand(ExecuteEditButton_Click);
            DetailButton_Click = new RelayCommand(ExecuteDetailButton_Click);
        }
    }
}
