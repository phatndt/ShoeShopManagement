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
    class StockReceiptDAL:DataProvider
    {
        private static StockReceiptDAL instance;

        public static StockReceiptDAL Instance
        {
            get { if (instance == null) instance = new StockReceiptDAL(); return StockReceiptDAL.instance; }
            private set { StockReceiptDAL.instance = value; }
        }
        private StockReceiptDAL()
        {

        }
        public List<StockIn> ConvertDBToList()
        {
            try
            {
                OpenConnection();
                string queryString = "select * from PHIEUNHAPHANG";
                SqlCommand command = new SqlCommand(queryString, conn);
                SqlDataAdapter adapter = new SqlDataAdapter(command);

                DataTable dataTable = new DataTable();
                adapter.Fill(dataTable);
                List<StockIn> stockReceiptList = new List<StockIn>();
                for (int i = 0; i < dataTable.Rows.Count; i++)
                {
                    int idAccount = -1;
                    if (dataTable.Rows[i].ItemArray[1].ToString() != "")
                    {
                        idAccount = int.Parse(dataTable.Rows[i].ItemArray[1].ToString());
                    }
                    StockIn acc = new StockIn(int.Parse(dataTable.Rows[i].ItemArray[0].ToString()),idAccount,
                        DateTime.Parse(dataTable.Rows[i].ItemArray[2].ToString()), int.Parse(dataTable.Rows[i].ItemArray[3].ToString()));
                    stockReceiptList.Add(acc);
                }
                return stockReceiptList;
            }
            catch
            {
                return new List<StockIn>();
            }
            finally
            {
                CloseConnection();
            }
        }
        public bool AddIntoDB(StockIn stockReceipt)
        {
            try
            {
                OpenConnection();
                string queryString = "insert into PHIEUNHAPHANG(MaPNH,NgayLapPhieu,TongTien) " +
                    "values(@MaPNH, @NgayLapPhieu, @TongTien)";
                SqlCommand command = new SqlCommand(queryString, conn);
                command.Parameters.AddWithValue("@MaPNH", stockReceipt.IdStockIn.ToString());
                //command.Parameters.AddWithValue("@MaKH", stockReceipt.IdAccount.ToString());
                command.Parameters.AddWithValue("@NgayLapPhieu", stockReceipt.DateStockIn);
                command.Parameters.AddWithValue("@TongTien", stockReceipt.Total.ToString());

                int rs = command.ExecuteNonQuery();
                if (rs != 1)
                {
                    throw new Exception();
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
        public bool UpdateOnDB(StockIn stockReceipt)
        {
            try
            {
                OpenConnection();
                string queryString = "update PHIEUNHAPHANG set NgayLapPhieu=@NgayLapPhieu,TongTien=@TongTien " +
                    "where MaPNH =" + stockReceipt.IdStockIn.ToString();
                SqlCommand command = new SqlCommand(queryString, conn);
                command.Parameters.AddWithValue("@NgayLapPhieu", stockReceipt.DateStockIn);
                command.Parameters.AddWithValue("@TongTien", stockReceipt.Total.ToString());
                command.ExecuteNonQuery();
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
        public bool DeleteFromDB(string idStockReceipt)
        {
            try
            {
                OpenConnection();
                string queryString = "delete from PHIEUNHAPHANG where MaPNH=" + idStockReceipt;
                SqlCommand command = new SqlCommand(queryString, conn);
                int rs = command.ExecuteNonQuery();
                if (rs < 1)
                {
                    throw new Exception();
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
        public bool UpdateIdAccount(string idAccount)
        {
            try
            {
                OpenConnection();
                string queryString = "update PHIEUNHAPHANG set MaKH = NULL where MaKH = " + idAccount;
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
        public int GetMaxId()
        {
            int res = 0;
            try
            {
                OpenConnection();
                string queryString = "select max(MaPNH) as id from PHIEUNHAPHANG";
                SqlCommand command = new SqlCommand(queryString, conn);

                SqlDataReader rdr = command.ExecuteReader();
                rdr.Read();
                res = int.Parse(rdr["id"].ToString());
                return res;
            }
            catch
            {
                return res;
            }
            finally
            {
                CloseConnection();
            }
        }

        public DataTable GetStockReceiptByDate(string day, string month, string year)
        {
            DataTable dataTable = new DataTable();
            try
            {
                OpenConnection();
                string queryString = string.Format("select * from PHIEUNHAPHANG " +
                    "where year(NgaylapPhieu) = {0} and month(NgayLapPhieu) = {1} and day(NgaylapPhieu) = {2} order by MaPNH", year, month, day);

                SqlCommand command = new SqlCommand(queryString, conn);
                SqlDataAdapter adapter = new SqlDataAdapter(command);
                adapter.Fill(dataTable);
                return dataTable;
            }
            catch
            {
                return dataTable;
            }
            finally
            {
                CloseConnection();
            }
        }
        public DataTable GetStockReceiptByMonth(string month, string year)
        {
            DataTable dataTable = new DataTable();
            try
            {
                OpenConnection();
                string queryString = string.Format("select * from PHIEUNHAPHANG " +
                    "where year(NhayLapPhieu) = {0} and month(NhayLapPhieu) = {1} order by MaPNH", year, month);

                SqlCommand command = new SqlCommand(queryString, conn);
                SqlDataAdapter adapter = new SqlDataAdapter(command);
                adapter.Fill(dataTable);
                return dataTable;
            }
            catch
            {
                return dataTable;
            }
            finally
            {
                CloseConnection();
            }
        }
        public DataTable GetStockReceiptByYear(string year)
        {
            DataTable dataTable = new DataTable();
            try
            {
                OpenConnection();
                string queryString = string.Format("select * from PHIEUNHAPHANG " +
                    "where year(NgayLapPhieu) = {0} order by MaPNH", year);

                SqlCommand command = new SqlCommand(queryString, conn);
                SqlDataAdapter adapter = new SqlDataAdapter(command);
                adapter.Fill(dataTable);
                return dataTable;
            }
            catch
            {
                return dataTable;
            }
            finally
            {
                CloseConnection();
            }
        }
    }
}
