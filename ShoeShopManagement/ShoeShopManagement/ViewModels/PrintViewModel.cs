using ShoeShopManagement.Resources.Template;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace ShoeShopManagement.ViewModels
{
    class PrintViewModel
    {
        public ICommand PrintStockReceiptCommand { get; set; }

        public PrintViewModel()
        {
           
            PrintStockReceiptCommand = new RelayCommand<StockReceiptTemplate>((parameter) => true, (parameter) => PrintStockReceipt(parameter));
        }
        public void PrintStockReceipt(StockReceiptTemplate parameter)
        {
            try
            {
                PrintDialog printDialog = new PrintDialog();
                if (printDialog.ShowDialog() == true)
                {
                    parameter.btnPrint.Visibility = Visibility.Hidden;
                    printDialog.PrintVisual(parameter.grdPrint, "Stock receipt");
                }
            }
            finally
            {
                parameter.btnPrint.Visibility = Visibility.Visible;
            }
        }
    }
}
