using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShoeShopManagement.Models
{
    class StockCheck
    {
        private int id;
        private DateTime datestock;

        public int Id { get => id; set => id = value; }
        public DateTime Datestock { get => datestock; set => datestock = value; }

        public StockCheck()
        {

        }

        public StockCheck(int id, DateTime dateTime)
        {
            this.id = id;
            this.datestock = dateTime;
        }
    }
}
