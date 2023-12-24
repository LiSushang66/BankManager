using System.Threading.Tasks;
using System;
using System.Windows;
using System.IO;
using System.Diagnostics;

using BankManage.utils;



namespace BankManage {
    /// <summary>
    /// App.xaml 的交互逻辑
    /// </summary>
    public partial class App : Application {
        public App() {
        }

        //protected override void OnStartup(StartupEventArgs e) {
        //    //检测程序唯一时，是否打开了两次应用
        //    CheckProcess();
        //    LogHelper.InitLog4Net();
        //    RegisterEvents();
        //    LogHelper.Loginfo.Info("程序启动");
        //    base.OnStartup(e);
        //}

        ////应用退出删除空日志
        //protected override void OnExit(ExitEventArgs e) {
        //    Console.WriteLine(@"----------------------------------------");
        //    Console.WriteLine(@"应用退出");
        //    //解除文件占用
        //    LogHelper.StopLog();
        //    string[] files = Directory.GetFiles("Log", "*.htm", SearchOption.AllDirectories);
        //    foreach (string filePath in files) {
        //        //跳过占用的文件
        //        if (FileUtils.IsFileInUse(filePath)) {
        //            continue;
        //        }
        //        if (new FileInfo(filePath).Length != 0) {
        //            continue;
        //        }
        //        //删除空日志文件
        //        try {
        //            File.Delete(filePath);
        //        } catch (Exception ex) {
        //            Console.WriteLine(ex);
        //        }
        //    }
        //    base.OnExit(e);
        //    Environment.Exit(0);
        //}

        //private void RegisterEvents() {
        //    //UI线程未捕获异常处理事件（UI主线程）
        //    DispatcherUnhandledException += App_DispatcherUnhandledException;

        //    //非UI线程未捕获异常处理事件(例如自己创建的一个子线程)
        //    AppDomain.CurrentDomain.UnhandledException += CurrentDomain_UnhandledException;

        //    //Task线程内未捕获异常处理事件
        //    TaskScheduler.UnobservedTaskException += TaskScheduler_UnobservedTaskException;
        //}

        ////Task线程内未捕获异常处理事件
        //private static void TaskScheduler_UnobservedTaskException(object sender, UnobservedTaskExceptionEventArgs e) {
        //    e.SetObserved();
        //    if (e.Exception is Exception exception) {
        //        HandleException(exception);
        //    }
        //}

        ////非UI线程未捕获异常处理事件
        //private static void CurrentDomain_UnhandledException(object sender, UnhandledExceptionEventArgs e) {
        //    if (e.ExceptionObject is Exception exception) {
        //        HandleException(exception);
        //    }
        //}

        ////UI线程未捕获异常处理事件（UI主线程）
        //private static void App_DispatcherUnhandledException(object sender, System.Windows.Threading.DispatcherUnhandledExceptionEventArgs e) {
        //    HandleException(e.Exception);
        //    e.Handled = true;
        //}

        ////日志记录
        //private static void HandleException(Exception ex) {
        //    //记录日志
        //    LogHelper.Logerror.Error(ex.Message);
        //    Current.Dispatcher.Invoke(
        //        () => {
        //            Current.Shutdown();
        //        }
        //    );
        //}
        //private static void CheckProcess() {
        //    Console.WriteLine(@"检查是否已开启实例");
        //    string MName = Process.GetCurrentProcess().MainModule.ModuleName;
        //    string PName = Path.GetFileNameWithoutExtension(MName);
        //    Process[] myProcess = Process.GetProcessesByName(PName);

        //    if (myProcess.Length > 1) {
        //        MessageBox.Show("本程序一次只能运行一个实例！", "提示");
        //        Environment.Exit(0); //退出程序
        //        return;
        //    }
        //}
    }
}