using System;
using System.Transactions;
namespace BankManage.utils {
    internal class TransHelper {

        public static void Excute(params Action[] actions) {
            //使用ReadCommitted隔离级别，保持与Sql Server的默认隔离级别一致
            Excute(IsolationLevel.ReadCommitted, null, actions);
        }

        public static void Excute(IsolationLevel level, params Action[] actions) {
            Excute(level, null, actions);
        }
        public static void Excute(int timeOut, params Action[] actions) {
            Excute(IsolationLevel.ReadCommitted, timeOut, actions);
        }
        public static void Excute(IsolationLevel level, int? timeOut, params Action[] actions) {
            if (actions == null || actions.Length == 0)
                return;

            TransactionOptions options = new TransactionOptions();
            options.IsolationLevel = level; //默认为Serializable,这里根据参数来进行调整
            if (timeOut.HasValue)
                options.Timeout = new TimeSpan(0, 0, timeOut.Value); //默认60秒
            using (TransactionScope tran = new TransactionScope(TransactionScopeOption.Required, options)) {
                Array.ForEach<Action>(actions, action => action());
                tran.Complete(); //通知事务管理器它可以提交事务
            }
        }
    }
}
/*示例 1：使用Action参数
csharp
using (var context = new YourDbContext())  
{  
    // 假设你要执行以下数据库操作  
    var actions = new Action[]  
    {  
        () => context.Table1.Add(new Table1 { Name = "Action1" }),  
        () => context.Table2.Add(new Table2 { Value = 100 }),  
        // ... 其他数据库操作  
    };  
  
    // 使用默认的 ReadCommitted 隔离级别执行这些操作  
    DBHelper.Excute(actions);  
}
示例 2：自定义隔离级别和超时时间
csharp
using (var context = new YourDbContext())  
{  
    // 假设你要执行以下数据库操作，并设置特定的隔离级别和超时时间  
    var actions = new Action[]  
    {  
        () => context.Table3.Add(new Table3 { Date = DateTime.Now }),  
        // ... 其他数据库操作  
    };  
  
    // 使用自定义的隔离级别和超时时间执行这些操作  
    DBHelper.Excute(IsolationLevel.Serializable, 30, actions); // 例如，使用Serializable隔离级别，超时时间为30秒  
}
示例 3：单个数据库操作
如果你只有一个数据库操作需要执行，你也可以直接调用Excute方法，例如：

csharp
using (var context = new YourDbContext())  
{  
    // 执行单个数据库操作  
    DBHelper.Excute(() => context.Table4.Add(new Table4 { Value = 200 }));  
}
*/
