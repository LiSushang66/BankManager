using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using MaterialDesignThemes.Wpf;

namespace BankManage.view.system {
    /// <summary>
    /// Theme.xaml 的交互逻辑
    /// </summary>
    public partial class Theme : Page {
        public Theme() {
            InitializeComponent();
        }

        private void Button_Click(object sender, RoutedEventArgs e) {
            var paletteHelper = new PaletteHelper();

            ITheme theme = paletteHelper.GetTheme();


            Button item = e.Source as Button;

            switch (item.Tag) {
                case "玉红":
                    theme.SetPrimaryColor(Color.FromRgb(192, 72, 81));
                    break;
                case "靛青":
                    theme.SetPrimaryColor(Color.FromRgb(22, 97, 171));
                    break;
                case "竹绿":
                    theme.SetPrimaryColor(Color.FromRgb(27, 167, 132));
                    break;
                case "新禾绿":
                    theme.SetPrimaryColor(Color.FromRgb(210, 177, 22));
                    break;
                case "河豚灰":
                    theme.SetPrimaryColor(Color.FromRgb(57, 55, 51));
                    break;
                case "美人焦橙":
                    theme.SetPrimaryColor(Color.FromRgb(250, 126, 35));
                    break;
                case "榴子红":
                    theme.SetPrimaryColor(Color.FromRgb(241, 144, 140));
                    break;
                case "钢青":
                    theme.SetPrimaryColor(Color.FromRgb(20, 35, 52));
                    break;
                case "默认":
                    theme.SetPrimaryColor(Color.FromRgb(130, 58, 183));
                    break;

                default:
                    break;
            }
            ThemeColor themeSetting = ThemeColor.Default;
            ThemeColor.Default.主题 = item.Tag.ToString();
            themeSetting.Save();
            paletteHelper.SetTheme(theme);

        }
    }
}
