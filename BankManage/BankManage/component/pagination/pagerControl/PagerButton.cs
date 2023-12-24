using System.Windows;
using System.Windows.Controls;

namespace BankManage.component.pagination.pagerControl {
    public class PagerButton : Button {
        public bool IsActive {
            get { return (bool)GetValue(IsActiveProperty); }
            set { SetValue(IsActiveProperty, value); }
        }

        public static readonly DependencyProperty IsActiveProperty =
            DependencyProperty.Register("IsActive", typeof(bool), typeof(PagerButton), new PropertyMetadata(false));

    }
}