using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BankManage.dao {
    internal interface EmpMapper {

        //查询雇员
        List<EmployeeInfo> GetEmp();
        List<EmployeeInfo> GetEmp(string id);
        List<EmployeeInfo> GetEmp(string name, string pass);

        //更新雇员
        bool UpdateEmp(EmployeeInfo emp, string newPass);

    }
}
