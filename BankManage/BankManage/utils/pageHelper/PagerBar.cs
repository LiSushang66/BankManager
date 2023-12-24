using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace BankManage.utils.pageHelper {
    public struct PageSize {
        public int Size { get; set; }

        public PageSize(int size) {
            Size = size;
        }

        public override string ToString() {
            return $"{Size}/页";
        }
    }

    public sealed class PagerBar : ContentControl {
        private readonly PagerButton _btn1;
        private readonly PagerButton _btn2;
        private readonly PagerButton _btn3;
        private readonly PagerButton _btn4;
        private readonly PagerButton _btn5;
        private readonly PagerButton _btnStart;
        private readonly PagerButton _btnEnd;
        private readonly PagerButton _btnLast;
        private readonly PagerButton _btnNext;
        private readonly ComboBox _pageSizeSelector;
        private readonly Label _tbkGoto;
        private readonly TextBox _tbxTargetPageIndex;
        private readonly List<FrameworkElement> _allElement;

        private readonly Style _normalBtnStyle;
        private readonly Style _holderBtnStyle;


        public int CurrentPageIndex {
            get { return (int)GetValue(CurrentPageIndexProperty); }
            set { SetValue(CurrentPageIndexProperty, value); }
        }
        public int PageSize {
            get { return (int)GetValue(PageSizeProperty); }
            set { SetValue(PageSizeProperty, value); }
        }
        public int PageCount {
            get { return (int)GetValue(PageCountProperty); }
            set { SetValue(PageCountProperty, value); }
        }

        public static readonly DependencyProperty CurrentPageIndexProperty = DependencyProperty.Register("CurrentPageIndex", typeof(int), typeof(PagerBar), new PropertyMetadata(-1, CurrentPageIndexPropertyChangedCallback));
        public static readonly DependencyProperty PageSizeProperty = DependencyProperty.Register("PageSize", typeof(int), typeof(PagerBar), new PropertyMetadata(10, PageSizePropertyChangedCallback));
        public static readonly DependencyProperty PageCountProperty = DependencyProperty.Register("PageCount", typeof(int), typeof(PagerBar), new PropertyMetadata(-1, PageCountPropertyChangedCallback));

        private static void CurrentPageIndexPropertyChangedCallback(DependencyObject d, DependencyPropertyChangedEventArgs e) {
            var pagerBar = d as PagerBar;
            pagerBar?.OnCurrentPageIndexChanged((int)e.NewValue);
        }
        private static void PageSizePropertyChangedCallback(DependencyObject d, DependencyPropertyChangedEventArgs e) {
            var pagerBar = d as PagerBar;
            pagerBar?.OnPageSizeChanged((int)e.NewValue);
        }
        private static void PageCountPropertyChangedCallback(DependencyObject d, DependencyPropertyChangedEventArgs e) {
            var pagerBar = d as PagerBar;
            pagerBar?.OnPageCountChanged((int)e.NewValue);
        }



        public PagerBar() {
            _normalBtnStyle = Application.Current.FindResource("PagerButtonStyle") as Style;
            _holderBtnStyle = Application.Current.FindResource("PagerButtonHolderStyle") as Style;

            _btn1 = new PagerButton() { Style = _normalBtnStyle };
            _btn2 = new PagerButton() { Style = _normalBtnStyle };
            _btn3 = new PagerButton() { Style = _normalBtnStyle };
            _btn4 = new PagerButton() { Style = _normalBtnStyle };
            _btn5 = new PagerButton() { Style = _normalBtnStyle };
            _btnStart = new PagerButton() { Style = _normalBtnStyle, Content = "1" };
            _btnEnd = new PagerButton() { Style = _normalBtnStyle, Content = $"{PageCount}" };
            _btnLast = new PagerButton() { Style = _normalBtnStyle, Content = "<" };
            _btnNext = new PagerButton() { Style = _normalBtnStyle, Content = ">" };

            _pageSizeSelector = new ComboBox() {
                Height = 32,
                Width = 100,
                Margin = new Thickness(4, 0, 4, 0),
                FontSize = 14,
                Style = Application.Current.FindResource("ComboBoxStyle") as Style,
            };
            _pageSizeSelector.Items.Add(new PageSize(8 * 1));
            _pageSizeSelector.Items.Add(new PageSize(8 * 2));
            _pageSizeSelector.Items.Add(new PageSize(8 * 5));
            _pageSizeSelector.Items.Add(new PageSize(8 * 10));
            _pageSizeSelector.Items.Add(new PageSize(8 * 20));
            _pageSizeSelector.Items.Add(new PageSize(8 * 50));
            _pageSizeSelector.Items.Add(new PageSize(8 * 100));
            _pageSizeSelector.SelectedIndex = 0;
            _tbkGoto = new Label() { Content = "Go to", FontSize = 14, HorizontalContentAlignment = HorizontalAlignment.Center, VerticalContentAlignment = VerticalAlignment.Center };
            _tbxTargetPageIndex = new TextBox() {
                Width = 50,
                Height = 32,
                FontSize = 14,
                Style = Application.Current.FindResource("TextBoxStyle") as Style,
            };

            var panel = new StackPanel() { Orientation = Orientation.Horizontal, HorizontalAlignment = HorizontalAlignment.Right };

            panel.Children.Add(_btnLast);
            panel.Children.Add(_btnStart);
            panel.Children.Add(_btn1);
            panel.Children.Add(_btn2);
            panel.Children.Add(_btn3);
            panel.Children.Add(_btn4);
            panel.Children.Add(_btn5);
            panel.Children.Add(_btnEnd);
            panel.Children.Add(_btnNext);

            panel.Children.Add(_pageSizeSelector);
            panel.Children.Add(_tbkGoto);
            panel.Children.Add(_tbxTargetPageIndex);

            this.AddChild(panel);

            _allElement = new List<FrameworkElement>
            {
                _btnLast,
                _btnStart,
                _btn1,
                _btn2,
                _btn3,
                _btn4,
                _btn5,
                _btnEnd,
                _btnNext,
                _pageSizeSelector,
                _tbkGoto,
                _tbxTargetPageIndex
            };

            _btnLast.Click += (sender, args) => { if (CurrentPageIndex > 1) CurrentPageIndex--; };
            _btnStart.Click += (sender, args) => { CurrentPageIndex = 1; };
            _btn1.Click += (sender, args) => { CurrentPageIndex = int.Parse(_btn1.Content.ToString()); };
            _btn2.Click += (sender, args) => { CurrentPageIndex = int.Parse(_btn2.Content.ToString()); };
            _btn3.Click += (sender, args) => { CurrentPageIndex = int.Parse(_btn3.Content.ToString()); };
            _btn4.Click += (sender, args) => { CurrentPageIndex = int.Parse(_btn4.Content.ToString()); };
            _btn5.Click += (sender, args) => { CurrentPageIndex = int.Parse(_btn5.Content.ToString()); };
            _btnEnd.Click += (sender, args) => { CurrentPageIndex = PageCount; };
            _btnNext.Click += (sender, args) => { if (CurrentPageIndex < PageCount) CurrentPageIndex++; };

            _pageSizeSelector.SelectionChanged += (sender, args) => {
                PageSize = ((PageSize)_pageSizeSelector.SelectedItem).Size;
            };
            _tbxTargetPageIndex.KeyDown += (sender, args) => {
                if (args.Key == Key.Enter) {
                    if (int.TryParse(_tbxTargetPageIndex.Text, out var result)) {
                        if (result > PageCount || result < 1) {
                            System.Windows.Forms.MessageBox.Show("超出了页码范围", "错误", System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Error);
                        } else {
                            CurrentPageIndex = result;
                        }
                    } else {
                        System.Windows.Forms.MessageBox.Show("请输入正确的页码格式", "错误", System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Error);
                    }

                    _tbxTargetPageIndex.Text = string.Empty;
                }
            };
        }

        public override void OnApplyTemplate() {
            base.OnApplyTemplate();
        }

        private void OnPageCountChanged(int newPageCount) {
            if (_allElement == null || newPageCount == 0 || newPageCount == -1) return;

            switch (newPageCount) {
                case 1: {
                        _allElement.ForEach(element => element.Visibility = Visibility.Collapsed);
                        _btnStart.Visibility = Visibility.Visible;
                        break;
                    }
                case 2:
                case 3:
                case 4:
                case 5:
                case 6:
                case 7: {
                        _btn1.Style = _normalBtnStyle;
                        _btn5.Style = _normalBtnStyle;
                        SetButtonsVisible(newPageCount);
                        break;
                    }
                default: {
                        SetButtonsVisible(7);
                        _btnEnd.Content = $"{newPageCount}";
                        _btn1.Style = _normalBtnStyle;
                        _btn5.Style = _holderBtnStyle;
                        _allElement.GetRange(10, 2).ForEach(e => e.Visibility = Visibility.Visible);
                        break;
                    }
            }

            _pageSizeSelector.Visibility = Visibility.Visible;
        }

        private void SetButtonsVisible(int count) {
            _allElement.ForEach(element => element.Visibility = Visibility.Collapsed);
            var visibleList = _allElement.GetRange(1, count);
            for (var i = 0; i < visibleList.Count; i++) {
                visibleList[i].Visibility = Visibility.Visible;
                if (visibleList[i] is Button btn) {
                    btn.Content = $"{i + 1}";
                }
            }
            _btnLast.Visibility = Visibility;
            _btnNext.Visibility = Visibility;
        }

        private void OnPageSizeChanged(int eNewValue) {

        }

        private void OnCurrentPageIndexChanged(int curIndex) {
            if (_allElement == null) return;
            if (curIndex > PageCount) return;

            foreach (var frameworkElement in _allElement.Take(9)) {
                if (frameworkElement is PagerButton btn) {
                    btn.IsActive = false;
                }
            }

            if (PageCount <= 7) {
                if (_allElement[curIndex] is PagerButton pb) {
                    pb.IsActive = true;
                }
            } else {
                if (curIndex < 4) {
                    if (_allElement[curIndex] is PagerButton btn) {
                        btn.IsActive = true;
                    }
                    _btn1.Style = _normalBtnStyle;
                    _btn5.Style = _holderBtnStyle;
                    _btn1.Content = $"2";
                    _btn2.Content = $"3";
                    _btn3.Content = $"4";
                    _btn4.Content = $"5";

                } else if (curIndex == 4) {
                    _btn3.IsActive = true;
                    _btn1.Style = _normalBtnStyle;
                    _btn5.Style = _holderBtnStyle;
                    SetButtonText(curIndex);
                } else if (curIndex > 4 && curIndex < PageCount - 3) {
                    _btn3.IsActive = true;
                    _btn1.Style = _holderBtnStyle;
                    _btn5.Style = _holderBtnStyle;
                    SetButtonText(curIndex);
                } else if (curIndex == PageCount - 3) {
                    _btn3.IsActive = true;
                    _btn1.Style = _holderBtnStyle;
                    _btn5.Style = _normalBtnStyle;
                    SetButtonText(curIndex);
                } else {
                    if (_allElement[7 - (PageCount - curIndex)] is PagerButton btn) {
                        btn.IsActive = true;
                    }
                    _btn1.Style = _holderBtnStyle;
                    _btn5.Style = _normalBtnStyle;
                    _btn2.Content = $"{PageCount - 4}";
                    _btn3.Content = $"{PageCount - 3}";
                    _btn4.Content = $"{PageCount - 2}";
                    _btn5.Content = $"{PageCount - 1}";
                }
            }


            if (curIndex == 1) {
                _btnLast.IsEnabled = false;
                _btnNext.IsEnabled = true;
            } else if (curIndex == PageCount) {
                _btnLast.IsEnabled = true;
                _btnNext.IsEnabled = false;
            } else {
                _btnLast.IsEnabled = true;
                _btnNext.IsEnabled = true;
            }
        }

        private void SetButtonText(int curIndex) {
            _btn1.Content = $"{curIndex - 2}";
            _btn2.Content = $"{curIndex - 1}";
            _btn3.Content = $"{curIndex - 0}";
            _btn4.Content = $"{curIndex + 1}";
            _btn5.Content = $"{curIndex + 2}";
        }
    }
}