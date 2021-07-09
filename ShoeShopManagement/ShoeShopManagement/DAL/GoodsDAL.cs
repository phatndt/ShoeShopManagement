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
    class GoodsDAL:DataProvider
    {
        private static GoodsDAL instance;
        public static GoodsDAL Instance {
            get { if (instance == null) instance = new GoodsDAL(); return GoodsDAL.instance; }
            private set { GoodsDAL.instance = value; }
        }
        private GoodsDAL()
        {

        }
        public List<Goods> ConvertDBToList()
        {
            List<Goods> goodsList = new List<Goods>();
            try
            {
                OpenConnection();
                string queryString = "select * from SANPHAM where MaSPXoa =0";

                SqlCommand command = new SqlCommand(queryString, conn);
                SqlDataAdapter adapter = new SqlDataAdapter(command);

                DataTable dataTable = new DataTable();
                adapter.Fill(dataTable);
                for (int i = 0; i < dataTable.Rows.Count; i++)
                {
                    Goods acc = new Goods(int.Parse(dataTable.Rows[i].ItemArray[0].ToString()), dataTable.Rows[i].ItemArray[1].ToString(),
                    dataTable.Rows[i].ItemArray[2].ToString(), int.Parse(dataTable.Rows[i].ItemArray[3].ToString()), long.Parse(dataTable.Rows[i].ItemArray[4].ToString()),
                    dataTable.Rows[i].ItemArray[5].ToString(), dataTable.Rows[i].ItemArray[6].ToString(), int.Parse(dataTable.Rows[i].ItemArray[7].ToString()),
                    Convert.FromBase64String(dataTable.Rows[i].ItemArray[7].ToString()));
                    goodsList.Add(acc);
                }
                return goodsList;
            }
            catch
            {
                return goodsList;
            }
            finally
            {
                CloseConnection();
            }
        }
        public DataTable LoadDatatable()
        {
            try
            {
                OpenConnection();
                string queryString = "select * from SANPHAM where MaSPXoa = 0 ";

                SqlCommand command = new SqlCommand(queryString, conn);
                SqlDataAdapter adapter = new SqlDataAdapter(command);

                DataTable dt = new DataTable();
                adapter.Fill(dt);
                return dt;
            }
            catch
            {
                return new DataTable();
            }
            finally
            {
                CloseConnection();
            }
        }
        public bool AddIntoDB(Goods goods)
        {
            try
            {
                OpenConnection();
                string queryString = "insert into SANPHAM(MaSP, TenSP,DonGia,Anh,SoLuong,MaMau,MaSIZE,MaDVT) " +
                    "values(@MaSp, @TenSP, @DonGia, @Soluong, @Anh, @MaMau, @MaSIZE,@MaDVT)";
                SqlCommand command = new SqlCommand(queryString, conn);
                command.Parameters.AddWithValue("@MaSP", goods.IdGood.ToString());
                command.Parameters.AddWithValue("@TenSP", goods.Name);
                command.Parameters.AddWithValue("@DonGia", goods.Price.ToString());
                command.Parameters.AddWithValue("@MaDVT", goods.DVT.ToString());
                command.Parameters.AddWithValue("@SoLuong", goods.Quantity.ToString());
                command.Parameters.AddWithValue("@Anh", Convert.ToBase64String(goods.Image));
                command.Parameters.AddWithValue("@MaMau", goods.Color);
                command.Parameters.AddWithValue("@MaSIZE", goods.Size.ToString());

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
        public bool UpdateOnDB(Goods goods)
        {
            try
            {
                OpenConnection();
                string queryString = "update Goods set TenSP=@TenSP,SoLuong=@SoLuong, DonGia=@DonGia, Anh=@Anh " +
                    "where MaSP =" + goods.IdGood.ToString();
                SqlCommand command = new SqlCommand(queryString, conn);
                command.Parameters.AddWithValue("@TenSP", goods.Name);
                command.Parameters.AddWithValue("@Soluong", goods.Quantity);
                command.Parameters.AddWithValue("@DonGia", goods.Price.ToString());
                command.Parameters.AddWithValue("@Anh", Convert.ToBase64String(goods.Image));

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
        public bool UpdateQuantity(int idGood, int quantity)
        {
            try
            {
                OpenConnection();
                string queryString = "update SANPHAM set SoLuong=@SoLuong where MaSP = " + idGood.ToString();
                SqlCommand command = new SqlCommand(queryString, conn);
                command.Parameters.AddWithValue("@quantity", quantity.ToString());
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
        public bool ImportToDB(Goods goods)
        {
            try
            {
                OpenConnection();
                string queryString = "update SANPHAM set SoLuong = SoLuong +@SoLuong where MaSP=" + goods.IdGood.ToString();
                SqlCommand command = new SqlCommand(queryString, conn);
                command.Parameters.AddWithValue("@quantity", goods.Quantity.ToString());
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
        public bool DeleteFromDB(string idGoods)
        {
            try
            {
                OpenConnection();
                string queryString = "update SANPHAM set MaSPXoa = 1 where MaSP = " + idGoods;
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
        public Goods GetGoods(string idGood) // lấy thông tin hàng hóa khi biết id 
        {
            try
            {
                OpenConnection();
                string queryString = "select * from SANPHAM where MASP = " + idGood;

                SqlCommand command = new SqlCommand(queryString, conn);
                SqlDataAdapter adapter = new SqlDataAdapter(command);

                DataTable dataTable = new DataTable();
                adapter.Fill(dataTable);

                Goods res = new Goods(int.Parse(idGood), dataTable.Rows[0].ItemArray[1].ToString(),
                    dataTable.Rows[0].ItemArray[2].ToString(), int.Parse(dataTable.Rows[0].ItemArray[3].ToString()),long.Parse(dataTable.Rows[0].ItemArray[4].ToString()),
                    dataTable.Rows[0].ItemArray[5].ToString(),dataTable.Rows[0].ItemArray[6].ToString(),int.Parse( dataTable.Rows[0].ItemArray[7].ToString()), 
                    Convert.FromBase64String(dataTable.Rows[0].ItemArray[7].ToString()));

                return res;
            }
            catch
            {
                return new Goods();
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
                string queryString = "select max(MaSP) from SANPHAM";
                SqlCommand command = new SqlCommand(queryString, conn);

                SqlDataReader rdr = command.ExecuteReader();
                rdr.Read();
                res = int.Parse(rdr["MaSP"].ToString());
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
        public bool IsExistGoodsName(string goodsName)
        {
            try
            {
                OpenConnection();
                string query = @"select * from SANPHAM where MaSPXoa = 0 and TenSP = '" + goodsName + "'";
                SqlCommand cmd = new SqlCommand(query, conn);
                SqlDataAdapter adapter = new SqlDataAdapter(cmd);
                DataTable dt = new DataTable();
                adapter.Fill(dt);
                if (dt.Rows.Count == 0)
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
                return true;
            }
            finally
            {
                CloseConnection();
            }
        }

    }
}
