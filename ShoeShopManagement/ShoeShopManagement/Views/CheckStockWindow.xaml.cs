using ShoeShopManagement.DAL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace ShoeShopManagement.Views
{
    /// <summary>
    /// Interaction logic for CheckStockWindow.xaml
    /// </summary>
    public partial class CheckStockWindow : Window
    {
        public CheckStockWindow()
        {
            InitializeComponent();
            txtGood.SelectionChanged += TxtGood_SelectionChanged;
        }

        private void TxtGood_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            ComboBox cmb = sender as ComboBox;
            txtId.Text = StockCheckDAL.Instance.GetIdProduct(cmb.SelectedItem.ToString()).ToString();
        }
    }
}
