using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BankManage.dao {
    internal interface AccMapper {
        

        //更新账户
        bool UpdateAcc(string id, string newPass);

        //查询账户
        List<AccountInfo> GetAcc(string id);
    }
}
