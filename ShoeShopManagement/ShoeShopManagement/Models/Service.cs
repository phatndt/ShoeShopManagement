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
        private bool status;

        public int IdService { get => idService; set => idService = value; }
        public string Name { get => name; set => name = value; }
        public long Price { get => price; set => price = value; }
        public bool Status { get => status; set => status = value; }

        public Service()
        {

        }
        public Service(int idService, string name, long price, bool status)
        {
            this.idService = idService;
            this.name = name;
            this.price = price;
            this.status = status;
        }
    }
}
