using System;

namespace BankManage.utils {
    internal class Uuid {
        public static string GetUid(string fileName) {
            String uuid = Guid.NewGuid().ToString("N");
            // 获得文件后缀名称
            String suffixName = fileName.Substring(fileName.LastIndexOf(".") + 1).ToLower();
            // 生成最新的uuid文件名称
            String newFileName = uuid + "." + suffixName;
            return newFileName;
        }
    }
}
