using ShoeShopManagement.DAL;
using ShoeShopManagement.Models;
using ShoeShopManagement.Resources.UserControls;
using ShoeShopManagement.Views;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Text.RegularExpressions;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;

namespace ShoeShopManagement.ViewModels
{
    class ServicesViewModel : BaseViewModel
    {
        private string nameCustomer = "";
        public string NameCustomer { get => nameCustomer; set { nameCustomer = value; OnPropertyChanged(); } }

        private string idService = "";
        public string IdService { get => idService; set { idService = value; OnPropertyChanged(); } }

        private string donGia = "";
        public string DonGia { get => donGia; set { donGia = value; OnPropertyChanged(); } }

        private string soLuong = "1";
        public string SoLuong { get => soLuong; set { soLuong = value; OnPropertyChanged(); } }

        private long total = 0;
        public long ToTal { get => total; set { total = value; OnPropertyChanged(); } }

        private string prePay = "";
        public string PrePay { get => prePay; set { prePay = value; OnPropertyChanged(); } }

        private int servicePrice = 0;

        public ICommand LoadServicesCommand { get; set; }

        public ICommand LoadServicesBillCommand { get; set; }

        public ICommand OpenAddBillWindowCommand { get; set; }

        public ICommand ExitServicesBillWindowCommand { get; set; }

        public ICommand ExitServicesBillButtonWindowCommand { get; set; }

        public ICommand GetCustomerCommand { get; set; }

        public ICommand SaveServicesBillCommand { get; set; }

        public ICommand AddBillDetailToBillCommand { get; set; }

        public ICommand GetIdServiceCommand { get; set; }

        public ICommand GetWindowCommand { get; set; }

        public ICommand DeleteServicesDetailCommand { get; set; }

        public ICommand GetDonGiaCommand { get; set; }

        public ICommand GetSoLuongCommand { get; set; }

        public ICommand GetPrePayCommand { get; set; }

        public ICommand GetWindowHomeCommand { get; set; }

        public ICommand DeleteBillCommand { get; set; }

        public ICommand DeleteServiceCommand { get; set; }

        public ICommand OpenAddServiceCommand { get; set; }

        public ICommand CloseAddServiceCommand { get; set; }

        public ICommand SaveServiceCommand { get; set; }

        public ICommand UpdateServiceCommand { get; set; }

        public ICommand OpenEditServiceCommand { get; set; }

        public ICommand CloseEditServiceCommand { get; set; }

        public HomeWindow homeWindow;

        public AddServicesBillWindow addServicesBillWindow1;
        public ServicesViewModel()
        {
            LoadServicesCommand = new RelayCommand<HomeWindow>(parameter => true, parameter => LoadServices(parameter));

            LoadServicesBillCommand = new RelayCommand<HomeWindow>(parameter => true, parameter => LoadServicesBill(parameter));

            OpenAddBillWindowCommand = new RelayCommand<HomeWindow>(parameter => true, parameter => OpenAddBillWindow(parameter));

            ExitServicesBillWindowCommand = new RelayCommand<AddServicesBillWindow>(parameter => true, parameter => ExitServicesBillWindow(parameter));

            ExitServicesBillButtonWindowCommand = new RelayCommand<AddServicesBillWindow>(parameter => true, parameter => ExitServicesBillButtonWindow(parameter));

            GetCustomerCommand = new RelayCommand<AddServicesBillWindow>(parameter => true, parameter => GetCustomer(parameter));

            SaveServicesBillCommand = new RelayCommand<AddServicesBillWindow>(parameter => true, parameter => SaveServicesBill(parameter));

            AddBillDetailToBillCommand = new RelayCommand<AddServicesBillWindow>(parameter => true, parameter => AddServicesToBill(parameter));

            GetIdServiceCommand = new RelayCommand<AddServicesBillWindow>(parameter => true, parameter => GetIdServices(parameter));

            GetWindowCommand = new RelayCommand<AddServicesBillWindow>(parameter => true, parameter => GetWindow(parameter));

            DeleteServicesDetailCommand = new RelayCommand<ServicesBillDetailUc>(parameter => true, parameter => DeleteServicesDetail(parameter));

            GetDonGiaCommand = new RelayCommand<AddServicesBillWindow>(parameter => true, parameter => GetDonGia(parameter));

            GetSoLuongCommand = new RelayCommand<AddServicesBillWindow>(parameter => true, parameter => GetSoLuong(parameter));

            GetPrePayCommand = new RelayCommand<AddServicesBillWindow>(parameter => true, parameter => GetPrePay(parameter));

            GetWindowHomeCommand = new RelayCommand<HomeWindow>(parameter => true, parameter => GetWindowHome(parameter));

            DeleteBillCommand = new RelayCommand<ServicesUc>(parameter => true, parameter => DeleteBill(parameter));

            DeleteServiceCommand = new RelayCommand<StockServicesUC>(parameter => true, parameter => DeleteService(parameter));

            OpenAddServiceCommand = new RelayCommand<HomeWindow>(parameter => true, parameter => OpenAddService(parameter));

            CloseAddServiceCommand = new RelayCommand<AddServiceWindow>(parameter => true, parameter => CloseAddService(parameter));

            SaveServiceCommand = new RelayCommand<AddServiceWindow>(parameter => true, parameter => SaveServices(parameter));

            UpdateServiceCommand = new RelayCommand<EditServiceWindow>(parameter => true, parameter => UpdateServices(parameter));

            OpenEditServiceCommand = new RelayCommand<StockServicesUC>(parameter => true, parameter => OpenEditService(parameter));

            CloseEditServiceCommand = new RelayCommand<EditServiceWindow>(parameter => true, parameter => parameter.Close());
        }

        private void LoadServices(HomeWindow homeWindow)
        {
            int i = 1;
            this.homeWindow = homeWindow;
            homeWindow.stkServices.Children.Clear();
            List<Service> services = ServicesDAL.Instance.LoadServices();
            bool flag = false;
            foreach (Service service in services)
            {
                StockServicesUC stockServices = new StockServicesUC();
                flag = !flag;
                if (flag)
                {
                    stockServices.grdMain.Background = (Brush)new BrushConverter().ConvertFrom("#FFF1D597");
                }
                stockServices.txtSTT.Text = i.ToString();
                stockServices.txtId.Text = service.IdService.ToString();
                stockServices.txtName.Text = service.Name.ToString();
                stockServices.txtPrice.Text = service.Price.ToString();

                homeWindow.stkServices.Children.Add(stockServices);
                i++;
            }
        }

        private void LoadServicesBill(HomeWindow parameter)
        {
            int i = 1;
            this.homeWindow = parameter;
            parameter.stkServicesBill.Children.Clear();
            List<ServicesBill> servicesBills = ServicesDAL.Instance.LoadServicesBill();
            bool flag = false;
            foreach (ServicesBill servicesBill in servicesBills)
            {
                ServicesUc servicesUc = new ServicesUc();
                flag = !flag;
                if (flag)
                {
                    servicesUc.grdMain.Background = (Brush)new BrushConverter().ConvertFrom("#FFF1D597");
                }
                servicesUc.txtSTT.Text = i.ToString();
                servicesUc.txtId.Text = servicesBill.IdServiceBill.ToString();
                servicesUc.txtName.Text = servicesBill.IdCustomer.ToString();
                servicesUc.txtDateCreate.Text = servicesBill.DateServicesBill.ToString();
                servicesUc.txtTotal.Text = servicesBill.Total1.ToString();
                servicesUc.txtPrePay.Text = servicesBill.PrePay.ToString();
                servicesUc.txtRest.Text = servicesBill.Rest1.ToString();
                servicesUc.txtDeliveryDate.Text = servicesBill.DeliveryDate.ToString();

                homeWindow.stkServicesBill.Children.Add(servicesUc);
                i++;
            }
        }

        public List<string> getNameServices()
        {
            List<string> namesServices = new List<string>();
            foreach (Service service in ServicesDAL.Instance.LoadServices())
            {
                namesServices.Add(service.Name);
            }
            return namesServices;
        }

        private void GetWindow(AddServicesBillWindow parameter)
        {
            this.addServicesBillWindow1 = parameter;
        }
        private void GetWindowHome(HomeWindow parameter)
        {
            this.homeWindow = parameter;
        }
        private void OpenAddBillWindow(HomeWindow parameter)
        {
            AddServicesBillWindow addServicesBill = new AddServicesBillWindow();
            IdService = "";
            try
            {
                int id = ServicesDAL.Instance.GetMaxId() + 1;
                addServicesBill.txtIdServicesBill.Text = id.ToString();
                addServicesBill.txtService.ItemsSource = getNameServices();
                ServicesBill servicesBill = new ServicesBill(id, "1", DateTime.Now, 0, 0, 0, DateTime.Now.AddDays(7), 0);
                ServicesBillDAL.Instance.AddIntoDB(servicesBill);
            }
            catch
            {
                ServicesBill servicesBill = new ServicesBill(1, "1", DateTime.Now, 0, 0, 0, DateTime.Now.AddDays(7), 0);
                ServicesBillDAL.Instance.AddIntoDB(servicesBill);
                addServicesBill.txtIdServicesBill.Text = "1";
            }
            addServicesBill.tbNgaygiao.Text = DateTime.Now.AddDays(7).ToString("dd/MM/yyyy");
            addServicesBill.txtDate.Text = DateTime.Now.ToString("dd/MM/yyyy");
            addServicesBill.ShowDialog();
        }

        private void ExitServicesBillWindow(AddServicesBillWindow parameter)
        {
            if (!new StackTrace().GetFrames().Any(x => x.GetMethod().Name == "Close"))
            {
                ServicesBillDAL.Instance.RemoveServicesDetailFromDatabase(int.Parse(parameter.txtIdServicesBill.Text));
                ServicesBillDAL.Instance.RemoveServicesBillFromDatabase(int.Parse(parameter.txtIdServicesBill.Text));
            }
        }

        private void ExitServicesBillButtonWindow(AddServicesBillWindow parameter)
        {
            ServicesBillDAL.Instance.RemoveServicesDetailFromDatabase(int.Parse(parameter.txtIdServicesBill.Text));
            ServicesBillDAL.Instance.RemoveServicesBillFromDatabase(int.Parse(parameter.txtIdServicesBill.Text));
            parameter.Close();
        }

        public void GetCustomer(AddServicesBillWindow parameter)
        {
            //AddServicesBillWindow addServicesBill = new AddServicesBillWindow();
            //string sdt = addServicesBill.tbSDT.Text;
            //Customer customer = ServicesBillDAL.Instance.GetCustomer(sdt);
            //addServicesBill.tbNameCustomer.Text = customer.Name;
            Customer customer = ServicesBillDAL.Instance.GetCustomer(parameter.tbSDT.Text);
            NameCustomer = customer.Name;
        }

        public void SaveServicesBill(AddServicesBillWindow parameter)
        {
            Regex regex = new Regex(@"^[0-9]+$");
            if (total != 0 && parameter.tbprePay.Text != "" && regex.IsMatch(parameter.tbSDT.Text))
            {     
                if(long.Parse(parameter.tbprePay.Text) < ToTal / 2)
                {
                    MessageBox.Show("Số tiền trả trước chưa đạt yêu cầu tối thiểu");
                }    
                else
                {
                    int mapdv = ServicesDAL.Instance.GetMaxId();
                    string makh = ServicesBillDAL.Instance.GetCustomer(parameter.tbSDT.Text).IdCustomer.ToString();
                    DateTime DayCreate = DateTime.Now;
                    long total = long.Parse(parameter.tbTotal.Text);
                    long prepay = long.Parse(parameter.tbprePay.Text);
                    long rest = total - prepay;
                    DateTime ngaygiao = DateTime.Now.AddDays(7);

                    ServicesBill servicesBill = new ServicesBill(mapdv, makh, DayCreate, total, prepay, rest, ngaygiao, 0);
                    ServicesBillDAL.Instance.UpdateIntoDB(servicesBill);

                    parameter.Close();
                    Notification.Instance.Success("Thêm thông tin thành công");
                    LoadServicesBill(this.homeWindow);
                }    
                
            }
            else
            {
                MessageBox.Show("Vui lòng kiểm tra thông tin");
            }    

        }

        private void AddServicesToBill(AddServicesBillWindow parameter)
        {
            if (!String.IsNullOrEmpty(parameter.tbNameCustomer.Text) && !String.IsNullOrEmpty(parameter.txtService.Text) && !String.IsNullOrEmpty(parameter.tbSoLuong.Text))
            {
                bool isExisted = false;
                List<ServicesBillDetail> servicesBillDetails = ServicesBillDAL.Instance.GetServicesBillDetailLById(int.Parse(parameter.txtIdServicesBill.Text));
                foreach (var servicesBillDetail in servicesBillDetails)
                {
                    if (servicesBillDetail.MaDV1.ToString() == parameter.tbIdService.Text)
                    {
                        isExisted = true;
                        MessageBox.Show("Đã thêm dịch vụ vào phiểu");
                        return;
                    }
                }
                if (!isExisted)
                {
                    int x = int.Parse(parameter.tbSoLuong.Text);
                    long y = long.Parse(parameter.tbPrice.Text);
                    int z = 0;
                    ServicesBillDetail servicesBillDetail = new ServicesBillDetail(
                        ServicesBillDAL.Instance.GetMaxIdServicesBillDetails() + 1,
                        int.Parse(parameter.txtIdServicesBill.Text),
                        int.Parse(parameter.tbIdService.Text),
                        x,
                        y,
                        z);
                    ServicesBillDAL.Instance.AddServicesBillDetailToDatabase(servicesBillDetail);
                    ToTal += y;
                    //Notification.Instance.Success("Thêm thành công");
                    LoadServicesBillDetail(addServicesBillWindow1);
                }
            }
            else
            {
                MessageBox.Show("Vui lòng nhập đầy đủ thông tin");
            }
        }
        public void LoadServicesBillDetail(AddServicesBillWindow addServicesBillWindow)
        {
            int i = 0;
            addServicesBillWindow.stkServicesBill.Children.Clear();
            List<ServicesBillDetail> servicesBillDetails = ServicesBillDAL.Instance.GetServicesBillDetailLById(int.Parse(addServicesBillWindow.txtIdServicesBill.Text));
            foreach (var servicesBillDetail in servicesBillDetails)
            {
                ServicesBillDetailUc servicesBill = new ServicesBillDetailUc();
                servicesBill.tbID.Text = i.ToString();
                servicesBill.tbName.Text = ServicesBillDAL.Instance.GetNameService(servicesBillDetail.MaDV1);
                servicesBill.tbSoLuong.Text = servicesBillDetail.SoLuong1.ToString();
                servicesBill.tbPrice.Text = servicesBillDetail.DonGiaDuocTinh1.ToString();
                servicesBill.tbStatus.Text = "Chưa hoàn thành";

                servicesBill.idHidden.Text = servicesBillDetail.MaCTPDV1.ToString();
                addServicesBillWindow.stkServicesBill.Children.Add(servicesBill);
                i++;
            }
        }
        private void GetIdServices(AddServicesBillWindow parameter)
        {
            IdService = ServicesDAL.Instance.GetIdServices(parameter.txtService.SelectedItem.ToString()).ToString();
        }
        private void GetDonGia(AddServicesBillWindow parameter)
        {
            string tempt = ServicesDAL.Instance.GetDonGia(parameter.txtService.SelectedItem.ToString()).ToString();
            int intTempt = int.Parse(tempt) * int.Parse(SoLuong);
            DonGia = intTempt.ToString();
            servicePrice = int.Parse(tempt);
        }
        private void GetSoLuong(AddServicesBillWindow parameter)
        {
            if (parameter.tbSoLuong.Text.ToString() != "") {
                int price = int.Parse(SoLuong) * servicePrice;
                parameter.tbPrice.Text = price.ToString();
            }
        }
        public void DeleteServicesDetail(ServicesBillDetailUc servicesBillDetailUc)
        {
            MessageBoxResult result = MessageBox.Show("Xác nhận xóa dịch vụ?", "Thông báo", MessageBoxButton.YesNo, MessageBoxImage.Question);

            if (result == MessageBoxResult.Yes)
            {
                string idServices = servicesBillDetailUc.idHidden.Text;
                bool isSuccessed = ServicesBillDAL.Instance.DeleteFromDB(idServices);
                if (isSuccessed)
                {
                    ToTal -= long.Parse(servicesBillDetailUc.tbPrice.Text);
                    addServicesBillWindow1.stkServicesBill.Children.Remove(servicesBillDetailUc);
                }
                else
                {
                    MessageBox.Show("Thực hiện thất bại!", "Thông báo", MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }
        }
        public void GetPrePay(AddServicesBillWindow parameter)
        {
            PrePay = "Trả trước tối thiểu là: " + (ToTal / 2).ToString();
        }
        public void DeleteBill(ServicesUc services)
        {
            MessageBoxResult result = MessageBox.Show("Xác nhận xóa phiếu dịch vụ?", "Thông báo", MessageBoxButton.YesNo, MessageBoxImage.Question);

            if (result == MessageBoxResult.Yes)
            {
                string idServices = services.txtId.Text;
                bool isSuccessed = ServicesBillDAL.Instance.DeleteBillFromDB(idServices);
                if (isSuccessed)
                {
                    homeWindow.stkServicesBill.Children.Remove(services);
                }
                else
                {
                    MessageBox.Show("Thực hiện thất bại!", "Thông báo", MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }
        }
        public void DeleteService(StockServicesUC services)
        {
            MessageBoxResult result = MessageBox.Show("Xác nhận xóa dịch vụ?", "Thông báo", MessageBoxButton.YesNo, MessageBoxImage.Question);

            if (result == MessageBoxResult.Yes)
            {
                string idServices = services.txtId.Text;
                bool isSuccessed = ServicesBillDAL.Instance.DeleteServicesFromDB(idServices);
                if (isSuccessed)
                {
                    homeWindow.stkServices.Children.Remove(services);
                    Notification.Instance.Success("Xóa   dịch vụ thành công");
                }
                else
                {
                    MessageBox.Show("Thực hiện thất bại!", "Thông báo", MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }
        }
        private void OpenAddService(HomeWindow parameter)
        {
            AddServiceWindow addServiceWindow = new AddServiceWindow();
            try
            {
                int id = ServicesDAL.Instance.GetMaxIdService() + 1;
                addServiceWindow.txtIdService.Text = id.ToString();
            }
            catch
            {
                addServiceWindow.txtIdService.Text = "1";
            }
            addServiceWindow.ShowDialog();
        }
        private void CloseAddService(AddServiceWindow parameter)
        {
            parameter.Close();
        }
        public void SaveServices(AddServiceWindow parameter)
        {
            Regex regex = new Regex(@"^[0-9]+$");
            if ( regex.IsMatch(parameter.txtPrice.Text) && parameter.txtService.Text != "")
            {
                int idService = int.Parse(parameter.txtIdService.Text);
                string service = parameter.txtService.Text;
                long price = long.Parse(parameter.txtPrice.Text);
                Service services = new Service(idService, service, price, 0);
                ServicesBillDAL.Instance.AddServiceToDB(services);

                parameter.Close();
                Notification.Instance.Success("Thêm dịch vụ thành công");
                LoadServices(this.homeWindow);

            }
            else
            {
                MessageBox.Show("Vui lòng kiểm tra thông tin");
            }

        }
        public void UpdateServices(EditServiceWindow parameter)
        {
            Regex regex = new Regex(@"^[0-9]+$");
            if (regex.IsMatch(parameter.txtPrice.Text) && parameter.txtService.Text != "")
            {
                int idService = int.Parse(parameter.txtIdService.Text);
                string service = parameter.txtService.Text;
                long price = long.Parse(parameter.txtPrice.Text);
                Service services = new Service(idService, service, price, 0);
                ServicesBillDAL.Instance.UpdateServiceToDB(services);

                parameter.Close();
                Notification.Instance.Success("Sửa dịch vụ thành công");
                LoadServices(this.homeWindow);

            }
            else
            {
                MessageBox.Show("Vui lòng kiểm tra thông tin");
            }

        }
        private void OpenEditService(StockServicesUC parameter)
        {
            EditServiceWindow editServiceWindow = new EditServiceWindow();
            try
            {
                int id = int.Parse(parameter.txtId.Text);
                editServiceWindow.txtIdService.Text = id.ToString();
                editServiceWindow.txtService.Text = parameter.txtName.Text;
                editServiceWindow.txtPrice.Text = parameter.txtPrice.Text;
            }
            catch
            {
                editServiceWindow.txtIdService.Text = "1";
            }
            editServiceWindow.ShowDialog();
        }
    }
}
