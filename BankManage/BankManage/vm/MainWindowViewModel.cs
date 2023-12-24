using System;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows.Input;

using BankManage.utils.pageHelper;

namespace BankManage.vm {
    public class MainWindowViewModel : NotifyProperty {
        private int Index { get; set; }
        private ObservableCollection<Student> _studentCollection;
        private ObservableCollection<Student> _studentCollectionPaging;

        private Pager<Student> _pager;

        public ObservableCollection<Student> StudentCollection {
            get => _studentCollection;
            set => SetProperty(ref _studentCollection, value);
        }

        public ObservableCollection<Student> StudentCollectionPaging {
            get => _studentCollectionPaging;
            set => SetProperty(ref _studentCollectionPaging, value);
        }

        public Pager<Student> Pager {
            get => _pager;
            set => SetProperty(ref _pager, value);
        }
        public ICommand AddCommand { get; set; }

        private void ExecuteAddCommand(object obj) {
            StudentCollection.Add(new Student() {
                Id = ++Index,
                Source = new Random().NextDouble() * 100,
            });
        }
        public ICommand DeleteCommand { get; set; }

        private void ExecuteDeleteCommand(object obj) {
            if (obj is Student student) {
                StudentCollection.Remove(student);
            } else {
                if (StudentCollection.Count > 0) {
                    StudentCollection.RemoveAt(0);
                }
            }
        }
        public ICommand SortCommand { get; set; }

        private void ExecuteSortCommand(object obj) {
            var list = StudentCollection.ToList().OrderByDescending(item => item.Source).ToList();
            StudentCollection.Clear();
            foreach (var item in list) {
                StudentCollection.Add(item);
            }
        }

        public MainWindowViewModel() {
            StudentCollection = new ObservableCollection<Student>();
            for (int i = 0; i < 10; i++) {
                StudentCollection.Add(new Student() {
                    Id = Index = i,
                    Source = 10 * (i + 1),
                });
            }

            AddCommand = new RelayCommand(ExecuteAddCommand);
            DeleteCommand = new RelayCommand(ExecuteDeleteCommand);
            SortCommand = new RelayCommand(ExecuteSortCommand);

            Pager = new Pager<Student>(8, StudentCollection);
            Pager.PagerUpdated += items => {
                StudentCollectionPaging = new ObservableCollection<Student>(items);
            };
            Pager.CurPageIndex = 1;
        }
    }
}
