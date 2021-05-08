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

namespace ShoeShopManagement.ViewModels
{
    class HomeViewModel : BaseViewModel
    {
        public ICommand GetNameTab { get; set; }
        public ICommand SwitchTabCommand { get; set; }
        public string Name;
        public HomeViewModel()
        {
            GetNameTab = new RelayCommand<Button>((parameter) => true, (parameter) => Name = parameter.Name);

            SwitchTabCommand = new RelayCommand<HomeWindow>((parameter) => true, (parameter) => SwitchTab(parameter));

        }
        public void SwitchTab(HomeWindow parameter)
        {
            //for (int i = 0; i < 20; i++)
            //{
            //    parameter.stkEmployee.Children.Add(new EmployeeUc());
            //}


            //string name = Name;
            //if (name == "btnHome")
            //{

                
            //}
            //if (name == "btnBusiness")
            //{
            //    BusinessWindow businessWindow = new BusinessWindow();

            //}
        }
    }
}
