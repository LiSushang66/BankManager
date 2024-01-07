using System.Globalization;
using System.Windows.Controls;
using System.Text.RegularExpressions;

namespace BankManage.vm.money.Validation
{
    public class PasswordValidationRule : ValidationRule
    {
        private static readonly string PASSWORD_STRENGTH = @"(?=(.*[a-z]))(?=(.*[A-Z]))(?=(.*\d))(?=(.*[ !""#$%&'()*+,-./:;<=>?@\[\]\^_`{|}~]))^.{8,32}$";
        public override ValidationResult Validate(object value, CultureInfo cultureInfo)
        {
            return new Regex(PASSWORD_STRENGTH).IsMatch(value.ToString()) ? new ValidationResult(true, null) : new ValidationResult(false, "密码格式不正确, 需要大小写字符各至少一个, 特殊字符例如'@'一个,密码长度至少为8");
        }
    }
}
