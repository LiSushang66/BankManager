using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Windows;
using System.Windows.Controls;
using BankManage.component.pagination.pagerControl;
using BankManage.utils;
using BankManage.vm.summary;


namespace BankManage.help.view.summary {
    /// <summary>
    /// DayQuery.xaml 的交互逻辑
    /// 当日汇总
    /// </summary>
    public partial class DayQuery : Page {
        BankEntities context = new BankEntities();
        public DayQuery() {
            InitializeComponent();
        }
    }
}