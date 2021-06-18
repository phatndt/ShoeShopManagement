using ShoeShopManagement.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
    }
}
