using ShoeShopManagement.Model;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace ShoeShopManagement.DAL
{
    class CustomerDAL : DataProvider
    {
        private static CustomerDAL instance;

        public static CustomerDAL Instance
        {
            get { if (instance == null) instance = new CustomerDAL(); return CustomerDAL.instance; }
            private set { CustomerDAL.instance = value; }
        }

        private CustomerDAL()
        {

        }
        public List<Customer> ConvertDBToList()
        {
            List<Customer> customers = new List<Customer>();
            try
            {
                OpenConnection();
                string queryString = @"Select * from KHACHHANG Where isDelete = '0'";

                SqlCommand command = new SqlCommand(queryString, conn);
                command.ExecuteNonQuery();
                SqlDataAdapter adapter = new SqlDataAdapter(command);

                DataTable dataTable = new DataTable();
                adapter.Fill(dataTable);
                for (int i = 0; i < dataTable.Rows.Count; i++)
                {
                    Customer employee = new Customer(int.Parse(dataTable.Rows[i].ItemArray[0].ToString()),
                        dataTable.Rows[i].ItemArray[1].ToString(),
                        dataTable.Rows[i].ItemArray[2].ToString(),
                        int.Parse(dataTable.Rows[i].ItemArray[3].ToString()));
                    customers.Add(employee);
                }
                return customers;
            }
            catch
            {
                return new List<Customer>();
            }
            finally
            {
                CloseConnection();
            }
        }
        public int GetBusinessBillByIdCustomer(int idCustomer)
        {
            try
            {
                OpenConnection();
                string query = String.Format("Select count(*) from PHIEUBANHANG Where MaKH = '{0}'", idCustomer);
                SqlCommand command = new SqlCommand(query, conn);
                SqlDataAdapter adapter = new SqlDataAdapter(command);
                DataTable dataTable = new DataTable();
                adapter.Fill(dataTable);
                return int.Parse(dataTable.Rows[0].ItemArray[0].ToString());
            }
            catch
            {
                return 0;
            }
            finally
            {
                CloseConnection();
            }
        }
        public int GetServiceBillByIdCustomer(int idCustomer)
        {
            try
            {
                OpenConnection();
                string query = String.Format("Select count(*) from PHIEUDICHVU Where MaKH = '{0}'", idCustomer);
                SqlCommand command = new SqlCommand(query, conn);
                SqlDataAdapter adapter = new SqlDataAdapter(command);
                DataTable dataTable = new DataTable();
                adapter.Fill(dataTable);
                return int.Parse(dataTable.Rows[0].ItemArray[0].ToString());
            }
            catch
            {
                return 0;
            }
            finally
            {
                CloseConnection();
            }
        }
        public int DeleteCustomer(int idCustomer)
        {
            try
            {
                OpenConnection();
                string query = String.Format("Update KHACHHANG Set isDelete = '1' Where MaKH = '{0}'", idCustomer);
                SqlCommand command = new SqlCommand(query, conn);
                SqlDataAdapter adapter = new SqlDataAdapter(command);
                DataTable dataTable = new DataTable();
                adapter.Fill(dataTable);
                return int.Parse(dataTable.Rows[0].ItemArray[0].ToString());
            }
            catch
            {
                return 0;
            }
            finally
            {
                CloseConnection();
            }
        }
        public int GetTotalMoneyBusinessBillByIdCustomer(int idCustomer)
        {
            try
            {
                OpenConnection();
                string query = String.Format("Select Sum(TongTien) from PHIEUBANHANG Where MaKH = '{0}'", idCustomer);
                SqlCommand command = new SqlCommand(query, conn);
                SqlDataAdapter adapter = new SqlDataAdapter(command);
                DataTable dataTable = new DataTable();
                adapter.Fill(dataTable);
                return int.Parse(dataTable.Rows[0].ItemArray[0].ToString());
            }
            catch
            {
                return 0;
            }
            finally
            {
                CloseConnection();
            }
        }
        public int GetTotalMoneyServiceBillByIdCustomer(int idCustomer)
        {
            try
            {
                OpenConnection();
                string query = String.Format("Select Sum(TongTien) from PHIEUDICHVU Where MaKH = '{0}'", idCustomer);
                SqlCommand command = new SqlCommand(query, conn);
                SqlDataAdapter adapter = new SqlDataAdapter(command);
                DataTable dataTable = new DataTable();
                adapter.Fill(dataTable);
                return int.Parse(dataTable.Rows[0].ItemArray[0].ToString());
            }
            catch
            {
                return 0;
            }
            finally
            {
                CloseConnection();
            }
        }
        public int SaveChangeCustomer(Customer customer)
        {
            try
            {
                OpenConnection();
                string query = String.Format("Update KHACHHANG Set TenKH = '{0}', SDT = '{1}' Where MaKH = '{2}'", customer.Name, customer.PhoneNumber, customer.IdCustomer);
                SqlCommand command = new SqlCommand(query, conn);
                SqlDataAdapter adapter = new SqlDataAdapter(command);
                DataTable dataTable = new DataTable();
                adapter.Fill(dataTable);
                return int.Parse(dataTable.Rows[0].ItemArray[0].ToString());
            }
            catch
            {
                return 0;
            }
            finally
            {
                CloseConnection();
            }
        }
        public int GetMaxId()
        {
            try
            {
                OpenConnection();
                string query = @"Select max(MaKH) from KHACHHANG";
                SqlCommand command = new SqlCommand(query, conn);
                SqlDataAdapter adapter = new SqlDataAdapter(command);
                DataTable dataTable = new DataTable();
                adapter.Fill(dataTable);
                return int.Parse(dataTable.Rows[0].ItemArray[0].ToString());
            }
            catch
            {
                return 0;
            }
            finally
            {
                CloseConnection();
            }
        }
        public bool AddCustomerToDatabase(Customer customer)
        {
            try
            {
                OpenConnection();
                string queryString = String.Format("Insert Into KHACHHANG Values('{0}',N'{1}','{2}','{3}')",customer.IdCustomer,customer.Name,customer.PhoneNumber,customer.IsDelete);
                SqlCommand command = new SqlCommand(queryString, conn);

                int rs = command.ExecuteNonQuery();
                if (rs != 1)
                {
                    return false;
                }
                else
                {
                    return true;
                }
            }
            catch (Exception e)
            {
                MessageBox.Show("Thêm thất bại!" + e, "Thông báo", MessageBoxButton.OK, MessageBoxImage.Error);
                return false;
            }
            finally
            {
                CloseConnection();
            }
        }
    }
}
