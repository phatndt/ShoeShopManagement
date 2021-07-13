using Microsoft.Expression.Interactivity.Core;
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
        public BusinessViewModel()
        {
            TestCommand = new RelayCommand<Button>((parameter) => true, (parameter) => Test(parameter));
            LoadCommand = new RelayCommand<BusinessWindow>((parameter) => true, (parameter) => Load(parameter));
            
        }
        public void Test(Button parameter)
        {
            MessageBox.Show("a");
            
        }
        public void Load(BusinessWindow parameter)
        {
            bool checkbackground = true;
            int a= 0;
            for (int i = 0; i < 30; i++)
            {
                FieldInfoBill fieldInfoBill = new FieldInfoBill();
                if(checkbackground)
                {
                    
                    fieldInfoBill.grdMain.Background = (Brush)new BrushConverter().ConvertFromString("#EEEEEE");
                    a++;
                    checkbackground = false;
                }
                else
                    checkbackground = true;
                parameter.PanelBills.Children.Add(fieldInfoBill);
            }
        }

        private ActionCommand loadBussinessCommand;

        public ICommand LoadBussinessCommand
        {
            get
            {
                if (loadBussinessCommand == null)
                {
                    loadBussinessCommand = new ActionCommand(LoadBussiness);
                }

                return loadBussinessCommand;
            }
        }

        private void LoadBussiness()
        {
        }
    }
}
