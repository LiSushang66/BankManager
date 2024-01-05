using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Navigation;
using BankManage.vm.money;
using BankManage.domain;

namespace BankManage.view.money
{
    /// <summary>
    /// NewAccount.xaml 的交互逻辑
    /// </summary>
    public partial class NewAccount : Page
    {
        string[] rates = Enum.GetNames(typeof(RateType));
        string[] accTypes = Enum.GetNames(typeof(MoneyAccountType));
        public NewAccount()
        {
            InitializeComponent();
            this.DataContext = new NewAccountVm(this);
            //放到页面加载的地方
            foreach (var accType in accTypes)
            {
                comboBoxAccountType.Items.Add(accType);
            }
        }

        //取消开户
        private void btnCancel_Click(object sender, RoutedEventArgs e)
        {
            this.NavigationService.Navigate(new OperateRecord());
        }


        //选择类型改变时
        private void comboBoxAccountType_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            string targetAccType = comboBoxAccountType.SelectedItem.ToString();
            
            using (BankEntities c = new BankEntities())
            {
                var q = from t in c.AccountInfo
                        where t.accountType == targetAccType
                        select t;
                if (q.Count() > 0)
                {
                    this.txtAccountNo.Text = string.Format("{0}", int.Parse(q.Max(x => x.accountNo)) + 1);
                }
                else
                {
                    txtAccountNo.Text = string.Format("{0}00001", comboBoxAccountType.SelectedIndex + 1);
                }
            }
            rateType.ItemsSource = rates.Where(rate => Regex.Match(rate, targetAccType.Substring(0, 2) + ".*年").Success);
            rateType.ItemsSource = rateType.Items.Count == 0 ? new List<string> { "活期" } : rateType.ItemsSource;
        }
    }
}