using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShoeShopManagement.Models
{
    class Salary
    {
        private int idSalary;
        private int idEmployee;
        private int salaryMonth;
        private DateTime salaryRecordDate;
        private int totalSalary;

        public int IdSalary { get => idSalary; set => idSalary = value; }
        public int IdEmployee { get => idEmployee; set => idEmployee = value; }
        public int SalaryMonth { get => salaryMonth; set => salaryMonth = value; }
        public DateTime SalaryRecordDate { get => salaryRecordDate; set => salaryRecordDate = value; }
        public int TotalSalary { get => totalSalary; set => totalSalary = value; }
        public Salary()
        {

        }
        public Salary(int idSalary,int idEmployee,int salaryMonth,DateTime salaryRecordDate,int totalSalary)
        {
            this.idSalary = idSalary;
            this.idEmployee = idEmployee;
            this.salaryMonth = salaryMonth;
            this.salaryRecordDate = salaryRecordDate;
            this.totalSalary = totalSalary;
        }
    }
}
