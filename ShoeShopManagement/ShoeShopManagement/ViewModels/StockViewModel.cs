using ShoeShopManagement.DAL;
using ShoeShopManagement.Models;
using ShoeShopManagement.Resources.UserControls;
using ShoeShopManagement.Views;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;

namespace ShoeShopManagement.ViewModels
{
    class StockViewModel: BaseViewModel
    {
        public ICommand LoadStockBillCommand { get; set; }
        public ICommand OpenStockBillWindowCommand { get; set; }
        public ICommand LoadStockBillWindowCommand { get; set; }
        public ICommand AddGoodToStockBillCommand { get; set; }

        public int id;

        public HomeWindow homeWindow;
        public StockViewModel()
        {

            LoadStockBillCommand = new RelayCommand<HomeWindow>(parameter => true, parameter => LoadStockBill(parameter));
            OpenStockBillWindowCommand = new RelayCommand<HomeWindow>(parameter => true, parameter => OpenStockBillWindow(parameter));
            LoadStockBillWindowCommand = new RelayCommand<CheckStockWindow>(parameter => true, parameter => LoadStockBillWindow(parameter));
            AddGoodToStockBillCommand = new RelayCommand<CheckStockWindow>(parameter => true, parameter => AddGoodToStockBill(parameter));
        }

        private void AddGoodToStockBill(CheckStockWindow parameter)
        {
            if (!String.IsNullOrEmpty(parameter.txtGood.Text))
            {
                
            }
        }

        private void LoadStockBillWindow(CheckStockWindow parameter)
        {
         
        }

        private void OpenStockBillWindow(HomeWindow parameter)
        {
            CheckStockWindow checkStockWindow = new CheckStockWindow();
            checkStockWindow.txtDate.Text = DateTime.Now.ToString("dd/MM/yyyy");
            int id = StockCheckDAL.Instance.GetMaxId() + 1;
            checkStockWindow.txtIdStockBill.Text = id.ToString();
            checkStockWindow.txtGood.ItemsSource = getNameGood();
            checkStockWindow.Show();
        }
        public List<string> getNameGood()
        {
            List<string> namesGood = new List<string>();
            foreach(Goods good in StockCheckDAL.Instance.LoadProduct())
            {
                namesGood.Add(good.Name);
            }
            return namesGood;
        }

        private void LoadStockBill(HomeWindow parameter)
        {
            this.homeWindow = parameter;
            parameter.stkStockGood.Children.Clear();
            List<StockCheck> stockChecks = StockCheckDAL.Instance.ConvertDBToList();
            foreach (StockCheck stockCheck in stockChecks)
            {
                StockBillUc stockBillUc = new StockBillUc();
                stockBillUc.txbID.Text = stockCheck.Id.ToString();
                stockBillUc.txbTime.Text = stockCheck.Datestock.ToString("dd/MM/yyyy");
                parameter.stkStockGood.Children.Add(stockBillUc);
            }
        }
    }
}
