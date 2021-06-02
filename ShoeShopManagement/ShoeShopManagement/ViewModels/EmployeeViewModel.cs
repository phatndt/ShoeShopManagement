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
        public ICommand OpenSalaryCommand { get; set; }
        public ICommand LoadEmployeeCommand { get; set; }

        public ICommand SelectImageCommand { get; set; }
        public ICommand SaveEmployeeCommand { get; set; }
        public ICommand CancalAddEmployeeCammand { get; set; }

        public ICommand LoadPositionCommand { get; set; }

        public ICommand UpdateCommand { get; set; }
        public ICommand DeleteCommand { get; set; }

        public ICommand ConfirmPaySalaryCommand { get; set; }
        public ICommand CancelPaySalaryCommand { get; set; }
        private string imageFileName;

        public HomeWindow homeWindow;

        public bool AorU;
        public EmployeeViewModel()
        {
            //grid_employee
            OpenWindowAddEmployeeCommand = new RelayCommand<AddEmployeeWindow>((parameter) => true, (parameter) => OpenWindowAddEmployee(parameter));
            OpenSalaryCommand = new RelayCommand<Button>((parameter) => true, (parameter) => OpenSalary(parameter));
            LoadEmployeeCommand = new RelayCommand<HomeWindow>((parameter) => true, (parameter) => LoadEmployee(parameter));
            //add_employee_window
            SelectImageCommand = new RelayCommand<Grid>((parameter) => true, (parameter) => SelectImage(parameter));
            SaveEmployeeCommand = new RelayCommand<AddEmployeeWindow>((parameter) => true, (parameter) => SaveEmployee(parameter));
            CancalAddEmployeeCammand = new RelayCommand<Window>((parameter) => true, (parameter) => parameter.Close());
            LoadPositionCommand = new RelayCommand<AddEmployeeWindow>((parameter) => true, (parameter) => LoadPosition(parameter));
            //employee_control
            UpdateCommand = new RelayCommand<EmployeeUc>((parameter) => true, (parameter) => UpdateEmployee(parameter));
            DeleteCommand = new RelayCommand<EmployeeUc>((parameter) => true, (parameter) => DeleteEmployee(parameter));
            //pay_salary_window
            ConfirmPaySalaryCommand = new RelayCommand<PaySalaryWindowxaml>((parameter) => true, (parameter) => ConfirmPaySalary(parameter));
            CancelPaySalaryCommand = new RelayCommand<PaySalaryWindowxaml>((parameter) => true, (parameter) => parameter.Close());
        }

        public void OpenWindowAddEmployee(AddEmployeeWindow paramater)
        {
            this.AorU = true;
            AddEmployeeWindow addEmployeeWindow = new AddEmployeeWindow();
            int id = EmployeeDAL.Instance.GetMaxId()+ 1;
            addEmployeeWindow.txtIdEmployee.Text = id.ToString();
            addEmployeeWindow.Show();
        }
        public void OpenSalary(Button paramater)
        {
            PaySalaryWindowxaml paySalaryWindowxaml = new PaySalaryWindowxaml();
            paySalaryWindowxaml.Show();
        }
        public void LoadEmployee(HomeWindow parameter)
        {
            this.homeWindow = parameter;
            parameter.stkEmployee.Children.Clear();
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
            }
            //List<Employee> employees = EmployeeDAL.Instance.ConvertDBToList();
            //foreach (Employee employee in employees)
            //{
            //    EmployeeUc employeeUc = new EmployeeUc();
            //    employeeUc.txbID.Text = employee.IdEmployee.ToString();
            //    employeeUc.txbName.Text = employee.Name;
            //    employeeUc.txbPosition.Text = employee.Position;
            //    employeeUc.txbTelephone.Text = employee.Telephone;
            //    employeeUc.txbStartDate.Text = employee.StartDate.ToString();
            //    parameter.stkEmployee.Children.Add(employeeUc);
            //    MessageBox.Show("oke");
            //}
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
            string sex;
            if (string.IsNullOrEmpty(parameter.txtName.Text))
            {
                parameter.txtName.Focus();
                return;
            }
            if (string.IsNullOrEmpty(parameter.txtDate.Text))
            {
                parameter.txtDate.Focus();
                return;
            } else
            {

            }
            if (parameter.rdoMale.IsChecked.Value == true)
            {
                sex = "Nam";
            } else sex = "Nữ";
            if (string.IsNullOrEmpty(parameter.txtStartDate.Text))
            {
                parameter.txtStartDate.Focus();
                return;
            }
            if (string.IsNullOrEmpty(parameter.txtPosition.Text))
            {
                parameter.txtPosition.Focus();
                return;
            }
            if (string.IsNullOrEmpty(parameter.txtTelephoneNumber.Text))
            {
                parameter.txtTelephoneNumber.Focus();
                return;
            }
            if (string.IsNullOrEmpty(parameter.txtAddress.Text))
            {
                parameter.txtAddress.Focus();
                return;
            }
            if (parameter.grdSelectImage.Background == null)
            {
                //parameter.grdSelectImage.Focus();
                //return;
            }
            try
            {
                Employee employee = new Employee(
                    int.Parse(parameter.txtIdEmployee.Text), 
                    parameter.txtName.Text, 
                    DateTime.Parse(parameter.txtDate.Text), 
                    sex,
                    DateTime.Parse(parameter.txtStartDate.Text), 
                    parameter.txtPosition.Text, 
                    parameter.txtTelephoneNumber.Text,
                    parameter.txtAddress.Text,
                    sex); 
                if (AorU)
                {
                    if (EmployeeDAL.Instance.AddEmployeeToDatabase(employee))
                    {
                        MessageBox.Show("Thêm thành công");
                    }
                } 
                else
                {
                    if (EmployeeDAL.Instance.UpdateOnDatabase(employee))
                    {
                        MessageBox.Show("Thêm thành công");
                    }
                }    
                
            } 
            catch (Exception e)
            {

            }
            finally
            {
                LoadEmployee(homeWindow);
                parameter.Close();
            }
        }
        public void LoadPosition(AddEmployeeWindow parameter)
        {
            parameter.txtPosition.Items.Add("Nhân viên");
            parameter.txtPosition.Items.Add("Quản lý");
        }

        public void UpdateEmployee(EmployeeUc parameter)
        {
            this.AorU = false;
            AddEmployeeWindow addEmployeeWindow = new AddEmployeeWindow();
            Employee employee = EmployeeDAL.Instance.GetEmployee(parameter.txbID.Text);
            addEmployeeWindow.txtIdEmployee.Text = employee.IdEmployee.ToString();
            addEmployeeWindow.txtName.Text = employee.Name;
            addEmployeeWindow.txtDate.Text = employee.Date.ToString();
            if (employee.Sex == "Nam")
                addEmployeeWindow.rdoMale.IsChecked = true;
            else
                addEmployeeWindow.rdoFemale.IsChecked = true;
            addEmployeeWindow.txtStartDate.Text = employee.StartDate.ToString();
            
            addEmployeeWindow.txtPosition.Text = employee.Position;
            addEmployeeWindow.txtTelephoneNumber.Text = employee.Telephone;
            addEmployeeWindow.txtAddress.Text = employee.Address;
            addEmployeeWindow.Show();
            foreach (var item in addEmployeeWindow.txtPosition.Items)
            {
                if (item.ToString() == employee.Position)
                {
                    addEmployeeWindow.txtPosition.SelectedItem = item;
                }
            }
        }
        public void DeleteEmployee(EmployeeUc parameter)
        {
            int id = int.Parse(parameter.txbID.Text);
            if(EmployeeDAL.Instance.DeleteEmployee(id)) {
                MessageBox.Show("Xóa thành công");
            }
            this.homeWindow.stkEmployee.Children.Remove(parameter);
        }

        private void ConfirmPaySalary(PaySalaryWindowxaml parameter)
        {
            parameter.stkSalary.Children.Add(new SalaryUc());
        }

    }
}
