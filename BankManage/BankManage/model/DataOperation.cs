﻿using BankManage.domain;
using System.Linq;

namespace BankManage.model
{
    public class DataOperation
    {
        public static readonly string[] AccountType = { "活期存款", "定期存款", "零存整取" };
        /// <summary>
        /// 获取操作员姓名
        /// </summary>
        /// <param name="id">操作员编号</param>
        /// <returns></returns>
        public static string GetOperateName(string id)
        {
            using (BankEntities c = new BankEntities())
            {
                var q = from t in c.EmployeeInfo
                         where t.EmployeeNo == id
                         select t;
                if (q != null && q.Count()>=1)
                {
                    return q.First().EmployeeName;
                }
                else
                {
                    return "";
                }
            }
        }

        /// <summary>
        /// 根据存款类型创建存款用户
        /// </summary>
        /// <param name="accountType">存款类型</param>
        /// <returns></returns>
        public static Custom CreateCustom(string accountType)
        {
            Custom custom = null;
            switch (accountType)
            {
                case "活期存款":
                    custom = new CustomChecking();
                    break;
                case "定期存款":
                    custom = new CustomFixed();
                    break;
                case "零存整取":
                    custom = new CustomSpecified();
                    break;
            }
            custom.AccountInfo.accountType = accountType;
            return custom;
        }

        /// <summary>
        /// 获取存款用户信息,并初始化余额
        /// </summary>
        /// <param name="accountNumber"></param>
        /// <returns></returns>
        public static Custom GetCustom(string accountNumber)
        {
            Custom custom = null;
            BankEntities c = new BankEntities();
            try
            {
                var query= from t in c.AccountInfo
                         where t.accountNo == accountNumber
                         select t;
                if (query.Count() > 0)
                {
                    var q = query.Single();
                    custom = CreateCustom(q.accountType);
                    custom.AccountInfo.accountNo = accountNumber;
                    custom.AccountInfo.accountName = q.accountName;
                    custom.AccountInfo.accountPass = q.accountPass;
                    custom.AccountInfo.IdCard = q.IdCard;
                    custom.AccountInfo.rateType = q.rateType;
                }
            }
            catch
            {
                return null;
            }
            var qt = from t in c.MoneyInfo
                      where t.accountNo == accountNumber
                      select t;
            if (qt != null && qt.Count() > 0)
            {
                custom.AccountBalance = qt.Sum(x => x.dealMoney);
            }
            return custom;
        }

        /// <summary>
        /// 获取指定类别的利率
        /// </summary>
        /// <param name="rateType">利率类别</param>
        /// <returns>对应类别的利率值</returns>
        public static double GetRate(RateType rateType)
        {
            string type = rateType.ToString();
            BankEntities c = new BankEntities();
            var q = (from t in c.RateInfo
                     where t.rationType == type
                     select t.rationValue).Single();
            return q.Value;
        }





        public static MoneyInfo CreateDealRecord() {
            MoneyInfo deal = new MoneyInfo();
            return deal;
        }

        public static MoneyInfo GetRecentDeal(string accountNumber) {
            MoneyInfo deal = null;
            BankEntities c = new BankEntities();
            try {
                var query = from t in c.MoneyInfo
                            where t.accountNo == accountNumber
                            orderby t.dealDate descending
                            select t;
                if (query.Count() > 0) {
                    var q = query.First();
                    deal = CreateDealRecord();
                    deal.accountNo = accountNumber;
                    deal.balance = q.balance;
                    deal.dealDate = q.dealDate;
                    deal.dealMoney = q.dealMoney;
                    deal.dealType = q.dealType;
                }

            } catch {
                return null;
            }
            return deal;
        }


        public static MoneyInfo GetLastDeal(string accountNumber) {
            MoneyInfo deal = null;
            BankEntities c = new BankEntities();
            try {
                var query = from t in c.MoneyInfo
                            where t.accountNo == accountNumber && t.dealType == "取款"
                            orderby t.dealDate descending
                            select t;
                if (query.Count() > 0) {
                    var q = query.First();
                    deal = CreateDealRecord();
                    deal.accountNo = accountNumber;
                    deal.balance = q.balance;
                    deal.dealDate = q.dealDate;
                    deal.dealMoney = q.dealMoney;
                    deal.dealType = q.dealType;
                }
            } catch {
                return null;
            }
            return deal;
        }


        public static MoneyInfo GetOpenAccount(string accountNumber) {
            MoneyInfo deal = null;
            BankEntities c = new BankEntities();
            try {
                var query = from t in c.MoneyInfo
                            where t.accountNo == accountNumber && t.dealType == "开户"
                            orderby t.dealDate descending
                            select t;
                if (query.Count() > 0) {
                    var q = query.First();
                    deal = CreateDealRecord();
                    deal.accountNo = accountNumber;
                    deal.balance = q.balance;
                    deal.dealDate = q.dealDate;
                    deal.dealMoney = q.dealMoney;
                    deal.dealType = q.dealType;
                }
            } catch {
                return null;
            }
            return deal;
        }


        public static MoneyInfo GetRecentDeposit(string accountNumber) {
            MoneyInfo deal = null;
            BankEntities c = new BankEntities();
            try {
                var query = from t in c.MoneyInfo
                            where t.accountNo == accountNumber && t.dealType == "存款"
                            orderby t.dealDate descending
                            select t;
                if (query.Count() > 0)
                {
                    var q = query.First();
                    deal = CreateDealRecord();
                    deal.accountNo = accountNumber;
                    deal.balance = q.balance;
                    deal.dealDate = q.dealDate;
                    deal.dealMoney = q.dealMoney;
                    deal.dealType = q.dealType;
                }
                else 
                {
                    deal = CreateDealRecord();
                    deal.dealType = "没有存款记录";
                }
            } catch {
                return null;
            }
            return deal;
        }


        public static MoneyInfo GetRecentWithDraw(string accountNumber) {
            MoneyInfo deal = null;
            BankEntities c = new BankEntities();
            try {
                var query = from t in c.MoneyInfo
                            where t.accountNo == accountNumber && t.dealType == "取款"
                            orderby t.dealDate descending
                            select t;
                if (query.Count() > 0) {
                    var q = query.First();
                    deal = CreateDealRecord();
                    deal.accountNo = accountNumber;
                    deal.balance = q.balance;
                    deal.dealDate = q.dealDate;
                    deal.dealMoney = q.dealMoney;
                    deal.dealType = q.dealType;
                } else {
                    deal = CreateDealRecord();
                    deal.dealType = "没有取款记录";
                }
            } catch {

                return null;
            }
            return deal;
        }
    }
}
