using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
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
        public GoodDetail GetProductDetail(int idProductDetail)
        {
            try
            {
                OpenConnection();
                string query = String.Format("Select * from CHITIETSP Where MaCTSP = '{0}'", idProductDetail);
                SqlCommand command = new SqlCommand(query, conn);
                SqlDataAdapter adapter = new SqlDataAdapter(command);
                DataTable dt = new DataTable();
                adapter.Fill(dt);
                GoodDetail stockCheck = new GoodDetail(
                       int.Parse(dt.Rows[0].ItemArray[0].ToString()),
                       int.Parse(dt.Rows[0].ItemArray[1].ToString()),
                       int.Parse(dt.Rows[0].ItemArray[2].ToString()),
                       int.Parse(dt.Rows[0].ItemArray[3].ToString()),
                       int.Parse(dt.Rows[0].ItemArray[4].ToString())
                       );
                return stockCheck;
            }
            catch
            {
                return new GoodDetail();
            }
            finally
            {
                CloseConnection();
            }
        }
        //SaleWindow

        public List<Goods> ConvertDBToListGood()
        {
            List<Goods> goodsList = new List<Goods>();
            try
            {
                OpenConnection();
                string queryString = "select * from SANPHAM,CHITIETSP where SANPHAM.MaSP=CHITIETSP.MaSP and SANPHAM.MaSPXoa = 0";

                SqlCommand command = new SqlCommand(queryString, conn);
                SqlDataAdapter adapter = new SqlDataAdapter(command);

                DataTable dataTable = new DataTable();
                adapter.Fill(dataTable);
                for (int i = 0; i < dataTable.Rows.Count; i++)
                {
                    Goods acc = new Goods(int.Parse(dataTable.Rows[i].ItemArray[0].ToString()), dataTable.Rows[i].ItemArray[1].ToString(),
                    int.Parse(dataTable.Rows[i].ItemArray[2].ToString()), int.Parse(dataTable.Rows[i].ItemArray[11].ToString()), int.Parse(dataTable.Rows[i].ItemArray[9].ToString()), int.Parse(dataTable.Rows[i].ItemArray[10].ToString()),
                    long.Parse(dataTable.Rows[i].ItemArray[4].ToString()), int.Parse(dataTable.Rows[i].ItemArray[3].ToString()), int.Parse(dataTable.Rows[i].ItemArray[6].ToString()),
                    Convert.FromBase64String(dataTable.Rows[i].ItemArray[5].ToString()));
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
        public string GetNameColor(int idColor)
        {
            try
            {
                OpenConnection();
                string query = String.Format("Select TenMau from Mau Where MaMau = '{0}'", idColor);
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
        public string GetNameSize(int idSize)
        {
            try
            {
                OpenConnection();
                string query = String.Format("Select TenSize from Size Where MaSIZE = '{0}'", idSize);
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
        public string GetNameCustomer(string idCustomer)
        {
            try
            {
                OpenConnection();
                string query = String.Format("Select TenKH from KHACHHANG Where SDT = '{0}'", idCustomer);
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
        public string GetMaxIdSaleBill()
        {
            try
            {
                OpenConnection();
                string query = String.Format("Select Max(MaPBH) from PHIEUBANHANG");
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
        public bool AddSaleBillToDatabase(SaleBill saleBill)
        {
            try
            {
                OpenConnection();
                string queryString = "Insert Into PHIEUBANHANG Values(@id, @idCustomer, @date, @total, @isDelete)";
                SqlCommand command = new SqlCommand(queryString, conn);
                command.Parameters.AddWithValue("@id", saleBill.IdSaleBill);
                command.Parameters.AddWithValue("@idCustomer", saleBill.IdCustomer);
                command.Parameters.AddWithValue("@date", saleBill.Date);
                command.Parameters.AddWithValue("@total", saleBill.Total);
                command.Parameters.AddWithValue("@isDelete", saleBill.IsDelete);

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
        public bool RemoveSaleBillFromDatabase(int id)
        {
            try
            {
                OpenConnection();
                string queryString = String.Format("Delete From PHIEUBANHANG Where MaPBH = '{0}'", id);
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
        public bool AddSaleBillDetailToDatabase(SaleBillDetail saleBillDetail)
        {
            try
            {
                OpenConnection();
                string queryString = "Insert Into CHITIETPBH Values(@idSaleBillDetail, @idSaleBill, @idGood, @quantity, @price)";
                SqlCommand command = new SqlCommand(queryString, conn);
                command.Parameters.AddWithValue("@idSaleBillDetail", saleBillDetail.IdSaleBillDetail);
                command.Parameters.AddWithValue("@idSaleBill", saleBillDetail.IdSaleBill);
                command.Parameters.AddWithValue("@idGood", saleBillDetail.IdGood);
                command.Parameters.AddWithValue("@quantity", saleBillDetail.Quantity);
                command.Parameters.AddWithValue("@price", saleBillDetail.Price);

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
        public bool RemoveSaleBillDetailToDatabase(int idSaleBill)
        {
            try
            {
                OpenConnection();
                string queryString = String.Format("Delete From CHITIETPBH Where MaPBH = {0}", idSaleBill);
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

        public string GetMaxIdSaleBillDetail()
        {
            try
            {
                OpenConnection();
                string query = String.Format("Select Max(MaCTPBH) from CHITIETPBH");
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

        public bool CheckExistSaleBillDetailToDatabase(int idSaleBill,int idGood)
        {
            try
            {
                OpenConnection();
                string queryString = String.Format("Select * From CHITIETPBH Where MaPBH = {0} And MaSP = {1}", idSaleBill, idGood);
                SqlCommand command = new SqlCommand(queryString, conn);
                SqlDataAdapter adapter = new SqlDataAdapter(command);
                DataTable dataTable = new DataTable();
                adapter.Fill(dataTable);
                if (dataTable.Rows.Count == 0)
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
        public bool UpdateQuantityExistSaleBillDetailToDatabase(int idSaleBill, int idGood)
        {
            try
            {
                OpenConnection();
                string queryString = String.Format("Update CHITIETPBH Set SoLuong = SoLuong + 1 Where MaPBH = {0} And MaSP = {1}", idSaleBill, idGood);
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
        public List<SaleBillDetail> ConvertDBToSaleBillDetailList(int idSaleBill)
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

        public int GetIdGoodDetail(int idGood, int mau, int size)
        {
            try
            {
                OpenConnection();
                string query = String.Format("Select MaCTSP From CHITIETSP Where MaSP = {0} And MaMau = {1} And MaSize = {2}", idGood, mau, size);
                SqlCommand command = new SqlCommand(query, conn);
                SqlDataAdapter adapter = new SqlDataAdapter(command);
                DataTable dataTable = new DataTable();
                adapter.Fill(dataTable);
                return int.Parse(dataTable.Rows[0].ItemArray[0].ToString());
            }
            catch
            {
                return 1;
            }
            finally
            {
                CloseConnection();
            }
        }
        public bool DeteleSaleBillDetailToDatabase(int idSaleBill)
        {
            try
            {
                OpenConnection();
                string queryString = String.Format("Delete From CHITIETPBH Where MaCTPBH = {0}", idSaleBill);
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
        public bool UpdateSaleBillDetailToDatabase(int idSaleBillDetail, int quantity, int price)
        {
            try
            {
                OpenConnection();
                string queryString = String.Format("Update CHITIETPBH Set SoLuong = {0}, DonGiaBanRa = {1} Where MaCTPBH = {2}", quantity, price,idSaleBillDetail);
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
        //public int UpdateTotalSaleBillToDatabase(int idSaleBill)
        //{
        //    int total = 0;
        //    try
        //    {
        //        OpenConnection();
        //        string queryString = String.Format("Select Sum(DonGiaBanRa) From CHITIETPBH Where MAPBH = {0}", idSaleBill);
        //        SqlCommand command = new SqlCommand(queryString, conn);
        //        SqlDataAdapter adapter = new SqlDataAdapter(command);
        //        DataTable dataTable = new DataTable();
        //        adapter.Fill(dataTable);
        //        if (dataTable.Rows[0].ItemArray[0].ToString() != "")
        //        {
        //            total = int.Parse(dataTable.Rows[0].ItemArray[0].ToString());

        //            string query = String.Format("Update PHIEUBANHANG Set TongTien = '{0}' Where MaPBH = {1}", total, idSaleBill);
        //            SqlCommand command1 = new SqlCommand(query, conn);

        //        }
        //        return total;
        //    }
        //    catch (Exception e)
        //    {
        //        MessageBox.Show("Thêm thất bại!" + e, "Thông báo", MessageBoxButton.OK, MessageBoxImage.Error);
        //        return 0;
        //    }
        //    finally
        //    {
        //        CloseConnection();
        //    }
        //}
        public bool UpdateTotalSaleBillToDatabase(int idSaleBill, int total)
        {
            try
            {
                OpenConnection();
                string queryString = String.Format("Update PHIEUBANHANG Set TongTien = '{0}' Where MaPBH = {1}", total, idSaleBill);
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
        public int GetTotalSaleBillToDatabase(int idSaleBill)
        {
            int total = 0;
            try
            {
                OpenConnection();
                string queryString = String.Format("Select Sum(DonGiaBanRa) From CHITIETPBH Where MAPBH = {0}", idSaleBill);
                SqlCommand command = new SqlCommand(queryString, conn);
                SqlDataAdapter adapter = new SqlDataAdapter(command);
                DataTable dataTable = new DataTable();
                adapter.Fill(dataTable);
                if (dataTable.Rows[0].ItemArray[0].ToString() != "")
                {
                    total = int.Parse(dataTable.Rows[0].ItemArray[0].ToString());
                }
                return total;
            }
            catch (Exception e)
            {
                MessageBox.Show("Thêm thất bại!" + e, "Thông báo", MessageBoxButton.OK, MessageBoxImage.Error);
                return 0;
            }
            finally
            {
                CloseConnection();
            }
        }
        public bool UpdateToDatabase(int idSaleBill, int total)
        {
            try
            {
                OpenConnection();
                string queryString = String.Format("Update PHIEUBANHANG Set TongTien = '{0}' Where MaPBH = {1}", total, idSaleBill);
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
        public int CheckSumSaleBill(int idSaleBil)
        {
            try
            {
                OpenConnection();
                string query = String.Format("Select Count(*) From CHITIETPBH Where MaPBH = {0}", idSaleBil);
                SqlCommand command = new SqlCommand(query, conn);
                SqlDataAdapter adapter = new SqlDataAdapter(command);
                DataTable dataTable = new DataTable();
                adapter.Fill(dataTable);
                return int.Parse(dataTable.Rows[0].ItemArray[0].ToString());
            }
            catch
            {
                return 1;
            }
            finally
            {
                CloseConnection();
            }
        }
        public int GetIdCustomer(string sdt)
        {
            try
            {
                OpenConnection();
                string query = String.Format("Select MaKH From KHACHHANG Where SDT = '{0}'", sdt);
                SqlCommand command = new SqlCommand(query, conn);

                SqlDataAdapter adapter = new SqlDataAdapter(command);
                DataTable dataTable = new DataTable();
                adapter.Fill(dataTable);
                return int.Parse(dataTable.Rows[0].ItemArray[0].ToString());
            }
            catch
            {
                return 1;
            }
            finally
            {
                CloseConnection();
            }
        }
        public bool UpdateCustomerSaleBill(int idCustomer, int idSaleBill)
        {
            try
            {
                OpenConnection();
                string query = String.Format("Update PHIEUBANHANG Set MaKH = {0} Where MAPBH = {1}",idCustomer, idSaleBill);
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
        public bool UpdateQuantitiyGood(int idGoood, int quantity)
        {
            try
            {
                OpenConnection();
                string query = String.Format("Update CHITIETSP Set SoLuong = SoLuong - {0} Where MaCTSP = {1}", quantity, idGoood);
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
    }
}
