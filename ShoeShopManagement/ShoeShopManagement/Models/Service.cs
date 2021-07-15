using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShoeShopManagement.Models
{
    class Service
    {
        private int idService;
        private string name;
        private long price;
        private int isDelete;

        public int IdService { get => idService; set => idService = value; }
        public string Name { get => name; set => name = value; }
        public long Price { get => price; set => price = value; }
        public int IsDelete { get => isDelete; set => isDelete = value; }
        public Service()
        {

        }
        public Service(int idService, string name, long price, int isdelete)
        {
            this.idService = idService;
            this.name = name;
            this.price = price;
            this.isDelete = isdelete;
        }
    }
}
