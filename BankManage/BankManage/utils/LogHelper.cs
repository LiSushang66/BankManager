using System;
using System.IO;

using log4net;
using log4net.Config;

namespace BankManage.utils {
    internal class LogHelper {
        public static readonly ILog Loginfo = LogManager.GetLogger("loginfo");
        public static readonly ILog Logerror = LogManager.GetLogger("logerror");

        public static void InitLog4Net() {
            FileInfo logCfg = new FileInfo(AppDomain.CurrentDomain.BaseDirectory + "log4net.config");
            XmlConfigurator.ConfigureAndWatch(logCfg);
        }

        /// <summary>
        /// 停止日志记录
        /// </summary>
        public static void StopLog() {
            //应用结束前要上传日志文件，我们就要停止日志，防止文件占用
            log4net.LogManager.Shutdown();
        }
    }
}
