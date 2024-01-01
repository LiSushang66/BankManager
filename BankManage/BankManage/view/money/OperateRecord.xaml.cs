using BankManage.component.pagination.pagerControl;
using BankManage.view.employee;
using BankManage.vm;
using BankManage.vm.money;
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

namespace BankManage.view.money
{
    /// <summary>
    /// OperateRecord.xaml 的交互逻辑
    /// </summary>
    public partial class OperateRecord : Page,INotifyPropertyChanged {
        


        public OperateRecord() {
            InitializeComponent();
            this.DataContext = this;


            BankEntities context = new BankEntities();
            var query = from t in context.MoneyInfo
                        select t;
            datagrid1 = new ObservableCollection<MoneyInfo>(query.ToList());



            //分页初始化
            Pager = new Pager<MoneyInfo>(8, datagrid1);
            Pager.PagerUpdated += items => {
                datagrid1 = new ObservableCollection<MoneyInfo>(items);
            };
            Pager.CurPageIndex = 1;
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

        private ObservableCollection<MoneyInfo> _datagrid1;
        public ObservableCollection<MoneyInfo> datagrid1 {
            get => _datagrid1;
            set {
                _datagrid1 = value;
                OnPropertyChanged(nameof(datagrid1));
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;
        public virtual void OnPropertyChanged([CallerMemberName] string propertyName = null) {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
