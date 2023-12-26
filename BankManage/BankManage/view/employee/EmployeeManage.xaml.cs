using BankManage.vm.employee;
using System.Windows.Controls;

namespace BankManage.view.employee
{
    /// <summary>
    /// EmployeeBase.xaml 的交互逻辑
    /// </summary>
    public partial class EmployeeManage : Page
    {
        public EmployeeManage()
        {
            InitializeComponent();
            this.DataContext = new EmployeeManageVm();
        }
    }
}
