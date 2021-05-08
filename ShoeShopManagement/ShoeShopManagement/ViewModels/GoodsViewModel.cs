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

namespace ShoeShopManagement.ViewModels
{
    class GoodsViewModel : BaseViewModel
    {
        public ICommand AddGoodsCommand { get; set; } //thêm mặt hàng
        public ICommand ImportStockWindowCommand { get; set; }
        public GoodsViewModel()
        {
            AddGoodsCommand = new RelayCommand<HomeWindow>((parameter) => true, (parameter) => OpenAddGoodsWindow(parameter));
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

    }
}
