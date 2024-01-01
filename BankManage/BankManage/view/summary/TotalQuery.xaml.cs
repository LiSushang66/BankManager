using BankManage;
using BankManage.component.pagination.pagerControl;
using BankManage.vm.summary;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
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
    public partial class TotalQuery : Page,INotifyPropertyChanged {
        BankEntities context = new BankEntities();
        public TotalQuery() {
            InitializeComponent();
            this.DataContext = this;
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
            dataGrid = new ObservableCollection<MoneyInfo>(query.ToList());

            //分页初始化
            Pager = new Pager<MoneyInfo>(8, dataGrid);
            Pager.PagerUpdated += items => {
                dataGrid = new ObservableCollection<MoneyInfo>(items);
            };
            Pager.CurPageIndex = 1;
        }

        private void QueryTypeSelectionChanged(object sender, SelectionChangedEventArgs e) {
            button_1.IsEnabled = true;
            switch (queryType.SelectedIndex) {
                case 0:
                    txtID.Clear(); //无条件
                    txtID.IsEnabled = false;
                    txtAccount.Clear();
                    txtAccount.IsEnabled = false;
                    beginTime.SelectedDate = null;
                    beginTime.IsEnabled = false;
                    endTime.SelectedDate = null;
                    endTime.IsEnabled = false;
                    break;
                case 1:
                    txtID.Clear(); //身份证
                    txtID.IsEnabled = true;
                    txtAccount.Clear();
                    txtAccount.IsEnabled = false;
                    beginTime.SelectedDate = null;
                    beginTime.IsEnabled = false;
                    endTime.SelectedDate = null;
                    endTime.IsEnabled = false;
                    break;
                case 2:
                    txtID.Clear(); //账号
                    txtID.IsEnabled = false;
                    txtAccount.Clear();
                    txtAccount.IsEnabled = true;
                    beginTime.SelectedDate = null;
                    beginTime.IsEnabled = false;
                    endTime.SelectedDate = null;
                    endTime.IsEnabled = false;
                    break;
                case 3:
                    txtID.Clear(); //时间
                    txtID.IsEnabled = false;
                    txtAccount.Clear();
                    txtAccount.IsEnabled = false;
                    beginTime.SelectedDate = null;
                    beginTime.IsEnabled = true;
                    endTime.SelectedDate = null;
                    endTime.IsEnabled = true;
                    break;
                case 4:
                    txtID.Clear(); //身份证和账号
                    txtID.IsEnabled = true;
                    txtAccount.Clear();
                    txtAccount.IsEnabled = true;
                    beginTime.SelectedDate = null;
                    beginTime.IsEnabled = false;
                    endTime.SelectedDate = null;
                    endTime.IsEnabled = false;
                    break;
                case 5:
                    txtID.Clear(); //身份证和时间
                    txtID.IsEnabled = true;
                    txtAccount.Clear();
                    txtAccount.IsEnabled = false;
                    beginTime.SelectedDate = null;
                    beginTime.IsEnabled = true;
                    endTime.SelectedDate = null;
                    endTime.IsEnabled = true;
                    break;
                case 6:
                    txtID.Clear(); //账号和时间
                    txtID.IsEnabled = false;
                    txtAccount.Clear();
                    txtAccount.IsEnabled = true;
                    beginTime.SelectedDate = null;
                    beginTime.IsEnabled = true;
                    endTime.SelectedDate = null;
                    endTime.IsEnabled = true;
                    break;
                case 7:
                    txtID.Clear(); //身份证和账号和时间
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



























        //以下是分页内容
        private Pager<MoneyInfo> _pager;
        public Pager<MoneyInfo> Pager {
            get => _pager;
            set {
                _pager = value;
                OnPropertyChanged(nameof(Pager));
            }
        }

        private ObservableCollection<MoneyInfo> _dataGrid;
        public ObservableCollection<MoneyInfo> dataGrid {
            get => _dataGrid;
            set {
                _dataGrid = value;
                OnPropertyChanged(nameof(dataGrid));
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;
        public virtual void OnPropertyChanged([CallerMemberName] string propertyName = null) {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
