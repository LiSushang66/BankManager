using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Navigation;
using BankManage.domain;
using BankManage.model;
using BankManage.vm.money;

namespace BankManage.view.money {
    /// <summary>
    /// NewAccount.xaml 的交互逻辑
    /// </summary>
    public partial class NewAccount : Page {
        string[] rates = Enum.GetNames(typeof(RateType));
        string[] accTypes = Enum.GetNames(typeof(MoneyAccountType));
        public NewAccount() {
            InitializeComponent();
            this.DataContext = new NewAccountVm();
            //放到页面加载的地方
            foreach (var accType in accTypes) {
                comboBoxAccountType.Items.Add(accType);
            }
            comboBoxAccountType.SelectedIndex = -1;
        }



        //开户
        private void btnOk_Click(object sender, RoutedEventArgs e) {
            //检查输入
            if (comboBoxAccountType.SelectedIndex == -1) 
            {
                MessageBox.Show("请选择开户类型");
                return;
            }
            if (rateType.SelectedItem == null) 
            {
                MessageBox.Show("请选择利率");
                return;
            }

            Custom custom = new Custom();
            custom.AccountInfo.accountNo = this.txtAccountNo.Text;
            custom.AccountInfo.IdCard = this.txtIDCard.Text;
            custom.AccountInfo.accountName = this.txtAccountName.Text;
            custom.AccountInfo.accountPass = this.txtPass.Password;
            custom.AccountInfo.rateType = this.rateType.SelectedItem.ToString();
            custom.AccountInfo.accountType = this.comboBoxAccountType.SelectedItem.ToString();
            custom.AccountBalance = double.Parse(this.txtMoney.Text);

            NavigationService.Navigate(new AccountChecking(custom));
        }




        //取消开户
        private void btnCancel_Click(object sender, RoutedEventArgs e) {
            NavigationService.GoBack();
        }


        //选择类型改变时
        private void comboBoxAccountType_SelectionChanged(object sender, SelectionChangedEventArgs e) {
            string targetAccType = comboBoxAccountType.SelectedItem.ToString();
            using (BankEntities c = new BankEntities()) {
                var q = from t in c.AccountInfo
                        where t.accountType == targetAccType
                        select t;
                if (q.Count() > 0) {
                    this.txtAccountNo.Text = string.Format("{0}", int.Parse(q.Max(x => x.accountNo)) + 1);
                } else {
                    txtAccountNo.Text = string.Format("{0}00001", comboBoxAccountType.SelectedIndex + 1);
                }
            }
            rateType.ItemsSource = rates.Where(rate => Regex.Match(rate, targetAccType.Substring(0, 2) + ".*年").Success);
            rateType.ItemsSource = rateType.Items.Count == 0 ? new List<string> { "活期" } : rateType.ItemsSource;
        }
    }
}