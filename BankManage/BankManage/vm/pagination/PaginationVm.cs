using System.Collections.ObjectModel;
using System.Linq;
using System.Windows.Input;

using BankManage.component.pagination.pagerControl;
using BankManage.model;

namespace BankManage.vm.pagination {
    public class PaginationVm : NotifyProperty {
        private int Index { get; set; }
        private ObservableCollection<PaginationItem> _paginationItemCollection;
        private ObservableCollection<PaginationItem> _paginationItemCollectionPaging;

        private Pager<PaginationItem> _pager;

        public ObservableCollection<PaginationItem> PaginationItemCollection {
            get => _paginationItemCollection;
            set => SetProperty(ref _paginationItemCollection, value);
        }

        public ObservableCollection<PaginationItem> PaginationItemCollectionPaging {
            get => _paginationItemCollectionPaging;
            set => SetProperty(ref _paginationItemCollectionPaging, value);
        }

        public Pager<PaginationItem> Pager {
            get => _pager;
            set => SetProperty(ref _pager, value);
        }
        public ICommand AddCommand { get; set; }

        private void ExecuteAddCommand(object obj) {
            PaginationItemCollection.Add(new PaginationItem() {
                Id = ++Index,
                //Source = new Random().NextDouble() * 100,
            });
        }
        public ICommand DeleteCommand { get; set; }

        private void ExecuteDeleteCommand(object obj) {
            if (obj is PaginationItem paginationItem) {
                PaginationItemCollection.Remove(paginationItem);
            } else {
                if (PaginationItemCollection.Count > 0) {
                    PaginationItemCollection.RemoveAt(0);
                }
            }
        }
        public ICommand SortCommand { get; set; }

        private void ExecuteSortCommand(object obj) {
            var list = PaginationItemCollection.ToList().OrderByDescending(item => item.Id).ToList();
            PaginationItemCollection.Clear();
            foreach (var item in list) {
                PaginationItemCollection.Add(item);
            }
        }

        public PaginationVm() {
            PaginationItemCollection = new ObservableCollection<PaginationItem>();
            BankEntities context = new BankEntities();
            var query = from t in context.MoneyInfo
                        select t;
            foreach (var v in query.ToList()) {
                PaginationItemCollection.Add(new PaginationItem() {
                    Id = v.Id,
                    accountNo = v.accountNo,
                    dealDate = v.dealDate,
                    dealType = v.dealType,
                    dealMoney = v.dealMoney,
                    balance = v.balance,
                });
            }
            AddCommand = new RelayCommand(ExecuteAddCommand);
            DeleteCommand = new RelayCommand(ExecuteDeleteCommand);
            SortCommand = new RelayCommand(ExecuteSortCommand);

            Pager = new Pager<PaginationItem>(8, PaginationItemCollection);
            Pager.PagerUpdated += items => {
                PaginationItemCollectionPaging = new ObservableCollection<PaginationItem>(items);
            };
            Pager.CurPageIndex = 1;
        }
    }
}
