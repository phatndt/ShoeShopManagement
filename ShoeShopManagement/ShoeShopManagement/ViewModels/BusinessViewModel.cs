using Microsoft.Expression.Interactivity.Core;
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
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;

namespace ShoeShopManagement.ViewModels
{
    class BusinessViewModel : BaseViewModel
    {
        public ICommand TestCommand { get; set; }
        public ICommand LoadCommand { get; set; }
        public ICommand LoadBillCommand { get; set; }
        public ICommand DeleteBillCommand { get; set; }
        public ICommand ShowDetailBillCommand { get; set; }
        public ICommand OpenSaleWindowCommand { get; set; }

        //SaleWindow
        public ICommand LoadGoodsCommand { get; set; }

        public HomeWindow homeWindow;

        public SaleWindow saleWindow;
        public BusinessViewModel()
        {
            LoadBillCommand = new RelayCommand<HomeWindow>((parameter) => true, (parameter) => LoadBill(parameter));
            DeleteBillCommand = new RelayCommand<ReceiptUc>((parameter) => true, (parameter) => DeleteBill(parameter));
            ShowDetailBillCommand = new RelayCommand<ReceiptUc>((parameter) => true, (parameter) => ShowDetailBill(parameter));
            OpenSaleWindowCommand = new RelayCommand<HomeWindow>((parameter) => true, (parameter) => OpenSaleWindow(parameter));
            LoadGoodsCommand = new RelayCommand<SaleWindow>((parameter) => true, (parameter) => LoadGoodSaleWindow(parameter));
        }
        public void LoadBill(HomeWindow parameter)
        {
            this.homeWindow = parameter;
            parameter.stkdataBusiness.Children.Clear();
            List<SaleBill> saleBills = BusinessDAL.Instance.ConvertDBToList();
            bool flag = false;
            int id = 1;
            foreach (SaleBill sale in saleBills)
            {
                ReceiptUc receiptUc = new ReceiptUc();
                flag = !flag;
                if (flag)
                {
                    receiptUc.grdMain.Background = (Brush)new BrushConverter().ConvertFrom("#FFF1D597");
                }
                receiptUc.txbId.Text = id.ToString();
                receiptUc.txbIdSaleBill.Text = sale.IdSaleBill.ToString();
                receiptUc.txbNameCustomer.Text = BusinessDAL.Instance.GetNameCustomerById(sale.IdCustomer);
                receiptUc.txbDate.Text = sale.Date.ToString("dd/MM/yyyy");
                receiptUc.textTotalMoney.Text = string.Format("{0:N0}", sale.Total); 
                parameter.stkdataBusiness.Children.Add(receiptUc);
                id++;
            }
        }
        public void DeleteBill(ReceiptUc parameter)
        {
            if (BusinessDAL.Instance.DeleteSaleBill(int.Parse(parameter.txbIdSaleBill.Text)))
                MessageBox.Show("TT");
            else
                MessageBox.Show("TB");
            LoadBill(homeWindow);
        }
        public void ShowDetailBill(ReceiptUc parameter)
        {
            ReceiptDetailWindow receiptDetailWindow = new ReceiptDetailWindow();
            int i = int.Parse(parameter.txbIdSaleBill.Text.ToString());
            List<SaleBillDetail> saleBillDetails = BusinessDAL.Instance.ConvertDBToSaleBillList(i);
            bool flag = false;
            int id = 1;
            foreach (SaleBillDetail saleBillDetail in saleBillDetails)
            {
                ReceiptDetailUc receiptDetailUc = new ReceiptDetailUc();
                flag = !flag;
                if (flag)
                {
                    receiptDetailUc.grdMain.Background = (Brush)new BrushConverter().ConvertFrom("#FFF1D597");
                }
                receiptDetailUc.txbId.Text = id.ToString();
                receiptDetailUc.txbNameGood.Text = BusinessDAL.Instance.GetNameProduct(saleBillDetail.IdGood);
                receiptDetailUc.txbQuantity.Text = saleBillDetail.Quantity.ToString();
                receiptDetailUc.textPrice.Text = string.Format("{0:N0}", saleBillDetail.Price); 

                receiptDetailWindow.stkReceiptDetail.Children.Add(receiptDetailUc);
                id++;
            }
            receiptDetailWindow.Show();
        }
        
        public void OpenSaleWindow(HomeWindow parameter)
        {
            SaleWindow saleWindow = new SaleWindow();
            this.saleWindow = saleWindow;

            saleWindow.ShowDialog();
        }
        public void LoadGoodSaleWindow(SaleWindow parameter)
        {
            parameter.stkGoodBill.Children.Clear();

        }
    }
}
