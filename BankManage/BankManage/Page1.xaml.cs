using BankManage.vm;
using System.Windows;
using System.Windows.Controls;

namespace BankManage {
    /// <summary>
    /// page.xaml 的交互逻辑
    /// </summary>
    public partial class Page1 : Page {
        public Page1() {
            InitializeComponent();

            this.DataContext = new MainWindowViewModel();
        }
    }
}
