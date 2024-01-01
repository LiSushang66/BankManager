using BankManage.domain;
using BankManage.model;
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

namespace BankManage.view.money {
    /// <summary>
    /// AccountChecking.xaml 的交互逻辑
    /// </summary>
    public partial class AccountChecking : Page {
        public Custom Custom;
        public AccountChecking(Custom cus) {

            InitializeComponent();
            Custom = DataOperation.CreateCustom(cus.AccountInfo.accountType);
            Custom.AccountInfo.accountNo = cus.AccountInfo.accountNo;
            Custom.AccountInfo.accountName = cus.AccountInfo.accountName;
            Custom.AccountInfo.accountPass = cus.AccountInfo.accountPass;
            Custom.AccountInfo.accountType = cus.AccountInfo.accountType;
            Custom.AccountInfo.IdCard = cus.AccountInfo.IdCard;
            Custom.AccountInfo.rateType = cus.AccountInfo.rateType;
            Custom.AccountBalance = cus.AccountBalance;

            txtIDCard.Content = Custom.AccountInfo.IdCard;
            txtAccountNo.Content = Custom.AccountInfo.accountNo;
            txtAccountName.Content = Custom.AccountInfo.accountName;
            txtPass.Content = Custom.AccountInfo.accountPass;
            rateType.Content = Custom.AccountInfo.rateType;
            accountType.Content = Custom.AccountInfo.accountType;
            txtMoney.Content = Custom.AccountBalance;
        }


        //确定
        private void Button_Click(object sender, RoutedEventArgs e) {
            Custom.Create(Custom.AccountInfo.accountNo, Custom.AccountBalance);
            OperateRecord page = new OperateRecord();
            NavigationService ns = NavigationService.GetNavigationService(this);
            ns.Navigate(page);
        }

        //取消
        private void Button_Click_1(object sender, RoutedEventArgs e) {
            NavigationService.GoBack();
        }
    }
}