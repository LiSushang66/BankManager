using BankManage.model;
using BankManage.utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace BankManage.domain {
    class CustomSpecified : Custom {

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
            MoneyInfo Open = DataOperation.GetOpenAccount(AccountInfo.accountNo);
            RateType type = Rate.FindRate(AccountInfo, Open);
            //结算利息
            if ((DataOperation.GetRecentDeal(AccountInfo.accountNo).dealType != DataOperation.GetOpenAccount(AccountInfo.accountNo).dealType)
                && (DataOperation.GetRecentDeal(AccountInfo.accountNo).dealType != DataOperation.GetRecentWithDraw(AccountInfo.accountNo).dealType)) {
                TimeSpan Cur = new TimeSpan(DateTime.Now.Ticks - DataOperation.GetRecentDeposit(AccountInfo.accountNo).dealDate.Ticks);
                if (Cur.TotalDays >= 45) {
                    MessageBox.Show("零存整取违规请取出钱");
                    return;
                }

                if (money < 500) {
                    MessageBox.Show("存款过少");
                    return;
                }
                double lastDeposit = DataOperation.GetRecentDeposit(AccountInfo.accountNo).dealMoney;
                string MoneyOnLaw = lastDeposit.ToString("f0");
                lastDeposit = Convert.ToDouble(MoneyOnLaw);
                if ((int)lastDeposit != (int)money) {
                    MessageBox.Show("请前后存储金额一致: 要存" + lastDeposit.ToString() + "元");
                    return;
                }
                base.Diposit("结息", DataOperation.GetRate(type) * money * (Cur.TotalDays / 365));
                base.Diposit("存款", money);

            } else {
                base.Diposit("存款", money);
            }

        }

        public override void Withdraw(double money) {
            if (!ValidBeforeWithdraw(money)) return;
            //取款
            if ((int)money != (int)AccountBalance) {
                MessageBox.Show("请整取" + ((int)AccountBalance).ToString() + "元");
                return;
            }
            if (DataOperation.GetRecentDeposit(AccountInfo.accountNo).dealType == "没有存款记录")
            {
                TimeSpan OpenAccTime = new TimeSpan(DateTime.Now.Ticks - DataOperation.GetOpenAccount(AccountInfo.accountNo).dealDate.Ticks);
                Settlement(OpenAccTime);
            }
            TimeSpan Over = new TimeSpan(DateTime.Now.Ticks - DataOperation.GetRecentDeposit(AccountInfo.accountNo).dealDate.Ticks);
            MoneyInfo Open = DataOperation.GetOpenAccount(AccountInfo.accountNo);
            MoneyInfo recWithDraw = DataOperation.GetRecentWithDraw(AccountInfo.accountNo);
            TimeSpan Cur;
            if (recWithDraw.dealType == "没有取款记录") {
                Cur = new TimeSpan(DateTime.Now.Ticks - DataOperation.GetOpenAccount(AccountInfo.accountNo).dealDate.Ticks);
                recWithDraw = DataOperation.GetOpenAccount(AccountInfo.accountNo);
            } else {
                Cur = new TimeSpan(DateTime.Now.Ticks - DataOperation.GetRecentWithDraw(AccountInfo.accountNo).dealDate.Ticks);
            }

            double rate = 0;
            if (Over.TotalDays >= 45) {
                Settlement(Cur);
                return;
            }

            RateType type = Rate.FindRate(AccountInfo, Open);
            int leap_year = Rate.LeapYearNum(MoneyInfo);

            MessageBoxButton button = MessageBoxButton.YesNo;
            if (type == RateType.零存整取1年) {
                if (Cur.TotalDays < leap_year * 366 + (1 - leap_year) * 365) {
                    MessageBoxResult Result = MessageBox.Show("期限未到是否提前取出？", "警告", button);
                    if (Result == MessageBoxResult.Yes) {
                        Settlement(Cur);
                        return;
                    } else {
                        return;
                    }
                }
            } else if (type == RateType.零存整取3年) {
                if (Cur.TotalDays < leap_year * 366 + (1 - leap_year) * 365) {
                    MessageBoxResult Result = MessageBox.Show("期限未到是否提前取出？", "警告", button);
                    if (Result == MessageBoxResult.Yes) {
                        Settlement(Cur);
                        return;
                    } else {
                        return;
                    }
                }
            } else if (type == RateType.零存整取5年) {
                if (Cur.TotalDays < leap_year * 366 + (1 - leap_year) * 365) {
                    MessageBoxResult Result = MessageBox.Show("期限未到是否提前取出？", "警告", button);
                    if (Result == MessageBoxResult.Yes) {
                        Settlement(Cur);
                        return;
                    } else {
                        return;
                    }
                }
            }

            if (type == RateType.零存整取1年) {
                rate = DataOperation.GetRate(type) * AccountBalance;
                rate += DataOperation.GetRate(RateType.零存整取超期部分) * (Cur.TotalDays - (Rate.LeapYearNum(recWithDraw) * 366 + (1 - Rate.LeapYearNum(recWithDraw) * 365)) / 365);
            } else if (type == RateType.零存整取3年) {
                rate = DataOperation.GetRate(type) * AccountBalance * 3;
                rate += DataOperation.GetRate(RateType.零存整取超期部分) * (Cur.TotalDays - (Rate.LeapYearNum(recWithDraw) * 366 + (3 - Rate.LeapYearNum(recWithDraw) * 365)) / 365);
            } else if (type == RateType.零存整取5年) {
                rate = DataOperation.GetRate(type) * AccountBalance * 5;
                rate += DataOperation.GetRate(RateType.零存整取超期部分) * (Cur.TotalDays - (Rate.LeapYearNum(recWithDraw) * 366 + (5 - Rate.LeapYearNum(recWithDraw) * 365)) / 365);
            }
            //添加利息
            base.Diposit("结息", rate);
            //取款
            base.Withdraw(money);
        }

        private void Settlement(TimeSpan Cur) {
            double rate = DataOperation.GetRate(RateType.零存整取违规) * (Cur.TotalDays / 365);
            base.Diposit("结息", rate);
            //取款
            base.Withdraw(AccountBalance);
            return;
        }
    }
}
