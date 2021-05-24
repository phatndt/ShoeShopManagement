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

namespace ShoeShopManagement.ViewModels
{
    class GoodsViewModel : BaseViewModel
    {
        private string imageFileName;
        public ICommand AddGoodsCommand { get; set; } //thêm mặt hàng
        public ICommand ImportStockWindowCommand { get; set; }
        public ICommand SelectImageCommand { get; set; }
        public ICommand SaveCommand { get; set; }
        public ICommand ExitCommand { get; set; }
        public ICommand SeparateThousandsCommand { get; set; }
        public GoodsViewModel()
        {
            AddGoodsCommand = new RelayCommand<HomeWindow>((parameter) => true, (parameter) => OpenAddGoodsWindow(parameter));
            
            SelectImageCommand = new RelayCommand<Grid>((parameter) => true, (parameter) => ChooseImage(parameter));
            SaveCommand = new RelayCommand<AddGoodsWindow>((parameter) => true, (parameter) => AddGoods(parameter));
            ExitCommand = new RelayCommand<AddGoodsWindow>((parameter) => true, (parameter) => parameter.Close());
            SeparateThousandsCommand = new RelayCommand<TextBox>((parameter) => true, (parameter) => SeparateThousands(parameter));
            ////
            ImportStockWindowCommand = new RelayCommand<HomeWindow>((parameter) => true, (parameter) => OpenImportStockWindow(parameter));
        }
        public void OpenAddGoodsWindow(HomeWindow parameter)
        {
            AddGoodsWindow wdAddGoods = new AddGoodsWindow();
            wdAddGoods.ShowDialog();
        }
        public void OpenImportStockWindow(HomeWindow parameter)
        {
            ImportGoodsWindow wdImportGoods = new ImportGoodsWindow();
            wdImportGoods.ShowDialog();
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
            if (string.IsNullOrEmpty(parameter.txtUnitPrice.Text))
            {
                parameter.txtUnitPrice.Focus();
                parameter.txtUnitPrice.Text = "";
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
                imgByteArr = GoodsDAL.Instance.GetGoods(parameter.txtIdGoods.Text).ImageFile;
            }
            //imageFileName = null;
            Goods newGoods = new Goods(int.Parse(parameter.txtIdGoods.Text), parameter.txtName.Text,
                parameter.cboUnit.Text, ConvertToNumber(parameter.txtUnitPrice.Text), imgByteArr);
            bool isSuccessed1 = true, isSuccessed2 = true;
            if (goodsList.Count == 0 || newGoods.IdGoods > goodsList[goodsList.Count - 1].IdGoods)
            {
                if (GoodsDAL.Instance.IsExistGoodsName(parameter.txtName.Text))
                {
                    CustomMessageBox.Show("Mặt hàng đã tồn tại, vui lòng nhập lại!", "Thông báo", MessageBoxButton.OK, MessageBoxImage.Error);
                    parameter.txtName.Focus();
                    parameter.txtName.Text = "";
                    return;
                }
                isSuccessed1 = GoodsDAL.Instance.AddIntoDB(newGoods);
                if (isSuccessed1)
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
                if (isSuccessed2)
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
}
