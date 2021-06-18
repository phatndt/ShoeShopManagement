using ShoeShopManagement.DAL;
using ShoeShopManagement.Models;
using ShoeShopManagement.Resources.UserControls;
using ShoeShopManagement.Views;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;

namespace ShoeShopManagement.ViewModels
{
    class StockViewModel: BaseViewModel
    {
        public ICommand LoadStockBillCommand { get; set; }
        public ICommand OpenStockDetailWindow { get; set; }
        public ICommand OpenStockBillWindowCommand { get; set;}
        public ICommand ExitStockBillWindowCommand { get; set; }
        public ICommand ExitStockBillButtonWindowCommand { get; set; }
        public ICommand GetWindowCommand { get; set; }
        public ICommand GetIdGoodCommand { get; set; }
        public ICommand LoadStockBillWindowCommand { get; set; }
        public ICommand AddGoodToStockBillCommand { get; set; }
        public ICommand DeleteCommand { get; set; }
        public ICommand SaveCommand { get; set; }

        public int id;

        public HomeWindow homeWindow;

        public CheckStockWindow CheckStockWindow;
        public StockViewModel()
        {

            LoadStockBillCommand = new RelayCommand<HomeWindow>(parameter => true, parameter => LoadStockBill(parameter));

            OpenStockDetailWindow = new RelayCommand<StockBillUc>(parameter => true, parameter => OpenStockDetail(parameter));

            OpenStockBillWindowCommand = new RelayCommand<HomeWindow>(parameter => true, parameter => OpenStockBillWindow(parameter));
            ExitStockBillWindowCommand = new RelayCommand<CheckStockWindow>(parameter => true, parameter => ExitStockBillWindow(parameter));
            ExitStockBillButtonWindowCommand = new RelayCommand<CheckStockWindow>(parameter => true, parameter => ExitStockBillButtonWindow(parameter));
            GetWindowCommand = new RelayCommand<CheckStockWindow>(parameter => true, parameter => GetWindow(parameter));
            LoadStockBillWindowCommand = new RelayCommand<CheckStockWindow>(parameter => true, parameter => LoadStockBillWindow(parameter));
            GetIdGoodCommand = new RelayCommand<CheckStockWindow>(parameter => true, parameter => GetIdGood(parameter));
            AddGoodToStockBillCommand = new RelayCommand<CheckStockWindow>(parameter => true, parameter => AddGoodToStockBill(parameter));
            DeleteCommand = new RelayCommand<StockBillGoodUc>(parameter => true, parameter => DeleteStockCheckGood(parameter));
            SaveCommand = new RelayCommand<StockBillGoodUc>(parameter => true, parameter => SaveStockCheck(parameter));
        }

        private void LoadStockBill(HomeWindow parameter)
        {
            this.homeWindow = parameter;
            parameter.stkStockGood.Children.Clear();
            List<StockCheck> stockChecks = StockCheckDAL.Instance.ConvertDBToList();
            bool flag = false;
            foreach (StockCheck stockCheck in stockChecks)
            {
                StockBillUc stockBillUc = new StockBillUc();
                flag = !flag;
                if (flag)
                {
                    stockBillUc.grdMain.Background = (Brush)new BrushConverter().ConvertFrom("#FFF1D597");
                }
                stockBillUc.txbID.Text = stockCheck.Id.ToString();
                stockBillUc.txbTime.Text = stockCheck.Datestock.ToString("dd/MM/yyyy");
                parameter.stkStockGood.Children.Add(stockBillUc);
            }
        }

        private void OpenStockDetail(StockBillUc parameter)
        {
            StockDetailWindow stockDetailWindow = new StockDetailWindow();
            int i = int.Parse(parameter.txbID.Text.ToString());
            List<StockCheckDetail> stockChecks = StockCheckDAL.Instance.ConvertDBToStockDetailList(i);
            foreach (StockCheckDetail stockCheck in stockChecks)
            {
                StockGoodUc stockGoodUc = new StockGoodUc();
                stockGoodUc.txbId.Text = stockCheck.IdStockCheckDetail.ToString();
                stockGoodUc.txbName.Text = StockCheckDAL.Instance.GetNameProduct(stockCheck.IdGood);
                stockGoodUc.txbFirstQuantity.Text = stockCheck.FirstQuantity.ToString();
                stockGoodUc.txbStockInQuantity.Text = stockCheck.StockInQuantity.ToString();
                stockGoodUc.txbStockOutQuantity.Text = stockCheck.StockOutQuantity.ToString();
                stockGoodUc.txbFinalQuantity.Text = stockCheck.FinalQuantity.ToString();
                stockDetailWindow.stkGoodStockCheck.Children.Add(stockGoodUc);
            }
            stockDetailWindow.Show();
        }

        private void OpenStockBillWindow(HomeWindow parameter)
        {
            this.homeWindow = parameter;
            CheckStockWindow checkStockWindow = new CheckStockWindow();
            checkStockWindow.txtDate.Text = DateTime.Now.ToString("dd/MM/yyyy");
            int id = StockCheckDAL.Instance.GetMaxId() + 1;
            checkStockWindow.txtIdStockBill.Text = id.ToString();
            checkStockWindow.txtGood.ItemsSource = getNameGood();
            StockCheck stockCheck = new StockCheck(id, DateTime.Now);
            StockCheckDAL.Instance.AddStockCheckToDatabase(stockCheck);
            checkStockWindow.ShowDialog();
        }

        private void ExitStockBillWindow(CheckStockWindow parameter)
        {
            if (!new StackTrace().GetFrames().Any(x => x.GetMethod().Name == "Close"))
            {
                StockCheckDetailDAL.Instance.RemoveStockCheckDetailFromDatabase(int.Parse(parameter.txtIdStockBill.Text));
                StockCheckDAL.Instance.RemoveStockCheckFromDatabase(int.Parse(parameter.txtIdStockBill.Text));
            }
        }

        private void ExitStockBillButtonWindow(CheckStockWindow parameter)
        {
            StockCheckDetailDAL.Instance.RemoveStockCheckDetailFromDatabase(int.Parse(parameter.txtIdStockBill.Text));
            StockCheckDAL.Instance.RemoveStockCheckFromDatabase(int.Parse(parameter.txtIdStockBill.Text));
            parameter.Close();
        }

        private void GetWindow(CheckStockWindow parameter)
        {
            this.CheckStockWindow = parameter;
        }

        private void LoadStockBillWindow(CheckStockWindow parameter)
        {

        }

        private void GetIdGood(CheckStockWindow parameter)
        {
            if (!String.IsNullOrEmpty(parameter.txtGood.Text))
            {
                MessageBox.Show("a");
                parameter.txtId.Text = StockCheckDAL.Instance.GetIdProduct(parameter.txtGood.Text).ToString();
            }
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

        public void LoadStockCheckGood(CheckStockWindow checkStockWindow)
        {
            int i = 0;
            checkStockWindow.stkGoodBill.Children.Clear();
            List<StockCheckDetail> stockCheckDetails = StockCheckDetailDAL.Instance.GetStockCheckDetailLById(int.Parse(checkStockWindow.txtIdStockBill.Text));
            foreach (var stockCheckDetail in stockCheckDetails)
            {
                StockBillGoodUc stockBillGood = new StockBillGoodUc();
                stockBillGood.txbId.Text = i.ToString();
                stockBillGood.txbName.Text = StockCheckDetailDAL.Instance.GetNameProduct(stockCheckDetail.IdGood);
                stockBillGood.txbFirstQuantity.Text = stockCheckDetail.FirstQuantity.ToString();
                stockBillGood.txbStockInQuantity.Text = stockCheckDetail.StockInQuantity.ToString();
                stockBillGood.txbStockOutQuantity.Text = stockCheckDetail.StockOutQuantity.ToString();
                stockBillGood.txbFinalQuantity.Text = stockCheckDetail.FinalQuantity.ToString();

                stockBillGood.txbStockCheckDetail.Text = stockCheckDetail.IdStockCheckDetail.ToString();
                checkStockWindow.stkGoodBill.Children.Add(stockBillGood);
                i++;
            }
        }

        private void AddGoodToStockBill(CheckStockWindow parameter)
        {
            if (!String.IsNullOrEmpty(parameter.txtId.Text))
            {
                bool isExisted = false;
                List<StockCheckDetail> stockCheckDetails = StockCheckDetailDAL.Instance.GetStockCheckDetailLById(int.Parse(parameter.txtIdStockBill.Text));
                foreach (var stockCheckDetail in stockCheckDetails)
                {
                    MessageBox.Show(stockCheckDetails.Count.ToString());
                    if (stockCheckDetail.IdGood.ToString() == parameter.txtId.Text)
                    {
                        isExisted = true;
                        MessageBox.Show("Đã thêm sản phẩm vào phiểu kiểm");
                        return;
                    }
                }
                if (!isExisted)
                {
                    int x = StockCheckDAL.Instance.GetQuantityGood(int.Parse(parameter.txtId.Text));
                    int y = StockCheckDAL.Instance.GetStockInGood(int.Parse(parameter.txtId.Text));
                    int z = StockCheckDAL.Instance.GetStockOutGood(int.Parse(parameter.txtId.Text));
                    int t = x + y - z;
                    StockCheckDetail checkDetail = new StockCheckDetail(
                        StockCheckDetailDAL.Instance.GetMaxId() + 1,
                        int.Parse(parameter.txtIdStockBill.Text),
                        int.Parse(parameter.txtId.Text),
                        x,
                        t,
                        y,
                        z);
                    StockCheckDetailDAL.Instance.AddStockCheckDetailToDatabase(checkDetail);
                    LoadStockCheckGood(CheckStockWindow);
                }
            }
            else
            {
                MessageBox.Show("Vui lòng chọn sản phẩm");
            }
        }

        private void DeleteStockCheckGood(StockBillGoodUc parameter)
        {
            StockCheckDetailDAL.Instance.RemoveStockCheckDetailByIdFromDatabase(int.Parse(parameter.txbStockCheckDetail.Text));
            this.CheckStockWindow.stkGoodBill.Children.Remove(parameter);
        }
    }
}
