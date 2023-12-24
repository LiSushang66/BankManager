using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BankManage.model {
    public class PaginationItem {
        public int Id { get; set; }
        public string accountNo { get; set; }
        public System.DateTime dealDate { get; set; }
        public string dealType { get; set; }
        public double dealMoney { get; set; }
        public double balance { get; set; }
    }
}