using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShoeShopManagement.Models
{
    class Bill
    {
        private int idBill;
        private int idEmployee;
        private int idCustomer;
        private DateTime date;
        private long total;

        public int IdBill { get => idBill; set => idBill = value; }
        public int IdEmployee { get => idEmployee; set => idEmployee = value; }
        public int IdCustomer { get => idCustomer; set => idCustomer = value; }
        public DateTime Date { get => date; set => date = value; }
        public long Total { get => total; set => total = value; }

        public Bill()
        {

        }
        public Bill(int idBill, int idEmployee, int idCustomer, DateTime date, long total)
        {
            this.idBill = idBill;
            this.idEmployee = idEmployee;
            this.idCustomer = idCustomer;
            this.date = date;
            this.total = total;
        }
    }
}
