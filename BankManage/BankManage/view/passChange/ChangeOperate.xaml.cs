using System;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using BankManage.vm.passChange;

using BankManage.utils;

namespace BankManage.view.passChange {
    /// <summary>
    /// ChangeOperate.xaml 的交互逻辑
    /// </summary>
    public partial class ChangeOperate : Page {
        public ChangeOperate() {
            InitializeComponent();
            this.DataContext = new ChangeOperateVm();
        }
        
    }
}
