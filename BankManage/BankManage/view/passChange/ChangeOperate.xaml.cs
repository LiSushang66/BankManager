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
        BankEntities context = new BankEntities();
        public ChangeOperate() {
            InitializeComponent();
            this.DataContext = new ChangeOperateVm();
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
            var query = from t in context.LoginInfo
                        where t.Bno == this.txtAccount.Text
                        select t;
            if (query.Count() > 0) {
                var q = query.First();
                //写入SHA256加密后的密码
                q.Password = Encrypt.SHA256Encrypt(newPass);
                try {
                    context.SaveChanges();
                    MessageBox.Show("更改密码成功！");
                } catch {
                    MessageBox.Show("更改密码失败！");
                }
            } else {
                MessageBox.Show("操作员不存在");
            }
        }
        //取消更改
        private void btnCancel_Click(object sender, RoutedEventArgs e) {
            this.txtNewPass.Clear();
            this.txtPassConf.Clear();
        }
    }
}
