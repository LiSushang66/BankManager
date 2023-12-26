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
        BankEntities context = new BankEntities();
        public ChangeAccount() {
            InitializeComponent();
            this.DataContext = new ChangeAccountVm();
        }
        //更改密码
        private void btnOk_Click(object sender, RoutedEventArgs e) {
            //先验证，减少数据库压力
            String newPass = this.txtNewPass.Password;
            //验证输入一致
            if (newPass != txtPassConf.Password) {
                MessageBox.Show("两次输入密码不一致！");
                return;
            }
            //验证密码复杂度
            if (!Validite.Password(newPass)) {
                MessageBox.Show("密码太简单！");
                return;
            }
            var query = from t in context.AccountInfo
                        where t.accountNo == this.txtAccount.Text
                        select t;
            if (query.Count() > 0) {
                var q = query.First();
                q.accountPass = Encrypt.SHA256Encrypt(newPass);
                try {
                    context.SaveChanges();
                    MessageBox.Show("更改密码成功！");
                } catch {
                    MessageBox.Show("更改密码失败！");
                }
            } else {
                MessageBox.Show("账号不存在");
            }
        }
        //取消更改
        private void btnCancel_Click(object sender, RoutedEventArgs e) {
            this.txtNewPass.Clear();
            this.txtPassConf.Clear();
        }
    }
}
