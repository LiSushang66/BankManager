using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.Linq;
using BankManage.vm;

namespace BankManage.component.pagination.pagerControl {
    public delegate void PagerUpdatedDelegate<T>(List<T> curPageItems);

    public sealed class Pager<T> : NotifyProperty {
        private readonly ObservableCollection<T> _itemsSource;
        private int _pageSize = 6;
        private int _pageCount;
        private int _curPageIndex = 0;



        public int PageSize {
            get => _pageSize;
            set {
                if (value == _pageSize)
                    return;
                _pageSize = value;
                OnPropertyChanged();
                SetPageSize(_pageSize);
            }
        }
        public int PageCount {
            get => _pageCount;
            set {
                if (value == _pageCount)
                    return;
                _pageCount = value;

                // 最少页为1页
                if (_pageCount == 0)
                    _pageCount = 1;
                OnPropertyChanged();
            }
        }
        public int CurPageIndex {
            get => _curPageIndex;
            set {
                if (value == _curPageIndex)
                    return;
                _curPageIndex = value;
                OnPropertyChanged();
                GotoPageOf(_curPageIndex);
            }
        }



        public Pager(int pageSize, List<T> source) {
            _pageSize = pageSize;
            _itemsSource = new ObservableCollection<T>(source);
            CalculatePaging();
        }

        public Pager(int pageSize, ObservableCollection<T> source) {
            _pageSize = pageSize;
            _itemsSource = source;
            _itemsSource.CollectionChanged += ItemsSourceOnCollectionChanged;
            CalculatePaging();
        }

        private void ItemsSourceOnCollectionChanged(object sender, NotifyCollectionChangedEventArgs e) {
            CalculatePaging();
            GotoPageOf(CurPageIndex);
        }

        private void CalculatePaging() {
            PageCount = (int)Math.Ceiling((double)_itemsSource.Count / PageSize);
        }


        public List<T> NextPage() {
            return GotoPageOf(CurPageIndex + 1);
        }

        public List<T> LastPage() {
            return GotoPageOf(CurPageIndex - 1);
        }

        public List<T> GotoPageOf(int pageIndex) {
            if (pageIndex < 1) pageIndex = 1;
            if (pageIndex > PageCount) pageIndex = PageCount;
            CurPageIndex = pageIndex;
            var skip = (pageIndex - 1) * PageSize;
            var maxIndex = pageIndex * PageSize - 1;
            if (maxIndex >= _itemsSource.Count) maxIndex = _itemsSource.Count - 1;

            var list = _itemsSource.ToList().GetRange(skip, maxIndex - skip + 1);
            OnPagerUpdated(list);

            return list;
        }

        public List<T> GotoEndPage() {
            return GotoPageOf(PageCount);
        }

        public List<T> GotoStartPage() {
            return GotoPageOf(1);
        }

        public List<T> SetPageSize(int pageSize) {
            PageSize = pageSize;
            CalculatePaging();
            return GotoPageOf(1);
        }



        public event PagerUpdatedDelegate<T> PagerUpdated;

        private void OnPagerUpdated(List<T> curPageItems) {
            PagerUpdated?.Invoke(curPageItems);
        }
    }
}
