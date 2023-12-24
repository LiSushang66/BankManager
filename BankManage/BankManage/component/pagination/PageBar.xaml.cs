using BankManage.vm;
using System.Windows;
using System.Windows.Controls;

namespace BankManage.component.pagination {
    /// <summary>
    /// page.xaml 的交互逻辑
    /// </summary>
    public partial class PageBar : Page {
        public PageBar() {
            InitializeComponent();

            this.DataContext = new MainWindowViewModel();
        }
    }
}
