using System.Windows;

using BankManage.vm.loginForm;

namespace BankManage.view.loginForm {
    /// <summary>
    /// LoginForm.xaml 的交互逻辑
    /// </summary>
    public partial class LoginForm : Window {
        public LoginForm() {
            InitializeComponent();
            this.DataContext = new LoginFormVm(this);
        }
    }
}
