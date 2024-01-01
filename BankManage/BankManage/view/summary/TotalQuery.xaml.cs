using BankManage;
using BankManage.vm.summary;
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

namespace BankManage.view.summary {
    /// <summary>
    /// TotalQuery.xaml 的交互逻辑
    /// </summary>
    public partial class TotalQuery : Page {
        BankEntities context = new BankEntities();
        public TotalQuery() {
            InitializeComponent();
            this.Unloaded += TotalQuery_Unloaded;
        }

        void TotalQuery_Unloaded(object sender, RoutedEventArgs e) {
            context.Dispose();
        }
        //查询当前账号的所有记录信息
        private void Button_Click_1(object sender, RoutedEventArgs e) {
            IQueryable<MoneyInfo> query;
            if (txtID.IsEnabled) {
                var userQuery = from user in context.AccountInfo
                                where user.IdCard == txtID.Text
                                select user.accountNo;
                query = from t in context.MoneyInfo
                        where userQuery.Contains(t.accountNo)
                        && ((beginTime.SelectedDate == null && endTime.SelectedDate == null) || (beginTime.SelectedDate <= t.dealDate && t.dealDate <= endTime.SelectedDate))
                        && (string.IsNullOrEmpty(txtAccount.Text) || txtAccount.Text == t.accountNo)
                        select t;
            } else {
                query = from t in context.MoneyInfo
                        where ((beginTime.SelectedDate == null && endTime.SelectedDate == null) || (beginTime.SelectedDate <= t.dealDate && t.dealDate <= endTime.SelectedDate))
                        && (string.IsNullOrEmpty(txtAccount.Text) || txtAccount.Text == t.accountNo)
                        select t;
            }
            datagrid1.ItemsSource = query.ToList();
        }

        private void QueryTypeSelectionChanged(object sender, SelectionChangedEventArgs e) {
            button_1.IsEnabled = true;
            switch (queryType.SelectedIndex) {
                case 0:
                    txtID.Clear(); //身份证
                    txtID.IsEnabled = true;
                    txtAccount.Clear();
                    txtAccount.IsEnabled = false;
                    beginTime.SelectedDate = null;
                    beginTime.IsEnabled = false;
                    endTime.SelectedDate = null;
                    endTime.IsEnabled = false;
                    break;
                case 1:
                    txtID.Clear(); //账号
                    txtID.IsEnabled = false;
                    txtAccount.Clear();
                    txtAccount.IsEnabled = true;
                    beginTime.SelectedDate = null;
                    beginTime.IsEnabled = false;
                    endTime.SelectedDate = null;
                    endTime.IsEnabled = false;
                    break;
                case 2:
                    txtID.Clear(); //时间
                    txtID.IsEnabled = false;
                    txtAccount.Clear();
                    txtAccount.IsEnabled = false;
                    beginTime.SelectedDate = null;
                    beginTime.IsEnabled = true;
                    endTime.SelectedDate = null;
                    endTime.IsEnabled = true;
                    break;
                case 3:
                    txtID.Clear(); //身份证和账号
                    txtID.IsEnabled = true;
                    txtAccount.Clear();
                    txtAccount.IsEnabled = true;
                    beginTime.SelectedDate = null;
                    beginTime.IsEnabled = false;
                    endTime.SelectedDate = null;
                    endTime.IsEnabled = false;
                    break;
                case 4:
                    txtID.Clear(); //身份证和时间
                    txtID.IsEnabled = true;
                    txtAccount.Clear();
                    txtAccount.IsEnabled = false;
                    beginTime.SelectedDate = null;
                    beginTime.IsEnabled = true;
                    endTime.SelectedDate = null;
                    endTime.IsEnabled = true;
                    break;
                case 5:
                    txtID.Clear(); //账号和时间
                    txtID.IsEnabled = false;
                    txtAccount.Clear();
                    txtAccount.IsEnabled = true;
                    beginTime.SelectedDate = null;
                    beginTime.IsEnabled = true;
                    endTime.SelectedDate = null;
                    endTime.IsEnabled = true;
                    break;
                case 6:
                    txtID.Clear(); //身份证和账号和时间
                    txtID.IsEnabled = true;
                    txtAccount.Clear();
                    txtAccount.IsEnabled = true;
                    beginTime.SelectedDate = null;
                    beginTime.IsEnabled = true;
                    endTime.SelectedDate = null;
                    endTime.IsEnabled = true;
                    break;
                default:
                    txtID.Clear(); //无条件
                    txtID.IsEnabled = true;
                    txtAccount.Clear();
                    txtAccount.IsEnabled = true;
                    beginTime.SelectedDate = null;
                    beginTime.IsEnabled = true;
                    endTime.SelectedDate = null;
                    endTime.IsEnabled = true;
                    break;
            }
        }
    }
}
