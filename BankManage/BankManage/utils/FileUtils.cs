using System.IO;


namespace BankManage.utils {
    /// <summary>
    /// 文件是否占用
    /// </summary>
    /// <param name="fileName"></param>
    /// <returns></returns>


    internal class FileUtils {
        public static bool IsFileInUse(string fileName) {
            bool inUse = true;

            FileStream fs = null;
            try {

                fs = new FileStream(fileName, FileMode.Open, FileAccess.Read, FileShare.None);

                inUse = false;
            } catch {
            } finally {
                if (fs != null)

                    fs.Close();
            }
            return inUse;//true表示正在使用,false没有使用
        }
    }
}
