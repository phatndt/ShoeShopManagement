
using ShoeShopManagement.Resources.UserControls;
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
using System.Diagnostics;

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
        public ICommand GetNameCustomerCommmand { get; set; }
        public ICommand ExitSaleWindowCommand { get; set; }
        public ICommand LoadBillUcCommand { get; set; }
        public ICommand SaveSaleBillCommand { get; set; }

        private string numberCustomer = "";
        public string NumberCustomer { get => numberCustomer; set { numberCustomer = value; OnPropertyChanged(); } }

        private string nameCustomer = "";
        public string NameCustomer { get => nameCustomer; set { nameCustomer = value; OnPropertyChanged(); } }

        private string total = "";
        public string Total { get => total; set { total = value; OnPropertyChanged(); } }

        private int idSaleBill = 0;

        // Product UC
        public ICommand AddGoodToSaleBillCommand { get; set; }

        //Bill UC
        public ICommand DeleteBillDetailCommand { get; set; }
        public ICommand DetailBillCommand { get; set; }
        public ICommand LoadPriceCommand { get; set; }
        public HomeWindow homeWindow;

        public SaleWindow saleWindow; 
        public BusinessViewModel()
        {
            LoadBillCommand = new RelayCommand<HomeWindow>((parameter) => true, (parameter) => LoadBill(parameter));
            DeleteBillCommand = new RelayCommand<ReceiptUc>((parameter) => true, (parameter) => DeleteBill(parameter));
            ShowDetailBillCommand = new RelayCommand<ReceiptUc>((parameter) => true, (parameter) => ShowDetailBill(parameter));
            OpenSaleWindowCommand = new RelayCommand<HomeWindow>((parameter) => true, (parameter) => OpenSaleWindow(parameter));

            LoadGoodsCommand = new RelayCommand<SaleWindow>((parameter) => true, (parameter) => LoadGoodSaleWindow(parameter));
            GetNameCustomerCommmand = new RelayCommand<SaleWindow>((parameter) => true, (parameter) => GetNameCustomer(parameter));
            ExitSaleWindowCommand = new RelayCommand<SaleWindow>((parameter) => true, (parameter) => ExitSaleWindow(parameter));
            SaveSaleBillCommand = new RelayCommand<SaleWindow>((parameter) => true, (parameter) => SaveSaleBill(parameter));

            AddGoodToSaleBillCommand = new RelayCommand<ProductUC>((parameter) => true, (parameter) => AddGoodToSaleBill(parameter));
            LoadBillUcCommand = new RelayCommand<SaleWindow>((parameter) => true, (parameter) => LoadBillUc(parameter));

            DeleteBillDetailCommand = new RelayCommand<BillUC>((parameter) => true, (parameter) => DeleteBillDetail(parameter));
            DetailBillCommand = new RelayCommand<BillUC>((parameter) => true, (parameter) => DetailBill(parameter));
            LoadPriceCommand = new RelayCommand<BillUC>((parameter) => true, (parameter) => LoadPrice(parameter));

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
            BusinessDAL.Instance.DeleteSaleBill(int.Parse(parameter.txbIdSaleBill.Text));
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
                GoodDetail goodDetail = BusinessDAL.Instance.GetProductDetail(saleBillDetail.IdGood);
                receiptDetailUc.txbNameGood.Text = BusinessDAL.Instance.GetNameProduct(goodDetail.IdGood);
                receiptDetailUc.txbColor.Text = BusinessDAL.Instance.GetNameColor(goodDetail.IdColor);
                receiptDetailUc.txbSize.Text = BusinessDAL.Instance.GetNameSize(goodDetail.IdSize);
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
            saleWindow.txtDate.Text = DateTime.Now.ToString("dd/MM/yyyy");
            int id = int.Parse(BusinessDAL.Instance.GetMaxIdSaleBill()) + 1;
            saleWindow.txtIdSaleBill.Text = id.ToString();
            SaleBill saleBill = new SaleBill(id, 1, DateTime.Now, 0, 0);
            BusinessDAL.Instance.AddSaleBillToDatabase(saleBill);
            idSaleBill = id;
            saleWindow.ShowDialog();
        }
        public void LoadGoodSaleWindow(SaleWindow parameter)
        {
            parameter.wrpGoods.Children.Clear();
            List<Goods> goods = BusinessDAL.Instance.ConvertDBToListGood();
            foreach (var good in goods)
            {
                ProductUC product = new ProductUC();
                product.imgGood.Source = Converter.Instance.ConvertByteToBitmapImage(good.Image);
                product.idGood.Text = good.IdGood.ToString();
                product.idColor.Text = good.IdColor.ToString();
                product.idSize.Text = good.IdSize.ToString();
                product.idName.Text = good.Name;
                product.Color.Text = "Màu " + BusinessDAL.Instance.GetNameColor(good.IdColor);
                product.Size.Text = "Size " + BusinessDAL.Instance.GetNameSize(good.IdSize);
                product.idPrice.Text = string.Format("{0:N0}", good.Price);
                product.Price.Text = good.Price.ToString();
                parameter.wrpGoods.Children.Add(product);
            }
        }

        public void GetNameCustomer(SaleWindow parameter)
        {
            if (parameter.txtNumberCustomer.Text != "")
            {
                NameCustomer = BusinessDAL.Instance.GetNameCustomer(parameter.txtNumberCustomer.Text);
            }
        }

        public void ExitSaleWindow(SaleWindow parameter)
        {
            if (!new StackTrace().GetFrames().Any(x => x.GetMethod().Name == "Close"))
            {
                BusinessDAL.Instance.RemoveSaleBillDetailToDatabase(int.Parse(parameter.txtIdSaleBill.Text));
                BusinessDAL.Instance.RemoveSaleBillFromDatabase(int.Parse(parameter.txtIdSaleBill.Text));
            }
        }

        public void AddGoodToSaleBill(ProductUC parameter)
        {
            if (BusinessDAL.Instance.CheckExistSaleBillDetailToDatabase(idSaleBill, int.Parse(parameter.idGood.Text)))
            {
                CustomMessageBox.Show("Đã thêm sản phẩm");
            } else
            {
                int id = int.Parse(BusinessDAL.Instance.GetMaxIdSaleBillDetail()) + 1;
                int idGoodDetail = BusinessDAL.Instance.GetIdGoodDetail(int.Parse(parameter.idGood.Text), int.Parse(parameter.idColor.Text), int.Parse(parameter.idSize.Text));
                SaleBillDetail saleBillDetail = new SaleBillDetail(id, idSaleBill, idGoodDetail, 1, long.Parse(parameter.Price.Text));
                BusinessDAL.Instance.AddSaleBillDetailToDatabase(saleBillDetail);
                LoadBillUc(this.saleWindow);
                this.Total = string.Format("{0:N0}", BusinessDAL.Instance.UpdateTotalSaleBillToDatabase(this.idSaleBill)); 
            }
        }
        public void LoadBillUc(SaleWindow parameter)
        {
            parameter.stkGoodBill.Children.Clear();
            int i = int.Parse(parameter.txtIdSaleBill.Text.ToString());
            List<SaleBillDetail> saleBillDetails = BusinessDAL.Instance.ConvertDBToSaleBillList(i);
            bool flag = false;
            int id = 1;
            foreach (SaleBillDetail saleBillDetail in saleBillDetails)
            {
                BillUC billUC = new BillUC();
                flag = !flag;
                if (flag)
                {
                    billUC.grdMain.Background = (Brush)new BrushConverter().ConvertFrom("#FFF1D597");
                }
                billUC.idBillDetail.Text = saleBillDetail.IdSaleBillDetail.ToString();
                billUC.idGooDetail.Text = saleBillDetail.IdGood.ToString();
                billUC.name.Text = BusinessDAL.Instance.GetNameProduct(saleBillDetail.IdGood);
                billUC.quantity.Text = saleBillDetail.Quantity.ToString();
                billUC.price.Text = saleBillDetail.Price.ToString();
                billUC.pricedefaul.Text = saleBillDetail.Price.ToString();

                parameter.stkGoodBill.Children.Add(billUC);
                id++;
            }
        }
        public void DeleteBillDetail(BillUC billUC)
        {
            BusinessDAL.Instance.DeteleSaleBillDetailToDatabase(int.Parse(billUC.idBillDetail.Text));
            LoadBillUc(this.saleWindow);
            this.Total = string.Format("{0:N0}", BusinessDAL.Instance.UpdateTotalSaleBillToDatabase(this.idSaleBill));
        }
        public void DetailBill(BillUC billUC)
        {
            GoodDetail goodDetail = BusinessDAL.Instance.GetProductDetail(int.Parse(billUC.idGooDetail.Text));
            this.saleWindow.txtDetailColor.Text = BusinessDAL.Instance.GetNameColor(goodDetail.IdColor);
            this.saleWindow.txtDetailSize.Text = BusinessDAL.Instance.GetNameSize(goodDetail.IdSize);
        }
        
        public void LoadPrice(BillUC billUC)
        {
            if (billUC.quantity.Text != "")
            {
                int price = int.Parse(billUC.quantity.Text) * int.Parse(billUC.pricedefaul.Text);
                billUC.price.Text = price.ToString();
                BusinessDAL.Instance.UpdateSaleBillDetailToDatabase(int.Parse(billUC.idBillDetail.Text), int.Parse(billUC.quantity.Text), price);
            }
            this.Total = string.Format("{0:N0}", BusinessDAL.Instance.UpdateTotalSaleBillToDatabase(this.idSaleBill));
        }
        public void SaveSaleBill(SaleWindow saleWindow)
        {
            if (saleWindow.txtNumberCustomer.Text != "" && BusinessDAL.Instance.CheckSumSaleBill(int.Parse(saleWindow.txtIdSaleBill.Text)) > 0)
            {
                int id = BusinessDAL.Instance.GetIdCustomer(saleWindow.txtNumberCustomer.Text);
                BusinessDAL.Instance.UpdateCustomerSaleBill(id, int.Parse(saleWindow.txtIdSaleBill.Text));
                int total = BusinessDAL.Instance.UpdateTotalSaleBillToDatabase(int.Parse(saleWindow.txtIdSaleBill.Text));
                Update(int.Parse(saleWindow.txtIdSaleBill.Text));
                saleWindow.Close();
                CustomMessageBox.Instance.Success("Tạo đơn hàng thành công");
                LoadBill(this.homeWindow);
            } else
            {
                if (saleWindow.txtNumberCustomer.Text == "")
                {
                    CustomMessageBox.Show("Thiếu thông tin khách hàng");
                }
                if (BusinessDAL.Instance.CheckSumSaleBill(int.Parse(saleWindow.txtIdSaleBill.Text)) == 0)
                {
                    CustomMessageBox.Show("Chưa có sản phẩm");
                }
            }
        }
        public void Update(int id)
        {
            List<SaleBillDetail> saleBillDetails = BusinessDAL.Instance.ConvertDBToSaleBillList(id);
            foreach (SaleBillDetail saleBillDetail in saleBillDetails)
            {
                BusinessDAL.Instance.UpdateQuantitiyGood(saleBillDetail.IdGood, saleBillDetail.Quantity);
            }
        }
    }
}
