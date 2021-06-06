using Phan_Mem_Ke_Toan.API;
using Phan_Mem_Ke_Toan.Model;
using Phan_Mem_Ke_Toan.View;
using Phan_Mem_Ke_Toan.ValidRule;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;

namespace Phan_Mem_Ke_Toan.ViewModel
{
    public class NhaCungCapViewModel : BaseViewModel
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

        private string _txtMaNCC;
        public string txtMaNCC
        {
            get => _txtMaNCC;
            set => SetProperty(ref _txtMaNCC, value);
        }

        private string _txtTenNCC;
        public string txtTenNCC
        {
            get => _txtTenNCC;
            set => SetProperty(ref _txtTenNCC, value);
        }
        private string _txtDiaChi;
        public string txtDiaChi
        {
            get => _txtDiaChi;
            set => SetProperty(ref _txtDiaChi, value);
        }
        private string _txtMaSoThue;
        public string txtMaSoThue
        {
            get => _txtMaSoThue;
            set => SetProperty(ref _txtMaSoThue, value);
        }
        private string _txtSDT;
        public string txtSDT
        {
            get => _txtSDT;
            set => SetProperty(ref _txtSDT, value);
        }
        private string _tbVisibility;
        public string tbVisibility
        {
            get => _tbVisibility;
            set => SetProperty(ref _tbVisibility, value);
        }

        public ICommand AddCommand { get; set; }
        public ICommand EditCommand { get; set; }
        public ICommand BtnCommand { get; set; }
        public ICommand DeleteItemCommand { get; set; }

        private ObservableCollection<NhaCungCap> _listData;
        public ObservableCollection<NhaCungCap> ListData
        {
            get => _listData;
            set => SetProperty(ref _listData, value);
        }
        public void ClearTextboxValue()
        {
            txtMaNCC = string.Empty;
            txtTenNCC = string.Empty;
            txtDiaChi = string.Empty;
            txtMaSoThue = string.Empty;
            txtSDT = string.Empty;
        }
        public NhaCungCapViewModel()
        {
            AddCommand = new RelayCommand<object>((p) => true, (p) =>
            {
                NhaCungCapDialog dialog = new NhaCungCapDialog();
                TitleDialog = "Thêm nhà cung cấp";
                BtnContent = "Thêm";
                tbVisibility = "Collapsed";
                ClearTextboxValue();
                dialog.ShowDialog();
            });

            EditCommand = new RelayCommand<object>((p) => true, (p) =>
            {
                NhaCungCapDialog dialog = new NhaCungCapDialog();
                TitleDialog = "Cập nhật nhà cung cấp";
                BtnContent = "Lưu";
                tbVisibility = "Visible";
                var itemData = p as NhaCungCap;
                txtMaNCC = itemData.MaNCC;
                txtTenNCC = itemData.TenNCC;
                txtDiaChi = itemData.DiaChi;
                txtMaSoThue = itemData.MaSoThue;
                txtSDT = itemData.SDT;
                dialog.ShowDialog();
            });
            BtnCommand = new RelayCommand<object>((p) =>
            {
                bool checkMaSoThue = string.IsNullOrEmpty(txtMaSoThue) ? true : Valid.isMaSoThue(txtMaSoThue);
                bool checkSDT = string.IsNullOrEmpty(txtSDT) ? true : Valid.isPhoneNumber(txtSDT);
                return Valid.IsValid(p as DependencyObject) && checkMaSoThue && checkSDT;
            }, (p) =>
            {
                if (BtnContent == "Thêm")
                {
                    NhaCungCap ncc = new NhaCungCap
                    {
                        MaNCC = ListData.Count() == 0 ? "NCC001" : CRUD.GeneratePrimaryKey(ListData[ListData.Count() - 1].MaNCC),
                        TenNCC = txtTenNCC,
                        DiaChi = txtDiaChi,
                        MaSoThue = txtMaSoThue,
                        SDT = txtSDT,
                    };
                    if (CRUD.InsertData("NhaCungCap", ncc))
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
                    NhaCungCap ncc = new NhaCungCap
                    {
                        MaNCC = txtMaNCC,
                        TenNCC = txtTenNCC,
                        DiaChi = txtDiaChi,
                        MaSoThue = txtMaSoThue,
                        SDT = txtSDT,
                    };
                    if (CRUD.UpdateData("NhaCungCap", ncc))
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
                var itemData = p as NhaCungCap;
                if (CRUD.DeleteData("NhaCungCap", itemData.MaNCC))
                {
                    MessageBox.Show("Thành công!", "Thông báo", MessageBoxButton.OK, MessageBoxImage.Information);
                    LoadTableData();
                }
                else
                {
                    MessageBox.Show("Đã có lỗi xảy ra, vui lòng thử lại sau", "Thông báo", MessageBoxButton.OK, MessageBoxImage.Error);
                }

            });
            LoadTableData();
        }
        public void LoadTableData()
        {
            string JsonData = CRUD.GetJsonData("NhaCungCap");
            ListData = JsonConvert.DeserializeObject<ObservableCollection<NhaCungCap>>(JsonData);
        }
    }
}
