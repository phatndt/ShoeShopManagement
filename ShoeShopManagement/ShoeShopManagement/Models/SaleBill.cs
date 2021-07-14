using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShoeShopManagement.Models
{
    class SaleBill
    {
        private int idSaleBill;
        private int idCustomer;
        private DateTime date;
        private long total;
        private int isDelete;

        public int IdSaleBill { get => idSaleBill; set => idSaleBill = value; }
        public int IdCustomer { get => idCustomer; set => idCustomer = value; }
        public DateTime Date { get => date; set => date = value; }
        public long Total { get => total; set => total = value; }
        public int IsDelete { get => isDelete; set => isDelete = value; }

        public SaleBill()
        {

        }
        public SaleBill(int a, int b, DateTime c, long d, int e)
        {
            this.idSaleBill = a;
            this.idCustomer = b;
            this.date = c;
            this.total = d;
            this.isDelete = e;
        }
    }
}
