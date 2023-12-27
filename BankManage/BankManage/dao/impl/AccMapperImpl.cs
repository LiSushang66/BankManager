using BankManage;
using BankManage.utils;
using BankManage.view.passChange;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BankManage.dao.impl {
    internal class AccMapperImpl : AccMapper {

        //查询指定id的账户
        public List<AccountInfo> GetAcc(string id) {
            using (BankEntities context = new BankEntities()) {
                var q = from t in context.AccountInfo
                        where t.accountNo == id
                        select t;
                return q.ToList();
            }
        }

        //更新指定账户的密码
        public bool UpdateAcc(AccountInfo acc, string newPass) {
            using (BankEntities context = new BankEntities()) {
                acc.accountPass = Encrypt.SHA256Encrypt(newPass);
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
