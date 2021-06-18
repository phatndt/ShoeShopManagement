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
    class StockCheckDAL: DataProvider
    {
        private static StockCheckDAL instance;

        public static StockCheckDAL Instance
        {
            get { if (instance == null) instance = new StockCheckDAL(); return StockCheckDAL.instance; }
            private set { StockCheckDAL.instance = value; }
        }

        private StockCheckDAL()
        {

        }
        public int GetMaxId()
        {
            try
            {
                OpenConnection();
                string query = @"Select max(MaPKK) from PHIEUKIEMKHO";
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
        public List<Goods> LoadProduct()
        {
            DataTable dt = new DataTable();
            List<Goods> goods = new List<Goods>();
            try
            {
                OpenConnection();
                string queryString = @"Select * from SANPHAM";

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
                Goods good = new Goods(int.Parse(dt.Rows[i].ItemArray[0].ToString()),
                    dt.Rows[i].ItemArray[1].ToString(),
                    int.Parse(dt.Rows[i].ItemArray[2].ToString()),
                    int.Parse(dt.Rows[i].ItemArray[3].ToString()),
                    long.Parse(dt.Rows[i].ItemArray[4].ToString()),
                    Convert.FromBase64String(dt.Rows[i].ItemArray[5].ToString()));
                goods.Add(good);
            }
            return goods;
        }
        public int GetQuantityGood(int id)
        {
            try
            {
                OpenConnection();
                string query = String.Format("Select SoLuong from CHITIETSP where MaSP = {0}",id);
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
        public int GetStockInGood(int id)
        {
            try
            {
                OpenConnection();
                string query = String.Format("Select sum(SoLuong) from CHITIETPHIEUNHAP where MaSP = {0}", id);
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
        public int GetStockOutGood(int id)
        {
            try
            {
                OpenConnection();
                string query = String.Format("Select sum(SoLuong) from CHITIETPBH where MaSP = {0}", id);
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
        public List<StockCheck> ConvertDBToList()
        {
            DataTable dt = new DataTable();
            List<StockCheck> stockChecks = new List<StockCheck>();
            try
            {
                OpenConnection();
                string queryString = @"Select * from PHIEUKIEMKHO";

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
                StockCheck employee = new StockCheck(
                    int.Parse(dt.Rows[i].ItemArray[0].ToString()),
                    DateTime.Parse(dt.Rows[i].ItemArray[1].ToString())
                    );
                stockChecks.Add(employee);
            }
            return stockChecks;
        }
        public List<StockCheckDetail> ConvertDBToStockDetailList(int idStockCheck)
        {
            DataTable dt = new DataTable();
            List<StockCheckDetail> stockCheckDetails = new List<StockCheckDetail>();
            try
            {
                OpenConnection();
                string queryString = String.Format("Select * from CHITIETPKK Where MaPKK = '{0}'",idStockCheck);

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
                StockCheckDetail stockCheck = new StockCheckDetail(
                    int.Parse(dt.Rows[i].ItemArray[0].ToString()),
                    int.Parse(dt.Rows[i].ItemArray[1].ToString()),
                    int.Parse(dt.Rows[i].ItemArray[2].ToString()),
                    int.Parse(dt.Rows[i].ItemArray[3].ToString()),
                    int.Parse(dt.Rows[i].ItemArray[4].ToString()),
                    int.Parse(dt.Rows[i].ItemArray[5].ToString()),
                    int.Parse(dt.Rows[i].ItemArray[6].ToString())
                    );
                stockCheckDetails.Add(stockCheck);
            }
            return stockCheckDetails;
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
        public int GetIdProduct(string Product)
        {
            try
            {
                OpenConnection();
                string query = String.Format("Select MaSP from SANPHAM Where TenSP = '{0}'", Product);
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
        public bool AddStockCheckToDatabase(StockCheck stockCheck)
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
        public bool RemoveStockCheckFromDatabase(int id)
        {
            try
            {
                OpenConnection();
                string queryString = String.Format("Delete From PHIEUKIEMKHO Where MaPKK = '{0}'", id);
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
    }
}
