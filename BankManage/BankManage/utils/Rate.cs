using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BankManage.utils {
    public class Rate {
        static public TimeSpan Span(MoneyInfo Finance) {
            TimeSpan daysSpan = new TimeSpan(DateTime.Now.Ticks - Finance.dealDate.Ticks);
            return daysSpan;
        }
        static public RateType FindRate(AccountInfo User, MoneyInfo Finance) {
            TimeSpan daysSpan = new TimeSpan(DateTime.Now.Ticks - Finance.dealDate.Ticks);
            bool leap_year = false;
            RateType rate;
            double days = daysSpan.TotalDays;
            if (User.rateType == "定期1年") {
                int year = Finance.dealDate.Year;
                //
                leap_year = IsLeap(year, leap_year);

                //
                if (leap_year) {
                    if (daysSpan.TotalDays >= 366.0) {
                        rate = RateType.定期1年;
                    } else {
                        rate = RateType.定期提前支取;
                    }
                } else {
                    if (daysSpan.TotalDays >= 365.0) {
                        rate = RateType.定期1年;
                    } else {
                        rate = RateType.定期提前支取;
                    }
                }
            } else if (User.rateType == "定期3年") {
                for (int year = Finance.dealDate.Year; year <= DateTime.Now.Year; year++) {
                    leap_year = IsLeap(year, leap_year);
                }
                if (leap_year) {
                    if (daysSpan.TotalDays >= 1096.0) {
                        rate = RateType.定期3年;
                    } else {
                        rate = RateType.定期提前支取;
                    }
                } else {
                    if (daysSpan.TotalDays >= 1095.0) {
                        rate = RateType.定期3年;
                    } else {
                        rate = RateType.定期提前支取;
                    }
                }

            } else if (User.rateType == "定期5年") {
                int leap_year_num = LeapYearNum(Finance, ref leap_year);
                if (leap_year) {
                    if (daysSpan.TotalDays >= 366.0 * leap_year_num + 365 * (5 - leap_year_num)) {
                        rate = RateType.定期5年;
                    } else {
                        rate = RateType.定期提前支取;
                    }
                } else {
                    if (daysSpan.TotalDays >= 1825.0) {
                        rate = RateType.定期5年;
                    } else {
                        rate = RateType.定期提前支取;
                    }
                }

            } else if (User.rateType == "零存整取1年")
                return RateType.零存整取1年;
            else if (User.rateType == "零存整取3年")
                return RateType.零存整取3年;
            else if (User.rateType == "零存整取5年")
                return RateType.零存整取5年;
            else return RateType.活期;

            return rate;
        }

        private static int LeapYearNum(MoneyInfo Finance, ref bool leap_year) {
            int leap_year_num = 0;
            for (int year = Finance.dealDate.Year; year <= DateTime.Now.Year; year++) {
                leap_year = IsLeap(year, leap_year);
                if (IsLeap(year, false)) {
                    leap_year_num++;
                }
            }

            return leap_year_num;
        }
        public static int LeapYearNum(MoneyInfo Finance) {
            int leap_year_num = 0;
            for (int year = Finance.dealDate.Year; year <= DateTime.Now.Year; year++) {
                if (IsLeap(year, false)) {
                    leap_year_num++;
                }
            }

            return leap_year_num;
        }

        public static bool IsLeap(int year, bool leap_year) {
            if ((year % 4 == 0 && year % 100 != 0) || year % 400 == 0) {
                leap_year = true;
            }

            return leap_year;
        }
    }
}