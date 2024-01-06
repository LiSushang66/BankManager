using BankManage.model.loginForm;
using BankManage.utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using BankManage.dao;
using BankManage.dao.impl;

namespace BankManage.vm.loginForm {
    internal class LoginFormVm : NotifyProperty {
        public static string UserName { get; set; }
        public static string Id { get; set; }
        private Window _curWindow;
        private EmpMapper _empMapper = new EmpMapperImpl();

        private LoginFormModel _loginForm=new LoginFormModel();

        public LoginFormModel loginForm {
            get => _loginForm;
            set => SetProperty(ref _loginForm, value);
        }



        //单击登录时进行身份验证
        public ICommand LogIn { get; set; }
        private void ExecuteLogIn(object obj) {
            if (obj is PasswordBox pass) {
                //验证码错误，直接返回
                if (loginForm.veriCode.ToLower() != veriCode.ToLower()) {
                    MessageBox.Show("验证码错误！");
                    ExecuteRefreshCaptcha(null);
                    return;
                }
                var query = _empMapper.GetEmp(loginForm.txtCombox, Encrypt.SHA256Encrypt(loginForm.password));
                if (query.Count() > 0) {
                    var q = query.First();
                    Id = _empMapper.GetEmp(q.EmployeeNo).First().EmployeeNo;
                    UserName = _empMapper.GetEmp(q.EmployeeNo).First().EmployeeName;
                    LogHelper.Loginfo.Info(UserName + "登录成功");
                    _curWindow.Close();
                } else {
                    LogHelper.Loginfo.Info("登录失败");
                    MessageBox.Show("登录失败！");
                    loginForm.password = "";
                    ExecuteRefreshCaptcha(null);
                    pass.Focus();
                }
            }
        }

        //关闭窗体
        public ICommand Exit { get; set; }
        private void ExecuteExit(object sender) {
            Application.Current.Shutdown();
        }
        //窗体关闭时进行关闭操作
        public ICommand Window_Closing { get; set; }
        private void ExecuteWindow_Closing(object sender) {
            if (string.IsNullOrEmpty(UserName) == true) {
                LogHelper.Loginfo.Info("程序关闭");
                Application.Current.Shutdown();
            }
        }
        //将账户表中的账号信息显示到此处
        public ICommand Window_Loaded { get; set; }
        private void ExecuteWindow_Loaded(object sender) {
            List<EmployeeInfo> allEmps = _empMapper.GetEmp();
            foreach (EmployeeInfo Emp in allEmps) {
                loginForm.txtComboxItem.Add(Emp.EmployeeNo);
            }
        }

        //刷新验证码
        public ICommand RefreshCaptcha { get; set; }
        string veriCode;
        private void ExecuteRefreshCaptcha(object sender) {
            veriCode = Captcha.VeriCode(new Random());
            string capName = Uuid.GetUid(".png");
            loginForm.captcha = Captcha.Generate(veriCode, capName);
            Console.WriteLine(capName);
        }
        public LoginFormVm(Window curWindow) {
            UserName = string.Empty;
            _curWindow = curWindow;
            _curWindow.WindowStartupLocation = WindowStartupLocation.CenterScreen;
            ExecuteRefreshCaptcha(null);

            LogIn = new RelayCommand(ExecuteLogIn);
            Exit = new RelayCommand(ExecuteExit);
            Window_Closing = new RelayCommand(ExecuteWindow_Closing);
            Window_Loaded = new RelayCommand(ExecuteWindow_Loaded);
            RefreshCaptcha = new RelayCommand(ExecuteRefreshCaptcha);
        }
    }
}
