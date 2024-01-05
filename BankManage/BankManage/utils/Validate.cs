using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace BankManage.utils {
    internal class Validate {


        //判断密码是否包含大小写数字特殊字符，且大于8位小于32位
        public static bool Password(string password) {
            if (password == null) {
                return false;
            }
            string PASSWORD_STRENGTH = @"(?=(.*[a-z]))(?=(.*[A-Z]))(?=(.*\d))(?=(.*[ !""#$%&'()*+,-./:;<=>?@\[\]\^_`{|}~]))^.{8,32}$";
            return Regex.IsMatch(password, PASSWORD_STRENGTH);
        }

        //判断手机号
        public static bool IsHandset(string str_handset) {
            return Regex.IsMatch(str_handset, @"^1[3456789]\d{9}$");
        }


        //判断身份证号
        public static int IDchecking(string ID) {
            string pattern = @"^\d{17}(?:\d|X)$";
            // 加权数组,用于验证最后一位的校验数字
            int[] arr_weight = { 7, 9, 10, 5, 8, 4, 2, 1, 6, 3, 7, 9, 10, 5, 8, 4, 2 };
            // 最后一位计算出来的校验数组，如果不等于这些数字将不正确
            string[] id_last = { "1", "0", "X", "9", "8", "7", "6", "5", "4", "3", "2" };
            int sum = 0;
            if (Regex.IsMatch(ID, pattern)) {   // 出生日期检查
                string birth = ID.Substring(6, 8).Insert(6, "-").Insert(4, "-");
                if (DateTime.TryParse(birth, out _)) {
                    //通过循环前16位计算出最后一位的数字
                    for (int i = 0; i < 17; i++) {
                        sum += arr_weight[i] * int.Parse(ID[i].ToString());
                    }
                    // 实际校验位的值
                    int result = sum % 11;
                    // 校验位检查
                    if (id_last[result] == ID[17].ToString()) {
                        return 0; //格式正确
                    } else {
                        return 1; //最后一位校验错误!
                    }
                } else {
                    return 2; //出生日期验证失败!
                }
            } else {
                //身份证号格式错误!
                return 3;
            }
        }
    }
}