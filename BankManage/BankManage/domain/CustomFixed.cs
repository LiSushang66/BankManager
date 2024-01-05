using System;
using System.Windows;
using BankManage.model;
using BankManage.utils;

namespace BankManage.domain {
    /// <summary>
    /// 定期存款
    /// </summary>
    public class CustomFixed : Custom {
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

            MoneyInfo deal = DataOperation.GetRecentDeal(AccountInfo.accountNo);
            if (deal.dealType == "开户" || deal.dealType == "取款") {
                //存款
                base.Diposit("存款", money);
            } else {
                MessageBox.Show("定期存款账户只能整存整取");
            }



        }

        /// <summary>
        ///取款 
        /// </summary>
        /// <param name="money">取款金额</param>
        public override void Withdraw(double money) {
            RateType type = RateType.活期;
            MessageBoxResult result = MessageBoxResult.Yes;

            if (!ValidBeforeWithdraw(money)) return;

            //计算利息
            if ((int)money != (int)AccountBalance) {
                MessageBox.Show("请整取" + ((int)AccountBalance).ToString() + "元");
                return;
            }


            MoneyInfo deal = DataOperation.GetRecentDeal(AccountInfo.accountNo);
            type = Rate.FindRate(AccountInfo, deal);
            double rate = 0;

            if (type != RateType.定期提前支取) {
                if (type == RateType.定期1年) {
                    rate = DataOperation.GetRate(type) * AccountBalance;
                    rate += DataOperation.GetRate(RateType.定期超期部分) * (Rate.Span(deal).TotalDays - (Rate.LeapYearNum(deal) * 366 + (1 - Rate.LeapYearNum(deal) * 365)) / 365);
                } else if (type == RateType.定期3年) {
                    rate = DataOperation.GetRate(type) * AccountBalance * 3;
                    rate += DataOperation.GetRate(RateType.定期超期部分) * (Rate.Span(deal).TotalDays - (Rate.LeapYearNum(deal) * 366 + (3 - Rate.LeapYearNum(deal) * 365)) / 365);
                } else if (type == RateType.定期5年) {
                    rate = DataOperation.GetRate(type) * AccountBalance * 5;
                    rate += DataOperation.GetRate(RateType.定期超期部分) * (Rate.Span(deal).TotalDays - (Rate.LeapYearNum(deal) * 366 + (5 - Rate.LeapYearNum(deal) * 365)) / 365);
                }
            }
            else if (type == RateType.定期提前支取) {
                MessageBoxButton button = MessageBoxButton.YesNo;
                result = MessageBox.Show("期限未到提前支取", "提前支取", button);
                TimeSpan Cur = Rate.Span(deal);
                rate = DataOperation.GetRate(RateType.定期提前支取) * AccountBalance * (Cur.TotalDays / 365);

            } 
            else {
                MessageBox.Show("账号利率类别异常");
                return;
            }

            if (result == MessageBoxResult.Yes) {
                //添加利息
                base.Diposit("结息", rate);
                //取款
                base.Withdraw(AccountBalance);
            }
        }
    }
}
