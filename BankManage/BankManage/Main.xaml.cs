using BankManage.vm;
using System.Windows;

namespace BankManage {
    /// <summary>
    /// Main.xaml 的交互逻辑
    /// </summary>
    public partial class Main : Window {
        public Main() {
            InitializeComponent();
            this.DataContext = new MainVm(this);
        }
    }
}
