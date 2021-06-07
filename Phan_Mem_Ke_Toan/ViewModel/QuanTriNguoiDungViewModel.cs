using Newtonsoft.Json;
using Phan_Mem_Ke_Toan.API;
using Phan_Mem_Ke_Toan.Model;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace Phan_Mem_Ke_Toan.ViewModel
{
    class QuanTriNguoiDungViewModel : BaseViewModel
    {
        private string _titleDialog;
        public string TitleDialog
        {
            get => _titleDialog;
            set => SetProperty(ref _titleDialog, value);
        }

        private string _btnContent;
        public string BtnContent
        {
            get => _btnContent;
            set => SetProperty(ref _btnContent, value);
        }

        private string _tbVisibility;
        public string tbVisibility
        {
            get => _tbVisibility;
            set => SetProperty(ref _tbVisibility, value);
        }
        private string _txtMaKho;
        public string txtMaKho
        {
            get => _txtMaKho;
            set => SetProperty(ref _txtMaKho, value);
        }
        private string _txtTenKho;
        public string txtTenKho
        {
            get => _txtTenKho;
            set => SetProperty(ref _txtTenKho, value);
        }
        private string _txtDiaChi;
        public string txtDiaChi
        {
            get => _txtDiaChi;
            set => SetProperty(ref _txtDiaChi, value);
        }
        private string _txtSDT;
        public string txtSDT
        {
            get => _txtSDT;
            set => SetProperty(ref _txtSDT, value);
        }

        private string _selectedMaNV;
        public string selectedMaNV
        {
            get => _selectedMaNV;
            set => SetProperty(ref _selectedMaNV, value);
        }

        public ICommand AddCommand { get; set; }
        public ICommand EditCommand { get; set; }
        public ICommand BtnCommand { get; set; }
        public ICommand DeleteItemCommand { get; set; }

        public ICommand LoadedCommand { get; set; }

        private ObservableCollection<AccountSystem> _listData;
        public ObservableCollection<AccountSystem> ListData
        {
            get => _listData;
            set => SetProperty(ref _listData, value);
        }
        public void ClearTextboxValue()
        {
            txtMaKho = string.Empty;
            txtTenKho = string.Empty;
            txtDiaChi = string.Empty;
            txtSDT = string.Empty;
            selectedMaNV = string.Empty;
        }
        public QuanTriNguoiDungViewModel()
        {
            Event();
            //GetListThuKho();
        }

        private void Event()
        {
            LoadedCommand = new RelayCommand<object>((p) => true, (p) =>
            {
                LoadTableData();
            });

            AddCommand = new RelayCommand<object>((p) => true, (p) =>
            {
                //KhoDialog dialog = new KhoDialog();
                //TitleDialog = "Thêm kho";
                //BtnContent = "Thêm";
                //tbVisibility = "Collapsed";
                //ClearTextboxValue();
                //dialog.ShowDialog();
            });

            EditCommand = new RelayCommand<object>((p) => true, (p) =>
            {
                //KhoDialog dialog = new KhoDialog();
                //TitleDialog = "Cập nhật kho";
                //BtnContent = "Lưu";
                //tbVisibility = "Visible";
                //var itemData = p as KhoDetail;
                //txtMaKho = itemData.MaKho;
                //txtTenKho = itemData.TenKho;
                //txtDiaChi = itemData.DiaChi;
                //txtSDT = itemData.SDT;
                //selectedMaNV = itemData.MaThuKho;
                //dialog.ShowDialog();
            });
            BtnCommand = new RelayCommand<object>((p) =>
            {
                //bool checkSDT = string.IsNullOrEmpty(txtSDT) ? true : Valid.isPhoneNumber(txtSDT);
                //return Valid.IsValid(p as DependencyObject) && checkSDT;
                return true;
            }, (p) =>
            {
                //if (BtnContent == "Thêm")
                //{
                //    Kho k = new Kho
                //    {
                //        MaKho = ListData.Count() == 0 ? "K001" : CRUD.GeneratePrimaryKey(ListData[ListData.Count() - 1].MaKho),
                //        TenKho = txtTenKho,
                //        DiaChi = txtDiaChi,
                //        SDT = txtSDT,
                //        MaThuKho = selectedMaNV == "" ? null : selectedMaNV,
                //    };
                //    if (CRUD.InsertData("Kho", k))
                //    {
                //        MessageBox.Show("Thành công!", "Thông báo", MessageBoxButton.OK, MessageBoxImage.Information);
                //        LoadTableData();
                //        ClearTextboxValue();
                //    }
                //    else
                //    {
                //        MessageBox.Show("Đã có lỗi xảy ra, vui lòng thử lại sau", "Thông báo", MessageBoxButton.OK, MessageBoxImage.Error);
                //    }
                //}
                //else
                //{
                //    Kho k = new Kho
                //    {
                //        MaKho = txtMaKho,
                //        TenKho = txtTenKho,
                //        DiaChi = txtDiaChi,
                //        SDT = txtSDT,
                //        MaThuKho = selectedMaNV == "" ? null : selectedMaNV,
                //    };
                //    if (CRUD.UpdateData("Kho", k))
                //    {
                //        MessageBox.Show("Thành công!", "Thông báo", MessageBoxButton.OK, MessageBoxImage.Information);
                //        LoadTableData();
                //    }
                //    else
                //    {
                //        MessageBox.Show("Đã có lỗi xảy ra, vui lòng thử lại sau", "Thông báo", MessageBoxButton.OK, MessageBoxImage.Error);
                //    }
                //    ((Window)p).Close();
                //}

            });
            DeleteItemCommand = new RelayCommand<object>((p) => true, (p) =>
            {
                //var itemData = p as KhoDetail;
                //if (CRUD.DeleteData("Kho", itemData.MaKho))
                //{
                //    MessageBox.Show("Thành công!", "Thông báo", MessageBoxButton.OK, MessageBoxImage.Information);
                //    LoadTableData();
                //}
                //else
                //{
                //    MessageBox.Show("Đã có lỗi xảy ra, vui lòng thử lại sau", "Thông báo", MessageBoxButton.OK, MessageBoxImage.Error);
                //}

            });
        }
        public void LoadTableData()
        {
            string JsonData = CRUD.GetJsonData("nguoidung");
            ListData = JsonConvert.DeserializeObject<ObservableCollection<AccountSystem>>(JsonData);
        }
        public void GetListThuKho()
        {
            //string data = CRUD.GetJoinTableData("NhanVien");
            //var list = JsonConvert.DeserializeObject<ObservableCollection<NhanVienDetail>>(data);
            //ListThuKho = new ObservableCollection<NhanVienDetail>(list.Where(item => item.TenBoPhan == "Kế toán vật tư").ToList());
            //foreach (var item in ListThuKho)
            //{
            //    item.TenNV = item.MaNV + " - " + item.TenNV;
            //}

        }
    }
}
