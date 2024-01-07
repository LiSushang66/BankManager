using System;
using System.Windows.Controls;
using System.Windows;
using System.Windows.Input;

using BankManage.model;
using BankManage.utils;
using BankManage.view.loginForm;
using MaterialDesignThemes.Wpf;
using System.Windows.Controls.Primitives;
using System.Windows.Media.Imaging;
using BankManage.dao;
using BankManage.dao.impl;
using BankManage.vm.loginForm;
using System.Linq;

namespace BankManage.vm {
    internal class MainVm : NotifyProperty {
        private Window _curWindow;
        private EmpMapper _empMapper = new EmpMapperImpl();

        private MainModel _main = new MainModel();

        public MainModel main {
            get => _main;
            set {
                if (value == _main) {
                    return;
                }

                _main = value;
                OnPropertyChanged();
            }
        }

        public BitmapImage image {
            get => FileUtils.ByteToImage(main.imgPhoto);
            set {
                main.imgPhoto = FileUtils.ImageToByte(value);
                OnPropertyChanged();
            }
        }


        public ICommand Button_Click { get; set; }
        private void ExecuteButton_Click(object obj) {
            var controlArray = (object[])obj;
            Button b = (Button)controlArray[0];
            ToggleButton tS = (ToggleButton)controlArray[1];
            DrawerHost dH = (DrawerHost)controlArray[2];
            Frame f=(Frame)controlArray[3];


            LogHelper.Loginfo.Info("跳转请求到" + b.Tag.ToString());
            main.uri = new Uri(b.Tag.ToString(), UriKind.Relative);

            tS.IsChecked = false;
            dH.IsLeftDrawerOpen = false;


            f.Source = new Uri(@"help/" + b.Tag.ToString(), UriKind.Relative);
        }

        public ICommand Button_Click_Exit { get; set; }
        private void ExecuteButton_Click_Exit(object obj) {
            _curWindow.Close();
        }

        public ICommand ToggleButton_Click_1 { get; set; }
        private void ExecuteToggleButton_Click_1(object obj) {
            if (obj is ToggleButton toggleButton) {
                var paletteHelper = new PaletteHelper();
                ITheme theme = paletteHelper.GetTheme();
                if (toggleButton.IsChecked.Value)
                    theme.SetBaseTheme(MaterialDesignThemes.Wpf.Theme.Dark);
                else {
                    theme.SetBaseTheme(MaterialDesignThemes.Wpf.Theme.Light);
                }
                paletteHelper.SetTheme(theme);
            }
        }

        public ICommand ToggleButton_Click { get; set; }
        private void ExecuteToggleButton_Click(object obj) {
            var controlArray = (object[])obj;
            ToggleButton tB = (ToggleButton)controlArray[0];
            DrawerHost dH = (DrawerHost)controlArray[1];
            dH.IsLeftDrawerOpen = tB.IsChecked.Value;
        }


        public ICommand DrawerHost_MouseMove { get; set; }
        private void ExecuteDrawerHost_MouseMove(object obj) {
            var controlArray = (object[])obj;
            ToggleButton tS = (ToggleButton)controlArray[0];
            DrawerHost dH = (DrawerHost)controlArray[1];
            tS.IsChecked = dH.IsLeftDrawerOpen;
        }


        public ICommand Button_Click_Help { get; set; }
        private void ExecuteButton_Click_Help(object obj) {
            var controlArray = (object[])obj;
            DrawerHost b = (DrawerHost)controlArray[0];
            b.IsRightDrawerOpen = true;
        }



        public MainVm(Window curWindow) {
            LogHelper.Loginfo.Info("主窗体程序启动");
            _curWindow = curWindow;
            _curWindow.WindowStartupLocation = WindowStartupLocation.CenterScreen;
            _curWindow.SourceInitialized += ExecuteMainWindow_Loaded;

            Button_Click = new RelayCommand(ExecuteButton_Click);
            Button_Click_Exit = new RelayCommand(ExecuteButton_Click_Exit);
            ToggleButton_Click_1 = new RelayCommand(ExecuteToggleButton_Click_1);
            ToggleButton_Click = new RelayCommand(ExecuteToggleButton_Click);
            DrawerHost_MouseMove = new RelayCommand(ExecuteDrawerHost_MouseMove);
            Button_Click_Help = new RelayCommand(ExecuteButton_Click_Help);
        }

        private void ExecuteMainWindow_Loaded(object sender, EventArgs e) {
            //默认显示当前页面
            main.uri = new Uri("view/system/Welcome.xaml", UriKind.Relative);
            //启动登陆窗体
            LoginForm login = new LoginForm();
            login.ShowDialog();
            if (!String.IsNullOrEmpty(LoginFormVm.Id)) {
                image = FileUtils.ByteToImage(_empMapper.GetEmp(LoginFormVm.Id).First().photo);
                main.employeeName = _empMapper.GetEmp(LoginFormVm.Id).First().EmployeeName;
            }
        }
    }
}