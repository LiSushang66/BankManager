using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using BankManage.domain;
using BankManage.model;
using BankManage.utils;
using BankManage.vm.money;

namespace BankManage.view.money {
    /// <summary>
    /// Withdraw.xaml 的交互逻辑
    /// </summary>
    public partial class Withdraw : Page {
        public Withdraw() {
            InitializeComponent();
        }
        //取款
        private void btnOk_Click(object sender, RoutedEventArgs e) {
            Custom custom = DataOperation.GetCustom(this.txtAccount.Text);
            if (custom == null) {
                MessageBox.Show("帐号不存在！");
                return;
            }
            if (custom.AccountInfo.accountPass != Encrypt.SHA256Encrypt(this.txtPassword.Password)) {
                MessageBox.Show("密码不正确");
                return;
            }
            custom.Withdraw(double.Parse(this.txtmount.Text));

            OperateRecord page = new OperateRecord();
            NavigationService ns = NavigationService.GetNavigationService(this);
            ns.Navigate(page);
        }
        //取消取款
        private void btnCancel_Click(object sender, RoutedEventArgs e) {
            OperateRecord page = new OperateRecord();
            NavigationService ns = NavigationService.GetNavigationService(this);
            ns.Navigate(page);
        }
    }
}