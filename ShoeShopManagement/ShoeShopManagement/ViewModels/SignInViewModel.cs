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
using FootballFieldManagement.DAL;

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
            List<Account> accounts = AccountDAL.Instance.ConvertDBToList();
            //check username
            if (string.IsNullOrEmpty(parameter.txtUsername.Text))
            {
                MessageBox.Show("Vui lòng nhập tên đăng nhập!", "Thông báo", MessageBoxButton.OK, MessageBoxImage.Warning);
                parameter.txtUsername.Focus();
                return;
            }
            //check password
            if (string.IsNullOrEmpty(parameter.txtPassword.Password))
            {
                MessageBox.Show("Vui lòng nhập mật khẩu!", "Thông báo", MessageBoxButton.OK, MessageBoxImage.Warning);
                parameter.txtPassword.Focus();
                return;
            }
            foreach (var account in accounts)
            {
                if (account.UserName == parameter.txtUsername.Text.ToString() && account.PassWord == password && account.TypeAcount != 3)
                {
                    CurrentAccount.Type = account.TypeAcount; // Kiểm tra quyền
                    if (CurrentAccount.Type != 0)
                    {
                        List<Employee> employees = EmployeeDAL.Instance.ConvertDBToList();
                        foreach (var employee in employees)
                        {
                            if (employee.IdAccount == account.IdAccount)
                            {
                                //Lấy thông tin người đăng nhập
                                CurrentAccount.DisplayName = employee.Name;
                                CurrentAccount.Image = employee.Image;
                                CurrentAccount.IdEmployee = employee.IdEmployee;
                                this.employee = employee;
                                break;
                            }
                        }
                    }
                    CurrentAccount.IdAccount = account.IdAccount;
                    CurrentAccount.Password = password;
                    isSignIn = true;
                    break;
                }
            }
        }
    }
}
