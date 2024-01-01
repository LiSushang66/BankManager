using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace BankManage.view.rateManage {
    /// <summary>
    /// SlideRation.xaml 的交互逻辑
    /// </summary>
    public partial class SlideRation : Page {
        BankEntities context = new BankEntities();
        public SlideRation() {
            InitializeComponent();
            this.Unloaded += ratePage_Unloaded;
            var rates = from t in context.RateInfo
                        select t;

            IEnumerable<RateInfo> Query;
            foreach (string type in RationType) {
                Query =
                rates.AsQueryable().Where(Rate => Rate.rationType == type);
                switch (type) {
                    case "活期":
                        Flow.Value = (double)Query.ToList()[0].rationValue;
                        break;
                    case "定期1年":
                        Fixed_Ration_1.Value = (double)Query.ToList()[0].rationValue;
                        break;
                    case "定期3年":
                        Fixed_Ration_2.Value = (double)Query.ToList()[0].rationValue;
                        break;
                    case "定期5年":
                        Fixed_Ration_3.Value = (double)Query.ToList()[0].rationValue;
                        break;
                    case "零存整取1年":
                        Specified_Ration_1.Value = (double)Query.ToList()[0].rationValue;
                        break;
                    case "零存整取3年":
                        Specified_Ration_2.Value = (double)Query.ToList()[0].rationValue;
                        break;
                    case "零存整取5年":
                        Specified_Ration_3.Value = (double)Query.ToList()[0].rationValue;
                        break;
                }
            }
        }

        public string[] RationType = {
                "活期",
                "定期1年",
                "定期3年",
                "定期5年",
                "零存整取1年",
                "零存整取3年",
                "零存整取5年"
            };

        void ratePage_Unloaded(object sender, RoutedEventArgs e) {
            context.Dispose();
        }

        //保存
        private void Button_Click(object sender, RoutedEventArgs e) {
            foreach (string type in RationType) {
                var Single_Rate = (from t in context.RateInfo
                                   where t.rationType == type
                                   select t).ToList().First();
                switch (type) {
                    case "活期":
                        Single_Rate.rationValue = Flow.Value;
                        break;
                    case "定期1年":
                        Single_Rate.rationValue = Fixed_Ration_1.Value;
                        break;
                    case "定期3年":
                        Single_Rate.rationValue = Fixed_Ration_2.Value;
                        break;
                    case "定期5年":
                        Single_Rate.rationValue = Fixed_Ration_3.Value;
                        break;
                    case "零存整取1年":
                        Single_Rate.rationValue = Specified_Ration_1.Value;
                        break;
                    case "零存整取3年":
                        Single_Rate.rationValue = Specified_Ration_2.Value;
                        break;
                    case "零存整取5年":
                        Single_Rate.rationValue = Specified_Ration_3.Value;
                        break;
                }

            }

            try {
                context.SaveChanges();
                MessageBox.Show("保存成功！");
            } catch (Exception ex) {
                MessageBox.Show(ex.Message, "保存失败");
            }
        }
    }
}