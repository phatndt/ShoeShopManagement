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
    class SignIn_UpViewModel : BaseViewModel
    {
        public ICommand GetNameTab { get; set; }
        public ICommand SwitchTabCommand { get; set; }
        public string Name;
        public SignIn_UpViewModel()
        {
            GetNameTab = new RelayCommand<Button>((parameter) => true, (parameter) => Name = parameter.Name);

            SwitchTabCommand = new RelayCommand<SignIn_Up>((parameter) => true, (parameter) => SwitchTab(parameter));
        }
        public void SwitchTab(SignIn_Up parameter)
        {

            //string name = Name;
            //if (name == "btnHome")
            //{

            //    parameter.grd.Children.Clear();

            //}
            //if (name == "btnBusiness")
            //{
            //    BusinessWindow businessWindow = new BusinessWindow();
            //    parameter.grd.Children.Add(businessWindow);

            //}
        }
    }
}
