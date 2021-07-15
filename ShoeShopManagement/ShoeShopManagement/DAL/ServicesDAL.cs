using ShoeShopManagement.Models;
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
    class ServicesDAL : DataProvider
    {
        private static ServicesDAL instance;

        public static ServicesDAL Instance
        {
            get { if (instance == null) instance = new ServicesDAL(); return ServicesDAL.instance; }
            private set { ServicesDAL.instance = value; }
        }

        private ServicesDAL()
        {

        }
        public List<Service> LoadServices()
        {
            DataTable dt = new DataTable();
            List<Service> services = new List<Service>();
            try
            {
                OpenConnection();
                string queryString = @"Select * from DICHVU Where isDelete = '0'";

                SqlCommand command = new SqlCommand(queryString, conn);
                command.ExecuteNonQuery();
                SqlDataAdapter adapter = new SqlDataAdapter(command);

                adapter.Fill(dt);
            }
            catch
            {

            }
            finally
            {
                CloseConnection();
            }
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                Service service = new Service(int.Parse(dt.Rows[i].ItemArray[0].ToString()),
                    dt.Rows[i].ItemArray[1].ToString(),
                    long.Parse(dt.Rows[i].ItemArray[2].ToString()),
                    int.Parse(dt.Rows[i].ItemArray[3].ToString()));
                services.Add(service);
            }
            return services;
        }

        public List<ServicesBill> LoadServicesBill()
        {
            DataTable dt = new DataTable();
            List<ServicesBill> servicesBills = new List<ServicesBill>();
            try
            {
                OpenConnection();
                string queryString = @"Select * from PHIEUDICHVU Where isDelete = '0'";

                SqlCommand command = new SqlCommand(queryString, conn);
                command.ExecuteNonQuery();
                SqlDataAdapter adapter = new SqlDataAdapter(command);

                adapter.Fill(dt);
            }
            catch
            {

            }
            finally
            {
                CloseConnection();
            }
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                ServicesBill servicesBill = new ServicesBill(int.Parse(dt.Rows[i].ItemArray[0].ToString()),
                    GetNameCustomer( int.Parse(dt.Rows[i].ItemArray[1].ToString())),
                     DateTime.Parse(dt.Rows[i].ItemArray[2].ToString()),
                    long.Parse(dt.Rows[i].ItemArray[3].ToString()),
                    long.Parse(dt.Rows[i].ItemArray[4].ToString()),
                    long.Parse(dt.Rows[i].ItemArray[5].ToString()),
                    DateTime.Parse(dt.Rows[i].ItemArray[6].ToString()),
                    int.Parse(dt.Rows[i].ItemArray[0].ToString()));
                servicesBills.Add(servicesBill);
            }
            return servicesBills;
        }

        public string GetNameCustomer(int idCustomer)
        {
            try
            {
                OpenConnection();
                string query = String.Format("Select TenKH from KHACHHANG Where MaKH = '{0}'", idCustomer);
                SqlCommand command = new SqlCommand(query, conn);
                SqlDataAdapter adapter = new SqlDataAdapter(command);
                DataTable dataTable = new DataTable();
                adapter.Fill(dataTable);
                return dataTable.Rows[0].ItemArray[0].ToString();
            }
            catch
            {
                return "null";
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
                string query = @"Select max(MaPDV) from PHIEUDICHVU";
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
        public int GetMaxIdService()
        {
            try
            {
                OpenConnection();
                string query = @"Select max(MaDV) from DICHVU";
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
        public bool AddBillToDatabase(StockCheck stockCheck)
        {
            try
            {
                OpenConnection();
                string queryString = "Insert Into PHIEUKIEMKHO Values(@id, @date)";
                SqlCommand command = new SqlCommand(queryString, conn);
                command.Parameters.AddWithValue("@id", stockCheck.Id);
                command.Parameters.AddWithValue("@date", stockCheck.Datestock);

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
        public int GetIdServices(string service)
        {
            try
            {
                OpenConnection();
                string query = String.Format("Select MaDV from DICHVU Where TenDV = N'{0}'", service);
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
        public long GetPriceServices(string service)
        {
            try
            {
                OpenConnection();
                string query = String.Format("Select DonGiaDV from DICHVU Where TenDV = '{0}'", service);
                SqlCommand command = new SqlCommand(query, conn);
                SqlDataAdapter adapter = new SqlDataAdapter(command);
                DataTable dataTable = new DataTable();
                adapter.Fill(dataTable);
                return long.Parse(dataTable.Rows[0].ItemArray[0].ToString());
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
        public int GetDonGia(string service)
        {
            try
            {
                OpenConnection();
                string query = String.Format("Select DonGiaDV from DICHVU Where TenDV = N'{0}'", service);
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
    }
}
