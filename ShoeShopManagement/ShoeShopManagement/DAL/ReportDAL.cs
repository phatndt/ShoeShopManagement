using LiveCharts;
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
    class ReportDAL : DataProvider
    {
        private static ReportDAL instance;

        public static ReportDAL Instance
        {
            get { if (instance == null) instance = new ReportDAL(); return ReportDAL.instance; }
            private set { ReportDAL.instance = value; }
        }
        private ReportDAL()
        {

        }
        public long GetNumberOfBusinessInMonth(string month, string year)
        {
            long res = 0;
            try
            {
                OpenConnection();
                string queryString = string.Format("select count(*) from PHIEUBANHANG " +
                    "where year(NgayLapPhieu) = {0} and month(NgayLapPhieu) = {1}", year, month);
                SqlCommand command = new SqlCommand(queryString, conn);

                SqlDataReader rdr = command.ExecuteReader();
                while (rdr.Read())
                {
                    res = long.Parse(rdr[0].ToString());
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
        public long GetNumberOfServiceInMonth(string month, string year)
        {
            long res = 0;
            try
            {
                OpenConnection();
                string queryString = string.Format("select count(*) from PHIEUDICHVU " +
                    "where year(NgayLapPhieu) = {0} and month(NgayLapPhieu) = {1}", year, month);
                SqlCommand command = new SqlCommand(queryString, conn);
                SqlDataAdapter adapter = new SqlDataAdapter(command);
                DataTable dataTable = new DataTable();
                adapter.Fill(dataTable);
                return int.Parse(dataTable.Rows[0].ItemArray[0].ToString());
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
        public long GetTotalMoneyOfServiceInMonth(string month, string year)
        {
            long res = 0;
            try
            {
                OpenConnection();
                string queryString = string.Format("select sum(TongTien) from PHIEUDICHVU " +
                    "where year(NgayLapPhieu) = {0} and month(NgayLapPhieu) = {1}", year, month);
                SqlCommand command = new SqlCommand(queryString, conn);
                SqlDataAdapter adapter = new SqlDataAdapter(command);
                DataTable dataTable = new DataTable();
                adapter.Fill(dataTable);
                return int.Parse(dataTable.Rows[0].ItemArray[0].ToString());
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
        public long GetTotalMoneyOfBusinessInMonth(string month, string year)
        {
            long res = 0;
            try
            {
                OpenConnection();
                string queryString = string.Format("select sum(TongTien) from PHIEUBANHANG " +
                    "where year(NgayLapPhieu) = {0} and month(NgayLapPhieu) = {1}", year, month);
                SqlCommand command = new SqlCommand(queryString, conn);
                SqlDataAdapter adapter = new SqlDataAdapter(command);
                DataTable dataTable = new DataTable();
                adapter.Fill(dataTable);
                return int.Parse(dataTable.Rows[0].ItemArray[0].ToString());
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

        //Pie chart
        public ChartValues<long> GetTotalMoneyOfServiceInDay(string day, string month, string year)
        {
            ChartValues<long> res = new ChartValues<long>();
            try
            {
                OpenConnection();
                string queryString = string.Format("select sum((CHITIETPDV.SoLuong * CHITIETPDV.DonGiaDuocTinh)) as revenue " +
                    "from PHIEUDICHVU, CHITIETPDV where PHIEUDICHVU.MaPDV = CHITIETPDV.MaPDV " +
                    "AND year(NgayLapPhieu) = {0} and month(NgayLapPhieu) = {1} and day(NgayLapPhieu) = {2}", year, month, day);
                SqlCommand command = new SqlCommand(queryString, conn);
                SqlDataReader rdr = command.ExecuteReader();
                while (rdr.Read())
                {
                    res.Add(long.Parse(rdr["revenue"].ToString()));
                }
                return res;
            }
            catch
            {
                res.Add(0);
                return res;
            }
            finally
            {
                CloseConnection();
            }
        }
        public ChartValues<long> GetTotalMoneyOfBusinessInDay(string day, string month, string year)
        {
            ChartValues<long> res = new ChartValues<long>();
            try
            {
                OpenConnection();
                string queryString = string.Format("select sum((CHITIETPBH.SoLuong * CHITIETPBH.DonGiaBanRa)) as revenue " +
                    "from PHIEUBANHANG, CHITIETPBH where PHIEUBANHANG.MaPBH = CHITIETPBH.MAPBH " +
                    "AND year(NgayLapPhieu) = {0} and month(NgayLapPhieu) = {1} and day(NgayLapPhieu) = {2}", year, month, day);
                SqlCommand command = new SqlCommand(queryString, conn);
                SqlDataReader rdr = command.ExecuteReader();
                while (rdr.Read())
                {
                    res.Add(long.Parse(rdr["revenue"].ToString()));
                }
                return res;
            }
            catch
            {
                res.Add(0);
                return res;
            }
            finally
            {
                CloseConnection();
            }
        }
        public ChartValues<long> GetTotalMoneyOfServiceInMonthPieChart(string month, string year)
        {
            ChartValues<long> res = new ChartValues<long>();
            try
            {
                OpenConnection();
                //string queryString = string.Format("select sum((CHITIETPDV.SoLuong * CHITIETPDV.DonGiaDuocTinh)) as revenue" +
                //    "from PHIEUDICHVU, CHITIETPDV where PHIEUDICHVU.MaPDV = CHITIETPDV.MaPDV " +
                //    "AND year(NgayLapPhieu) = {0} and month(NgayLapPhieu) = {1}", year, month);
                string queryString = string.Format("select sum(TongTien) as revenue from PHIEUDICHVU " +
                    "where year(NgayLapPhieu) = {0} and month(NgayLapPhieu) = {1}", year, month);
                SqlCommand command = new SqlCommand(queryString, conn);
                SqlDataReader rdr = command.ExecuteReader();
                while (rdr.Read())
                {
                    res.Add(long.Parse(rdr["revenue"].ToString()));
                }
                return res;
            }
            catch
            {
                res.Add(0);
                return res;
            }
            finally
            {
                CloseConnection();
            }
        }
        public ChartValues<long> GetTotalMoneyOfBusinessInMonthPieChart(string month, string year)
        {
            ChartValues<long> res = new ChartValues<long>();
            try
            {
                OpenConnection();
                string queryString = string.Format("select sum(TongTien) as revenue from PHIEUBANHANG " +
                    "where year(NgayLapPhieu) = {0} and month(NgayLapPhieu) = {1}", year, month);
                SqlCommand command = new SqlCommand(queryString, conn);
                SqlDataReader rdr = command.ExecuteReader();
                while (rdr.Read())
                {
                    res.Add(long.Parse(rdr["revenue"].ToString()));
                }
                return res;
            }
            catch
            {
                res.Add(0);
                return res;
            }
            finally
            {
                CloseConnection();
            }
        }
        //ColumnChart
        public string[] QueryDayInMonth(string month, string year)
        {
            List<string> res = new List<string>();
            try
            {
                OpenConnection();
                string queryString = string.Format("select day(NgayLapPhieu) as day from PHIEUDICHVU " +
                    "where month(NgayLapPhieu) = {0} and year(NgayLapPhieu) = {1} group by day(NgayLapPhieu) " +
                    "union select day(NgayLapPhieu) as day from PHIEUBANHANG where month(NgayLapPhieu) = {0} " +
                    "and year(NgayLapPhieu) = {1} group by day(NgayLapPhieu) " +
                    "union select day(NgayLapPhieu) as day from PHIEUNHAPHANG where month(NgayLapPhieu) = {0} " +
                    "and year(NgayLapPhieu) = {1} group by day(NgayLapPhieu)", month, year);
                SqlCommand command = new SqlCommand(queryString, conn);

                SqlDataReader rdr = command.ExecuteReader();
                while (rdr.Read())
                {
                    res.Add(rdr["day"].ToString());
                }
                return res.ToArray();
            }
            catch
            {
                return res.ToArray();
            }
            finally
            {
                CloseConnection();
            }
        }
        public string[] QueryQuarterInYear(string year)
        {
            List<string> res = new List<string>();
            try
            {
                OpenConnection();
                string queryString = string.Format("select datepart(quarter, NgayLapPhieu) as quarter from PHIEUBANHANG where year(NgayLapPhieu) = {0} group by datepart(quarter, NgayLapPhieu) " +
                    "union select datepart(quarter, NgayLapPhieu) as quarter from PHIEUBANHANG where year(NgayLapPhieu) = {0} group by datepart(quarter, NgayLapPhieu) " +
                    "union select datepart(quarter,NgayLapPhieu) as quarter from PHIEUNHAPHANG where year(NgayLapPhieu) = {0} group by datepart(quarter,NgayLapPhieu)", year);
                SqlCommand command = new SqlCommand(queryString, conn);

                SqlDataReader rdr = command.ExecuteReader();
                while (rdr.Read())
                {
                    res.Add(rdr["quarter"].ToString());
                }
                return res.ToArray();
            }
            catch
            {
                return res.ToArray();
            }
            finally
            {
                CloseConnection();
            }
        }
        public string[] QueryMonthInYear(string year)
        {
            List<string> res = new List<string>();
            try
            {
                OpenConnection();
                string queryString = string.Format("select month(NgayLapPhieu) as month from PHIEUBANHANG where year(NgayLapPhieu) = {0} group by month(NgayLapPhieu) " +
                    "union select month(NgayLapPhieu) as month from PHIEUDICHVU where year(NgayLapPhieu) = {0} group by month(NgayLapPhieu) " +
                    "union select month(NgayLapPhieu) as month from PHIEUNHAPHANG where year(NgayLapPhieu) = {0} group by month(NgayLapPhieu)", year);
                SqlCommand command = new SqlCommand(queryString, conn);

                SqlDataReader rdr = command.ExecuteReader();
                while (rdr.Read())
                {
                    res.Add(rdr["month"].ToString());
                }
                return res.ToArray();
            }
            catch
            {
                return res.ToArray();
            }
            finally
            {
                CloseConnection();
            }
        }
        public ChartValues<long> QueryMoneyBusinessByMonth(string month, string year)
        {
            ChartValues<long> res = new ChartValues<long>();
            try
            {
                string[] daysOfMonth = ReportDAL.Instance.QueryDayInMonth(month, year);

                OpenConnection();
                string queryString = string.Format("select day(NgayLapPhieu), sum(TongTien) from PHIEUBANHANG where month(NgayLapPhieu) = {0} " +
                    "and year(NgayLapPhieu) = {1} group by day(NgayLapPhieu)", month, year);
                SqlCommand command = new SqlCommand(queryString, conn);

                SqlDataAdapter adapter = new SqlDataAdapter(command);
                DataTable dataTable = new DataTable();
                adapter.Fill(dataTable);

                long[] revenue = new long[daysOfMonth.Length];
                int j = 0;
                int numOfRows = dataTable.Rows.Count;
                for (int i = 0; i < daysOfMonth.Length && j < numOfRows; i++)
                {
                    if (daysOfMonth[i] == dataTable.Rows[j].ItemArray[0].ToString())
                    {
                        revenue[i] = long.Parse(dataTable.Rows[j].ItemArray[1].ToString());
                        j++;
                    }
                }
                res = new ChartValues<long>(revenue);
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
        public ChartValues<long> QueryMoneyServiceByMonth(string month, string year)
        {
            ChartValues<long> res = new ChartValues<long>();
            try
            {
                string[] daysOfMonth = ReportDAL.Instance.QueryDayInMonth(month, year);

                OpenConnection();
                string queryString = string.Format("select day(NgayLapPhieu), sum(TongTien) from PHIEUDICHVU where month(NgayLapPhieu) = {0} " +
                    "and year(NgayLapPhieu) = {1} group by day(NgayLapPhieu)", month, year);
                SqlCommand command = new SqlCommand(queryString, conn);

                SqlDataAdapter adapter = new SqlDataAdapter(command);
                DataTable dataTable = new DataTable();
                adapter.Fill(dataTable);

                long[] revenue = new long[daysOfMonth.Length];
                int j = 0;
                int numOfRows = dataTable.Rows.Count;
                for (int i = 0; i < daysOfMonth.Length && j < numOfRows; i++)
                {
                    if (daysOfMonth[i] == dataTable.Rows[j].ItemArray[0].ToString())
                    {
                        revenue[i] = long.Parse(dataTable.Rows[j].ItemArray[1].ToString());
                        j++;
                    }
                }
                res = new ChartValues<long>(revenue);
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
        public ChartValues<long> QueryMoneyStockInByMonth(string month, string year)
        {
            ChartValues<long> res = new ChartValues<long>();
            try
            {
                string[] daysOfMonth = ReportDAL.Instance.QueryDayInMonth(month, year);

                OpenConnection();
                string queryString = string.Format("select day(NgayLapPhieu), sum(TongTien) from PHIEUNHAPHANG where month(NgayLapPhieu) = {0} " +
                    "and year(NgayLapPhieu) = {1} group by day(NgayLapPhieu)", month, year);
                SqlCommand command = new SqlCommand(queryString, conn);

                SqlDataAdapter adapter = new SqlDataAdapter(command);
                DataTable dataTable = new DataTable();
                adapter.Fill(dataTable);

                long[] revenue = new long[daysOfMonth.Length];
                int j = 0;
                int numOfRows = dataTable.Rows.Count;
                for (int i = 0; i < daysOfMonth.Length && j < numOfRows; i++)
                {
                    if (daysOfMonth[i] == dataTable.Rows[j].ItemArray[0].ToString())
                    {
                        revenue[i] = long.Parse(dataTable.Rows[j].ItemArray[1].ToString());
                        j++;
                    }
                }
                res = new ChartValues<long>(revenue);
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
        public ChartValues<long> QueryMoneyBusinessByQuarter(string year)
        {
            ChartValues<long> res = new ChartValues<long>();
            try
            {
                string[] quartersOfYear = ReportDAL.Instance.QueryQuarterInYear(year);

                OpenConnection();
                string queryString = string.Format("select datepart(quarter, NgayLapPhieu), sum(TongTien) from PHIEUBANHANG " +
                    "where year(NgayLapPhieu) = {0} group by datepart(quarter, NgayLapPhieu)", year);
                SqlCommand command = new SqlCommand(queryString, conn);
                SqlDataAdapter adapter = new SqlDataAdapter(command);
                DataTable dataTable = new DataTable();
                adapter.Fill(dataTable);

                long[] revenue = new long[quartersOfYear.Length];
                int j = 0;
                int numOfRows = dataTable.Rows.Count;

                for (int i = 0; i < quartersOfYear.Length && j < numOfRows; i++)
                {
                    if (quartersOfYear[i] == dataTable.Rows[j].ItemArray[0].ToString())
                    {
                        revenue[i] = long.Parse(dataTable.Rows[j].ItemArray[1].ToString());
                        j++;
                    }
                }
                res = new ChartValues<long>(revenue);
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
        public ChartValues<long> QueryMoneyServiceByQuarter(string year)
        {
            ChartValues<long> res = new ChartValues<long>();
            try
            {
                string[] quartersOfYear = ReportDAL.Instance.QueryQuarterInYear(year);

                OpenConnection();
                string queryString = string.Format("select datepart(quarter, NgayLapPhieu), sum(TongTien) from PHIEUDICHVU " +
                    "where year(NgayLapPhieu) = {0} group by datepart(quarter, NgayLapPhieu)", year);
                SqlCommand command = new SqlCommand(queryString, conn);
                SqlDataAdapter adapter = new SqlDataAdapter(command);
                DataTable dataTable = new DataTable();
                adapter.Fill(dataTable);

                long[] revenue = new long[quartersOfYear.Length];
                int j = 0;
                int numOfRows = dataTable.Rows.Count;

                for (int i = 0; i < quartersOfYear.Length && j < numOfRows; i++)
                {
                    if (quartersOfYear[i] == dataTable.Rows[j].ItemArray[0].ToString())
                    {
                        revenue[i] = long.Parse(dataTable.Rows[j].ItemArray[1].ToString());
                        j++;
                    }
                }
                res = new ChartValues<long>(revenue);
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
        public ChartValues<long> QueryMoneyStockInByQuarter(string year)
        {
            ChartValues<long> res = new ChartValues<long>();
            try
            {
                string[] quartersOfYear = ReportDAL.Instance.QueryQuarterInYear(year);

                OpenConnection();
                string queryString = string.Format("select datepart(quarter, NgayLapPhieu), sum(TongTien) from PHIEUNHAPHANG " +
                    "where year(NgayLapPhieu) = {0} group by datepart(quarter, NgayLapPhieu)", year);
                SqlCommand command = new SqlCommand(queryString, conn);
                SqlDataAdapter adapter = new SqlDataAdapter(command);
                DataTable dataTable = new DataTable();
                adapter.Fill(dataTable);

                long[] revenue = new long[quartersOfYear.Length];
                int j = 0;
                int numOfRows = dataTable.Rows.Count;

                for (int i = 0; i < quartersOfYear.Length && j < numOfRows; i++)
                {
                    if (quartersOfYear[i] == dataTable.Rows[j].ItemArray[0].ToString())
                    {
                        revenue[i] = long.Parse(dataTable.Rows[j].ItemArray[1].ToString());
                        j++;
                    }
                }
                res = new ChartValues<long>(revenue);
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
        public ChartValues<long> QueryMoneyBusinessByYear(string year)
        {
            ChartValues<long> res = new ChartValues<long>();
            try
            {
                string[] monthsOfYear = ReportDAL.Instance.QueryMonthInYear(year);

                OpenConnection();
                string queryString = string.Format("select month(NgayLapPhieu), sum(TongTien) from PHIEUBANHANG where year(NgayLapPhieu) = {0} " +
                    "group by month(NgayLapPhieu)", year);
                SqlCommand command = new SqlCommand(queryString, conn);

                SqlDataAdapter adapter = new SqlDataAdapter(command);
                DataTable dataTable = new DataTable();
                adapter.Fill(dataTable);

                long[] revenue = new long[monthsOfYear.Length];
                int j = 0;
                int numOfRows = dataTable.Rows.Count;

                for (int i = 0; i < monthsOfYear.Length && j < numOfRows; i++)
                {
                    if (monthsOfYear[i] == dataTable.Rows[j].ItemArray[0].ToString())
                    {
                        revenue[i] = long.Parse(dataTable.Rows[j].ItemArray[1].ToString());
                        j++;
                    }
                }
                res = new ChartValues<long>(revenue);
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
        public ChartValues<long> QueryMoneyServiceByYear(string year)
        {
            ChartValues<long> res = new ChartValues<long>();
            try
            {
                string[] monthsOfYear = ReportDAL.Instance.QueryMonthInYear(year);

                OpenConnection();
                string queryString = string.Format("select month(NgayLapPhieu), sum(TongTien) from PHIEUDICHVU where year(NgayLapPhieu) = {0} " +
                    "group by month(NgayLapPhieu)", year);
                SqlCommand command = new SqlCommand(queryString, conn);

                SqlDataAdapter adapter = new SqlDataAdapter(command);
                DataTable dataTable = new DataTable();
                adapter.Fill(dataTable);

                long[] revenue = new long[monthsOfYear.Length];
                int j = 0;
                int numOfRows = dataTable.Rows.Count;

                for (int i = 0; i < monthsOfYear.Length && j < numOfRows; i++)
                {
                    if (monthsOfYear[i] == dataTable.Rows[j].ItemArray[0].ToString())
                    {
                        revenue[i] = long.Parse(dataTable.Rows[j].ItemArray[1].ToString());
                        j++;
                    }
                }
                res = new ChartValues<long>(revenue);
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
        public ChartValues<long> QueryMoneyStockInByYear(string year)
        {
            ChartValues<long> res = new ChartValues<long>();
            try
            {
                string[] monthsOfYear = ReportDAL.Instance.QueryMonthInYear(year);

                OpenConnection();
                string queryString = string.Format("select month(NgayLapPhieu), sum(TongTien) from PHIEUNHAPHANG where year(NgayLapPhieu) = {0} " +
                    "group by month(NgayLapPhieu)", year);
                SqlCommand command = new SqlCommand(queryString, conn);

                SqlDataAdapter adapter = new SqlDataAdapter(command);
                DataTable dataTable = new DataTable();
                adapter.Fill(dataTable);

                long[] revenue = new long[monthsOfYear.Length];
                int j = 0;
                int numOfRows = dataTable.Rows.Count;

                for (int i = 0; i < monthsOfYear.Length && j < numOfRows; i++)
                {
                    if (monthsOfYear[i] == dataTable.Rows[j].ItemArray[0].ToString())
                    {
                        revenue[i] = long.Parse(dataTable.Rows[j].ItemArray[1].ToString());
                        j++;
                    }
                }
                res = new ChartValues<long>(revenue);
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
    }
}
