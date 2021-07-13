using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShoeShopManagement.Models
{
    class StockCheckDetail
    {
        private int idStockCheckDetail;
        private int idStockCheck;
        private int idGood;
        private int firstQuantity;
        private int finalQuantity;
        private int stockInQuantity;
        private int stockOutQuantity;

        public int IdStockCheckDetail { get => idStockCheckDetail; set => idStockCheckDetail = value; }
        public int IdStockCheck { get => idStockCheck; set => idStockCheck = value; }
        public int IdGood { get => idGood; set => idGood = value; }
        public int FirstQuantity { get => firstQuantity; set => firstQuantity = value; }
        public int FinalQuantity { get => finalQuantity; set => finalQuantity = value; }
        public int StockInQuantity { get => stockInQuantity; set => stockInQuantity = value; }
        public int StockOutQuantity { get => stockOutQuantity; set => stockOutQuantity = value; }

        public StockCheckDetail()
        {

        }
        public StockCheckDetail(int a, int b, int c, int d, int e, int f, int g)
        {
            this.idStockCheckDetail = a;
            this.idStockCheck = b;
            this.idGood = c;
            this.firstQuantity = d;
            this.finalQuantity = e;
            this.stockInQuantity = f;
            this.stockOutQuantity = g;
        }
    }
}
