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
    class SignInViewModel : BaseViewModel
    {
        public ICommand LogInCommand { get; set; }
        private string password;
        public string Password { get => password; set { password = value; OnPropertyChanged(); } }
        private string userName;
        public string UserName { get => userName; set { userName = value; OnPropertyChanged(); } }
        private bool isLogin;
        public bool IsLogin { get => isLogin; set => isLogin = value; }

        public string Name;
        public SignInViewModel()
        {
            LogInCommand = new RelayCommand<SignInWindow>((parameter) => true, (parameter) => SignIn(parameter));
        }

        public void SignIn(SignInWindow parameter)
        {
            isLogin = false;
            if (parameter == null)
            {
                return;
            }
            List<Account> accounts = AccountDAL.Instance.ConvertDBToList();
            check username
            if (string.IsNullOrEmpty(parameter.txtUsername.Text))
            {
                CustomMessageBox.Show("Vui lòng nhập tên đăng nhập!", "Thông báo", MessageBoxButton.OK, MessageBoxImage.Warning);
                parameter.txtUsername.Focus();
                return;
            }
            check password
            if (string.IsNullOrEmpty(parameter.txtPassword.Password))
            {
                CustomMessageBox.Show("Vui lòng nhập mật khẩu!", "Thông báo", MessageBoxButton.OK, MessageBoxImage.Warning);
                parameter.txtPassword.Focus();
                return;
            }
            foreach (var account in accounts)
            {
                if (account.Username == parameter.txtUsername.Text.ToString() && account.Password == password && account.Type != 3)
                {
                    CurrentAccount.Type = account.Type; // Kiểm tra quyền
                    if (CurrentAccount.Type != 0)
                    {
                        List<Employee> employees = EmployeeDAL.Instance.ConvertDBToList();
                        foreach (var employee in employees)
                        {
                            if (employee.IdAccount == account.IdAccount)
                            {
                                Lấy thông tin người đăng nhập
                                CurrentAccount.DisplayName = employee.Name;
                                CurrentAccount.Image = employee.ImageFile;
                                CurrentAccount.IdEmployee = employee.IdEmployee;
                                this.employee = employee;
                                break;
                            }
                        }
                    }
                    CurrentAccount.IdAccount = account.IdAccount;
                    CurrentAccount.Password = password;
                    isLogin = true;
                    break;
                }
            }
            if (isLogin)
            {
                if (AttendanceDAL.Instance.GetMonth() != DateTime.Now.Month && AttendanceDAL.Instance.GetMonth() != 0)
                {
                    if (!AttendanceDAL.Instance.DeleteData())
                    {
                        CustomMessageBox.Show("Lỗi hệ thống!", "Thông báo", MessageBoxButton.OK, MessageBoxImage.Error);
                        return;
                    }
                }
                HomeWindow home = new HomeWindow();
                Gán thông tin cho các uc chú thích
                home.txbFieldName.Text = new DataProvider().LoadData("Information").Rows[0].ItemArray[0].ToString();
                home.ucField1.icn1.Visibility = Visibility.Visible;
                home.ucField2.icn3.Visibility = Visibility.Visible;
                home.ucField1.btn.Cursor = null;
                home.ucField2.btn.Cursor = null;
                home.ucField3.btn.Cursor = null;
                home.ucField4.btn.Cursor = null;
                home.ucField2.bdrOut.BorderBrush = (Brush)new BrushConverter().ConvertFrom("#FF1976D2");
                home.ucField3.bdrOut.BorderBrush = (Brush)new BrushConverter().ConvertFrom("#FF333333");
                home.ucField3.icn2.Visibility = Visibility.Visible;
                home.ucField4.recColor.Visibility = Visibility.Visible;
                home.ucField4.icn4.Visibility = Visibility.Visible;
                home.ucField5.icn1.Visibility = Visibility.Visible;
                home.ucField5.bdrOut.BorderBrush = (Brush)new BrushConverter().ConvertFrom("#FFCDECDA");
                SetJurisdiction(home);
                DisplayAccount(home);
                DisplayEmployee(employee, home);
                parameter.Hide();
                home.ShowDialog();
                parameter.txtPassword.Password = null;
                parameter.Show();
            }
            else
            {
                CustomMessageBox.Show("Tên đăng nhập hoặc mật khẩu không chính xác!", "Thông báo", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }
    }
}
