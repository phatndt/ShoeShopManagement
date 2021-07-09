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

        public ICommand LoadCommand { get; set; }
        public ICommand InitPieChartCommand { get; set; }
        public ICommand InitColumnChartCommand { get; set; }
        public ICommand SelectionChangedCommand { get; set; }
        public ReportViewModel()
        {
            LoadCommand = new RelayCommand<HomeWindow>(parameter => true, parameter => LoadDefaultChart(parameter));
            InitPieChartCommand = new RelayCommand<HomeWindow>(parameter => true, parameter => InitPieChart(parameter));
            InitColumnChartCommand = new RelayCommand<HomeWindow>(parameter => true, parameter => InitColumnChart(parameter));
            SelectionChangedCommand = new RelayCommand<HomeWindow>(parameter => true, parameter => SelectionChanged(parameter));
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
    }
}
