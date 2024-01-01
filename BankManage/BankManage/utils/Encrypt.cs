using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace BankManage.utils {
    internal class Encrypt {

        /// <summary>
        /// SHA256加密
        /// </summary>
        /// <param name="strIN">要加密的string字符串</param>
        /// <returns>SHA256加密之后的密文</returns>
        public static string SHA256Encrypt(string strIN) {
            if (string.IsNullOrEmpty(strIN)) {
                return "";
            }
            byte[] tmpByte;
            SHA256 sha256 = new SHA256Managed();
            tmpByte = sha256.ComputeHash(GetKeyByteArray(strIN));

            StringBuilder rst = new StringBuilder();
            for (int i = 0; i < tmpByte.Length; i++) {
                rst.Append(tmpByte[i].ToString("x2"));
            }
            sha256.Clear();
            return rst.ToString();
        }

        /// <summary>
        /// 获取要加密的string字符串字节数组
        /// </summary>
        /// <param name="strKey">待加密字符串</param>
        /// <returns>加密数组</returns>
        private static byte[] GetKeyByteArray(string strKey) {
            UTF8Encoding Asc = new UTF8Encoding();
            int tmpStrLen = strKey.Length;
            return Asc.GetBytes(strKey);
        }
    }
}
