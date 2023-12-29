using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
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
using BankManage.utils;
using BankManage.vm.employee;
using Microsoft.Win32;

namespace BankManage.view.employee {
    /// <summary>
    /// AddEmp.xaml 的交互逻辑
    /// </summary>
    public partial class AddEmp : Page {

        public AddEmp() {
            InitializeComponent();
            this.DataContext = new AddEmpVm(this);
        }
    }
}
