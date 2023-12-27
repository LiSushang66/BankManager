using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BankManage.dao {
    internal interface AccMapper {
        //查询账户
        List<AccountInfo> GetAcc(string id);

        //更新账户
        bool UpdateAcc(AccountInfo acc, string newPass);
    }
}
