using BankManage.dao;
using BankManage.dao.impl;
using BankManage.model.passChange;
using BankManage.utils;
using BankManage.view.loginForm;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Remoting.Contexts;
using System.Text;
using System.Threading.Tasks;
using System.Web.UI.WebControls;
using System.Windows;
using System.Windows.Input;

namespace BankManage.vm.passChange {
    internal class ChangeAccountVm : NotifyProperty {

        private AccMapper _accMapper = new AccMapperImpl();
        

        private ChangeAccountModel _changeAccount=new ChangeAccountModel();

        public ChangeAccountModel changeAccount {
            get => _changeAccount;
            set => SetProperty(ref _changeAccount, value);
        }


        //更改密码
        public ICommand btnOk_Click { get; set; }
        private void ExecutebtnOk_Click(object obj) {

            //先验证，减少数据库压力
            String newPass = changeAccount.txtNewPass;
            //验证输入一致
            if (newPass != changeAccount.txtPassConf) {
                MessageBox.Show("两次输入密码不一致！");
                return;
            }
            //验证密码复杂度
            if (!Validite.Password(newPass)) {
                MessageBox.Show("密码太简单！");
                return;
            }
            var query = _accMapper.GetAcc(changeAccount.txtAccount);
            if (query.Count() > 0) {
                var q = query.First();
                if (_accMapper.UpdateAcc(q, newPass)) {
                    MessageBox.Show("更改密码成功！");
                } else {
                    MessageBox.Show("更改密码失败！");
                }
            } else {
                MessageBox.Show("账号不存在");
            }
        }

        //取消更改
        public ICommand btnCancel_Click { get; set; }
        private void ExecutebtnCancel_Click(object obj) {
            changeAccount.txtNewPass = "";
            changeAccount.txtPassConf = "";
        }

        public ChangeAccountVm() {
            btnOk_Click = new RelayCommand(ExecutebtnOk_Click);
            btnCancel_Click = new RelayCommand(ExecutebtnCancel_Click);
        }
    }
}
