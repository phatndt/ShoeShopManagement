using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShoeShopManagement.Models
{
    class Employee
    {
        private int idEmployee;
        private string name;
        private DateTime date;
        private string sex;
        private DateTime startDate;
        private string position;
        private string telephone;
        private string address;
        private string image;
        private int idAccount;

        public int IdEmployee { get => idEmployee; set => idEmployee = value; }
        public string Name { get => name; set => name = value; }
        public DateTime Date { get => date; set => date = value; }
        public string Sex { get => sex; set => sex = value; }
        public DateTime StartDate { get => startDate; set => startDate = value; }
        public string Image { get => image; set => image = value; }
        public int IdAccount { get => idAccount; set => idAccount = value; }
        public string Position { get => position; set => position = value; }
        public string Telephone { get => telephone; set => telephone = value; }
        public string Address { get => address; set => address = value; }

        public Employee()
        {

        }
        public Employee(int idEmployee,string name,DateTime date,string sex,DateTime startDate,string position, string telephone, string address, string image)
        {
            this.idEmployee = idEmployee;
            this.name = name;
            this.date = date;
            this.sex = sex;
            this.startDate = startDate;
            this.position = position;
            this.telephone = telephone;
            this.address = address;
            this.image = image;
        }
    }
}
