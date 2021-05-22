using FootballFieldManagement.DAL;
using Microsoft.Win32;
using ShoeShopManagement.Models;
using ShoeShopManagement.Resources.UserControls;
using ShoeShopManagement.Views;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;

namespace ShoeShopManagement.ViewModels
{
    class EmployeeViewModel : BaseViewModel
    {
        public ICommand OpenWindowAddEmployeeCommand { get; set; }
        public ICommand ExportSalaryCommand { get; set; }
        public ICommand LoadEmployeeCommand { get; set; }

        public ICommand SelectImageCommand { get; set; }
        public ICommand SaveEmployeeCommand { get; set; }
        public ICommand CancalAddEmployeeCammand { get; set; }

        public ICommand UpdateCommand { get; set; }
        public ICommand DeleteCommand { get; set; }

        private string imageFileName;

        public EmployeeViewModel()
        {
            //grid_employee
            OpenWindowAddEmployeeCommand = new RelayCommand<AddEmployeeWindow>((parameter) => true, (parameter) => OpenWindowAddEmployee(parameter));
            ExportSalaryCommand = new RelayCommand<Button>((parameter) => true, (parameter) => ExportSalary(parameter));
            LoadEmployeeCommand = new RelayCommand<HomeWindow>((parameter) => true, (parameter) => LoadEmployee(parameter));
            //add_employee_window
            SelectImageCommand = new RelayCommand<Grid>((parameter) => true, (parameter) => SelectImage(parameter));
            SaveEmployeeCommand = new RelayCommand<AddEmployeeWindow>((parameter) => true, (parameter) => SaveEmployee(parameter));
            CancalAddEmployeeCammand = new RelayCommand<Window>((parameter) => true, (parameter) => parameter.Close());

            UpdateCommand = new RelayCommand<AddEmployeeWindow>((parameter) => true, (parameter) => SaveEmployee(parameter));
            DeleteCommand = new RelayCommand<AddEmployeeWindow>((parameter) => true, (parameter) => SaveEmployee(parameter));
        }
        public void OpenWindowAddEmployee(AddEmployeeWindow paramater)
        {
            AddEmployeeWindow addEmployeeWindow = new AddEmployeeWindow();
            addEmployeeWindow.Show();
        }
        public void ExportSalary(Button paramater)
        {
            //DataTable dt = new DataTable();
            //if(DataProvider.Instance.LoadData("Employee").Rows.Count >= 1)
            //{
            //    MessageBox.Show("Ok");
            //} else MessageBox.Show("CC");
        }
        public void LoadEmployee(HomeWindow parameter)
        {
            List<Employee> employees = EmployeeDAL.Instance.ConvertDBToList();
            foreach (Employee employee in employees)
            {
                EmployeeUc employeeUc = new EmployeeUc();
                employeeUc.txbID.Text = employee.IdEmployee.ToString();
                employeeUc.txbName.Text = employee.Name;
                employeeUc.txbPosition.Text = employee.Position;
                employeeUc.txbTelephone.Text = employee.Telephone;
                employeeUc.txbStartDate.Text = employee.StartDate.ToString();
                parameter.stkEmployee.Children.Add(employeeUc);
                MessageBox.Show("oke");
            }
        }
        public void SelectImage(Grid parameter)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.Title = "Select a picture";
            openFileDialog.Filter = "All supported graphics|*.jpg;*.jpeg;*.png|" + "JPEG (*.jpg;*.jpeg)|*.jpg;*.jpeg|" + "Portable Network Graphic (*.png)|*.png";
            if (openFileDialog.ShowDialog() == true)
            {
                imageFileName = openFileDialog.FileName;
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
        public void SaveEmployee(AddEmployeeWindow parameter)
        {

        }

    }
}
