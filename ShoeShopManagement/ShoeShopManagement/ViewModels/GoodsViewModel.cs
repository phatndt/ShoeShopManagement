using System;
using System.Collections.Generic;
using System.Linq;
using System.Data;
using System.Data.SqlClient;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using ShoeShopManagement.Views;
using Microsoft.Win32;
using ShoeShopManagement.Models;
using ShoeShopManagement.DAL;
using ShoeShopManagement.Resources.UserControls;
using System.ComponentModel;
using System.Diagnostics;
using ShoeShopManagement.Resources.Template;
using MaterialDesignThemes.Wpf;

namespace ShoeShopManagement.ViewModels
{
    class GoodsViewModel : BaseViewModel, INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;
        private string imageFileName;
        private string quantity;
        public string Quantity { get => quantity; set => quantity = value; }
        private string importPrice;
        public string ImportPrice { get => importPrice; set => importPrice = value; }
        public long total;

        private HomeWindow homeWindow;
        public HomeWindow HomeWindow { get => homeWindow; set => homeWindow = value; }
        public long Total
        {
            get => total;
            set
            {
                total = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(Total)));
            }
        }
        private ImportGoodsWindow importGoodsWindow;
        public ICommand LoadStkGoodsCommand { get; set; } //show AddGoodsWindow -> edit
        public ImportGoodsWindow ImportGoodsWindow { get => importGoodsWindow ; set => importGoodsWindow = value; }
        public ICommand OpenImportGoodsWindowCommand { get; set; }
        public ICommand GetWindowCommand { get; set; }
        public ICommand AddGoodsCommand { get; set; } //thêm mặt hàng
        public ICommand EditGoodsCommand { get; set; } //show AddGoodsWindow -> edit
        public ICommand DeleteGoodsCommand { get; set; } //xóa mặt hàng
        public ICommand CalculateTotalCommand { get; set; } //tính tổng tiền
        public ICommand ImportGoodsCommand { get; set; } //show ImportGoodsWindow
        public ICommand SelectImageCommand { get; set; }
        public ICommand SaveCommand { get; set; }
        public ICommand ExitCommand { get; set; }
        public ICommand BackCommand { get; set; }
        public ICommand DeleteImportGoodsDetailsCommand { get; set; }
        public ICommand ImportStockCommand { get; set; }
        public ICommand StockInDetailChangeCommand { get; set; }
        public ICommand LoadGoodsCommand { get; set; }
        public ICommand ExitImportGoodsWindowCommand { get; set; }
        public ICommand SeparateThousandsCommand { get; set; }
        public ICommand PickGoodsCommand { get; set; }
        public ICommand ViewStockReceiptTemplateCommand { get; set; }
        public GoodsViewModel()
        {
            AddGoodsCommand = new RelayCommand<HomeWindow>((parameter) => true, (parameter) => OpenAddGoodsWindow(parameter));

            LoadStkGoodsCommand = new RelayCommand<HomeWindow>((parameter) => true, (parameter) => LoadStkGoods(parameter));

            SelectImageCommand = new RelayCommand<Grid>((parameter) => true, (parameter) => ChooseImage(parameter));
            SaveCommand = new RelayCommand<AddGoodsWindow>((parameter) => true, (parameter) => AddGoods(parameter));
            ExitCommand = new RelayCommand<AddGoodsWindow>((parameter) => true, (parameter) => parameter.Close());
            SeparateThousandsCommand = new RelayCommand<TextBox>((parameter) => true, (parameter) => SeparateThousands(parameter));
            PickGoodsCommand = new RelayCommand<ImportGoodsControl>((parameter) => true, (parameter) => PickGoods(parameter));
            //
            EditGoodsCommand = new RelayCommand<TextBlock>((parameter) => true, (parameter) => ShowEditGoods(parameter));
            DeleteGoodsCommand = new RelayCommand<GoodUc>((parameter) => true, (parameter) => DeleteGoods(parameter));

            //
            StockInDetailChangeCommand = new RelayCommand<ImportGoodUC>((parameter) => true, (parameter) => UpdateStockReceiptInfo(parameter));
            OpenImportGoodsWindowCommand = new RelayCommand<HomeWindow>((parameter) => true, (parameter) => OpenImportGoodsWindow(parameter));
            GetWindowCommand = new RelayCommand<ImportGoodsWindow>((parameter) => true, (parameter) => SetWindow(parameter));
            LoadGoodsCommand = new RelayCommand<ImportGoodsWindow>((parameter) => true, (parameter) => LoadGoodsToView(parameter));
            ImportStockCommand = new RelayCommand<ImportGoodsWindow>((parameter) => true, (parameter) => CompleteStockReceipt(parameter));
            DeleteImportGoodsDetailsCommand = new RelayCommand<ImportGoodUC>((parameter) => true, (parameter) => DeleteImportGoodsDetails(parameter));
            ExitImportGoodsWindowCommand = new RelayCommand<ImportGoodsWindow>((parameter) => true, (parameter) => ExitImportGoods(parameter));
        }

        private void ExitImportGoods(ImportGoodsWindow parameter)
        {
            if (!new StackTrace().GetFrames().Any(x => x.GetMethod().Name == "Close"))
            {
                StockReceiptDAL.Instance.DeleteFromDB(parameter.txbIdStockReceipt.Text);
            }
        }

        public void DeleteImportGoodsDetails(ImportGoodUC importGoodsDetailsControl)
        {
            string idStockReceipt = importGoodsDetailsControl.txbIdStockReceipt.Text;
            StockInDetailDAL.Instance.DeleteByIdStock(importGoodsDetailsControl.txbIdGoods.Text, idStockReceipt);
            ImportGoodsWindow.stkPickedGoods.Children.Remove(importGoodsDetailsControl);
            ImportGoodsWindow.txbTotal.Text = string.Format("{0:N0}", StockInDetailDAL.Instance.CalculateTotalMoney(idStockReceipt));
        }
        public void CompleteStockReceipt(ImportGoodsWindow importStockWindow)
        {
            int idStockReceipt = int.Parse(importStockWindow.txbIdStockReceipt.Text);

            StockIn stockReceipt = new StockIn(idStockReceipt, CurrentAccount.IdAccount, DateTime.Now, ConvertToNumber(importStockWindow.txbTotal.Text));
            if (StockReceiptDAL.Instance.UpdateOnDB(stockReceipt))
            {
                List<StockInDetail> listStockReceiptInfo = StockInDetailDAL.Instance.GetStockInDetailById(idStockReceipt.ToString());
                foreach (var stockReceiptInfo in listStockReceiptInfo)
                {
                    if (stockReceiptInfo.donGia == 0)
                    {
                        CustomMessageBox.Show("Vui lòng nhập giá nhập kho!", "Thông báo", MessageBoxButton.OK, MessageBoxImage.Warning);
                        return;
                    }
                    Goods goods = GoodsDAL.Instance.GetGoods(stockReceiptInfo.mASP.ToString());
                    goods.Quantity = stockReceiptInfo.sOLuong;
                    GoodsDAL.Instance.ImportToDB(goods);
                    goods.Price = stockReceiptInfo.donGia;
                    GoodsDAL.Instance.ImportToDBSP(goods);
                }
                CustomMessageBox.Show("Nhập hàng thành công!", "Thông báo", MessageBoxButton.OK, MessageBoxImage.Asterisk);
                importStockWindow.Close();
                LoadStkGoods(homeWindow);
            }
        }
        public void LoadGoodsToView(ImportGoodsWindow parameter)
        {
            parameter.wrpGoods.Children.Clear();
            DataTable goodsList = GoodsDAL.Instance.LoadDatatable();
            for (int i = 0; i < goodsList.Rows.Count; i++)
            {
                string name = goodsList.Rows[i].ItemArray[1].ToString();
                if (name.ToLower().Contains(parameter.txtSearch.Text.ToLower()))
                {
                    ImportGoodsControl goods = new ImportGoodsControl();
                    goods.txbName.Text = goodsList.Rows[i].ItemArray[1].ToString();
                    goods.txbIdGoods.Text = goodsList.Rows[i].ItemArray[0].ToString();
                    goods.imgGood.Source = Converter.Instance.ConvertByteToBitmapImage(Convert.FromBase64String(goodsList.Rows[i].ItemArray[3].ToString()));
                    goods.txbQuantityOfInventory.Text = goodsList.Rows[i].ItemArray[4].ToString();
                    goods.txbIdStockReceipt.Text = parameter.txbIdStockReceipt.Text;
                    parameter.wrpGoods.Children.Add(goods);
                }
            }
        }
        private void UpdateStockReceiptInfo(ImportGoodUC importGoodUc)
        {
            SeparateThousands(importGoodUc.txtImportPrice);

            string idStockReceipt = importGoodUc.txbIdStockReceipt.Text;
            int quantity = int.Parse(importGoodUc.nmsQuantity.Text.ToString());
            long importPrice = 0;
            if (!string.IsNullOrWhiteSpace(importGoodUc.txtImportPrice.Text))
            {
                importPrice = ConvertToNumber(importGoodUc.txtImportPrice.Text);
            }
            int idCTPN = StockReceiptDAL.Instance.GetMaxId();
            StockInDetail stockInDetail = new StockInDetail(idCTPN+1,int.Parse(idStockReceipt),
                int.Parse(importGoodUc.txbIdGoods.Text), quantity, importPrice);

            StockInDetailDAL.Instance.UpdateOnDB(stockInDetail);
            importGoodUc.txbtotal.Text = string.Format("{0:N0}", quantity * importPrice);
            ImportGoodsWindow.txbTotal.Text = string.Format("{0:N0}", StockInDetailDAL.Instance.CalculateTotalMoney(idStockReceipt));
        }
        public void LoadImportGoodsDetails(ImportGoodsWindow importGoodsWindow)
        {
            int i = 1;
            string idStockReceipt = importGoodsWindow.txbIdStockReceipt.Text;

            importGoodsWindow.stkPickedGoods.Children.Clear();

            List<StockInDetail> listStockInDetail = StockInDetailDAL.Instance.GetStockInDetailById(idStockReceipt);

            foreach (var stockInDetail in listStockInDetail)
            {
                Goods goods = GoodsDAL.Instance.GetGoods(stockInDetail.mASP.ToString());
                ImportGoodUC importGoodsDetailsControl = new ImportGoodUC();
                importGoodsDetailsControl.txbNo.Text = i.ToString();
                importGoodsDetailsControl.txbIdStockReceipt.Text = idStockReceipt;
                importGoodsDetailsControl.txbIdGoods.Text = stockInDetail.mASP.ToString();
                importGoodsDetailsControl.txbName.Text = goods.Name;
              
                importGoodsDetailsControl.nmsQuantity.Text = stockInDetail.sOLuong;
                importGoodsDetailsControl.nmsQuantity.MinValue = 1;
                importGoodsDetailsControl.nmsQuantity.MaxValue = 99999;

                long importPrice = 0;

                if (stockInDetail.donGia != 0)
                {
                    importGoodsDetailsControl.txtImportPrice.Text = string.Format("{0:N0}", stockInDetail.donGia);
                    importPrice = stockInDetail.donGia;
                }
                importGoodsDetailsControl.txbtotal.Text = string.Format("{0:N0}", importPrice * stockInDetail.sOLuong);

                importGoodsWindow.stkPickedGoods.Children.Add(importGoodsDetailsControl);
                i++;
            }
            ImportGoodsWindow.txbTotal.Text = string.Format("{0:N0}", StockInDetailDAL.Instance.CalculateTotalMoney(idStockReceipt));
        }
        public void PickGoods(ImportGoodsControl importGoodsControl)
        {
            bool isExisted = false; 

            List<StockInDetail> listStockInDetail = StockInDetailDAL.Instance.GetStockInDetailById(importGoodsControl.txbIdStockReceipt.Text);
            int idCTPN = StockInDetailDAL.Instance.GetMaxId();

            foreach (var stockInDetail in listStockInDetail)
            {
                if (stockInDetail.mASP.ToString() == importGoodsControl.txbIdGoods.Text)
                {
                    
                    isExisted = true;
                    stockInDetail.sOLuong += 1;

                    if (StockInDetailDAL.Instance.UpdateOnDB(stockInDetail))
                    {
                        LoadImportGoodsDetails(ImportGoodsWindow);
                        
                    }
                    return;
                }
            }
            
            if (!isExisted)
                {
                
                StockInDetail stockReceiptInfo = new StockInDetail(idCTPN+1, int.Parse(importGoodsControl.txbIdStockReceipt.Text),
                        int.Parse(importGoodsControl.txbIdGoods.Text), 1, 0);

                    StockInDetailDAL.Instance.AddIntoDB(stockReceiptInfo);

                    LoadImportGoodsDetails(ImportGoodsWindow);
                    
            }
                 

        }
        private void SetWindow(ImportGoodsWindow importGoodsWindow)
        {
            ImportGoodsWindow = importGoodsWindow;
        }
        public void OpenAddGoodsWindow(HomeWindow parameter)
        {
            AddGoodsWindow wdAddGoods = new AddGoodsWindow();
            try
            {
                wdAddGoods.txtIdGoods.Text = (GoodsDAL.Instance.GetMaxId() + 1).ToString();
            }
            catch
            {
                wdAddGoods.txtIdGoods.Text = "1";
            }
            wdAddGoods.txtName.Text = null;
            wdAddGoods.txtUnitPrice.Text = null;
            wdAddGoods.ShowDialog();
        }
        public void LoadStkGoods(HomeWindow homeWindow)
        {
            this.homeWindow = homeWindow;
            homeWindow.stkGoods.Children.Clear();

            List<Goods> goodsList = GoodsDAL.Instance.ConvertDBToList();
            
            bool flag = false;
            int i = 1;
            foreach (var goods in goodsList)
            {
                string Size = GoodsDAL.Instance.GetSize(goods.IdSize);
                string Color = GoodsDAL.Instance.GetColor(goods.IdColor);
                string DVT = GoodsDAL.Instance.GetDVT(goods.IdDvt, goods.IdGood);
                GoodUc temp = new GoodUc();
                flag = !flag;
                if (flag)
                {
                    temp.grdMainGood.Background = (Brush)new BrushConverter().ConvertFrom("#FFFFFFFF");
                }
                temp.txbSTT.Text = goods.IdGood.ToString();
                temp.txbNameGood.Text = goods.Name.ToString();
                temp.txbQuantity.Text = goods.Quantity.ToString();
                temp.txbSize.Text = Size;
                temp.txbColor.Text = Color;
                temp.txbUnit.Text = DVT;
                temp.txbPrice.Text = string.Format("{0:N0}", goods.Price);
                //if (CurrentAccount.Type == 2)
                //{
                //temp.btnDeleteGood.IsEnabled = false;
                //temp.btnEditGood.IsEnabled = false;
                //}
                homeWindow.stkGoods.Children.Add(temp);
                i++;
            }
        }
        public void OpenImportGoodsWindow(HomeWindow parameter)
        {
            this.homeWindow = parameter;
            ImportGoodsWindow importGoodsWD = new ImportGoodsWindow();
            try
            {
                string idStockReceipt = (StockReceiptDAL.Instance.GetMaxId() + 1).ToString();

                StockIn stockIn = new StockIn(int.Parse(idStockReceipt),1 /*CurrentAccount.IdAccount*/, DateTime.Now, 0);
                StockReceiptDAL.Instance.AddIntoDB(stockIn);
                importGoodsWD.txbIdStockReceipt.Text = idStockReceipt;
            }
            catch
            {
                StockIn stockReceipt = new StockIn(1,1 /*CurrentAccount.IdAccount*/, DateTime.Now, 0);
                StockReceiptDAL.Instance.AddIntoDB(stockReceipt);
                importGoodsWD.txbIdStockReceipt.Text = "1";
            }
            importGoodsWD.txbDate.Text = DateTime.Now.ToString("dd/MM/yyyy");
            importGoodsWD.txbTime.Text = DateTime.Now.ToString("HH:mm");
            importGoodsWD.ShowDialog();
        }
        
        public void ShowEditGoods(TextBlock parameter)
        {
            Goods goods = GoodsDAL.Instance.GetGoods(parameter.Text);
            AddGoodsWindow updateWindow = new AddGoodsWindow();
            updateWindow.txtIdGoods.Text = goods.IdGood.ToString();
            updateWindow.txtName.Text = goods.Name;
            updateWindow.txtName.SelectionStart = updateWindow.txtName.Text.Length;
            updateWindow.txtName.Select(0, updateWindow.txtName.Text.Length);

            ImageBrush imageBrush = new ImageBrush();
            imageBrush.ImageSource = Converter.Instance.ConvertByteToBitmapImage(goods.Image);
            updateWindow.grdSelectImg.Background = imageBrush;
            if (updateWindow.grdSelectImg.Children.Count > 1)
            {
                updateWindow.grdSelectImg.Children.Remove(updateWindow.grdSelectImg.Children[0]);
                updateWindow.grdSelectImg.Children.Remove(updateWindow.grdSelectImg.Children[1]);
            }
            updateWindow.Title = "Cập nhật thông tin hàng hóa";
            updateWindow.ShowDialog();
        }
        public void DeleteGoods(GoodUc goodsControl)
        {
            MessageBoxResult result = CustomMessageBox.Show("Xác nhận xóa hàng hóa?", "Thông báo", MessageBoxButton.YesNo, MessageBoxImage.Question);

            if (result == MessageBoxResult.Yes)
            {
                string idGoods = goodsControl.txbSTT.Text;
                bool isSuccessed = GoodsDAL.Instance.DeleteFromDB(idGoods);
                if (isSuccessed)
                {
                    homeWindow.stkGoods.Children.Remove(goodsControl);
                }
                else
                {
                    CustomMessageBox.Show("Thực hiện thất bại!", "Thông báo", MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }
        }
        public void ChooseImage(Grid parameter)
        {
            OpenFileDialog op = new OpenFileDialog();
            op.Title = "Chọn ảnh";
            op.Filter = "All supported graphics|*.jpg;*.jpeg;*.png|" + "JPEG (*.jpg;*.jpeg)|*.jpg;*.jpeg|" + "Portable Network Graphic (*.png)|*.png";
            if (op.ShowDialog() == true)
            {
                imageFileName = op.FileName;
                ImageBrush imageBrush = new ImageBrush();
                BitmapImage bitmap = new BitmapImage();
                bitmap.BeginInit();
                bitmap.CacheOption = BitmapCacheOption.OnLoad;
                bitmap.UriSource = new Uri(imageFileName);
                bitmap.EndInit();
                imageBrush.ImageSource = bitmap;
                parameter.Background = imageBrush;
                if (parameter.Children.Count > 1)
                {
                    parameter.Children.Remove(parameter.Children[0]);
                    parameter.Children.Remove(parameter.Children[1]);
                }
            }
        }
        public void AddGoods(AddGoodsWindow parameter)
        {
            List<Goods> goodsList = GoodsDAL.Instance.ConvertDBToList();
            if (string.IsNullOrWhiteSpace(parameter.txtName.Text))
            {
                parameter.txtName.Focus();
                parameter.txtName.Text = "";
                return;
            }
            if (string.IsNullOrEmpty(parameter.cboUnit.Text))
            {
                parameter.cboUnit.Focus();
                return;
            }
            if (string.IsNullOrEmpty(parameter.cboType.Text))
            {
                parameter.cboType.Focus();
                return;
            }
            if (string.IsNullOrEmpty(parameter.txtUnitPrice.Text))
            {
                parameter.txtUnitPrice.Focus();
                parameter.txtUnitPrice.Text = "";
                return;
            }
            if (string.IsNullOrEmpty(parameter.txtSize.Text))
            {
                parameter.txtSize.Focus();
                parameter.txtSize.Text = "";
                return;
            }
            if (string.IsNullOrEmpty(parameter.txtColor.Text))
            {
                parameter.txtColor.Focus();
                parameter.txtColor.Text = "";
                return;
            }
            if (parameter.grdSelectImg.Background == null)
            {
                CustomMessageBox.Show("Vui lòng thêm hình ảnh!", "Thông báo", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }
            byte[] imgByteArr;
            try
            {
                imgByteArr = Converter.Instance.ConvertImageToBytes(imageFileName);
            }
            catch
            {
                imgByteArr = GoodsDAL.Instance.GetGoods(parameter.txtIdGoods.Text).Image;
            }
            //imageFileName = null;
            Goods newGoods = new Goods(int.Parse(parameter.txtIdGoods.Text), parameter.txtName.Text, ConvertToType(parameter.cboType.Text),
                 0, ConvertToSize(int.Parse(parameter.txtSize.Text)), ConvertToColor(parameter.txtColor.Text),
                 ConvertToNumber(parameter.txtUnitPrice.Text), ConvertToUnit(parameter.cboUnit.Text), 0, imgByteArr);

            bool isSuccessed1 = true, isSuccessed2 = true, isSuccessed3=true,isSuccessed4=true ;

            if (goodsList.Count == 0 || newGoods.IdGood > goodsList[goodsList.Count - 1].IdGood)
            {
                if (GoodsDAL.Instance.IsExistGoodsName(parameter.txtName.Text))
                {
                    CustomMessageBox.Show("Mặt hàng đã tồn tại, vui lòng nhập lại!", "Thông báo", MessageBoxButton.OK, MessageBoxImage.Error);
                    parameter.txtName.Focus();
                    parameter.txtName.Text = "";
                    return;
                }
                isSuccessed1 = GoodsDAL.Instance.AddIntoDB(newGoods);
                isSuccessed3 = GoodsDAL.Instance.AddIntoDBCT(newGoods);
                if (isSuccessed1 && isSuccessed3)
                {
                    CustomMessageBox.Show("Thêm mặt hàng thành công!", "Thông báo", MessageBoxButton.OK, MessageBoxImage.Asterisk);
                }
            }
            else
            {
                if (GoodsDAL.Instance.GetGoods(parameter.txtIdGoods.Text).Name != parameter.txtName.Text)
                {
                    if (GoodsDAL.Instance.IsExistGoodsName(parameter.txtName.Text))
                    {
                        CustomMessageBox.Show("Mặt hàng đã tồn tại, vui lòng nhập lại!", "Thông báo", MessageBoxButton.OK, MessageBoxImage.Error);
                        parameter.txtName.Focus();
                        parameter.txtName.Text = "";
                        return;
                    }
                }
                isSuccessed2 = GoodsDAL.Instance.UpdateOnDB(newGoods);
                isSuccessed4 = GoodsDAL.Instance.UpdateOnDBCT(newGoods);
                if (isSuccessed2 && isSuccessed4)
                {
                    CustomMessageBox.Show("Cập nhật thành công!", "Thông báo", MessageBoxButton.OK, MessageBoxImage.Asterisk);
                }
            }
            if (!isSuccessed1 || !isSuccessed2)
            {
                CustomMessageBox.Show("Thực hiện thất bại", "Thông báo", MessageBoxButton.OK, MessageBoxImage.Error);
            }
            parameter.Close();
            LoadStkGoods(homeWindow);
        }
        
    }
}
