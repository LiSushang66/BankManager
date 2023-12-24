using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace BankManage.vm {
    public class NotifyProperty : INotifyPropertyChanged {
        public event PropertyChangedEventHandler PropertyChanged;
        protected void SetProperty<T>(ref T prop, T value, [CallerMemberName] string propertyName = null) {
            if (EqualityComparer<T>.Default.Equals(prop, value) == false) {
                prop = value;
                OnPropertyChanged(propertyName);
            }
        }

        public virtual void OnPropertyChanged([CallerMemberName] string propertyName = null) {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
