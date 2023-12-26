using System;
using System.Windows.Controls;
using System.Windows;
using BankManage.vm;
using StackExchange.Redis;
using Microsoft.Xaml.Behaviors;

namespace BankManage.component.password {
    /// <summary>
    /// 增加Password扩展属性
    /// </summary>
    internal class PasswordBoxHelper {
        public static string GetPassword(DependencyObject obj) {
            return (string)obj.GetValue(PasswordProperty);
        }

        public static void SetPassword(DependencyObject obj, string value) {
            obj.SetValue(PasswordProperty, value);
        }

        public static readonly DependencyProperty PasswordProperty =

        }
        }
    }

        }


        }

            }
        }
    }
}