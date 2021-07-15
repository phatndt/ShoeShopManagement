using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ShoeShopManagement.Models;
using System.Data;
using System.Data.SqlClient;
using System.Windows;

namespace ShoeShopManagement.DAL
{
    class ServicesBillDAL : DataProvider
    {
        private static ServicesBillDAL instance;

        public static ServicesBillDAL Instance
        {
            get { if (instance == null) instance = new ServicesBillDAL(); return ServicesBillDAL.instance; }
            private set { ServicesBillDAL.instance = value; }
        }

        private ServicesBillDAL()
        {

        }

        public bool AddIntoDB(ServicesBill servicesBill)
        {
            try
            {
                OpenConnection();
                string queryString = String.Format("insert into PHIEUDICHVU(MaPDV, MaKH, NgayLapPhieu, TongTien, TraTruoc, ConLai, NgayGiao, IsDelete) " +
                    "values('{0}','{1}','{2}','{3}','{4}','{5}','{6}','{7}')", servicesBill.IdServiceBill, servicesBill.IdCustomer, servicesBill.DateServicesBill, servicesBill.Total1, servicesBill.PrePay, servicesBill.Rest1, servicesBill.DeliveryDate, servicesBill.IsDelete);
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

        public bool RemoveServicesBillFromDatabase(int id)
        {
            try
            {
                OpenConnection();
                string queryString = String.Format("Delete From PHIEUDICHVU Where MaPDV = '{0}'", id);
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
                MessageBox.Show("Xóa thất bại!" + e, "Thông báo", MessageBoxButton.OK, MessageBoxImage.Error);
                return false;
            }
            finally
            {
                CloseConnection();
            }
        }

        public bool RemoveServicesDetailFromDatabase(int id)
        {
            try
            {
                OpenConnection();
                string queryString = String.Format("Delete From CHITIETPDV Where MaPDV = '{0}'", id);
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
                MessageBox.Show("Xóa thất bại!" + e, "Thông báo", MessageBoxButton.OK, MessageBoxImage.Error);
                return false;
            }
            finally
            {
                CloseConnection();
            }
        }

        public Customer GetCustomer(string sdt)
        {
            DataTable dt = new DataTable();

            try
            {
                OpenConnection();
                string queryString = String.Format("Select * From KHACHHANG Where SDT = {0}", sdt);

                SqlCommand command = new SqlCommand(queryString, conn);
                command.ExecuteNonQuery();
                SqlDataAdapter adapter = new SqlDataAdapter(command);

                adapter.Fill(dt);
                Customer customer = new Customer(
                    int.Parse(dt.Rows[0].ItemArray[0].ToString()),
                    dt.Rows[0].ItemArray[1].ToString(),
                    dt.Rows[0].ItemArray[2].ToString(),
                    int.Parse(dt.Rows[0].ItemArray[3].ToString()));
                return customer;
            }
            catch
            {
                return new Customer();
            }
            finally
            {
                CloseConnection();
            }
        }

        public bool UpdateIntoDB(ServicesBill servicesBill)
        {
            try
            {
                OpenConnection();
                string queryString = "update PHIEUDICHVU set MaKH=@makh, NgayLapPhieu=@ngaylapphieu, TongTien=@tongtien, TraTruoc=@tratruoc, ConLai=@conlai, NgayGiao=@ngaygiao " +
                    "where MaPDV = " + servicesBill.IdServiceBill.ToString();
                SqlCommand command = new SqlCommand(queryString, conn);
                command.Parameters.AddWithValue("@makh", int.Parse(servicesBill.IdCustomer));
                command.Parameters.AddWithValue("@ngaylapphieu", servicesBill.DateServicesBill);
                command.Parameters.AddWithValue("@tongtien", servicesBill.Total1);
                command.Parameters.AddWithValue("@tratruoc", servicesBill.PrePay);
                command.Parameters.AddWithValue("@conlai", servicesBill.Rest1);
                command.Parameters.AddWithValue("@ngaygiao", servicesBill.DeliveryDate);
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
            catch
            {
                return false;
            }
            finally
            {
                CloseConnection();
            }


        }
        public List<ServicesBillDetail> GetServicesBillDetailLById(int idServicesBillDetail)
        {
            List<ServicesBillDetail> servicesBillDetails = new List<ServicesBillDetail>();
            try
            {
                OpenConnection();
                string query = String.Format("Select * From CHITIETPDV Where MaPDV = '{0}'", idServicesBillDetail);
                SqlCommand command = new SqlCommand(query, conn);
                SqlDataAdapter adapter = new SqlDataAdapter(command);

                DataTable dataTable = new DataTable();
                adapter.Fill(dataTable);

                for (int i = 0; i < dataTable.Rows.Count; i++)
                {
                    ServicesBillDetail servicesBillDetail = new ServicesBillDetail(
                        int.Parse(dataTable.Rows[i].ItemArray[0].ToString()),
                        int.Parse(dataTable.Rows[i].ItemArray[1].ToString()),
                        int.Parse(dataTable.Rows[i].ItemArray[2].ToString()),
                        int.Parse(dataTable.Rows[i].ItemArray[3].ToString()),
                        long.Parse(dataTable.Rows[i].ItemArray[4].ToString()),
                        int.Parse(dataTable.Rows[i].ItemArray[5].ToString())
                        );
                    servicesBillDetails.Add(servicesBillDetail);
                }
                return servicesBillDetails;
            }
            catch
            {
                return new List<ServicesBillDetail>();
            }
            finally
            {
                CloseConnection();
            }
        }
        public int GetMaxIdServicesBillDetails()
        {
            try
            {
                OpenConnection();
                string query = @"Select max(MaCTPDV) from CHITIETPDV";
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
        public bool AddServicesBillDetailToDatabase(ServicesBillDetail servicesBillDetail)
        {
            try
            {
                OpenConnection();
                string queryString = "Insert Into CHITIETPDV Values(@MaCTPDV, @MaPDV, @MaDV, @SoLuong, @DonGiaDuocTinh, @TinhTrang)";
                SqlCommand command = new SqlCommand(queryString, conn);
                command.Parameters.AddWithValue("@MaCTPDV", servicesBillDetail.MaCTPDV1);
                command.Parameters.AddWithValue("@MaPDV", servicesBillDetail.MaPDV1);
                command.Parameters.AddWithValue("@MaDV", servicesBillDetail.MaDV1);
                command.Parameters.AddWithValue("@SoLuong", servicesBillDetail.SoLuong1);
                command.Parameters.AddWithValue("@DonGiaDuocTinh", servicesBillDetail.DonGiaDuocTinh1);
                command.Parameters.AddWithValue("@TinhTrang", servicesBillDetail.TinhTrang1);

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
        public string GetNameService(int idServices)
        {
            try
            {
                OpenConnection();
                string query = String.Format("Select TenDV from DICHVU Where MaDV = '{0}'", idServices);
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
        public bool DeleteFromDB(string idServices)
        {
            try
            {
                OpenConnection();
                string queryString = "delete CHITIETPDV where MaCTPDV = " + idServices;
                SqlCommand command = new SqlCommand(queryString, conn);
                int rs = command.ExecuteNonQuery();
                return true;
            }
            catch
            {
                return false;
            }
            finally
            {
                CloseConnection();
            }
        }
        public bool DeleteBillFromDB(string idServices)
        {
            try
            {
                OpenConnection();
                string queryString = "Update PHIEUDICHVU Set IsDelete = '1' Where MaPDV = " + idServices;
                SqlCommand command = new SqlCommand(queryString, conn);
                int rs = command.ExecuteNonQuery();
                return true;
            }
            catch
            {
                return false;
            }
            finally
            {
                CloseConnection();
            }
        }
        public bool DeleteServicesFromDB(string idServices)
        {
            try
            {
                OpenConnection();
                string queryString = "Update DICHVU Set IsDelete = '1' Where MaDV = " + idServices;
                SqlCommand command = new SqlCommand(queryString, conn);
                int rs = command.ExecuteNonQuery();
                return true;
            }
            catch
            {
                return false;
            }
            finally
            {
                CloseConnection();
            }
        }
        public bool AddServiceToDB(Service service)
        {
            try
            {
                OpenConnection();
                string queryString = "Insert Into DICHVU Values(@MaDV, @TenDV, @DonGiaDV, @IsDelete)";
                SqlCommand command = new SqlCommand(queryString, conn);
                command.Parameters.AddWithValue("@MaDV", service.IdService);
                command.Parameters.AddWithValue("@TenDV", service.Name);
                command.Parameters.AddWithValue("@DonGiaDV", service.Price);
                command.Parameters.AddWithValue("@IsDelete", 0);
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
        public bool UpdateServiceToDB(Service service)
        {
            try
            {
                OpenConnection();
                string queryString = "update DICHVU set TenDV=@tendv, DonGiaDV=@dongiadv " +
                        "where MaDV = " + service.IdService.ToString();
                SqlCommand command = new SqlCommand(queryString, conn);
                command.Parameters.AddWithValue("@tendv", service.Name);
                command.Parameters.AddWithValue("@dongiadv", service.Price);

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
