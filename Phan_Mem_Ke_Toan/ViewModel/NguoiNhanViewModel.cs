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
    public class NguoiNhanViewModel : BaseViewModel
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
        private string _txtMaNguoiNhan;
        public string txtMaNguoiNhan
        {
            get => _txtMaNguoiNhan;
            set => SetProperty(ref _txtMaNguoiNhan, value);
        }
        private string _txtTenNguoiNhan;
        public string txtTenNguoiNhan
        {
            get => _txtTenNguoiNhan;
            set => SetProperty(ref _txtTenNguoiNhan, value);
        }
        private string _txtDiaChi;
        public string txtDiaChi
        {
            get => _txtDiaChi;
            set => SetProperty(ref _txtDiaChi, value);
        }
        private string _selectedMaCongTrinh;
        public string selectedMaCongTrinh
        {
            get => _selectedMaCongTrinh;
            set => SetProperty(ref _selectedMaCongTrinh, value);
        }

        public ICommand AddCommand { get; set; }
        public ICommand EditCommand { get; set; }
        public ICommand BtnCommand { get; set; }
        public ICommand DeleteItemCommand { get; set; }

        private ObservableCollection<NguoiNhanDetail> _listData;
        public ObservableCollection<NguoiNhanDetail> ListData
        {
            get => _listData;
            set => SetProperty(ref _listData, value);
        }
        private ObservableCollection<CongTrinh> _ListCongTrinh;
        public ObservableCollection<CongTrinh> ListCongTrinh
        {
            get => _ListCongTrinh;
            set => SetProperty(ref _ListCongTrinh, value);
        }
        public void ClearTextboxValue()
        {
            txtMaNguoiNhan = string.Empty;
            txtTenNguoiNhan = string.Empty;
            txtDiaChi = string.Empty;
            selectedMaCongTrinh = string.Empty;
        }
        public NguoiNhanViewModel()
        {
            AddCommand = new RelayCommand<object>((p) => true, (p) =>
            {
                NguoiNhanDialog dialog = new NguoiNhanDialog();
                TitleDialog = "Thêm người nhận";
                BtnContent = "Thêm";
                tbVisibility = "Collapsed";
                ClearTextboxValue();
                dialog.ShowDialog();
            });

            EditCommand = new RelayCommand<object>((p) => true, (p) =>
            {
                NguoiNhanDialog dialog = new NguoiNhanDialog();
                TitleDialog = "Cập nhật người nhận";
                BtnContent = "Lưu";
                tbVisibility = "Visible";
                var itemData = p as NguoiNhanDetail;
                txtMaNguoiNhan = itemData.MaNguoiNhan;
                txtTenNguoiNhan = itemData.TenNguoiNhan;
                txtDiaChi = itemData.DiaChi;
                selectedMaCongTrinh = itemData.MaCongTrinh;
                dialog.ShowDialog();
            });
            BtnCommand = new RelayCommand<object>((p) =>
            {
                return Valid.IsValid(p as DependencyObject);
            }, (p) =>
            {
                if (BtnContent == "Thêm")
                {
                    NguoiNhan nn = new NguoiNhan
                    {
                        MaNguoiNhan = ListData.Count() == 0 ? "NN001" : CRUD.GeneratePrimaryKey(ListData[ListData.Count() - 1].MaNguoiNhan),
                        TenNguoiNhan = txtTenNguoiNhan,
                        DiaChi = txtDiaChi,
                        MaCongTrinh = selectedMaCongTrinh == "" ? null : selectedMaCongTrinh,
                    };
                    if (CRUD.InsertData("NguoiNhan", nn))
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
                    NguoiNhan nn = new NguoiNhan
                    {
                        MaNguoiNhan = txtMaNguoiNhan,
                        TenNguoiNhan = txtTenNguoiNhan,
                        DiaChi = txtDiaChi,
                        MaCongTrinh = selectedMaCongTrinh == "" ? null : selectedMaCongTrinh,
                    };
                    if (CRUD.UpdateData("NguoiNhan", nn))
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
                var itemData = p as NguoiNhanDetail;
                if (CRUD.DeleteData("NguoiNhan", itemData.MaNguoiNhan))
                {
                    MessageBox.Show("Thành công!", "Thông báo", MessageBoxButton.OK, MessageBoxImage.Information);
                    LoadTableData();
                }
                else
                {
                    MessageBox.Show("Đã có lỗi xảy ra, vui lòng thử lại sau", "Thông báo", MessageBoxButton.OK, MessageBoxImage.Error);
                }

            });
            GetListCongTrinh();
            LoadTableData();
        }
        public void LoadTableData()
        {
            string JsonData = CRUD.GetJoinTableData("NguoiNhan");
            ListData = JsonConvert.DeserializeObject<ObservableCollection<NguoiNhanDetail>>(JsonData);
        }
        public void GetListCongTrinh()
        {
            string data = CRUD.GetJsonData("CongTrinh");
            ListCongTrinh = JsonConvert.DeserializeObject<ObservableCollection<CongTrinh>>(data);
        }
    }
}
