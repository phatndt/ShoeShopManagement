using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ShoeShopManagement.Models;

namespace ShoeShopManagement.DAL
{
    class BusinessDAL : DataProvider
    {
        private static BusinessDAL instance;

        public static BusinessDAL Instance
        {
            get { if (instance == null) instance = new BusinessDAL(); return BusinessDAL.instance; }
            private set { BusinessDAL.instance = value; }
        }

        private BusinessDAL()
        {

        }
        public List<SaleBill> ConvertDBToList()
        {
            List<SaleBill> saleBills = new List<SaleBill>();
            try
            {
                OpenConnection();
                string queryString = @"Select * from PHIEUBANHANG Where isDelete = '0'";

                SqlCommand command = new SqlCommand(queryString, conn);
                command.ExecuteNonQuery();
                SqlDataAdapter adapter = new SqlDataAdapter(command);

                DataTable dataTable = new DataTable();
                adapter.Fill(dataTable);
                for (int i = 0; i < dataTable.Rows.Count; i++)
                {
                    SaleBill saleBill = new SaleBill(int.Parse(dataTable.Rows[i].ItemArray[0].ToString()),
                        int.Parse(dataTable.Rows[i].ItemArray[1].ToString()),
                        DateTime.Parse(dataTable.Rows[i].ItemArray[2].ToString()),
                        long.Parse(dataTable.Rows[i].ItemArray[3].ToString()),
                        int.Parse(dataTable.Rows[i].ItemArray[4].ToString()));
                    saleBills.Add(saleBill);
                }
                return saleBills;
            }
            catch
            {
                return new List<SaleBill>();
            }
            finally
            {
                CloseConnection();
            }
        }
        public string GetNameCustomerById(int idCustomer)
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
                return "";
            }
            finally
            {
                CloseConnection();
            }
        }
        public bool DeleteSaleBill(int idSaleBill)
        {
            try
            {
                OpenConnection();
                string query = String.Format("Update PHIEUBANHANG set isDelete = '1' Where MaPBH = '{0}'", idSaleBill);
                SqlCommand command = new SqlCommand(query, conn);
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
        public List<SaleBillDetail> ConvertDBToSaleBillList(int idSaleBill)
        {
            DataTable dt = new DataTable();
            List<SaleBillDetail> saleBillDetails = new List<SaleBillDetail>();
            try
            {
                OpenConnection();
                string queryString = String.Format("Select * from CHITIETPBH Where MaPBH = '{0}'", idSaleBill);

                SqlCommand command = new SqlCommand(queryString, conn);
                command.ExecuteNonQuery();
                SqlDataAdapter adapter = new SqlDataAdapter(command);

                adapter.Fill(dt);

                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    SaleBillDetail stockCheck = new SaleBillDetail(
                        int.Parse(dt.Rows[i].ItemArray[0].ToString()),
                        int.Parse(dt.Rows[i].ItemArray[1].ToString()),
                        int.Parse(dt.Rows[i].ItemArray[2].ToString()),
                        int.Parse(dt.Rows[i].ItemArray[3].ToString()),
                        long.Parse(dt.Rows[i].ItemArray[4].ToString())
                        );
                    saleBillDetails.Add(stockCheck);
                }
                return saleBillDetails;
            }
            catch
            {
                return new List<SaleBillDetail>();
            }
            finally
            {
                CloseConnection();
            }
        }
        public string GetNameProduct(int idProduct)
        {
            try
            {
                OpenConnection();
                string query = String.Format("Select TenSP from SANPHAM Where MaSP = '{0}'", idProduct);
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
        //SaleWindow

        public List<SaleBill> ConvertDBToListGood()
        {
            List<SaleBill> saleBills = new List<SaleBill>();
            try
            {
                OpenConnection();
                string queryString = @"select * from SANPHAM,CHITIETSP where SANPHAM.MaSP=CHITIETSP.MaSP and SANPHAM.MaSPXoa = 0";

                SqlCommand command = new SqlCommand(queryString, conn);
                command.ExecuteNonQuery();
                SqlDataAdapter adapter = new SqlDataAdapter(command);

                DataTable dataTable = new DataTable();
                adapter.Fill(dataTable);
                for (int i = 0; i < dataTable.Rows.Count; i++)
                {
                    Goods saleBill = new Goods(int.Parse(dataTable.Rows[i].ItemArray[0].ToString()),
                        int.Parse(dataTable.Rows[i].ItemArray[1].ToString()),
                        DateTime.Parse(dataTable.Rows[i].ItemArray[2].ToString()),
                        long.Parse(dataTable.Rows[i].ItemArray[3].ToString()),
                        int.Parse(dataTable.Rows[i].ItemArray[4].ToString()));
                    saleBills.Add(saleBill);
                }
                return saleBills;
            }
            catch
            {
                return new List<SaleBill>();
            }
            finally
            {
                CloseConnection();
            }
        }
    }
}
