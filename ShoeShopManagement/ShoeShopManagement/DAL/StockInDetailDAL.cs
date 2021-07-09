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
    class StockInDetailDAL:DataProvider
    {
        private static StockInDetailDAL instance;
        public static StockInDetailDAL Instance
        {
            get { if (instance == null) instance = new StockInDetailDAL();return StockInDetailDAL.instance; }
            private set { StockInDetailDAL.instance = value; }
        }
        private StockInDetailDAL()
        {

        }
        public bool AddIntoDB(StockInDetail stockInDetail)
        {
            try
            {
                OpenConnection();
                string queryString = "insert into CHITIETPHIEUNHAP(MaCTPN,MaPNH , MaSP,SoLuong,DonGia) " +
                    "values(@MaCTPN, @MaPNH, @MaSP, @SoLuong,@DonGia )";
                SqlCommand command = new SqlCommand(queryString, conn);
                command.Parameters.AddWithValue("@MaCTPN", stockInDetail.mACTPN.ToString());
                command.Parameters.AddWithValue("@MaPNH", stockInDetail.mAPNH.ToString());
                command.Parameters.AddWithValue("@MaSP", stockInDetail.mASP.ToString());
                command.Parameters.AddWithValue("@SoLuong", stockInDetail.sOLuong.ToString());
                command.Parameters.AddWithValue("@DonGia", stockInDetail.donGia.ToString());

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
        public bool DeleteFromDB(string idGoods)
        {
            try
            {
                OpenConnection();
                string queryString = "delete from CHITIETPHIEUNHAP where MaSP=" + idGoods;
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
        public bool DeleteByIdStock(string idGoods, string idStockIn)
        {
            try
            {
                OpenConnection();
                string queryString = string.Format("delete from CHITIETPHIEUNHAP where MaSP = {0} and MAPNH = {1}", idGoods, idStockIn);
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
        public bool DeleteByIdStockReceipt(string idStockReceipt)
        {
            try
            {
                OpenConnection();
                string queryString = "delete from CHITIEUPHIEUNHAP where MAPNH = " + idStockReceipt;
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
        public bool UpdateOnDB(StockInDetail stockInDetail)
        {
            try
            {
                OpenConnection();
                string queryString = "update CHITIETPHIEUNHAP set SoLuong=@SoLuong, DonGia=@DonGia " +
                    "where MaSP=@MaSP and MaPNH=@MaPNH";
                SqlCommand command = new SqlCommand(queryString, conn);
                command.Parameters.AddWithValue("@MaPNH", stockInDetail.mAPNH.ToString());
                command.Parameters.AddWithValue("@MaSP", stockInDetail.mASP.ToString());
                command.Parameters.AddWithValue("@SoLuong", stockInDetail.sOLuong.ToString());
                command.Parameters.AddWithValue("@DonGia", stockInDetail.donGia.ToString());
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
        public List<string> QueryIdStockReceipt(string idGoods)
        {
            List<string> res = new List<string>();
            try
            {
                OpenConnection();
                string queryString = "select MaPNH from CHITIETPHIEUNHAP where MaSP=" + idGoods;
                SqlCommand command = new SqlCommand(queryString, conn);

                SqlDataReader rdr = command.ExecuteReader();
                while (rdr.Read())
                {
                    res.Add(rdr["idStockReceipt"].ToString());
                }
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
        public long CalculateTotalMoney(string MaPNH)
        {
            long res = 0;
            try
            {
                OpenConnection();
                string queryString = string.Format("select sum(DonGia * SoLuong) as total from CHITIETPHIEUNHAP " +
                    "where MAPNH = {0} group by MAPNH", MaPNH);
                SqlCommand command = new SqlCommand(queryString, conn);

                SqlDataReader rdr = command.ExecuteReader();
                rdr.Read();
                res = long.Parse(rdr["total"].ToString());
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
        public List<StockInDetail> GetStockInDetailById(string MaPNH)
        {
            List<StockInDetail> listStockInDetail = new List<StockInDetail>();
            try
            {
                OpenConnection();
                string queryString = "select * from CHITIETPHIEUNHAP where MaPNH = " + MaPNH;
                SqlCommand command = new SqlCommand(queryString, conn);
                SqlDataAdapter adapter = new SqlDataAdapter(command);

                DataTable dataTable = new DataTable();
                adapter.Fill(dataTable);

                for (int i = 0; i < dataTable.Rows.Count; i++)
                {
                    StockInDetail stockInDetail = new StockInDetail(int.Parse(dataTable.Rows[i].ItemArray[0].ToString()),
                        int.Parse(dataTable.Rows[i].ItemArray[1].ToString()), int.Parse(dataTable.Rows[i].ItemArray[2].ToString()),
                        int.Parse(dataTable.Rows[i].ItemArray[3].ToString()), int.Parse(dataTable.Rows[i].ItemArray[4].ToString()));
                    listStockInDetail.Add(stockInDetail);
                }
                return listStockInDetail;
            }
            catch
            {
                return new List<StockInDetail>();
            }
            finally
            {
                CloseConnection();
            }
        }
    }
}
