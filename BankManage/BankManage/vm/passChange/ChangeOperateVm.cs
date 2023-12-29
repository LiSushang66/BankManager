using BankManage.dao.impl;
using BankManage.dao;
using BankManage.model.passChange;
using BankManage.utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Remoting.Contexts;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using System.Linq.Expressions;

namespace BankManage.vm.passChange {
    internal class ChangeOperateVm : NotifyProperty {

        private EmpMapper _empMapper = new EmpMapperImpl();


        private ChangeOperateModel _changeOperate = new ChangeOperateModel();

        public ChangeOperateModel changeOperate {
            get => _changeOperate;
            set => SetProperty(ref _changeOperate, value);
        }


        //更改密码
        public ICommand btnOk_Click { get; set; }
        private void ExecutebtnOk_Click(object obj) {

            //先验证，减少数据库压力
            String newPass = changeOperate.txtNewPass;
            //验证输入一致
            if (newPass != changeOperate.txtPassConf) {
                MessageBox.Show("两次输入密码不一致！");
                return;
            }
            //验证密码复杂度
            if (!Validate.Password(newPass)) {
                MessageBox.Show("密码太简单！");
                return;
            }
            var query = _empMapper.GetEmp(changeOperate.txtEmployee);
            if (query.Count() > 0) {
                var q = query.First();
                if (_empMapper.UpdateEmp(q.EmployeeNo, newPass)) {
                    MessageBox.Show("更改密码成功！");
                } else {
                    MessageBox.Show("更改密码失败！");
                }
            } else {
                MessageBox.Show("雇员不存在");
            }
        }



        //取消更改
        public ICommand btnCancel_Click { get; set; }
        private void ExecutebtnCancel_Click(object obj) {
            changeOperate.txtNewPass = "";
            changeOperate.txtPassConf = "";
        }

        public ChangeOperateVm() {
            btnOk_Click = new RelayCommand(ExecutebtnOk_Click);
            btnCancel_Click = new RelayCommand(ExecutebtnCancel_Click);
        }
    }
}
