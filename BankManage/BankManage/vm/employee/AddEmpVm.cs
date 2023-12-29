using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows;
using System.Runtime.CompilerServices;
using System.Windows.Controls;
using BankManage.utils;
using MaterialDesignThemes.Wpf;
using BankManage.model.employee;
using BankManage.dao;
using BankManage.dao.impl;
using BankManage.view.employee;
using System.Windows.Input;
using BankManage.component.pagination.pagerControl;

namespace BankManage.vm.employee {
    internal class AddEmpVm : NotifyProperty {
        private Page _curPage;

        private EmpMapper _empMapper = new EmpMapperImpl();

        private AddEmpModel _addEmp = new AddEmpModel();

        public AddEmpModel addEmp {
            get => _addEmp;
            set => SetProperty(ref _addEmp, value);
        }


        public BitmapImage image {
            get => FileUtils.ByteToImage(addEmp.imgPhoto);
            set {
                addEmp.imgPhoto = FileUtils.ImageToByte(value);
                OnPropertyChanged();
            }
        }


        public string salary {
            get => addEmp.txtSalary.ToString();
            set {
                addEmp.txtSalary = decimal.TryParse(value, out _) ? decimal.Parse(value) : addEmp.txtSalary;
                OnPropertyChanged();
            }
        }

        //选择本地图片提交
        public ICommand SelectPhotoButton_Click { get; set; }
        private void ExecuteSelectPhotoButton_Click(object obj) {
            image = FileUtils.UploadPicture();
        }

        //确认提交
        public ICommand ConfirmButton_Click { get; set; }
        private void ExecuteConfirmButton_Click(object obj) {
            // 清除所有错误提示
            addEmp.txtEmployeeIdError=string.Empty;
            addEmp.txtEmployeeNameError = string.Empty;
            addEmp.passwordError = string.Empty;
            addEmp.txtSexError = string.Empty;
            addEmp.txtSalaryError = string.Empty;
            addEmp.txtTelphoneError = string.Empty;
            addEmp.txtIdCardError = string.Empty;

            // 检查是否有空白的信息
            if (string.IsNullOrWhiteSpace(addEmp.txtEmployeeId)
                || string.IsNullOrWhiteSpace(addEmp.txtEmployeeName)
                || string.IsNullOrWhiteSpace(addEmp.password)
                || string.IsNullOrWhiteSpace(addEmp.txtSex)
                || string.IsNullOrWhiteSpace(salary.ToString())
                || string.IsNullOrWhiteSpace(addEmp.txtTelphone)
                || string.IsNullOrWhiteSpace(addEmp.txtIdCard)) {
                MessageBox.Show("请填写完整职员信息！", "提示", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }

            // 验证用户输入
            if (ValidateInput()) {

                // 将 newEmployee 插入到数据库中

                if (_empMapper.InsertEmp(addEmp.txtEmployeeId,addEmp.txtEmployeeName, addEmp.password, addEmp.txtSex, addEmp.txtSalary, addEmp.txtTelphone, addEmp.txtIdCard, addEmp.imgPhoto)) {

                    // 提示用户添加成功
                    MessageBox.Show("职员信息添加成功！", "成功", MessageBoxButton.OK, MessageBoxImage.Information);
                    _curPage.NavigationService.Navigate(new Uri("view/employee/EmpManage.xaml", UriKind.Relative));
                } else {
                    // 提示用户添加失败
                    MessageBox.Show("职员信息添加失败！", "失败", MessageBoxButton.OK, MessageBoxImage.Information);
                }
            }
        }

        //取消增加
        public ICommand CancelButton_Click { get; set; }
        private void ExecuteCancelButton_Click(object obj) {
            _curPage.NavigationService.GoBack();
        }



        public AddEmpVm(Page curPage) {
            _curPage = curPage;
            SelectPhotoButton_Click = new RelayCommand(ExecuteSelectPhotoButton_Click);
            ConfirmButton_Click = new RelayCommand(ExecuteConfirmButton_Click);
            CancelButton_Click = new RelayCommand(ExecuteCancelButton_Click);

        }

        //检查输入是否合法并给出提示
        private bool ValidateInput() {
            bool isValid = true;
            // 验证编号
            if (_empMapper.GetEmp(addEmp.txtEmployeeId).Count!=0) {
                addEmp.txtEmployeeIdError = "员工编号已存在";
                isValid = false;
            }else if(!(addEmp.txtEmployeeId.Length == 5 && int.TryParse(addEmp.txtEmployeeId, out _))) {
                addEmp.txtEmployeeIdError = "名字长度必须是5且是数字";
                isValid = false;
            }

            

            // 验证名字长度
            if (addEmp.txtEmployeeName.Length > 20) {
                addEmp.txtEmployeeNameError = "名字长度不能超过20个字符";
                isValid = false;
            }

            // 验证密码
            if (!Validate.Password(addEmp.password)) {
                addEmp.passwordError = "密码太简单";
                isValid = false;
            }

            // 验证性别
            if (addEmp.txtSex.ToLower() != "男" && addEmp.txtSex.ToLower() != "女") {
                addEmp.txtSexError = "性别必须填写为“男”或“女”";
                isValid = false;
            }

            // 验证薪水
            if (!decimal.TryParse(salary, out _)) {
                addEmp.txtSexError = "薪水必须是数字";
                isValid = false;
            }

            // 验证电话号码
            if (!Validate.IsHandset(addEmp.txtTelphone)) {
                addEmp.txtTelphoneError = "请输入有效的电话号码";
                isValid = false;
            }

            // 验证身份证号
            if (Validate.IDchecking(addEmp.txtIdCard) != 0) {
                addEmp.txtIdCardError = "请输入有效的身份证号码";
                isValid = false;
            }

            return isValid;
        }
    }
}
