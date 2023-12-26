using BankManage.vm.employee;
using System.Windows.Controls;

namespace BankManage.view.employee
{
    /// <summary>
    /// ChangePay.xaml 的交互逻辑
    /// </summary>
    public partial class ChangePay : Page
    {
        public ChangePay()
        {
            InitializeComponent();
            this.DataContext = new ChangePayVm();
        }
    }
}
