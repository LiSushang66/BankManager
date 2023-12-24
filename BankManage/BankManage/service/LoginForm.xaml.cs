using System;
using System.Linq;
using System.Windows;

using BankManage.common;
using BankManage.utils;

namespace BankManage {
    /// <summary>
    /// LoginForm.xaml 的交互逻辑
    /// </summary>
    public partial class LoginForm : Window {
        public string UserName { get; set; }
        private BankEntities dbEntity = new BankEntities();
        public LoginForm() {
            InitializeComponent();
            this.WindowStartupLocation = WindowStartupLocation.CenterScreen;
            UserName = string.Empty;
        }
        //单击登录时进行身份验证
        private void LogIn(object sender, RoutedEventArgs e) {
            var query = from t in dbEntity.LoginInfo
                        where t.Bno == this.combox.Text && t.Password == this.pass.Password
                        select t;
            if (query.Count() > 0) {
                var q = query.First();
                UserName = DataOperation.GetOperateName(q.Bno);
                this.Close();
            } else {
                MessageBox.Show("登录失败！");
                this.pass.Clear();
                this.pass.Focus();
            }

        }
        //关闭窗体
        private void Exit(object sender, RoutedEventArgs e) {
            Application.Current.Shutdown();
        }
        //窗体关闭时进行关闭操作
        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e) {
            if (string.IsNullOrEmpty(this.UserName) == true) {
                App.Current.Shutdown();
            }
        }
        //将账户表中的账号信息显示到此处
        private void Window_SourceInitialized_1(object sender, EventArgs e) {
            var query = from t in dbEntity.LoginInfo
                        select t.Bno;
            this.combox.ItemsSource = query.ToList();
        }

        private void RefreshCaptcha(object sender, RoutedEventArgs e) {
            string capName = Uuid.GetUid("hh.png");
            captcha.Source = Captcha.Generate("1234", capName);
            Console.WriteLine(capName);
           //string path = System.Environment.CurrentDirectory + "..\\..\\..\\static\\images\\captcha\\692ad51c8c2b434abb89421f7d6278b6.png";//获取图片绝对路径
           // BitmapImage image = new BitmapImage(new Uri(path, UriKind.Absolute));//打开图片
           // captcha.Source = image;//将控件和图片绑定，logo为Image控件名称
           // captcha.Source = new BitmapImage(new Uri(@"static\images\captcha\ff536d119459413cb7422428128100ed.png", UriKind.Relative));
        }
    }
}
