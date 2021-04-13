using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShoeShopManagement.Models
{
    class Account
    {
        private int idAccount;
        private int idEmployee;
        private string userName;
        private string passWord;
        private int typeAcount;

        public int IdAccount { get => idAccount; set => idAccount = value; }
        public int IdEmployee { get => idEmployee; set => idEmployee = value; }
        public string UserName { get => userName; set => userName = value; }
        public string PassWord { get => passWord; set => passWord = value; }
        public int TypeAcount { get => typeAcount; set => typeAcount = value; }
        public Account()
        {

        }
        public Account(int idAccount,int idEmployee,string userName,string passWord,int typeAccount)
        {
            this.idAccount = idAccount;
            this.idEmployee = idEmployee;
            this.userName = userName;
            this.passWord = passWord;
            this.typeAcount = typeAccount;
        }
    }
}
