using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShoeShopManagement.Models
{
    class BillDetail
    {
        private int idBill;
        private int idGood;
        private int idService;
        private int quantity;
        private long price;
        private long discount;
        private long total;

        public int IdBill { get => idBill; set => idBill = value; }
        public int IdGood { get => idGood; set => idGood = value; }
        public int IdService { get => idService; set => idService = value; }
        public int Quantity { get => quantity; set => quantity = value; }
        public long Price { get => price; set => price = value; }
        public long Discount { get => discount; set => discount = value; }
        public long Total { get => total; set => total = value; }
        
        public BillDetail()
        {

        }

        public BillDetail(int idBill, int idGood, int idService, int quantity, long price, long discount, long total)
        {
            this.idBill = idBill;
            this.idGood = idGood;
            this.idService = idService;
            this.quantity = quantity;
            this.price = price;
            this.discount = discount;
            this.total = total;
        }
    }
}
