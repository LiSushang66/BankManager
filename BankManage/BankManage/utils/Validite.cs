using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace BankManage.utils {
    internal class Validite {

        private static readonly string PASSWORD_STRENGTH = @"(?=(.*[a-z]))(?=(.*[A-Z]))(?=(.*\d))(?=(.*[ !""#$%&'()*+,-./:;<=>?@\[\]\^_`{|}~]))^.{8,32}$";

        public static bool Password(string password) {
            Regex regex = new Regex(PASSWORD_STRENGTH);
            return regex.IsMatch(password);
        }
        public static bool UserName(string userName) {
            return true;
        }
    }
}
