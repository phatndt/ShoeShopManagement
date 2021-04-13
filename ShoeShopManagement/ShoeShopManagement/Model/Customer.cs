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

        private string address;

        private string phoneNumber;

        public int IdCustomer { get => idCustomer; set => idCustomer = value; }
        public string Name { get => name; set => name = value; }
        public string Address { get => address; set => address = value; }
        public string PhoneNumber { get => phoneNumber; set => phoneNumber = value; }

        public Customer()
        {

        }
        public Customer(int idCustomer, string name, string address, string phoneNumber)
        {
            this.idCustomer = idCustomer;
            this.name = name;
            this.address = address;
            this.phoneNumber = phoneNumber;
        }
    }
}
