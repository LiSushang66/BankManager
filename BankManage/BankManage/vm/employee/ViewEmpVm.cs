using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows;
using System.Windows.Input;
using System.Windows.Controls;
using BankManage.dao;
using BankManage.dao.impl;
using BankManage.view.employee;
using BankManage.utils;

namespace BankManage.vm.employee {
    internal class ViewEmpVm:NotifyProperty {
        private Page _curPage;
        private EmployeeInfo _curEmp;

        private EmpMapper _empMapper = new EmpMapperImpl();

        public EmployeeInfo curEmp {
            get => _curEmp;
            set => SetProperty(ref _curEmp, value);
        }

        public BitmapImage image {
            get => FileUtils.ByteToImage(curEmp.photo);
            set {
                curEmp.photo = FileUtils.ImageToByte(value);
                OnPropertyChanged();
            }
        }


        public string salary {
            get => curEmp.Salary.ToString();
            set {
                curEmp.Salary = decimal.TryParse(value, out _) ? decimal.Parse(value) : curEmp.Salary;
                OnPropertyChanged();
            }
        }

        public ICommand Page_Loaded { get; set; }
        private void ExecutePage_Loaded(object obj) {
            curEmp = _empMapper.GetEmp(curEmp.EmployeeNo).First();
        }


        //取消
        public ICommand CloseButton_Click { get; set; }
        private void ExecuteCloseButton_Click(object obj) {
            _curPage.NavigationService.GoBack();
        }

        public ViewEmpVm(Page curPage,EmployeeInfo employee) {
            _curPage = curPage;
            _curEmp = employee;

            Page_Loaded = new RelayCommand(ExecutePage_Loaded);
            CloseButton_Click=new RelayCommand(ExecuteCloseButton_Click);
        }
    }
}
