﻿using System;
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
            DependencyProperty.RegisterAttached("Password", typeof(string), typeof(PasswordBoxHelper), new PropertyMetadata("", OnPasswordPropertyChanged));

        private static void OnPasswordPropertyChanged(DependencyObject sender, DependencyPropertyChangedEventArgs e) {
            PasswordBox box = sender as PasswordBox;
            string password = (string)e.NewValue;
            if (box != null && box.Password != password) {
                box.Password = password;
            }
        }
    }

    /// <summary>
    /// 接收PasswordBox的密码修改事件
    /// </summary>
    public class PasswordBoxBehavior : Behavior<PasswordBox> {
        protected override void OnAttached() {
            base.OnAttached();

            AssociatedObject.PasswordChanged += OnPasswordChanged;
        }

        protected override void OnDetaching() {
            base.OnDetaching();

            AssociatedObject.PasswordChanged -= OnPasswordChanged;
        }

        private static void OnPasswordChanged(object sender, RoutedEventArgs e) {
            PasswordBox box = sender as PasswordBox;
            string password = PasswordBoxHelper.GetPassword(box);
            if (box != null && box.Password != password) {
                PasswordBoxHelper.SetPassword(box, box.Password);
            }
        }
    }
}