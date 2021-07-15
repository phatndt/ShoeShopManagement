using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Threading;
using ShoeShopManagement.ViewModels;
using ShoeShopManagement.DAL;
using ShoeShopManagement.Models;
using ShoeShopManagement.Views;

namespace ShoeShopManagement.ViewModels
{
    class SignInViewModel
    {
        public ICommand SignInCommand { get; set; }
        private string password;
        private string userName;
        private bool isSignIn;
        public bool IsSignIn { get => isSignIn; set => isSignIn = value; }
        public Employee employee;
        public SignInViewModel()
        {
            SignInCommand = new RelayCommand<SignInWindow>((parameter) => true, (parameter) => SignIn(parameter));
        }
        public void SignIn(SignInWindow parameter)
        {
            isSignIn = false;
            if (parameter == null)
            {
                return;
            }
            if (string.IsNullOrEmpty(parameter.txtUsername.Text) || string.IsNullOrEmpty(parameter.txtPassword.Password))
            {
                CustomMessageBox.Show("Thông tin chưa đầy đủ");
                parameter.txtUsername.Focus();
                return;
            } else
            {
                string convertPass = Converter.Instance.MD5Hash(parameter.txtPassword.Password);
                if (AccountDAL.Instance.CheckLogin(parameter.txtUsername.Text, convertPass))
                {
                    HomeWindow homeWindow = new HomeWindow();
                    parameter.txtUsername.Clear();
                    parameter.txtUsername.Clear();
                    parameter.Close();
                    homeWindow.ShowDialog();
                }
            }
            
        }
    }
}
