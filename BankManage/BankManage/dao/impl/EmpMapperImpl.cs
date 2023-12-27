using BankManage.utils;
using BankManage.view.loginForm;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Remoting.Contexts;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace BankManage.dao.impl {
    internal class EmpMapperImpl : EmpMapper {
        //查询所有员工
        public List<EmployeeInfo> GetEmp() {
            using (BankEntities context = new BankEntities()) {
                var query = from t in context.EmployeeInfo
                            select t;
                return query.ToList();
            }
        }

        //根据员工序号返回用户信息
        public List<EmployeeInfo> GetEmp(string id) {
            using (BankEntities context = new BankEntities()) {
                var q = from t in context.EmployeeInfo
                        where t.EmployeeNo == id
                        select t;
                return q.ToList();
            }
        }
        //根据雇员序号和密码返回LoginInfo
        public List<EmployeeInfo> GetEmp(string name, string pass) {
            using (BankEntities context = new BankEntities()) {
                var query = from t in context.EmployeeInfo
                            where t.EmployeeNo == name && t.Password == pass
                            select t;
                return query.ToList();
            }
        }

        public bool UpdateEmp(EmployeeInfo emp, string newPass) {
            using (BankEntities context = new BankEntities()) {
                emp.Password = Encrypt.SHA256Encrypt(newPass);
                try {
                    context.SaveChanges();
                } catch {
                    return false;
                }
            }
            return true;
        }
    }
}
