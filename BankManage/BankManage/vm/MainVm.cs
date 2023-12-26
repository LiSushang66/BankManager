using BankManage.view.loginForm;
using System;
using System.Windows.Controls;
using System.Windows;
using System.Windows.Input;
using BankManage.model;



namespace BankManage.vm {
    internal class MainVm : NotifyProperty {
        private Window _curWindow;

        private MainModel _main = new MainModel();

        public MainModel main {
            get => _main;
            set {
                if (value == _main)
                    return;
                _main = value;
                OnPropertyChanged();
            }
        }

        public ICommand Button_Click { get; set; }
        private void ExecuteButton_Click(object obj) {
            Console.WriteLine(obj);
            if (obj is Button button) {
                main.uri = new Uri(button.Tag.ToString(), UriKind.Relative);
            }
        }

        private void ExecuteMainWindow_Loaded(object sender, EventArgs e) {
            //默认显示当前页面
            main.uri = new Uri("view/money/OperateRecord.xaml", UriKind.Relative);
            //启动登陆窗体
            LoginForm login = new LoginForm();
            login.ShowDialog();
        }
        public MainVm(Window curWindow) {
            _curWindow = curWindow;
            _curWindow.WindowStartupLocation = WindowStartupLocation.CenterScreen;
            _curWindow.SourceInitialized += ExecuteMainWindow_Loaded;

            Button_Click = new RelayCommand(ExecuteButton_Click);
        }

        
    }
}