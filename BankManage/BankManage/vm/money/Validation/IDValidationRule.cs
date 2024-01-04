using System;
using System.Globalization;
using System.Text.RegularExpressions;
using System.Windows.Controls;


namespace BankManage.vm.money.Validation
{
    public class IDValidationRule : ValidationRule
    {
        private static readonly string pattern = @"^\d{17}(?:\d|X)$";
        public override ValidationResult Validate(object value, CultureInfo cultureInfo)
        {
            string ID = value.ToString();
            if (ID.Length < 18 || ID.Length > 18) 
            {
                return new ValidationResult(false, "身份证长度不为18,为"+ ID.Length);
            }
            // 加权数组,用于验证最后一位的校验数字
            int[] arr_weight = { 7, 9, 10, 5, 8, 4, 2, 1, 6, 3, 7, 9, 10, 5, 8, 4, 2 };
            // 最后一位计算出来的校验数组，如果不等于这些数字将不正确
            string[] id_last = { "1", "0", "X", "9", "8", "7", "6", "5", "4", "3", "2" };
            int sum = 0;
            // 实际校验位的值
            
            if (Regex.IsMatch(ID, pattern))
            {   // 出生日期检查
                string birth = ID.Substring(6, 8).Insert(6, "-").Insert(4, "-");
                if (DateTime.TryParse(birth, out _))
                {
                    //通过循环前16位计算出最后一位的数字
                    for (int i = 0; i < 17; i++)
                    {
                        sum += arr_weight[i] * int.Parse(ID[i].ToString());
                    }
                    int result = sum % 11;
                    // 校验位检查
                    if (id_last[result] == ID[17].ToString())
                    {
                        return new ValidationResult(true, null); //格式正确
                        
                    }
                    else
                    {
                        return new ValidationResult(false, "身份证最后一位校验错误!"); //最后一位校验错误!
                        
                    }
                }
                else
                {
                    return new ValidationResult(false, "身份证出生日期验证失败!"); //出生日期验证失败!
                }
            }
            else
            {
                return new ValidationResult(false, "身份证号格式错误!"); //身份证号格式错误!
            }
        }
    }
}
