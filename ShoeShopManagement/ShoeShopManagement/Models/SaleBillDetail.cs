using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShoeShopManagement.Models
{
    class SaleBillDetail
    {
        private int idSaleBillDetail;
        private int idSaleBill;
        private int idGood;
        private int quantity;
        private long price;

        public int IdSaleBillDetail { get => idSaleBillDetail; set => idSaleBillDetail = value; }
        public int IdSaleBill { get => idSaleBill; set => idSaleBill = value; }
        public int IdGood { get => idGood; set => idGood = value; }
        public int Quantity { get => quantity; set => quantity = value; }
        public long Price { get => price; set => price = value; }

        public SaleBillDetail()
        {

        }

        public SaleBillDetail(int a, int b, int c, int d, long e)
        {
            this.idSaleBillDetail = a;
            this.idSaleBill = b;
            this.idGood = c;
            this.quantity = d;
            this.price = e;
        }
    }
}
