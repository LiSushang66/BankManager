using BankManage.vm.employee;
using System;
using System.Collections.Generic;
using System.IO;
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

namespace BankManage.view.employee {
    /// <summary>
    /// EditEmp.xaml 的交互逻辑
    /// </summary>
    public partial class EditEmp : Page {

        public EditEmp(EmployeeInfo employee) {
            InitializeComponent();
            this.DataContext = new EditEmpVm(this, employee);
        }
    }
}