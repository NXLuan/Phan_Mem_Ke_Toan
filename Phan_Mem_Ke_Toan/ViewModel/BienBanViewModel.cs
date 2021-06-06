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
    public class BienBanViewModel : BaseViewModel
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
        #region BienBanDialog
        private string _txtSoBienBan;
        public string txtSoBienBan
        {
            get => _txtSoBienBan;
            set => SetProperty(ref _txtSoBienBan, value);
        }
        private DateTime _selectedNgayLap;
        public DateTime selectedNgayLap
        {
            get => _selectedNgayLap;
            set => SetProperty(ref _selectedNgayLap, value);
        }
    
        private string _selectedMaKho;
        public string selectedMaKho
        {
            get => _selectedMaKho;
            set => SetProperty(ref _selectedMaKho, value);
        }
        private string _selectedTruongBan;
        public string selectedTruongBan
        {
            get => _selectedTruongBan;
            set
            {   
                SetProperty(ref _selectedTruongBan, value);
            }
        }
        private string _selectedUyVien1;
        public string selectedUyVien1
        {
            get => _selectedUyVien1;
            set
            {
                SetProperty(ref _selectedUyVien1, value);     
            }
        }

        private string _selectedUyVien2;
        public string selectedUyVien2
        {
            get => _selectedUyVien2;
            set
            {
                SetProperty(ref _selectedUyVien2, value);
            }
        }
        private ObservableCollection<Kho> _ListKho;
        public ObservableCollection<Kho> ListKho
        {
            get => _ListKho;
            set => SetProperty(ref _ListKho, value);
        }
        public ObservableCollection<NhanVienDetail> ListKeToan { get; set; }
        private ObservableCollection<NhanVienDetail> _ListTruongBan;
        public ObservableCollection<NhanVienDetail> ListTruongBan
        {
            get => _ListTruongBan;
            set => SetProperty(ref _ListTruongBan, value);
        
        }
        private ObservableCollection<NhanVienDetail> _ListUyVien1;
        public ObservableCollection<NhanVienDetail> ListUyVien1
        {
            get => _ListUyVien1;
            set => SetProperty(ref _ListUyVien1, value);
        }
        private ObservableCollection<NhanVienDetail> _ListUyVien2;
        public ObservableCollection<NhanVienDetail> ListUyVien2
        {
            get => _ListUyVien2;
            set => SetProperty(ref _ListUyVien2, value);
        }
        private BienBanDetail _selectedBienBan;
        public BienBanDetail selectedBienBan
        {
            get => _selectedBienBan;
            set
            {
                SetProperty(ref _selectedBienBan, value);
                if (selectedBienBan != null)
                {
                    GetListCT(selectedBienBan.SoBienBan);
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

        private ObservableCollection<BienBanDetail> _listData;
        public ObservableCollection<BienBanDetail> ListData
        {
            get => _listData;
            set => SetProperty(ref _listData, value);
        }
        public void ClearTextboxValue()
        {
            txtSoBienBan = string.Empty;
            selectedMaKho = string.Empty;
            selectedNgayLap = DateTime.Now;
            selectedTruongBan = string.Empty;
            selectedUyVien1 = string.Empty;
            selectedUyVien2 = string.Empty;
        }
        public void InitBienBanCommand()
        {
            AddCommand = new RelayCommand<object>((p) => true, (p) =>
            {
                GetListKeToan();
                ListTruongBan = ListUyVien1 = ListUyVien2 = ListKeToan;
                BienBanDialog dialog = new BienBanDialog();
                TitleDialog = "Thêm biên bản kiểm kê";
                BtnContent = "Thêm";
                ClearTextboxValue();
                dialog.ShowDialog();
            });

            EditCommand = new RelayCommand<object>((p) => true, (p) =>
            {
                GetListKeToan();
                ListTruongBan = ListUyVien1 = ListUyVien2 = ListKeToan;
                BienBanDialog dialog = new BienBanDialog();
                TitleDialog = "Cập nhật biên bản kiểm kê";
                BtnContent = "Lưu";
                var itemData = p as BienBanDetail;
                txtSoBienBan = itemData.SoBienBan;
                selectedMaKho = itemData.MaKho;
                selectedNgayLap = itemData.NgayLap;
                selectedTruongBan = itemData.TruongBan;
                selectedUyVien1 = itemData.UyVien1;
                selectedUyVien2 = itemData.UyVien2;   
                dialog.ShowDialog();
            });
            BtnCommand = new RelayCommand<object>((p) =>
            {
                return Valid.IsValid(p as DependencyObject);
            }, (p) =>
            {
                if (BtnContent == "Thêm")
                {
                    BienBan bb = new BienBan
                    {
                        SoBienBan = ListData.Count() == 0 ? "BB001" : CRUD.GeneratePrimaryKey(ListData[ListData.Count() - 1].SoBienBan),
                        NgayLap = selectedNgayLap.Date,
                        MaKho = selectedMaKho == "" ? null : selectedMaKho,
                        TruongBan = selectedTruongBan == "" ? null : selectedTruongBan,
                        UyVien1 = selectedUyVien1 == "" ? null : selectedUyVien1,
                        UyVien2 = selectedUyVien2 == "" ? null : selectedUyVien2,
                    };
                    if (CRUD.InsertData("BBKiemKe", bb))
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
                    BienBan bb = new BienBan
                    {
                        SoBienBan = txtSoBienBan,
                        NgayLap = selectedNgayLap.Date,
                        MaKho = selectedMaKho == "" ? null : selectedMaKho,
                        TruongBan = selectedTruongBan == "" ? null : selectedTruongBan,
                        UyVien1 = selectedUyVien1 == "" ? null : selectedUyVien1,
                        UyVien2 = selectedUyVien2 == "" ? null : selectedUyVien2,
                    };
                    if (CRUD.UpdateData("BBKiemKe", bb))
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
                var itemData = p as BienBanDetail;
                if (CRUD.DeleteData("BBKiemKe", itemData.SoBienBan))
                {
                    MessageBox.Show("Thành công!", "Thông báo", MessageBoxButton.OK, MessageBoxImage.Information);
                    LoadTableData();
                } else
                {
                    MessageBox.Show("Đã có lỗi xảy ra, vui lòng thử lại sau", "Thông báo", MessageBoxButton.OK, MessageBoxImage.Error);
                }

            });
        }
        #endregion

        #region CTBienBanDialog
        private ObservableCollection<CT_BienBanDetail> _listDataCT;
        public ObservableCollection<CT_BienBanDetail> ListDataCT
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
        private string _txtSoBienBanCT;
        public string txtSoBienBanCT
        {
            get => _txtSoBienBanCT;
            set => SetProperty(ref _txtSoBienBanCT, value);
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
        private double _txtSLSoSach;
        public double txtSLSoSach
        {
            get => _txtSLSoSach;
            set 
            {
                if (value < 0)
                    value *= -1;
                SetProperty(ref _txtSLSoSach, value);
                if (txtSLSoSach >= txtSLThucTe)
                {
                    txtSLThieu = txtSLSoSach - txtSLThucTe;
                    txtSLThua = 0;
                } else
                {
                    txtSLThua = txtSLThucTe - txtSLSoSach;
                    txtSLThieu = 0;
                }
            }
        }
        private double _txtSLThucTe;
        public double txtSLThucTe
        {
            get => _txtSLThucTe;
            set
            {
                if (value < 0)
                    value *= -1;
                SetProperty(ref _txtSLThucTe, value);
                if (txtSLSoSach >= txtSLThucTe)
                {
                    txtSLThieu = txtSLSoSach - txtSLThucTe;
                    txtSLThua = 0;
                }
                else
                {
                    txtSLThua = txtSLThucTe - txtSLSoSach;
                    txtSLThieu = 0;
                }
            }
        }
        private double _txtSLThua;
        public double txtSLThua
        {
            get => _txtSLThua;
            set => SetProperty(ref _txtSLThua, value);
        }
        private double _txtSLThieu;
        public double txtSLThieu
        {
            get => _txtSLThieu;
            set => SetProperty(ref _txtSLThieu, value);
        }
        private double _txtSLPhamChatTot;
        public double txtSLPhamChatTot
        {
            get => _txtSLPhamChatTot;
            set
            {
                if (value < 0)
                    value *= -1;
                if (value > txtSLThucTe)
                    value = txtSLThucTe;
                SetProperty(ref _txtSLPhamChatTot, value);
                txtSLMatPhamChat = txtSLThucTe - txtSLPhamChatTot - txtSLPhamChatKem;
            } 
        }
        private double _txtSLPhamChatKem;
        public double txtSLPhamChatKem
        {
            get => _txtSLPhamChatKem;
            set
            {
                if (value < 0)
                    value *= -1;
                if (value > txtSLThucTe)
                    value = txtSLThucTe;
                SetProperty(ref _txtSLPhamChatKem, value);
                txtSLMatPhamChat = txtSLThucTe - txtSLPhamChatTot - txtSLPhamChatKem;
            }
        }
        private double _txtSLMatPhamChat;
        public double txtSLMatPhamChat
        {
            get => _txtSLMatPhamChat;
            set
            {
                if (value < 0)
                    value = 0;
                SetProperty(ref _txtSLMatPhamChat, value);
            }
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
            txtSLSoSach = txtSLThucTe = txtSLThua = txtSLThieu = txtSLPhamChatTot = txtSLPhamChatKem = txtSLMatPhamChat = 0;
        }
        public void InitCTBienBanCommand()
        {
            AddCommandCT = new RelayCommand<object>((p) => selectedBienBan != null, (p) =>
            {
                GetListVatTu();
                CT_BienBanDialog dialog = new CT_BienBanDialog();
                TitleDialogCT = "Thêm chi tiết biên bản";
                txtSoBienBanCT = selectedBienBan.SoBienBan;
                BtnContent = "Thêm";
                EnableListVT = true;
                ClearTextboxValueCT();
                RemoveExistedVatTu();
                dialog.ShowDialog();
            });

            EditCommandCT = new RelayCommand<object>((p) => true, (p) =>
            {
                GetListVatTu();
                CT_BienBanDialog dialog = new CT_BienBanDialog();
                TitleDialogCT = "Cập nhật chi tiết biên bản";
                BtnContent = "Lưu";
                var itemData = p as CT_BienBanDetail;
                MaSo = itemData.MaSo;
                txtSoBienBanCT = itemData.SoBienBan;
                selectedMaVT = itemData.MaVT;
                txtTenVT = itemData.TenVT;
                txtTenDVT = itemData.TenDVT;
                txtSLSoSach = itemData.SLSoSach;
                txtSLThucTe = itemData.SLThucTe;
                txtSLThua = itemData.SLThua;
                txtSLThieu = itemData.SLThieu;
                txtSLPhamChatTot = itemData.SLPhamChatTot;
                txtSLPhamChatKem = itemData.SLPhamChatKem;
                txtSLMatPhamChat = itemData.SLMatPhamChat;
                EnableListVT = false;
                dialog.ShowDialog();
            });
            BtnCommandCT = new RelayCommand<object>((p) =>
            {
                bool checkSL = txtSLThucTe == (txtSLPhamChatTot + txtSLPhamChatKem + txtSLMatPhamChat);
                return Valid.IsValid(p as DependencyObject) && checkSL;
            }, (p) =>
            {

                if (BtnContent == "Thêm")
                {
                    CT_BienBan ctbb = new CT_BienBan
                    {
                        SoBienBan = txtSoBienBanCT,
                        MaVT = selectedMaVT,
                        SLSoSach = txtSLSoSach,
                        SLThucTe = txtSLThucTe,
                        SLThua = txtSLThua,
                        SLThieu = txtSLThieu,
                        SLPhamChatTot = txtSLPhamChatTot,
                        SLPhamChatKem = txtSLPhamChatKem,
                        SLMatPhamChat = txtSLMatPhamChat,
                    };
                    if (CRUD.InsertData("CT_BBKiemKe", ctbb))
                    {
                        MessageBox.Show("Thành công!", "Thông báo", MessageBoxButton.OK, MessageBoxImage.Information);
                        GetListCT(txtSoBienBanCT);
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
                    CT_BienBan ctbb = new CT_BienBan
                    {
                        MaSo = MaSo,
                        SoBienBan = txtSoBienBanCT,
                        MaVT = selectedMaVT,
                        SLSoSach = txtSLSoSach,
                        SLThucTe = txtSLThucTe,
                        SLThua = txtSLThua,
                        SLThieu = txtSLThieu,
                        SLPhamChatTot = txtSLPhamChatTot,
                        SLPhamChatKem = txtSLPhamChatKem,
                        SLMatPhamChat = txtSLMatPhamChat,
                    };
                    if (CRUD.UpdateData("CT_BBKiemKe", ctbb))
                    {
                        MessageBox.Show("Thành công!", "Thông báo", MessageBoxButton.OK, MessageBoxImage.Information);
                        GetListCT(txtSoBienBanCT);
                    }
                    else
                    {
                        MessageBox.Show("Đã có lỗi xảy ra, vui lòng thử lại sau", "Thông báo", MessageBoxButton.OK, MessageBoxImage.Error);
                    }
                    ((Window)p).Close();
                }
            });
            DeleteItemCommandCT = new RelayCommand<object>((p) => true, (p) =>
            {
                var itemData = p as CT_BienBanDetail;
                if (CRUD.DeleteData("CT_BBKiemKe", itemData.MaSo.ToString()))
                {
                    MessageBox.Show("Thành công!", "Thông báo", MessageBoxButton.OK, MessageBoxImage.Information);
                    GetListCT(selectedBienBan.SoBienBan);
                } else
                {
                    MessageBox.Show("Đã có lỗi xảy ra, vui lòng thử lại sau", "Thông báo", MessageBoxButton.OK, MessageBoxImage.Error);
                }

            });
        }
        #endregion
        public BienBanViewModel()
        {
            InitBienBanCommand();
            InitCTBienBanCommand();
            GetListKho();
            GetListVatTu();
            LoadTableData();
            GetListKeToan();
        }
        public void LoadTableData()
        {
            string JsonData = CRUD.GetJoinTableData("BBKiemKe");
            ListData = JsonConvert.DeserializeObject<ObservableCollection<BienBanDetail>>(JsonData);
        }
        public void GetListKeToan()
        {
            string data = CRUD.GetJoinTableData("NhanVien");
            var list = JsonConvert.DeserializeObject<ObservableCollection<NhanVienDetail>>(data);
            ListKeToan = new ObservableCollection<NhanVienDetail>(list.Where(item => item.TenBoPhan == "Kế toán").ToList());
            foreach (var item in ListKeToan)
            {
                item.TenNV = item.MaNV + " - " + item.TenNV;
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

        public void GetListCT(string SoBienBan)
        {
            string data = CRUD.GetDataByColumnName("CT_BBKiemKe", SoBienBan);
            ListDataCT = JsonConvert.DeserializeObject<ObservableCollection<CT_BienBanDetail>>(data);
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
    }
}
