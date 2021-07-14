using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShoeShopManagement.Models
{
    class ServicesBill
    {
        private int idServiceBill;
        private string idCustomer;
        private DateTime dateServicesBill;
        private long Total;
        private long prePay;
        private long Rest;
        private DateTime deliveryDate;
        private int isDelete;

        public ServicesBill() { }

        public ServicesBill(int idServiceBill, string idCustomer, DateTime dateServicesBill, long total, long prePay, long rest, DateTime deliveryDate, int isdelete)
        {
            this.idServiceBill = idServiceBill;
            this.idCustomer = idCustomer;
            this.dateServicesBill = dateServicesBill;
            Total = total;
            this.prePay = prePay;
            Rest = rest;
            this.deliveryDate = deliveryDate;
            this.IsDelete = isdelete;
        }

        public int IdServiceBill { get => idServiceBill; set => idServiceBill = value; }
        public string IdCustomer { get => idCustomer; set => idCustomer = value; }
        public DateTime DateServicesBill { get => dateServicesBill; set => dateServicesBill = value; }
        public long Total1 { get => Total; set => Total = value; }
        public long PrePay { get => prePay; set => prePay = value; }
        public long Rest1 { get => Rest; set => Rest = value; }
        public DateTime DeliveryDate { get => deliveryDate; set => deliveryDate = value; }
        public int IsDelete { get => isDelete; set => isDelete = value; }
    }
}
