using BankManage.utils;
using BankManage.view.employee;
using BankManage.view.loginForm;
using BankManage.vm.employee;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Runtime.Remoting.Contexts;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Media.Imaging;
using System.Xml.Linq;
using static log4net.Appender.RollingFileAppender;

namespace BankManage.dao.impl {
    internal class EmpMapperImpl : EmpMapper {


        //新增员工
        public bool InsertEmp(string employeeId, string employeeName, string password, string sex, decimal salary, string telphone, string idCard, byte[] photo) {
            using (BankEntities context = new BankEntities()) {
                context.EmployeeInfo.Add(new EmployeeInfo {
                    EmployeeNo = employeeId,
                    EmployeeName = employeeName,
                    Password = Encrypt.SHA256Encrypt(password),
                    sex = sex,
                    workDate = DateTime.Now,
                    Salary = salary,
                    telphone = telphone,
                    idCard = idCard,
                    photo = photo
                });
                try {
                    context.SaveChanges();
                    return true;
                } catch {
                    return false;
                }
            }
        }
        //删除雇员             
        public bool DeleteEmp(string id) {
            using (BankEntities context = new BankEntities()) {
                var employeeToDelete = context.EmployeeInfo.Find(id);

                if (employeeToDelete != null) {
                    context.EmployeeInfo.Remove(employeeToDelete);
                    try {
                        context.SaveChanges();
                        return true;
                    } catch {
                        return false;
                    }
                }
                return false;
            }
        }

        //修改员工密码
        public bool UpdateEmp(string id, string newPass) {
            using (BankEntities context = new BankEntities()) {
                var q = context.EmployeeInfo.Find(id);
                q.Password = Encrypt.SHA256Encrypt(newPass);
                try {
                    context.SaveChanges();
                    return true;
                } catch {
                    return false;
                }
            }
        }

        //修改员工薪水
        public bool UpdateEmp(string id, decimal salary) {
            using (BankEntities context = new BankEntities()) {
                var q = context.EmployeeInfo.Find(id);
                q.Salary = salary;
                try {
                    context.SaveChanges();
                    return true;
                } catch {
                    return false;
                }
            }
        }


        //修改员工信息
        public bool UpdateEmp(string id, EmployeeInfo emp) {
            using (BankEntities context = new BankEntities()) {
                // 找到数据库中对应的员工信息
                var employee = context.EmployeeInfo.Find(id);

                // 更新员工信息
                employee.EmployeeNo = emp.EmployeeNo;
                employee.EmployeeName = emp.EmployeeName;
                employee.sex = emp.sex;
                employee.Salary = emp.Salary;
                employee.telphone = emp.telphone;
                employee.idCard = emp.idCard;
                employee.photo = emp.photo;
                try {
                    context.SaveChanges();
                    return true;
                } catch {
                    return false;
                }
            }
        }


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
        public List<EmployeeInfo> GetEmp(string id, string pass) {
            using (BankEntities context = new BankEntities()) {
                var query = from t in context.EmployeeInfo
                            where t.EmployeeNo == id && t.Password == pass
                            select t;
                return query.ToList();
            }
        }
    }
}
