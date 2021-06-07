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
    public class PhieuNhapViewModel : BaseViewModel
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
        #region PhieuNhapDialog
        private string _txtSoPhieu;
        public string txtSoPhieu
        {
            get => _txtSoPhieu;
            set => SetProperty(ref _txtSoPhieu, value);
        }
        private DateTime _selectedNgayNhap;
        public DateTime selectedNgayNhap
        {
            get => _selectedNgayNhap;
            set => SetProperty(ref _selectedNgayNhap, value);
        }
        private string _selectedMaNguoiGiao;
        public string selectedMaNguoiGiao
        {
            get => _selectedMaNguoiGiao;
            set
            {
                SetProperty(ref _selectedMaNguoiGiao, value);
                foreach (var item in ListNguoiGiaoDetail)
                {
                    if (item.MaNguoiGiao == value)
                    {
                        selectedMaNCC = item.MaNCC;
                        txtTenNCC = item.TenNCC;
                    }
                }

            }
        }
        private string _selectedMaNCC;
        public string selectedMaNCC
        {
            get => _selectedMaNCC;
            set => SetProperty(ref _selectedMaNCC, value);
        }
        private string _txtTenNCC;
        public string txtTenNCC
        {
            get => _txtTenNCC;
            set => SetProperty(ref _txtTenNCC, value);
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
        private ObservableCollection<NguoiGiaoDetail> _ListNguoiGiaoDetail;
        public ObservableCollection<NguoiGiaoDetail> ListNguoiGiaoDetail
        {
            get => _ListNguoiGiaoDetail;
            set => SetProperty(ref _ListNguoiGiaoDetail, value);
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
        private PhieuNhapDetail _selectedPhieuNhap;
        public PhieuNhapDetail selectedPhieuNhap
        {
            get => _selectedPhieuNhap;
            set
            {
                SetProperty(ref _selectedPhieuNhap, value);
                if (selectedPhieuNhap != null)
                {
                    GetListCT(selectedPhieuNhap.SoPhieu);
                } else
                {
                    ListDataCT = null;
                }
            }
        }
        public ICommand AddCommand { get; set; }
        public ICommand EditCommand { get; set; }
        public ICommand BtnCommand { get; set; }
        public ICommand DeleteItemCommand { get; set; }

        private ObservableCollection<PhieuNhapDetail> _listData;
        public ObservableCollection<PhieuNhapDetail> ListData
        {
            get => _listData;
            set => SetProperty(ref _listData, value);
        }
        public void ClearTextboxValue()
        {
            txtSoPhieu = string.Empty;
            selectedMaNguoiGiao = string.Empty;
            selectedMaKho = string.Empty;
            selectedMaNCC = string.Empty;
            selectedNgayNhap = DateTime.Now;
            txtTenNCC = string.Empty;
            txtChungTuLQ = string.Empty;
            txtLyDo = string.Empty;
            selectedTKNo = string.Empty;
            selectedTKCo = string.Empty;
            txtTongTien = 0;
        }
        public void InitPhieuNhapCommand()
        {
            AddCommand = new RelayCommand<object>((p) => true, (p) =>
            {
                PhieuNhapDialog dialog = new PhieuNhapDialog();
                TitleDialog = "Thêm phiếu nhập";
                BtnContent = "Thêm";
                ClearTextboxValue();
                dialog.ShowDialog();
            });

            EditCommand = new RelayCommand<object>((p) => true, (p) =>
            {
                PhieuNhapDialog dialog = new PhieuNhapDialog();
                TitleDialog = "Cập nhật phiếu nhập";
                BtnContent = "Lưu";
                var itemData = p as PhieuNhapDetail;
                txtSoPhieu = itemData.SoPhieu;
                selectedNgayNhap = itemData.NgayNhap;
                selectedMaNguoiGiao = itemData.MaNguoiGiao;
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
                    PhieuNhap pn = new PhieuNhap
                    {
                        SoPhieu = ListData.Count() == 0 ? "PN001" : CRUD.GeneratePrimaryKey(ListData[ListData.Count() - 1].SoPhieu),
                        NgayNhap = selectedNgayNhap.Date,
                        MaNguoiGiao = selectedMaNguoiGiao == "" ? null : selectedMaNguoiGiao,
                        MaNCC = selectedMaNCC == "" ? null : selectedMaNCC,
                        MaKho = selectedMaKho == "" ? null : selectedMaKho,
                        LyDo = txtLyDo,
                        TKNo = selectedTKNo == "" ? null : selectedTKNo,
                        TKCo = selectedTKCo == "" ? null : selectedTKCo,
                        TongTien = txtTongTien,
                        ChungTuLQ = txtChungTuLQ,
                    };
                    if (CRUD.InsertData("PhieuNhap", pn))
                    {
                        Console.WriteLine("Success");
                        LoadTableData();
                        ClearTextboxValue();
                    }
                    else
                    {
                        Console.WriteLine("Error");
                    }
                }
                else
                {
                    PhieuNhap pn = new PhieuNhap
                    {
                        SoPhieu = txtSoPhieu,
                        NgayNhap = selectedNgayNhap.Date,
                        MaNguoiGiao = selectedMaNguoiGiao == "" ? null : selectedMaNguoiGiao,
                        MaNCC = selectedMaNCC == "" ? null : selectedMaNCC,
                        MaKho = selectedMaKho == "" ? null : selectedMaKho,
                        LyDo = txtLyDo,
                        TKNo = selectedTKNo == "" ? null : selectedTKNo,
                        TKCo = selectedTKCo == "" ? null : selectedTKCo,
                        TongTien = txtTongTien,
                        ChungTuLQ = txtChungTuLQ,
                    };
                    if (CRUD.UpdateData("PhieuNhap", pn))
                    {
                        Console.WriteLine("Success");
                        LoadTableData();
                    }
                    else
                    {
                        Console.WriteLine("Error");
                    }
                    ((Window)p).Close();
                }

            });
            DeleteItemCommand = new RelayCommand<object>((p) => true, (p) =>
            {
                var itemData = p as PhieuNhapDetail;
                if (CRUD.DeleteData("PhieuNhap", itemData.SoPhieu))
                {
                    Console.WriteLine("Success");
                    LoadTableData();
                }

            });
        }
        #endregion

        #region CTPhieuNhapDialog
        private ObservableCollection<CT_PhieuNhapDetail> _listDataCT;
        public ObservableCollection<CT_PhieuNhapDetail> ListDataCT
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
                    foreach(var item in ListVT) 
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
        public void InitCTPhieuNhapCommand()
        {
            AddCommandCT = new RelayCommand<object>((p) => selectedPhieuNhap != null, (p) =>
            {
                GetListVatTu();
                CT_PhieuNhapDialog dialog = new CT_PhieuNhapDialog();
                TitleDialogCT = "Thêm chi tiết phiếu nhập";
                txtSoPhieuCT = selectedPhieuNhap.SoPhieu;
                BtnContent = "Thêm";
                EnableListVT = true;
                ClearTextboxValueCT();
                RemoveExistedVatTu();
                dialog.ShowDialog();
            });

            EditCommandCT = new RelayCommand<object>((p) => true, (p) =>
            {
                GetListVatTu();
                CT_PhieuNhapDialog dialog = new CT_PhieuNhapDialog();
                TitleDialogCT = "Cập nhật chi tiết phiếu nhập";
                BtnContent = "Lưu";
                var itemData = p as CT_PhieuNhapDetail;
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
                    CT_PhieuNhap ctpn = new CT_PhieuNhap
                    {
                        SoPhieu = txtSoPhieuCT,
                        MaVT = selectedMaVT,
                        SoLuong = txtSoLuong,
                        DonGia = txtDonGia,
                        ThanhTien = txtThanhTien,
                    };
                    if (CRUD.InsertData("CT_PhieuNhap", ctpn))
                    {
                        Console.WriteLine("Success");
                        GetListCT(txtSoPhieuCT);
                        ClearTextboxValueCT();
                        RemoveExistedVatTu();
                    }
                    else
                    {
                        Console.WriteLine("Error");
                    }
                }
                else
                {
                    CT_PhieuNhap ctpn = new CT_PhieuNhap
                    {
                        MaSo = MaSo,
                        SoPhieu = txtSoPhieuCT,
                        MaVT = selectedMaVT,
                        SoLuong = txtSoLuong,
                        DonGia = txtDonGia,
                        ThanhTien = txtThanhTien,
                    };
                    if (CRUD.UpdateData("CT_PhieuNhap", ctpn))
                    {
                        Console.WriteLine("Success");
                        GetListCT(txtSoPhieuCT);
                    }
                    else
                    {
                        Console.WriteLine("Error");
                    }
                    ((Window)p).Close();
                }
                UpdateTongTienPN();
            });
            DeleteItemCommandCT = new RelayCommand<object>((p) => true, (p) =>
            {
                var itemData = p as CT_PhieuNhapDetail;
                if (CRUD.DeleteData("CT_PhieuNhap", itemData.MaSo.ToString()))
                {
                    Console.WriteLine("Success");
                    GetListCT(selectedPhieuNhap.SoPhieu);
                }
                UpdateTongTienPN();

            });
        }
        #endregion
        public PhieuNhapViewModel()
        {
            InitPhieuNhapCommand();
            InitCTPhieuNhapCommand();
            GetListNguoiGiao();
            GetListKho();
            GetListVatTu();
            GetListTaiKhoan();
            LoadTableData();
        }
        public void LoadTableData()
        {
            string JsonData = CRUD.GetJoinTableData("PhieuNhap");
            ListData = JsonConvert.DeserializeObject<ObservableCollection<PhieuNhapDetail>>(JsonData);
        }
        public void GetListNguoiGiao()
        {
            string data = CRUD.GetJoinTableData("NguoiGiao");
            ListNguoiGiaoDetail = JsonConvert.DeserializeObject<ObservableCollection<NguoiGiaoDetail>>(data);
            foreach (var item in ListNguoiGiaoDetail)
            {
                item.TenNguoiGiao = item.MaNguoiGiao + " - " + item.TenNguoiGiao;
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
            string data = CRUD.GetDataByColumnName("CT_PhieuNhap", SoPhieu);
            ListDataCT = JsonConvert.DeserializeObject<ObservableCollection<CT_PhieuNhapDetail>>(data);
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
        public void UpdateTongTienPN()
        {
            decimal Tong = 0;
            foreach(var item in ListDataCT)
            {
                Tong += item.ThanhTien;
            }
            selectedPhieuNhap.TongTien = Tong;
            CRUD.UpdateTongTien("phieunhap", selectedPhieuNhap.SoPhieu, selectedPhieuNhap);
            var SoPhieu = selectedPhieuNhap.SoPhieu;
            LoadTableData();
            foreach (var item in ListData)
                if (item.SoPhieu == SoPhieu)
                    selectedPhieuNhap = item;
        }
    }
}
