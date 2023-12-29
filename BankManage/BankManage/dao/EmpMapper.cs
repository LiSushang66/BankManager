using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BankManage.dao {
    internal interface EmpMapper {
        //新增员工
        bool InsertEmp(string employeeId,string employeeName, string password, string sex, decimal salary, string telphone, string idCard, byte[] photo);


        //删除雇员
        bool DeleteEmp(string id);

        //更新雇员
        bool UpdateEmp(string id, string newPass);
        bool UpdateEmp(string id,decimal salary);
        bool UpdateEmp(string id, EmployeeInfo emp);

        //查询雇员
        List<EmployeeInfo> GetEmp();
        List<EmployeeInfo> GetEmp(string id);
        List<EmployeeInfo> GetEmp(string id, string pass);
    }
}
