using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShoeShopManagement.Models
{
    class StockIn
    {
        private int idStockIn;
        private DateTime dateStockIn;
        private double total;

        public int IdStockIn { get => idStockIn; set => idStockIn = value; }
        public DateTime DateStockIn { get => dateStockIn; set => dateStockIn = value; }
        public double Total { get => total; set => total = value; }
        public StockIn()
        {

        }
        public StockIn(int idStockIn,DateTime dateStockIn,double total)
        {
            this.idStockIn = idStockIn;
            this.dateStockIn = dateStockIn;
            this.total = total;
        }
    }
}
