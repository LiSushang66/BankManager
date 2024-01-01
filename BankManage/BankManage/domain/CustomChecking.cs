using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using BankManage;
using BankManage.model;

namespace BankManage.domain {
    public class CustomChecking : Custom {
        /// 开户
        /// </summary>
        /// <param name="accountNumber">帐号</param>
        /// <param name="money">开户金额</param>
        public override void Create(string accountNumber, double money) {
            base.Create(accountNumber, money);
        }

        /// <summary>
        ///存款 
        /// </summary>
        public override void Diposit(string genType, double money) {
            //结算利息 
            TimeSpan Cur = new TimeSpan(DateTime.Now.Ticks - DataOperation.GetRecentDeal(AccountInfo.accountNo).dealDate.Ticks);
            base.Diposit("结息", DataOperation.GetRate(RateType.活期) * money * (Cur.TotalDays / 365.0));
            base.Diposit("存款", money);
        }

        /// <summary>
        ///取款 
        /// </summary>
        /// <param name="money">取款金额</param>
        public override void Withdraw(double money) {
            TimeSpan Cur = new TimeSpan(DateTime.Now.Ticks - DataOperation.GetRecentDeal(AccountInfo.accountNo).dealDate.Ticks);
            //添加利息
            base.Diposit("结息", DataOperation.GetRate(RateType.活期) * money * (Cur.TotalDays / 365));
            base.Withdraw(money);
        }
    }
}
