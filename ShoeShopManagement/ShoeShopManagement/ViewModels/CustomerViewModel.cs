using ShoeShopManagement.DAL;
using ShoeShopManagement.Model;
using ShoeShopManagement.Resources.UserControls;
using ShoeShopManagement.Views;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using System.Windows.Media;

namespace ShoeShopManagement.ViewModels
{
    class CustomerViewModel: BaseViewModel
    {
        //HomeWindow
        public ICommand LoadCustomerCommand { get; set; }
        //CustomerUc
        public ICommand OpenChangeCustomerCommand { get; set; }
        public ICommand DeleteCustomerCommand { get; set; }
        //CustomerDetailWindow
        public ICommand SaveCommand { get; set; }
        public ICommand CloseCommand { get; set; }
        public string Name {
            get => name;
            set
            {
                name = value;
                SaveCommand = new RelayCommand<CustomerDetailWindow>(parameter => CanSave(), parameter => Save(parameter));
            } 
        }
        public string Number { 
            get => number;
            set 
            { 
                number = value;
                SaveCommand = new RelayCommand<CustomerDetailWindow>(parameter => CanSave(), parameter => Save(parameter));
            }
        }

        private string name;
        private string number;
        public HomeWindow homeWindow;
        public CustomerViewModel()
        {
            LoadCustomerCommand = new RelayCommand<HomeWindow>(parameter => true, parameter => LoadCustomer(parameter));

            OpenChangeCustomerCommand = new RelayCommand<CustomerUc>(parameter => true, parameter => OpenChangeCustomer(parameter));
            DeleteCustomerCommand = new RelayCommand<CustomerUc>(parameter => true, parameter => DeleteCustomer(parameter));

            CloseCommand = new RelayCommand<CustomerDetailWindow>(parameter => true, parameter => parameter.Close());
            SaveCommand = new RelayCommand<CustomerDetailWindow>(parameter => CanSave(), parameter => Save(parameter));
        }

        private void LoadCustomer(HomeWindow parameter)
        {
            this.homeWindow = parameter;
            parameter.stkCustomer.Children.Clear();
            List<Customer> customers = CustomerDAL.Instance.ConvertDBToList();
            bool flag = false;
            int id = 1;
            foreach (Customer customer in customers)
            {
                CustomerUc customerUc = new CustomerUc();
                flag = !flag;
                if (flag)
                {
                    customerUc.grdMain.Background = (Brush)new BrushConverter().ConvertFrom("#FFF1D597");
                }
                customerUc.txbId.Text = id.ToString();
                customerUc.txbName.Text = customer.Name;
                customerUc.txbTelephone.Text = customer.PhoneNumber;
                int countBill = CustomerDAL.Instance.GetBusinessBillByIdCustomer(customer.IdCustomer) + CustomerDAL.Instance.GetServiceBillByIdCustomer(customer.IdCustomer);
                customerUc.textBill.Text = countBill.ToString();
                int moneyBill = CustomerDAL.Instance.GetTotalMoneyBusinessBillByIdCustomer(customer.IdCustomer) + CustomerDAL.Instance.GetTotalMoneyServiceBillByIdCustomer(customer.IdCustomer);
                customerUc.textMoneyBill.Text = moneyBill.ToString();
                customerUc.txbIdCustomer.Text = customer.IdCustomer.ToString();
                parameter.stkCustomer.Children.Add(customerUc);
                id++;
            }
        }

        private void OpenChangeCustomer(CustomerUc parameter)
        {
            CustomerDetailWindow customerDetailWindow = new CustomerDetailWindow();
            customerDetailWindow.txtIdCustomer.Text = parameter.txbIdCustomer.Text;
            customerDetailWindow.txtCustomer.Text = parameter.txbName.Text;
            customerDetailWindow.txtPhoneCustomer.Text = parameter.txbTelephone.Text;
            customerDetailWindow.txtBusinessBill.Text = CustomerDAL.Instance.GetBusinessBillByIdCustomer(int.Parse(parameter.txbIdCustomer.Text)).ToString();
            customerDetailWindow.txtServiceBill.Text = CustomerDAL.Instance.GetServiceBillByIdCustomer(int.Parse(parameter.txbIdCustomer.Text)).ToString();
            customerDetailWindow.txtMoneyBusinessBill.Text = CustomerDAL.Instance.GetTotalMoneyBusinessBillByIdCustomer(int.Parse(parameter.txbIdCustomer.Text)).ToString();
            customerDetailWindow.txtMoneyServiceBill.Text = CustomerDAL.Instance.GetTotalMoneyServiceBillByIdCustomer(int.Parse(parameter.txbIdCustomer.Text)).ToString();
            customerDetailWindow.ShowDialog();
        }

        private void DeleteCustomer(CustomerUc parameter)
        {
            int id = int.Parse(parameter.txbIdCustomer.Text);
            CustomerDAL.Instance.DeleteCustomer(id);
            LoadCustomer(this.homeWindow);
        }
        private void Save(CustomerDetailWindow parameter)
        {
            Customer customer = new Customer(int.Parse(parameter.txtIdCustomer.Text), parameter.txtCustomer.Text, parameter.txtPhoneCustomer.Text, 0);
            CustomerDAL.Instance.SaveChangeCustomer(customer);
            parameter.Close();
            Notification.Instance.Success("Thay đổi thông tin công");
            LoadCustomer(this.homeWindow);
        }
        private bool CanSave()
        {
            return (!String.IsNullOrEmpty(name) && number.Length == 10);
        }
    }
}
