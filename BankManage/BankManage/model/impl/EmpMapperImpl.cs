using BankManage.view.loginForm;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace BankManage.dao.impl {
    internal class EmpMapperImpl : EmpMapper {
        //查询所有员工
        public List<EmployeeInfo> GetEmp() {
            using (var dbEntity = new BankEntities()) {
                var query = from t in dbEntity.EmployeeInfo
                            select t;
                return query.ToList();
            }
        }

        //根据员工序号返回用户名
        public EmployeeInfo GetEmp(string id) {
            using (BankEntities c = new BankEntities()) {
                var q = from t in c.EmployeeInfo
                        where t.EmployeeNo == id
                        select t;
                if (q != null && q.Count() >= 1) {
                    return q.First();
                } else {
                    return new EmployeeInfo();
                }
            }
        }
        //根据雇员序号和密码返回LoginInfo
        public List<EmployeeInfo> GetEmp(string name, string pass) {
            using (var dbEntity = new BankEntities()) {
                var query = from t in dbEntity.EmployeeInfo
                            where t.EmployeeNo == name && t.Password == pass
                            select t;
                return query.ToList();
            }
        }
        
    }
}
