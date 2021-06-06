using Phan_Mem_Ke_Toan.API;
using Phan_Mem_Ke_Toan.Model;
using Phan_Mem_Ke_Toan.ValidRule;
using Phan_Mem_Ke_Toan.View;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;

namespace Phan_Mem_Ke_Toan.ViewModel
{
    public class VatTuViewModel : BaseViewModel
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
        private string _txtMaVT;
        public string txtMaVT
        {
            get => _txtMaVT;
            set => SetProperty(ref _txtMaVT, value);
        }
        private string _txtTenVT;
        public string txtTenVT
        {
            get => _txtTenVT;
            set => SetProperty(ref _txtTenVT, value);
        }
        private string _selectedMaLoai;
        public string selectedMaLoai
        {
            get => _selectedMaLoai;
            set => SetProperty(ref _selectedMaLoai, value);
        }
        private string _selectedMaDVT;
        public string selectedMaDVT
        {
            get => _selectedMaDVT;
            set => SetProperty(ref _selectedMaDVT, value);
        }

        private string _selectedMaTK;
        public string selectedMaTK
        {
            get => _selectedMaTK;
            set => SetProperty(ref _selectedMaTK, value);
        }

        public ICommand AddCommand { get; set; }
        public ICommand EditCommand { get; set; }
        public ICommand BtnCommand { get; set; }
        public ICommand DeleteItemCommand { get; set; }

        private ObservableCollection<VatTuDetail> _listData;
        public ObservableCollection<VatTuDetail> ListData
        {
            get => _listData;
            set => SetProperty(ref _listData, value);
        }
        private ObservableCollection<LoaiVatTu> _ListLoaiVT;
        public ObservableCollection<LoaiVatTu> ListLoaiVT
        {
            get => _ListLoaiVT;
            set => SetProperty(ref _ListLoaiVT, value);
        }
        private ObservableCollection<DonViTinh> _ListDVT;
        public ObservableCollection<DonViTinh> ListDVT
        {
            get => _ListDVT;
            set => SetProperty(ref _ListDVT, value);
        }
        private ObservableCollection<TaiKhoan> _ListTaiKhoan;
        public ObservableCollection<TaiKhoan> ListTaiKhoan
        {
            get => _ListTaiKhoan;
            set => SetProperty(ref _ListTaiKhoan, value);
        }
        public void ClearTextboxValue()
        {
            txtMaVT = string.Empty;
            txtTenVT = string.Empty;
            selectedMaLoai = string.Empty;
            selectedMaDVT = string.Empty;
            selectedMaTK = string.Empty;
        }
        public VatTuViewModel()
        {
            AddCommand = new RelayCommand<object>((p) => true, (p) =>
            {
                VatTuDialog dialog = new VatTuDialog();
                TitleDialog = "Thêm vật tư";
                BtnContent = "Thêm";
                tbVisibility = "Collapsed";
                ClearTextboxValue();
                dialog.ShowDialog();
            });

            EditCommand = new RelayCommand<object>((p) => true, (p) =>
            {
                VatTuDialog dialog = new VatTuDialog();
                TitleDialog = "Cập nhật vật tư";
                BtnContent = "Lưu";
                tbVisibility = "Visible";
                var itemData = p as VatTuDetail;
                txtMaVT = itemData.MaVT;
                txtTenVT = itemData.TenVT;
                selectedMaLoai = itemData.MaLoai;
                selectedMaDVT = itemData.MaDVT;
                selectedMaTK = itemData.MaTK;
                dialog.ShowDialog();
            });
            BtnCommand = new RelayCommand<object>((p) =>
            {
                return Valid.IsValid(p as DependencyObject);
            }, (p) =>
            {
                if (BtnContent == "Thêm")
                {
                    VatTu vt = new VatTu
                    {
                        MaVT = ListData.Count() == 0 ? "VT001" : CRUD.GeneratePrimaryKey(ListData[ListData.Count() - 1].MaVT),
                        TenVT = txtTenVT,
                        MaLoai = selectedMaLoai == "" ? null : selectedMaLoai ,
                        MaDVT = selectedMaDVT == "" ? null : selectedMaDVT,
                        MaTK = selectedMaTK == "" ? null : selectedMaTK,
                    };
                    if (CRUD.InsertData("VatTu", vt))
                    {
                        MessageBox.Show("Thành công!", "Thông báo", MessageBoxButton.OK, MessageBoxImage.Information);
                        LoadTableData();
                        ClearTextboxValue();
                    }
                    else
                    {
                        MessageBox.Show("Đã có lỗi xảy ra, vui lòng thử lại sau", "Thông báo", MessageBoxButton.OK, MessageBoxImage.Error);
                    }
                }
                else
                {
                    VatTu vt = new VatTu
                    {
                        MaVT = txtMaVT,
                        TenVT = txtTenVT,
                        MaLoai = selectedMaLoai,
                        MaDVT = selectedMaDVT,
                        MaTK = selectedMaTK,
                    };
                    if (CRUD.UpdateData("VatTu", vt))
                    {
                        MessageBox.Show("Thành công!", "Thông báo", MessageBoxButton.OK, MessageBoxImage.Information);
                        LoadTableData();
                    }
                    else
                    {
                        MessageBox.Show("Đã có lỗi xảy ra, vui lòng thử lại sau", "Thông báo", MessageBoxButton.OK, MessageBoxImage.Error);
                    }
                    ((Window)p).Close();
                }

            });
            DeleteItemCommand = new RelayCommand<object>((p) => true, (p) =>
            {
                var itemData = p as VatTuDetail;
                if (CRUD.DeleteData("VatTu", itemData.MaVT))
                {
                    MessageBox.Show("Thành công!", "Thông báo", MessageBoxButton.OK, MessageBoxImage.Information);
                    LoadTableData();
                }
                else
                {
                    MessageBox.Show("Đã có lỗi xảy ra, vui lòng thử lại sau", "Thông báo", MessageBoxButton.OK, MessageBoxImage.Error);
                }

            });
            GetListLoaiVT();
            GetListDVT();
            GetListTK();
            LoadTableData();
        }
        public void LoadTableData()
        {
            string JsonData = CRUD.GetJoinTableData("VatTu");
            ListData = JsonConvert.DeserializeObject<ObservableCollection<VatTuDetail>>(JsonData);
        }
        public void GetListLoaiVT()
        {
            string data = CRUD.GetJsonData("LoaiVatTu");
            ListLoaiVT = JsonConvert.DeserializeObject<ObservableCollection<LoaiVatTu>>(data);
        }
        public void GetListDVT()
        {
            string data = CRUD.GetJsonData("DonViTinh");
            ListDVT = JsonConvert.DeserializeObject<ObservableCollection<DonViTinh>>(data);
        }
        public void GetListTK()
        {
            string data = CRUD.GetJsonData("TaiKhoan");
            ListTaiKhoan = JsonConvert.DeserializeObject<ObservableCollection<TaiKhoan>>(data);
            foreach(var item in ListTaiKhoan)
            {
                item.TenTK = item.MaTK + " - " + item.TenTK;
            }
        }
    }
}
