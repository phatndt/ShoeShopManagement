using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShoeShopManagement.Model
{
    class Customer
    {
        private int idCustomer;

        private string name;

        private string phoneNumber;

        private int isDelete;

        public int IdCustomer { get => idCustomer; set => idCustomer = value; }
        public string Name { get => name; set => name = value; }
        public string PhoneNumber { get => phoneNumber; set => phoneNumber = value; }
        public int IsDelete { get => isDelete; set => isDelete = value; }

        public Customer()
        {

        }
        public Customer(int idCustomer, string name, string phoneNumber, int isDelete)
        {
            this.idCustomer = idCustomer;
            this.name = name;
            this.phoneNumber = phoneNumber;
            this.isDelete = isDelete;
        }
    }
}
