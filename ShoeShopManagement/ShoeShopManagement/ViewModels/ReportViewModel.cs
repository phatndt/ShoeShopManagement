using LiveCharts;
using LiveCharts.Wpf;
using ShoeShopManagement.DAL;
using ShoeShopManagement.Views;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using System.Windows.Media;

namespace ShoeShopManagement.ViewModels
{
    class ReportViewModel : BaseViewModel
    {

        private string thisMonthMoney = "0 đồng";
        public string ThisMonthMoney { get => thisMonthMoney; set { thisMonthMoney = value; OnPropertyChanged(); } }

        private string increasingPercent = "0%";
        public string IncreasingPercent { get => increasingPercent; set { increasingPercent = value; OnPropertyChanged(); } }


        private string numOfBuy = "100";
        public string NumOfBuy { get => numOfBuy; set { numOfBuy = value; OnPropertyChanged(); } }


        private string today;
        public string Today { get => today; set { today = value; OnPropertyChanged(); } }

        private string thisMonth;
        public string ThisMonth { get => thisMonth; set { thisMonth = value; OnPropertyChanged(); } }

        //Pie chart
        private SeriesCollection pieSeriesCollection;
        public SeriesCollection PieSeriesCollection { get => pieSeriesCollection; set { pieSeriesCollection = value; OnPropertyChanged(); } }

        private Func<ChartPoint, string> labelPoint;
        public Func<ChartPoint, string> LabelPoint { get => labelPoint; set => labelPoint = value; }

        //Column Chart
        private ObservableCollection<string> itemSourceTime = new ObservableCollection<string>();
        public ObservableCollection<string> ItemSourceTime { get => itemSourceTime; set { itemSourceTime = value; OnPropertyChanged(); } }

        private SeriesCollection seriesCollection;
        public SeriesCollection SeriesCollection { get => seriesCollection; set { seriesCollection = value; OnPropertyChanged(); } }

        private Func<double, string> formatter;
        public Func<double, string> Formatter { get => formatter; set { formatter = value; OnPropertyChanged(); } }

        private string axisYTitle;
        public string AxisYTitle { get => axisYTitle; set { axisYTitle = value; OnPropertyChanged(); } }

        private string axisXTitle;
        public string AxisXTitle { get => axisXTitle; set { axisXTitle = value; OnPropertyChanged(); } }

        private string[] labels;
        public string[] Labels { get => labels; set { labels = value; OnPropertyChanged(); } }

        //Report

        private ObservableCollection<string> itemSourceYear = new ObservableCollection<string>();
        public ObservableCollection<string> ItemSourceYear { get => itemSourceYear; set { itemSourceYear = value; OnPropertyChanged(); } }

        private ObservableCollection<string> itemSourceMonth = new ObservableCollection<string>();
        public ObservableCollection<string> ItemSourceMonth { get => itemSourceMonth; set { itemSourceMonth = value; OnPropertyChanged(); } }

        private SeriesCollection seriesReportCollection;
        public SeriesCollection SeriesReportCollection { get => seriesReportCollection; set { seriesReportCollection = value; OnPropertyChanged(); } }

        private Func<double, string> formatterReport;
        public Func<double, string> FormatterReport { get => formatterReport; set { formatterReport = value; OnPropertyChanged(); } }

        private string axisYTitleReport;
        public string AxisYTitleReport { get => axisYTitleReport; set { axisYTitleReport = value; OnPropertyChanged(); } }

        private string axisXTitleReport;
        public string AxisXTitleReport { get => axisXTitleReport; set { axisXTitleReport = value; OnPropertyChanged(); } }

        private string[] labelsReport;
        public string[] LabelsReport { get => labelsReport; set { labelsReport = value; OnPropertyChanged(); } }


        private string totalBusiness;
        public string TotalBusiness { get => totalBusiness; set { totalBusiness = value; OnPropertyChanged(); } }

        private string totalService;
        public string TotalService { get => totalService; set { totalService = value; OnPropertyChanged(); } }

        private string totalStock;
        public string TotalStock { get => totalStock; set { totalStock = value; OnPropertyChanged(); } }


        public ICommand LoadCommand { get; set; }
        public ICommand InitPieChartCommand { get; set; }
        public ICommand InitColumnChartCommand { get; set; }
        public ICommand SelectionChangedCommand { get; set; }
        public ICommand LoadYearCommand { get; set; }
        public ICommand SelectionChangedYearCommand { get; set; }
        public ICommand InitColumnChartReportCommand { get; set; }
        public ICommand AddReportCommand { get; set; }

        public ReportViewModel()
        {
            LoadCommand = new RelayCommand<HomeWindow>(parameter => true, parameter => LoadDefaultChart(parameter));
            InitPieChartCommand = new RelayCommand<HomeWindow>(parameter => true, parameter => InitPieChart(parameter));
            InitColumnChartCommand = new RelayCommand<HomeWindow>(parameter => true, parameter => InitColumnChart(parameter));
            SelectionChangedCommand = new RelayCommand<HomeWindow>(parameter => true, parameter => SelectionChanged(parameter));

            LoadYearCommand = new RelayCommand<HomeWindow>(parameter => true, parameter => LoadYear(parameter));
            SelectionChangedYearCommand = new RelayCommand<HomeWindow>(parameter => true, parameter => SelectionChangedYear(parameter));
            InitColumnChartReportCommand = new RelayCommand<HomeWindow>(parameter => true, parameter => InitColumnChartReport(parameter));

            AddReportCommand = new RelayCommand<HomeWindow>(parameter => true, parameter => AddReport(parameter));
        }
        public ReportViewModel(HomeWindow homeWindow)
        {
            homeWindow.cboSelectTimePie.SelectedIndex = -1;
            homeWindow.cboSelectPeriod.SelectedIndex = -1;
            LoadDefaultChart(homeWindow);
        }
        public ReportViewModel(HomeWindow homeWindow, int i)
        {
            LoadYear(homeWindow);
        }
        private void LoadDefaultChart(HomeWindow parameter)
        {
            string currentDay = DateTime.Now.Day.ToString();
            string currentMonth = DateTime.Now.Month.ToString();
            string lastMonth = (int.Parse(currentMonth) - 1).ToString();
            string currentYear = DateTime.Now.Year.ToString();
            ThisMonth = DateTime.Now.ToString("MM/yyyy");
            ThisMonth = DateTime.Now.ToString("MM/yyyy");
            long num = ReportDAL.Instance.GetNumberOfBusinessInMonth(currentMonth, currentYear) + ReportDAL.Instance.GetNumberOfServiceInMonth(currentMonth, currentYear);
            NumOfBuy = num.ToString() + " lượt";
            long money = ReportDAL.Instance.GetTotalMoneyOfBusinessInMonth(currentMonth, currentYear) + ReportDAL.Instance.GetTotalMoneyOfServiceInMonth(currentMonth, currentYear);
            ThisMonthMoney = string.Format("{0:N0}", money) + " đồng";
            try
            {
                double res = 0;
                if (currentMonth != "1")
                {
                    long moneyLastMonth = ReportDAL.Instance.GetTotalMoneyOfBusinessInMonth(currentMonth, currentYear) + ReportDAL.Instance.GetTotalMoneyOfServiceInMonth(lastMonth, currentYear);
                    res = ((double)(money) / (double)(moneyLastMonth)) * 100;
                }
                else
                {
                    long moneyLastMonthOne = ReportDAL.Instance.GetTotalMoneyOfBusinessInMonth("1", currentYear) + ReportDAL.Instance.GetTotalMoneyOfServiceInMonth("1", currentYear);
                    long moneyLastMonthTwelve = ReportDAL.Instance.GetTotalMoneyOfBusinessInMonth("12", (int.Parse(currentYear) - 1).ToString()) + ReportDAL.Instance.GetTotalMoneyOfServiceInMonth("12", (int.Parse(currentYear) - 1).ToString());
                    res = ((double)(moneyLastMonthOne) / (double)(moneyLastMonthTwelve)) * 100;
                }
                IncreasingPercent = Math.Round(res, 2).ToString() + "%";
            }
            catch
            {
                IncreasingPercent = "100%";
            }
        }
        public void InitPieChart(HomeWindow homeWindow)
        {
            labelPoint = chartPoint => string.Format("{0:N0}", chartPoint.Y);
            if (homeWindow.cboSelectTimePie.SelectedIndex == 0)
            {
                string currentDay = DateTime.Now.Day.ToString();
                string currentMonth = DateTime.Now.Month.ToString();
                string currentYear = DateTime.Now.Year.ToString();
                PieSeriesCollection = new SeriesCollection
                {
                    new PieSeries
                    {
                        Title = "Bán hàng",
                        Values = ReportDAL.Instance.GetTotalMoneyOfBusinessInDay(currentDay, currentMonth, currentYear),
                        Fill = (Brush)new BrushConverter().ConvertFrom("#FF2a9d8f"),
                        DataLabels = true,
                        FontSize = 16,
                        LabelPoint = labelPoint,
                    },
                    new PieSeries
                    {
                        Title="Dịch vụ",
                        Values = ReportDAL.Instance.GetTotalMoneyOfServiceInDay(currentDay, currentMonth, currentYear),
                        Fill = (Brush)new BrushConverter().ConvertFrom("#FFff6666"),
                        DataLabels = true,
                        FontSize = 16,
                        LabelPoint = labelPoint,
                    },
                };
            }
            else
            {
                string currentMonth = DateTime.Now.Month.ToString();
                string currentYear = DateTime.Now.Year.ToString();
                PieSeriesCollection = new SeriesCollection
                {
                    new PieSeries
                    {
                        Title = "Bán hàng",
                        Values = ReportDAL.Instance.GetTotalMoneyOfBusinessInMonthPieChart(currentMonth, currentYear),
                        Fill = (Brush)new BrushConverter().ConvertFrom("#FF2a9d8f"),
                        DataLabels = true,
                        FontSize = 16,
                        LabelPoint = labelPoint,
                    },
                    new PieSeries
                    {
                        Title="Dịch vụ",
                        Values = ReportDAL.Instance.GetTotalMoneyOfServiceInMonthPieChart(currentMonth, currentYear),
                        Fill = (Brush)new BrushConverter().ConvertFrom("#FFff6666"),
                        DataLabels = true,
                        FontSize = 16,
                        LabelPoint = labelPoint,
                    },
                };
            }
        }
        public void InitColumnChart(HomeWindow homeWindow)
        {
            if (homeWindow.cboSelectPeriod.SelectedIndex == 0) //Theo tháng => 31 ngày
            {
                if (homeWindow.cboSelectTime.SelectedIndex != -1)
                {
                    AxisXTitle = "Ngày";
                    string[] tmp = homeWindow.cboSelectTime.SelectedValue.ToString().Split(' ');
                    string selectedMonth = tmp[1];
                    string currentYear = DateTime.Now.Year.ToString();
                    SeriesCollection = new SeriesCollection
                    {
                        new ColumnSeries
                        {
                            Title = "Bán hàng",
                            Fill = (Brush)new BrushConverter().ConvertFrom("#FF2a9d8f"),
                            Values = ReportDAL.Instance.QueryMoneyBusinessByMonth(selectedMonth, currentYear),
                        },
                        new ColumnSeries
                        {
                            Title = "Dịch vụ",
                            Fill = (Brush)new BrushConverter().ConvertFrom("#FFff6666"),
                            Values = ReportDAL.Instance.QueryMoneyServiceByMonth(selectedMonth, currentYear),
                        },
                        new ColumnSeries
                        {
                            Title = "Chi nhập",
                            Fill = (Brush)new BrushConverter().ConvertFrom("#FFccff66"),
                            Values = ReportDAL.Instance.QueryMoneyStockInByMonth(selectedMonth, currentYear),
                        }
                    };
                    Labels = ReportDAL.Instance.QueryDayInMonth(selectedMonth, currentYear);
                    Formatter = value => string.Format("{0:N0}", value);
                }
            }
            else if (homeWindow.cboSelectPeriod.SelectedIndex == 1) //Theo quý => 4 quý
            {
                if (homeWindow.cboSelectTime.SelectedIndex != -1)
                {
                    AxisXTitle = "Quý";
                    string[] tmp = homeWindow.cboSelectTime.SelectedValue.ToString().Split(' ');
                    string selectedYear = tmp[1];
                    SeriesCollection = new SeriesCollection
                    {
                        new ColumnSeries
                        {
                            Title = "Bán hàng",
                            Fill = (Brush)new BrushConverter().ConvertFrom("#FF2a9d8f"),
                            Values = ReportDAL.Instance.QueryMoneyBusinessByQuarter(selectedYear),
                        },
                        new ColumnSeries
                        {
                            Title = "Dịch vụ",
                            Fill = (Brush)new BrushConverter().ConvertFrom("#FFff6666"),
                            Values = ReportDAL.Instance.QueryMoneyServiceByQuarter(selectedYear),
                        },
                        new ColumnSeries
                        {
                            Title = "Nhập kho",
                            Fill = (Brush)new BrushConverter().ConvertFrom("#FFccff66"),
                            Values = ReportDAL.Instance.QueryMoneyStockInByQuarter(selectedYear),
                        }
                    };
                    Labels = ReportDAL.Instance.QueryQuarterInYear(selectedYear);
                    Formatter = value => string.Format("{0:N0}", value);
                }
            }
            else
            {
                if (homeWindow.cboSelectTime.SelectedIndex != -1) //Theo năm => 12 tháng
                {
                    AxisXTitle = "Tháng";
                    string[] tmp = homeWindow.cboSelectTime.SelectedValue.ToString().Split(' ');
                    string selectedYear = tmp[1];
                    SeriesCollection = new SeriesCollection
                    {
                        new ColumnSeries
                        {
                            Title = "Bán hàng",
                            Fill = (Brush)new BrushConverter().ConvertFrom("#FF2a9d8f"),
                            Values = ReportDAL.Instance.QueryMoneyBusinessByYear(selectedYear),
                        },
                        new ColumnSeries
                        {
                            Title = "Dịch vụ",
                            Fill = (Brush)new BrushConverter().ConvertFrom("#FFff6666"),
                            Values = ReportDAL.Instance.QueryMoneyServiceByYear(selectedYear)
                        },
                        new ColumnSeries
                        {
                            Title = "Nhập kho",
                            Fill = (Brush)new BrushConverter().ConvertFrom("#FFccff66"),
                            Values = ReportDAL.Instance.QueryMoneyStockInByYear(selectedYear),
                        }
                    };
                    Labels = ReportDAL.Instance.QueryMonthInYear(selectedYear);
                    Formatter = value => string.Format("{0:N0}", value);
                }
            }
        }
        public void SelectionChanged(HomeWindow homeWindow)
        {
            ItemSourceTime.Clear();
            if (homeWindow.cboSelectPeriod.SelectedIndex == 0) //Theo tháng
            {
                int currentMonth = DateTime.Now.Month;
                for (int i = 0; i < currentMonth; i++)
                {
                    ItemSourceTime.Add("Tháng " + (i + 1).ToString());
                }
            }
            else
            {
                int currentYear = DateTime.Now.Year;
                ItemSourceTime.Add("Năm " + (currentYear - 2).ToString());
                ItemSourceTime.Add("Năm " + (currentYear - 1).ToString());
                ItemSourceTime.Add("Năm " + (currentYear).ToString());
            }
        }

        public void LoadYear(HomeWindow parameter)
        {
            ItemSourceYear.Clear();
            string[] year = ReportDAL.Instance.GetYearInDB();
            for(int i = 0; i < year.Length; i++)
            {
                ItemSourceYear.Add(year[i]);
            }
        }
        private void SelectionChangedYear(HomeWindow parameter)
        {
            if (parameter.cboSelectYear.SelectedItem != null)
            {
                ItemSourceMonth.Clear();
                string[] month = ReportDAL.Instance.GetMonthInDB(parameter.cboSelectYear.SelectedItem.ToString());
                for (int i = 0; i < month.Length; i++)
                {
                    ItemSourceMonth.Add(month[i]);
                }
            }
        }
        private void InitColumnChartReport(HomeWindow parameter)
        {
            if (parameter.cboSelectMonth.SelectedItem != null && parameter.cboSelectYear.SelectedItem != null)
            {
                AxisXTitleReport = "Ngày";
                string selectedMonth = parameter.cboSelectMonth.SelectedItem.ToString();
                string currentYear = parameter.cboSelectYear.SelectedItem.ToString();
                SeriesReportCollection = new SeriesCollection
                {
                new ColumnSeries
                {
                    Title = "Bán hàng",
                    Fill = (Brush)new BrushConverter().ConvertFrom("#FF2a9d8f"),
                    Values = ReportDAL.Instance.QueryMoneyBusinessByMonth(selectedMonth, currentYear),
                },
                new ColumnSeries
                {
                    Title = "Dịch vụ",
                    Fill = (Brush)new BrushConverter().ConvertFrom("#FFff6666"),
                    Values = ReportDAL.Instance.QueryMoneyServiceByMonth(selectedMonth, currentYear),
                },
                new ColumnSeries
                {
                    Title = "Chi nhập",
                    Fill = (Brush)new BrushConverter().ConvertFrom("#FFccff66"),
                    Values = ReportDAL.Instance.QueryMoneyStockInByMonth(selectedMonth, currentYear),
                }
                };
                LabelsReport = ReportDAL.Instance.QueryDayInMonth(selectedMonth, currentYear);
                FormatterReport = value => string.Format("{0:N0}", value);
                int id = ReportDAL.Instance.GetIdReport(selectedMonth, currentYear);
                TotalBusiness = string.Format("{0:N0}", ReportDAL.Instance.GetBusinessInDB(id));
                TotalService = string.Format("{0:N0}", ReportDAL.Instance.GetServicesInDB(id));
                TotalStock = string.Format("{0:N0}", ReportDAL.Instance.GetStockInDB(id));
            }
        }

        public void AddReport(HomeWindow parameter)
        {
            string dt = DateTime.Now.ToString();
            string m = DateTime.Now.Month.ToString();
            string y = DateTime.Now.Year.ToString();
            if (ReportDAL.Instance.CheckMonthIntoDatabase(y, m))
            {
                int idDelete = ReportDAL.Instance.GetIdReport(m, y);
                ReportDAL.Instance.DeleteReportDetail(idDelete);
                ReportDAL.Instance.DeleteReport(m, y);
            }
            int id = ReportDAL.Instance.GetMaxId() + 1;
            ReportDAL.Instance.AddMonthToDatabase(id, dt);
            int idDetailReport = ReportDAL.Instance.GetMaxIdDetailReport() + 1;
            long a = ReportDAL.Instance.GetTotalMoneyOfBusinessInMonth(m, y);
            long b = ReportDAL.Instance.GetTotalMoneyOfServiceInMonth(m, y);
            long c = ReportDAL.Instance.AddStockInMonth(m, y);
            ReportDAL.Instance.AddDetailReport(idDetailReport, id, a, b, c);
            ItemSourceMonth.Clear();
            ItemSourceYear.Clear();
            LoadYear(parameter);

        }
    }
}
