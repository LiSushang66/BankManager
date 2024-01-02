using System.Windows;
using System.Windows.Controls;
using System.Windows.Navigation;
using BankManage.vm.money;
using BankManage.model;
using BankManage.domain;

namespace BankManage.view.money
{
    /// <summary>
    /// Deposit.xaml 的交互逻辑
    /// </summary>
    public partial class Deposit : Page {
        public Deposit() {
            InitializeComponent();
            this.DataContext = new DepositVm();
        }
    }
}
