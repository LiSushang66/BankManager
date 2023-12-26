using BankManage.model;
using BankManage.model.loginForm;
using BankManage.utils;
using System;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace BankManage.vm.loginForm {
    internal class LoginFormVm : NotifyProperty {
        public string UserName { get; set; }
        private BankEntities dbEntity = new BankEntities();
        private Window _curWindow = new Window();

        private LoginFormModel _loginform=new LoginFormModel();

        public LoginFormModel loginForm {
            get => _loginform;
            set {
                if (value == _loginform)
                    return;
                _loginform = value;
                OnPropertyChanged();
            }
        }



        //单击登录时进行身份验证
        public ICommand LogIn { get; set; }
        private void ExecuteLogIn(object obj) {
            if (obj is PasswordBox pass) {
                var query = from t in dbEntity.LoginInfo
                            where t.Bno == loginForm.txtCombox && t.Password == loginForm.password
                            select t;
                if (query.Count() > 0) {
                    var q = query.First();
                    UserName = DataOperation.GetOperateName(q.Bno);
                    _curWindow.Close();
                } else {
                    MessageBox.Show("登录失败！");
                    loginForm.password = "";
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
            if (string.IsNullOrEmpty(this.UserName) == true) {
                Application.Current.Shutdown();
            }
        }
        //将账户表中的账号信息显示到此处
        public ICommand Window_Loaded { get; set; }
        private void ExecuteWindow_Loaded(object sender) {
            var query = from t in dbEntity.LoginInfo
                        select t.Bno;
            loginForm.txtComboxItem = query.ToList();
        }

        //刷新验证码
        public ICommand RefreshCaptcha { get; set; }
        private void ExecuteRefreshCaptcha(object sender) {
            string capName = Uuid.GetUid("hh.png");
            loginForm.captcha = Captcha.Generate("1234", capName);
            Console.WriteLine(capName);
        }
        public LoginFormVm(Window curWindow) {
            UserName = string.Empty;
            _curWindow = curWindow;
            _curWindow.WindowStartupLocation = WindowStartupLocation.CenterScreen;

            LogIn = new RelayCommand(ExecuteLogIn);
            Exit = new RelayCommand(ExecuteExit);
            Window_Closing = new RelayCommand(ExecuteWindow_Closing);
            Window_Loaded = new RelayCommand(ExecuteWindow_Loaded);
            RefreshCaptcha = new RelayCommand(ExecuteRefreshCaptcha);
        }
    }
}
