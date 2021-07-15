using ShoeShopManagement.Resources.UserControls;
using ShoeShopManagement.Views;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
namespace ShoeShopManagement.ViewModels
{
    class HomeViewModel : BaseViewModel
    {
        public ICommand GetNameTab { get; set; }
        public ICommand SwitchTabCommand { get; set; }
        public ICommand LogOutCommand { get; set; }

    public string Name;
        public HomeViewModel()
        {
            GetNameTab = new RelayCommand<Button>((parameter) => true, (parameter) => Name = parameter.Uid);

            SwitchTabCommand = new RelayCommand<HomeWindow>((parameter) => true, (parameter) => SwitchTab(parameter));

            LogOutCommand = new RelayCommand<HomeWindow>((parameter) => true, (parameter) => LogOut(parameter));


        }
        public void SwitchTab(HomeWindow parameter)
        {
            int index = int.Parse(this.Name);

            parameter.grdCursor.Margin = new Thickness(0, (170 + 60 * index), 40, 0);

            parameter.grdBody_Home.Visibility = Visibility.Hidden;
            parameter.grdBody_Business.Visibility = Visibility.Hidden;
            parameter.grdBody_Service.Visibility = Visibility.Hidden;
            parameter.grdBody_Goods.Visibility = Visibility.Hidden;
            parameter.grdBody_Customer.Visibility = Visibility.Hidden;
            parameter.grdBody_Report.Visibility = Visibility.Hidden;
            parameter.grdBody_Employee.Visibility = Visibility.Hidden;
            parameter.grdBody_Stock.Visibility = Visibility.Hidden;

            parameter.btnHome.Foreground = (Brush)new BrushConverter().ConvertFrom("#FFFEECEC");
            parameter.btnBusiness.Foreground = (Brush)new BrushConverter().ConvertFrom("#FFFEECEC");
            parameter.btnService.Foreground = (Brush)new BrushConverter().ConvertFrom("#FFFEECEC");
            parameter.btnGoods.Foreground = (Brush)new BrushConverter().ConvertFrom("#FFFEECEC");
            parameter.btnCustomer.Foreground = (Brush)new BrushConverter().ConvertFrom("#FFFEECEC");
            parameter.btnReport.Foreground = (Brush)new BrushConverter().ConvertFrom("#FFFEECEC");
            //parameter.btnEmployee.Foreground = (Brush)new BrushConverter().ConvertFrom("#FFFEECEC");
            parameter.btnStock.Foreground = (Brush)new BrushConverter().ConvertFrom("#FFFEECEC");

            parameter.iconHome.Foreground = (Brush)new BrushConverter().ConvertFrom("#FFFEECEC");
            parameter.iconBusiness.Foreground = (Brush)new BrushConverter().ConvertFrom("#FFFEECEC");
            parameter.iconService.Foreground = (Brush)new BrushConverter().ConvertFrom("#FFFEECEC");
            parameter.iconGood.Foreground = (Brush)new BrushConverter().ConvertFrom("#FFFEECEC");
            parameter.iconCustomer.Foreground = (Brush)new BrushConverter().ConvertFrom("#FFFEECEC");
            parameter.iconReport.Foreground = (Brush)new BrushConverter().ConvertFrom("#FFFEECEC");
            //parameter.iconEmployee.Foreground = (Brush)new BrushConverter().ConvertFrom("#FFFEECEC");
            parameter.iconStock.Foreground = (Brush)new BrushConverter().ConvertFrom("#FFFEECEC");
            switch (index)
            {
                case 0:
                    ReportViewModel reportViewModel = new ReportViewModel(parameter);
                    parameter.grdBody_Home.Visibility = Visibility.Visible;
                    parameter.btnHome.Foreground = (Brush)new BrushConverter().ConvertFrom("#FF45526c");
                    parameter.iconHome.Foreground = (Brush)new BrushConverter().ConvertFrom("#FF45526c");
                    break;
                case 1:
                    BusinessViewModel businessViewModel = new BusinessViewModel();
                    parameter.grdBody_Business.Visibility = Visibility.Visible;
                    parameter.btnBusiness.Foreground = (Brush)new BrushConverter().ConvertFrom("#FF45526c");
                    parameter.iconBusiness.Foreground = (Brush)new BrushConverter().ConvertFrom("#FF45526c");
                    break;
                case 2:
                    ServicesViewModel servicesViewModel = new ServicesViewModel();
                    parameter.grdBody_Service.Visibility = Visibility.Visible;
                    parameter.btnService.Foreground = (Brush)new BrushConverter().ConvertFrom("#FF45526c");
                    parameter.iconService.Foreground = (Brush)new BrushConverter().ConvertFrom("#FF45526c");
                    break;
                case 3:
                    GoodsViewModel goodsViewModel = new GoodsViewModel();
                    parameter.grdBody_Goods.Visibility = Visibility.Visible;
                    parameter.btnGoods.Foreground = (Brush)new BrushConverter().ConvertFrom("#FF45526c");
                    parameter.iconGood.Foreground = (Brush)new BrushConverter().ConvertFrom("#FF45526c");
                    break;
                case 4:
                    CustomerViewModel customerViewModel = new CustomerViewModel();
                    parameter.grdBody_Customer.Visibility = Visibility.Visible;
                    parameter.btnCustomer.Foreground = (Brush)new BrushConverter().ConvertFrom("#FF45526c");
                    parameter.iconCustomer.Foreground = (Brush)new BrushConverter().ConvertFrom("#FF45526c");
                    break;
                case 5:
                    ReportViewModel reportViewModel1 = new ReportViewModel(parameter, 1);
                    parameter.grdBody_Report.Visibility = Visibility.Visible;
                    parameter.btnReport.Foreground = (Brush)new BrushConverter().ConvertFrom("#FF45526c");
                    parameter.iconReport.Foreground = (Brush)new BrushConverter().ConvertFrom("#FF45526c");
                    break;
                case 7:
                    parameter.grdBody_Employee.Visibility = Visibility.Visible;
                    //parameter.btnEmployee.Foreground = (Brush)new BrushConverter().ConvertFrom("#FF45526c");
                    //parameter.iconEmployee.Foreground = (Brush)new BrushConverter().ConvertFrom("#FF45526c");
                    break;
                case 6:
                    StockViewModel stockViewModel = new StockViewModel();
                    parameter.grdBody_Stock.Visibility = Visibility.Visible;
                    parameter.btnStock.Foreground = (Brush)new BrushConverter().ConvertFrom("#FF45526c");
                    parameter.iconStock.Foreground = (Brush)new BrushConverter().ConvertFrom("#FF45526c");
                    break;
                default:
                    break;
            }
        }

        public void LogOut(HomeWindow parameter)
        {
            SignInWindow signInWindow = new SignInWindow();
            signInWindow.Show();
            parameter.Close();
        }
    }
}
