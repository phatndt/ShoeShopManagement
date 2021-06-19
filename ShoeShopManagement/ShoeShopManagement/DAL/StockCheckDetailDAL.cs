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
    class StockCheckDetailDAL : DataProvider
    {
        private static StockCheckDetailDAL instance;

        public static StockCheckDetailDAL Instance
        {
            get { if (instance == null) instance = new StockCheckDetailDAL(); return StockCheckDetailDAL.instance; }
            private set { StockCheckDetailDAL.instance = value; }
        }
        private StockCheckDetailDAL()
        {

        }
        public List<StockCheckDetail> GetStockCheckDetailLById(int idStockCheckDetail)
        {
            List<StockCheckDetail> stockCheckDetails = new List<StockCheckDetail>();
            try
            {
                OpenConnection();
                string query = String.Format("Select * From CHITIETPKK Where MaPKK = '{0}'", idStockCheckDetail);
                SqlCommand command = new SqlCommand(query, conn);
                SqlDataAdapter adapter = new SqlDataAdapter(command);

                DataTable dataTable = new DataTable();
                adapter.Fill(dataTable);

                for (int i = 0; i < dataTable.Rows.Count; i++)
                {
                    StockCheckDetail stockCheckDetail = new StockCheckDetail(
                        int.Parse(dataTable.Rows[i].ItemArray[0].ToString()),
                        int.Parse(dataTable.Rows[i].ItemArray[1].ToString()),
                        int.Parse(dataTable.Rows[i].ItemArray[2].ToString()),
                        int.Parse(dataTable.Rows[i].ItemArray[3].ToString()),
                        int.Parse(dataTable.Rows[i].ItemArray[4].ToString()),
                        int.Parse(dataTable.Rows[i].ItemArray[5].ToString()),
                        int.Parse(dataTable.Rows[i].ItemArray[6].ToString())
                        );
                    stockCheckDetails.Add(stockCheckDetail);
                }
                return stockCheckDetails;
            }
            catch
            {
                return new List<StockCheckDetail>();
            }
            finally
            {
                CloseConnection();
            }
        }
        public bool AddStockCheckDetailToDatabase(StockCheckDetail stockCheckDetail)
        {
            try
            {
                OpenConnection();
                string queryString = "Insert Into CHITIETPKK Values(@id, @idStockCheck, @idGood, @firstQuantity, @stockInQuantity, @stockOutQuantity, @finalQuantity)";
                SqlCommand command = new SqlCommand(queryString, conn);
                command.Parameters.AddWithValue("@id", stockCheckDetail.IdStockCheckDetail);
                command.Parameters.AddWithValue("@idStockCheck", stockCheckDetail.IdStockCheck);
                command.Parameters.AddWithValue("@idGood", stockCheckDetail.IdGood);
                command.Parameters.AddWithValue("@firstQuantity", stockCheckDetail.FirstQuantity);
                command.Parameters.AddWithValue("@stockInQuantity", stockCheckDetail.StockInQuantity);
                command.Parameters.AddWithValue("@stockOutQuantity", stockCheckDetail.StockOutQuantity);
                command.Parameters.AddWithValue("@finalQuantity", stockCheckDetail.FirstQuantity);

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
        public bool RemoveStockCheckDetailFromDatabase(int id)
        {
            try
            {
                OpenConnection();
                string queryString = String.Format("Delete From CHITIETPKK Where MaPKK = '{0}'", id);
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
        public bool RemoveStockCheckDetailByIdFromDatabase(int idStockCheckDetail)
        {
            try
            {
                OpenConnection();
                string queryString = String.Format("Delete From CHITIETPKK Where MaCTPKK = '{0}'", idStockCheckDetail);
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
        public int GetMaxId()
        {
            try
            {
                OpenConnection();
                string query = @"Select max(MaCTPKK) from CHITIETPKK";
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
        public string GetNameProduct(int idProduct)
        {
            try
            {
                OpenConnection();
                string query = String.Format("Select TenSP from SANPHAM Where MaSP = '{0}'",idProduct);
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
    }
}
