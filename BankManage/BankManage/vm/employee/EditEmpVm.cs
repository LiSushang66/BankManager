using System;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using BankManage.dao;
using BankManage.dao.impl;
using BankManage.utils;
using BankManage.view.employee;
using BankManage.model.employee;
using System.Linq;

namespace BankManage.vm.employee {
    internal class EditEmpVm : NotifyProperty {

        private Page _curPage;
        private EmployeeInfo _curEmployee;

        private EmpMapper _empMapper = new EmpMapperImpl();

        private EditEmpModel _editEmp=new EditEmpModel();
        public EditEmpModel editEmp {
            get => _editEmp;
            set => SetProperty(ref _editEmp, value);
        }

        public string salary {
            get => editEmp.txtSalary.ToString();
            set {
                editEmp.txtSalary = decimal.TryParse(value, out _) ? decimal.Parse(value) : editEmp.txtSalary;
                OnPropertyChanged();
            }
        }

        public BitmapImage image {
            get => FileUtils.ByteToImage(editEmp.imgPhoto);
            set {
                editEmp.imgPhoto = FileUtils.ImageToByte(value);
                OnPropertyChanged();
            }
        }

        public ICommand Page_Loaded { get; set; }
        private void ExecutePage_Loaded(object obj) {
            editEmp.txtEmployeeNo = _curEmployee.EmployeeNo.ToString();
            editEmp.txtEmployeeName = _curEmployee.EmployeeName;
            editEmp.txtSex = _curEmployee.sex;
            salary = _curEmployee.Salary.ToString();
            editEmp.txtTelphone = _curEmployee.telphone;
            editEmp.txtIdCard = _curEmployee.idCard;
            image = FileUtils.ByteToImage(_curEmployee.photo);
        }

        public ICommand SaveButton_Click { get; set; }
        private void ExecuteSaveButton_Click(object obj) {
            // 清除所有错误提示
            editEmp.txtEmployeeNoError = string.Empty;
            editEmp.txtEmployeeNameError = string.Empty;
            editEmp.txtSexError = string.Empty;
            editEmp.txtSalaryError = string.Empty;
            editEmp.txtTelphoneError = string.Empty;
            editEmp.txtIdCardError = string.Empty;

            // 验证用户输入
            if (!ValidateInput())
                return;

            // 保存修改后的信息到数据库
            if (_empMapper.UpdateEmp(_editEmp.txtEmployeeNo, new EmployeeInfo {
                EmployeeNo = editEmp.txtEmployeeNo,
                EmployeeName = editEmp.txtEmployeeName,
                Password = _curEmployee.Password,
                sex = editEmp.txtSex,
                workDate = _curEmployee.workDate,
                Salary = editEmp.txtSalary,
                telphone = editEmp.txtTelphone,
                idCard = editEmp.txtIdCard,
                photo = editEmp.imgPhoto
            })) {
                // 提示保存成功
                MessageBox.Show("信息保存成功！", "成功", MessageBoxButton.OK, MessageBoxImage.Information);
                // 返回
                _curPage.NavigationService.Navigate(new Uri("view/employee/EmpManage.xaml", UriKind.Relative));
            } else {
                // 提示保存失败
                MessageBox.Show("信息保存失败！", "失败", MessageBoxButton.OK, MessageBoxImage.Information);
            }
        }

        public ICommand SelectPhotoButton_Click { get; set; }
        private void ExecuteSelectPhotoButton_Click(object obj) {
            image = FileUtils.UploadPicture(image);
        }

        public ICommand CancelButton_Click { get; set; }
        private void ExecuteCancelButton_Click(object obj) {
            _curPage.NavigationService.GoBack();
        }



        public EditEmpVm(Page curpage, EmployeeInfo employee) {
            _curPage = curpage;
            _curEmployee = employee;

            Page_Loaded = new RelayCommand(ExecutePage_Loaded);
            SaveButton_Click = new RelayCommand(ExecuteSaveButton_Click);
            SelectPhotoButton_Click = new RelayCommand(ExecuteSelectPhotoButton_Click);
            CancelButton_Click = new RelayCommand(ExecuteCancelButton_Click);
        }



        //检查输入是否合法并给出提示
        private bool ValidateInput() {
            bool isValid = true;
            // 验证编号
            if (_empMapper.GetEmp(editEmp.txtEmployeeNo).Count==1) {
                if (_empMapper.GetEmp(editEmp.txtEmployeeNo).First().EmployeeNo != _curEmployee.EmployeeNo) {
                    editEmp.txtEmployeeNoError = "员工编号已存在";
                    isValid = false;
                }
            } else if (!(editEmp.txtEmployeeNo.Length == 5 && int.TryParse(editEmp.txtEmployeeNo, out _))) {
                editEmp.txtEmployeeNoError = "名字长度必须是5且是数字";
                isValid = false;
            }

            // 验证名字长度
            if (editEmp.txtEmployeeName.Length > 20) {
                editEmp.txtEmployeeNameError = "名字长度不能超过20个字符";
                isValid = false;
            }

            // 验证性别
            if (editEmp.txtSex.ToLower() != "男" && editEmp.txtSex.ToLower() != "女") {
                editEmp.txtSexError = "性别必须填写为“男”或“女”";
                isValid = false;
            }

            // 验证薪水
            if (!decimal.TryParse(salary, out _)) {
                editEmp.txtSexError = "薪水必须是数字";
                isValid = false;
            }

            // 验证电话号码
            if (!Validate.IsHandset(editEmp.txtTelphone)) {
                editEmp.txtTelphoneError = "请输入有效的电话号码";
                isValid = false;
            }

            // 验证身份证号
            if (Validate.IDchecking(editEmp.txtIdCard) != 0) {
                editEmp.txtIdCardError = "请输入有效的身份证号码";
                isValid = false;
            }

            return isValid;
        }
    }
}
