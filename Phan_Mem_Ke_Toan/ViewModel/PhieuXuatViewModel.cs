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
    public class PhieuXuatViewModel : BaseViewModel
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
        #region PhieuXuatDialog
        private string _txtSoPhieu;
        public string txtSoPhieu
        {
            get => _txtSoPhieu;
            set => SetProperty(ref _txtSoPhieu, value);
        }
        private DateTime _selectedNgayXuat;
        public DateTime selectedNgayXuat
        {
            get => _selectedNgayXuat;
            set => SetProperty(ref _selectedNgayXuat, value);
        }
        private string _selectedMaNguoiNhan;
        public string selectedMaNguoiNhan
        {
            get => _selectedMaNguoiNhan;
            set
            {
                SetProperty(ref _selectedMaNguoiNhan, value);
                foreach (var item in ListNguoiNhanDetail)
                {
                    if (item.MaNguoiNhan == value)
                    {
                        selectedMaCongTrinh = item.MaCongTrinh;
                        txtTenCongTrinh = item.TenCongTrinh;
                    }
                }

            }
        }
        private string _selectedMaCongTrinh;
        public string selectedMaCongTrinh
        {
            get => _selectedMaCongTrinh;
            set => SetProperty(ref _selectedMaCongTrinh, value);
        }
        private string _txtTenCongTrinh;
        public string txtTenCongTrinh
        {
            get => _txtTenCongTrinh;
            set => SetProperty(ref _txtTenCongTrinh, value);
        }
        private string _selectedMaKho;
        public string selectedMaKho
        {
            get => _selectedMaKho;
            set => SetProperty(ref _selectedMaKho, value);
        }
        private string _txtChungTuLQ;
        public string txtChungTuLQ
        {
            get => _txtChungTuLQ;
            set => SetProperty(ref _txtChungTuLQ, value);
        }
        private string _txtLyDo;
        public string txtLyDo
        {
            get => _txtLyDo;
            set => SetProperty(ref _txtLyDo, value);
        }
        private string _selectedTKNo;
        public string selectedTKNo
        {
            get => _selectedTKNo;
            set => SetProperty(ref _selectedTKNo, value);
        }
        private string _selectedTKCo;
        public string selectedTKCo
        {
            get => _selectedTKCo;
            set => SetProperty(ref _selectedTKCo, value);
        }
        private decimal _txtTongTien;
        public decimal txtTongTien
        {
            get => _txtTongTien;
            set => SetProperty(ref _txtTongTien, value);
        }
        private ObservableCollection<NguoiNhanDetail> _ListNguoiNhanDetail;
        public ObservableCollection<NguoiNhanDetail> ListNguoiNhanDetail
        {
            get => _ListNguoiNhanDetail;
            set => SetProperty(ref _ListNguoiNhanDetail, value);
        }
        private ObservableCollection<Kho> _ListKho;
        public ObservableCollection<Kho> ListKho
        {
            get => _ListKho;
            set => SetProperty(ref _ListKho, value);
        }
        private ObservableCollection<TaiKhoan> _ListTK;
        public ObservableCollection<TaiKhoan> ListTK
        {
            get => _ListTK;
            set => SetProperty(ref _ListTK, value);
        }
        private PhieuXuatDetail _selectedPhieuXuat;
        public PhieuXuatDetail selectedPhieuXuat
        {
            get => _selectedPhieuXuat;
            set
            {
                SetProperty(ref _selectedPhieuXuat, value);
                if (selectedPhieuXuat != null)
                {
                    GetListCT(selectedPhieuXuat.SoPhieu);
                }
                else
                {
                    ListDataCT = null;
                }
            }
        }
        public ICommand AddCommand { get; set; }
        public ICommand EditCommand { get; set; }
        public ICommand BtnCommand { get; set; }
        public ICommand DeleteItemCommand { get; set; }

        private ObservableCollection<PhieuXuatDetail> _listData;
        public ObservableCollection<PhieuXuatDetail> ListData
        {
            get => _listData;
            set => SetProperty(ref _listData, value);
        }
        public void ClearTextboxValue()
        {
            txtSoPhieu = string.Empty;
            selectedMaNguoiNhan = string.Empty;
            selectedMaKho = string.Empty;
            selectedMaCongTrinh = string.Empty;
            selectedNgayXuat = DateTime.Now;
            txtTenCongTrinh = string.Empty;
            txtChungTuLQ = string.Empty;
            txtLyDo = string.Empty;
            selectedTKNo = string.Empty;
            selectedTKCo = string.Empty;
            txtTongTien = 0;
        }
        public void InitPhieuXuatCommand()
        {
            AddCommand = new RelayCommand<object>((p) => true, (p) =>
            {
                PhieuXuatDialog dialog = new PhieuXuatDialog();
                TitleDialog = "Thêm phiếu xuất";
                BtnContent = "Thêm";
                ClearTextboxValue();
                dialog.ShowDialog();
            });

            EditCommand = new RelayCommand<object>((p) => true, (p) =>
            {
                PhieuXuatDialog dialog = new PhieuXuatDialog();
                TitleDialog = "Cập nhật phiếu xuất";
                BtnContent = "Lưu";
                var itemData = p as PhieuXuatDetail;
                txtSoPhieu = itemData.SoPhieu;
                selectedNgayXuat = itemData.NgayXuat;
                selectedMaNguoiNhan = itemData.MaNguoiNhan;
                selectedMaKho = itemData.MaKho;
                txtChungTuLQ = itemData.ChungTuLQ;
                txtLyDo = itemData.LyDo;
                selectedTKNo = itemData.TKNo;
                selectedTKCo = itemData.TKCo;
                txtTongTien = itemData.TongTien;
                dialog.ShowDialog();
            });
            BtnCommand = new RelayCommand<object>((p) =>
            {
                return Valid.IsValid(p as DependencyObject);
            }, (p) =>
            {
                if (BtnContent == "Thêm")
                {
                    PhieuXuat px = new PhieuXuat
                    {
                        SoPhieu = ListData.Count() == 0 ? "PX001" : CRUD.GeneratePrimaryKey(ListData[ListData.Count() - 1].SoPhieu),
                        NgayXuat = selectedNgayXuat.Date,
                        MaNguoiNhan = selectedMaNguoiNhan == "" ? null : selectedMaNguoiNhan,
                        MaCongTrinh = selectedMaCongTrinh == "" ? null : selectedMaCongTrinh,
                        MaKho = selectedMaKho == "" ? null : selectedMaKho,
                        LyDo = txtLyDo,
                        TKNo = selectedTKNo == "" ? null : selectedTKNo,
                        TKCo = selectedTKCo == "" ? null : selectedTKCo,
                        TongTien = txtTongTien,
                        ChungTuLQ = txtChungTuLQ,
                    };
                    if (CRUD.InsertData("PhieuXuat", px))
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
                    PhieuXuat px = new PhieuXuat
                    {
                        SoPhieu = txtSoPhieu,
                        NgayXuat = selectedNgayXuat.Date,
                        MaNguoiNhan = selectedMaNguoiNhan == "" ? null : selectedMaNguoiNhan,
                        MaCongTrinh = selectedMaCongTrinh == "" ? null : selectedMaCongTrinh,
                        MaKho = selectedMaKho == "" ? null : selectedMaKho,
                        LyDo = txtLyDo,
                        TKNo = selectedTKNo == "" ? null : selectedTKNo,
                        TKCo = selectedTKCo == "" ? null : selectedTKCo,
                        TongTien = txtTongTien,
                        ChungTuLQ = txtChungTuLQ,
                    };
                    if (CRUD.UpdateData("PhieuXuat", px))
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
                var itemData = p as PhieuXuatDetail;
                if (CRUD.DeleteData("PhieuXuat", itemData.SoPhieu))
                {
                    MessageBox.Show("Thành công!", "Thông báo", MessageBoxButton.OK, MessageBoxImage.Information);
                    LoadTableData();
                }
                else
                {
                    MessageBox.Show("Đã có lỗi xảy ra, vui lòng thử lại sau", "Thông báo", MessageBoxButton.OK, MessageBoxImage.Error);
                }

            });
        }
        #endregion

        #region CTPhieuXuatDialog
        private ObservableCollection<CT_PhieuXuatDetail> _listDataCT;
        public ObservableCollection<CT_PhieuXuatDetail> ListDataCT
        {
            get => _listDataCT;
            set => SetProperty(ref _listDataCT, value);
        }
        private ObservableCollection<VatTuDetail> _listVT;
        public ObservableCollection<VatTuDetail> ListVT
        {
            get => _listVT;
            set => SetProperty(ref _listVT, value);
        }
        private int _MaSo;
        public int MaSo
        {
            get => _MaSo;
            set => SetProperty(ref _MaSo, value);
        }
        private string _TitleDialogCT;
        public string TitleDialogCT
        {
            get => _TitleDialogCT;
            set => SetProperty(ref _TitleDialogCT, value);
        }
        private string _selectedMaVT;
        public string selectedMaVT
        {
            get => _selectedMaVT;
            set
            {
                SetProperty(ref _selectedMaVT, value);
                if (!string.IsNullOrEmpty(selectedMaVT))
                {
                    foreach (var item in ListVT)
                        if (item.MaVT == selectedMaVT)
                        {
                            txtTenVT = item.TenVT;
                            txtTenDVT = item.TenDVT;
                        }
                }
            }
        }
        private string _txtSoPhieuCT;
        public string txtSoPhieuCT
        {
            get => _txtSoPhieuCT;
            set => SetProperty(ref _txtSoPhieuCT, value);
        }
        private string _txtTenVT;
        public string txtTenVT
        {
            get => _txtTenVT;
            set => SetProperty(ref _txtTenVT, value);
        }
        private string _txtTenDVT;
        public string txtTenDVT
        {
            get => _txtTenDVT;
            set => SetProperty(ref _txtTenDVT, value);
        }
        private double _txtSoLuong;
        public double txtSoLuong
        {
            get => _txtSoLuong;
            set
            {
                if (value < 0)
                    value *= -1;
                SetProperty(ref _txtSoLuong, value);
                txtThanhTien = (decimal)txtSoLuong * txtDonGia;
            }
        }
        private decimal _txtDonGia;
        public decimal txtDonGia
        {
            get => _txtDonGia;
            set
            {
                if (value < 0)
                    value *= -1;
                SetProperty(ref _txtDonGia, value);
                txtThanhTien = (decimal)txtSoLuong * txtDonGia;
            }
        }
        private decimal _txtThanhTien;
        public decimal txtThanhTien
        {
            get => _txtThanhTien;
            set => SetProperty(ref _txtThanhTien, value);
        }
        private bool _EnableListVT;
        public bool EnableListVT
        {
            get => _EnableListVT;
            set => SetProperty(ref _EnableListVT, value);
        }
        public ICommand AddCommandCT { get; set; }
        public ICommand EditCommandCT { get; set; }
        public ICommand BtnCommandCT { get; set; }
        public ICommand DeleteItemCommandCT { get; set; }
        public void ClearTextboxValueCT()
        {
            selectedMaVT = string.Empty;
            txtTenVT = string.Empty;
            txtTenDVT = string.Empty;
            txtSoLuong = 0;
            txtDonGia = 0;
            txtThanhTien = 0;

        }
        public void InitCTPhieuXuatCommand()
        {
            AddCommandCT = new RelayCommand<object>((p) => selectedPhieuXuat != null, (p) =>
            {
                GetListVatTu();
                CT_PhieuXuatDialog dialog = new CT_PhieuXuatDialog();
                TitleDialogCT = "Thêm chi tiết phiếu nhập";
                txtSoPhieuCT = selectedPhieuXuat.SoPhieu;
                BtnContent = "Thêm";
                EnableListVT = true;
                ClearTextboxValueCT();
                RemoveExistedVatTu();
                dialog.ShowDialog();
            });

            EditCommandCT = new RelayCommand<object>((p) => true, (p) =>
            {
                GetListVatTu();
                CT_PhieuXuatDialog dialog = new CT_PhieuXuatDialog();
                TitleDialogCT = "Cập nhật chi tiết phiếu nhập";
                BtnContent = "Lưu";
                var itemData = p as CT_PhieuXuatDetail;
                MaSo = itemData.MaSo;
                txtSoPhieuCT = itemData.SoPhieu;
                selectedMaVT = itemData.MaVT;
                txtTenVT = itemData.TenVT;
                txtTenDVT = itemData.TenDVT;
                txtSoLuong = itemData.SoLuong;
                txtDonGia = itemData.DonGia;
                txtThanhTien = itemData.ThanhTien;
                EnableListVT = false;
                dialog.ShowDialog();
            });
            BtnCommandCT = new RelayCommand<object>((p) =>
            {
                return Valid.IsValid(p as DependencyObject);
            }, (p) =>
            {

                if (BtnContent == "Thêm")
                {
                    CT_PhieuXuat ctpn = new CT_PhieuXuat
                    {
                        SoPhieu = txtSoPhieuCT,
                        MaVT = selectedMaVT,
                        SoLuong = txtSoLuong,
                        DonGia = txtDonGia,
                        ThanhTien = txtThanhTien,
                    };
                    if (CRUD.InsertData("CT_PhieuXuat", ctpn))
                    {
                        MessageBox.Show("Thành công!", "Thông báo", MessageBoxButton.OK, MessageBoxImage.Information);
                        GetListCT(txtSoPhieuCT);
                        ClearTextboxValueCT();
                        RemoveExistedVatTu();
                    }
                    else
                    {
                        MessageBox.Show("Đã có lỗi xảy ra, vui lòng thử lại sau", "Thông báo", MessageBoxButton.OK, MessageBoxImage.Error);
                    }
                }
                else
                {
                    CT_PhieuXuat ctpn = new CT_PhieuXuat
                    {
                        MaSo = MaSo,
                        SoPhieu = txtSoPhieuCT,
                        MaVT = selectedMaVT,
                        SoLuong = txtSoLuong,
                        DonGia = txtDonGia,
                        ThanhTien = txtThanhTien,
                    };
                    if (CRUD.UpdateData("CT_PhieuXuat", ctpn))
                    {
                        MessageBox.Show("Thành công!", "Thông báo", MessageBoxButton.OK, MessageBoxImage.Information);
                        GetListCT(txtSoPhieuCT);
                    }
                    else
                    {
                        MessageBox.Show("Đã có lỗi xảy ra, vui lòng thử lại sau", "Thông báo", MessageBoxButton.OK, MessageBoxImage.Error);
                    }
                    ((Window)p).Close();
                }
                UpdateTongTienPX();
            });
            DeleteItemCommandCT = new RelayCommand<object>((p) => true, (p) =>
            {
                var itemData = p as CT_PhieuXuatDetail;
                if (CRUD.DeleteData("CT_PhieuXuat", itemData.MaSo.ToString()))
                {
                    MessageBox.Show("Thành công!", "Thông báo", MessageBoxButton.OK, MessageBoxImage.Information);
                    GetListCT(selectedPhieuXuat.SoPhieu);
                }
                else
                {
                    MessageBox.Show("Đã có lỗi xảy ra, vui lòng thử lại sau", "Thông báo", MessageBoxButton.OK, MessageBoxImage.Error);
                }
                UpdateTongTienPX();

            });
        }
        #endregion
        public PhieuXuatViewModel()
        {
            InitPhieuXuatCommand();
            InitCTPhieuXuatCommand();
            GetListNguoiNhan();
            GetListKho();
            GetListVatTu();
            GetListTaiKhoan();
            LoadTableData();
        }
        public void LoadTableData()
        {
            string JsonData = CRUD.GetJoinTableData("PhieuXuat");
            ListData = JsonConvert.DeserializeObject<ObservableCollection<PhieuXuatDetail>>(JsonData);
        }
        public void GetListNguoiNhan()
        {
            string data = CRUD.GetJoinTableData("NguoiNhan");
            ListNguoiNhanDetail = JsonConvert.DeserializeObject<ObservableCollection<NguoiNhanDetail>>(data);
            foreach (var item in ListNguoiNhanDetail)
            {
                item.TenNguoiNhan = item.MaNguoiNhan + " - " + item.TenNguoiNhan;
            }
        }
        public void GetListKho()
        {
            string data = CRUD.GetJsonData("Kho");
            ListKho = JsonConvert.DeserializeObject<ObservableCollection<Kho>>(data);
            foreach (var item in ListKho)
            {
                item.TenKho = item.MaKho + " - " + item.TenKho;
            }
        }
        public void GetListTaiKhoan()
        {
            string data = CRUD.GetJsonData("TaiKhoan");
            ListTK = JsonConvert.DeserializeObject<ObservableCollection<TaiKhoan>>(data);
            foreach (var item in ListTK)
            {
                item.TenTK = item.MaTK + " - " + item.TenTK;
            }
        }

        public void GetListCT(string SoPhieu)
        {
            string data = CRUD.GetDataByColumnName("CT_PhieuXuat", SoPhieu);
            ListDataCT = JsonConvert.DeserializeObject<ObservableCollection<CT_PhieuXuatDetail>>(data);
        }
        public void GetListVatTu()
        {
            string data = CRUD.GetJoinTableData("VatTu");
            ListVT = JsonConvert.DeserializeObject<ObservableCollection<VatTuDetail>>(data);
            foreach (var item in ListVT)
                item.TenVT = item.MaVT + " - " + item.TenVT;
        }
        public void RemoveExistedVatTu()
        {
            foreach (var item in ListVT.ToList())
                foreach (var ctpn in ListDataCT.ToList())
                    if (item.MaVT == ctpn.MaVT)
                        ListVT.Remove(item);
        }
        public void UpdateTongTienPX()
        {
            decimal Tong = 0;
            foreach (var item in ListDataCT)
            {
                Tong += item.ThanhTien;
            }
            selectedPhieuXuat.TongTien = Tong;
            CRUD.UpdateTongTien("phieuxuat", selectedPhieuXuat.SoPhieu, selectedPhieuXuat);
            var SoPhieu = selectedPhieuXuat.SoPhieu;
            LoadTableData();
            foreach (var item in ListData)
                if (item.SoPhieu == SoPhieu)
                    selectedPhieuXuat = item;
        }
    }
}
