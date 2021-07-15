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
        private int idAccount;
        public int IdAccount { get => idAccount; set => idAccount = value; }
        public StockIn()
        {

        }
        public StockIn(int idStockIn,int idAccount,DateTime dateStockIn,double total)
        {
            this.idStockIn = idStockIn;
            this.idAccount = idAccount;
            this.dateStockIn = dateStockIn;
            this.total = total;
        }
    }
}
