using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using BankManage.vm.passChange;

using BankManage.utils;

namespace BankManage.view.passChange {
    /// <summary>
    /// ChangeAccount.xaml 的交互逻辑
    /// </summary>
    public partial class ChangeAccount : Page {
        public ChangeAccount() {
            InitializeComponent();
            this.DataContext = new ChangeAccountVm();
        }
        
    }
}
