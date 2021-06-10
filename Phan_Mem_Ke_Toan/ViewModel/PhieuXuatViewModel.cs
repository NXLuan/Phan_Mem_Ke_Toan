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
using System.Windows.Forms;
using Microsoft.Office.Interop.Word;
using Application = Microsoft.Office.Interop.Word.Application;
using MessageBox = System.Windows.MessageBox;
using System.Diagnostics;
using System.Globalization;
using Window = System.Windows.Window;

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
        public ICommand ExportCommand { get; set; }
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
            txtTongTien = 0;
        }
        public void InitPhieuXuatCommand()
        {
            ExportCommand = new RelayCommand<object>((p) => selectedPhieuXuat != null && ListDataCT.Count > 0, (p) =>
            {
                ExportPhieuXuat();
            });
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
        private double _txtSLSoSach;
        public double txtSLSoSach
        {
            get => _txtSLSoSach;
            set
            {
                if (value < 0)
                    value *= -1;
                SetProperty(ref _txtSLSoSach, value);
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
                txtThanhTien = (decimal)txtSLThucTe * txtDonGia;
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
                txtThanhTien = (decimal)txtSLThucTe * txtDonGia;
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
            txtSLSoSach = txtSLThucTe = 0;
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
                txtSLSoSach = itemData.SLSoSach;
                txtSLThucTe = itemData.SLThucTe;
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
                    int n = ListDataCT.Count;
                    CT_PhieuXuat ctpn = new CT_PhieuXuat
                    {  
                        SoPhieu = txtSoPhieuCT,
                        MaVT = selectedMaVT,
                        SLSoSach = txtSLSoSach,
                        SLThucTe = txtSLThucTe,
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
                        SLSoSach = txtSLSoSach,
                        SLThucTe = txtSLThucTe,
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
        public void ExportPhieuXuat()
        {
            string Ngay = selectedPhieuXuat.NgayXuat.Day.ToString();
            string Thang = selectedPhieuXuat.NgayXuat.Month.ToString();
            string Nam = selectedPhieuXuat.NgayXuat.Year.ToString();
            using (SaveFileDialog sfd = new SaveFileDialog() { FileName = "Phiếu xuất kho ngày " + Ngay + "-" + Thang + "-" + Nam, Filter = "Word Document | *.docx", ValidateNames = true })
            {
                if (sfd.ShowDialog() == DialogResult.OK)
                {
                    Application app = new Application();
                    app.Visible = false;
                    object missing = System.Reflection.Missing.Value;
                    object oEndOfDoc = "\\endofdoc";
                    Document document = app.Documents.Add(ref missing, ref missing, ref missing, ref missing);
                    document.Content.Font.Name = "Times New Roman";
                    document.Content.Font.Size = 11;
                    document.PageSetup.Orientation = WdOrientation.wdOrientLandscape;
                    document.PageSetup.PaperSize = WdPaperSize.wdPaperA4;
                    document.PageSetup.TopMargin = app.InchesToPoints(0.5f);
                    document.PageSetup.BottomMargin = app.InchesToPoints(0.5f);
                    document.PageSetup.LeftMargin = app.InchesToPoints(0.5f);
                    document.PageSetup.RightMargin = app.InchesToPoints(0.5f);

                    float PageWidth = document.PageSetup.PageWidth - document.PageSetup.LeftMargin - document.PageSetup.RightMargin;

                    Range wordRange = document.Bookmarks.get_Item(ref oEndOfDoc).Range;
                    Table HeaderTable = document.Tables.Add(wordRange, 1, 2);
                    HeaderTable.PreferredWidthType = WdPreferredWidthType.wdPreferredWidthPoints;
                    HeaderTable.Columns[1].Width = app.CentimetersToPoints(17);
                    HeaderTable.Columns[2].Width = PageWidth - HeaderTable.Columns[1].Width;

                    Range col1Range = HeaderTable.Cell(1, 1).Range;
                    col1Range.Text = "Đơn vị:......\vĐịa chỉ:......";
                    col1Range.Font.Bold = 1;

                    object oCollapseStart = WdCollapseDirection.wdCollapseStart;
                    object oCollapseEnd = WdCollapseDirection.wdCollapseEnd;
                    Range col2Range = HeaderTable.Cell(1, 2).Range;
                    col2Range.ParagraphFormat.Alignment = WdParagraphAlignment.wdAlignParagraphCenter;
                    col2Range.Collapse(ref oCollapseStart);
                    col2Range.Text = "Mẫu số 02 - VT\v";
                    col2Range.Font.Bold = 1;
                    col2Range.Collapse(oCollapseEnd);
                    col2Range.Text = "(Ban hành theo Thông tư số 200/2014/TT-BTC\vngày 22/12/2014 của Bộ Tài chính)";

                    Paragraph Title = document.Content.Paragraphs.Add(ref missing);
                    Title.Range.Text = "PHIẾU XUẤT KHO";
                    Title.Range.ParagraphFormat.Alignment = WdParagraphAlignment.wdAlignParagraphCenter;
                    Title.Range.Font.Size = 13;
                    Title.Range.Font.Bold = 1;
                    Title.SpaceAfter = 0;
                    Title.Range.InsertParagraphAfter();

                    Range range = document.Bookmarks.get_Item(ref oEndOfDoc).Range;
                    Table TitleTable = document.Tables.Add(range, 1, 3);
                    TitleTable.Range.Font.Bold = 0;
                    TitleTable.Range.Font.Size = 11;

                    Range rngCol2 = TitleTable.Cell(1, 2).Range;
                    rngCol2.ParagraphFormat.Alignment = WdParagraphAlignment.wdAlignParagraphCenter;
                    rngCol2.Collapse(ref oCollapseStart);
                    rngCol2.Text = "Ngày " + Ngay + " tháng " + Thang + " năm " + Nam + "\v";
                    rngCol2.Font.Italic = 1;
                    rngCol2.Collapse(oCollapseEnd);
                    rngCol2.Text = "Số: " + selectedPhieuXuat.SoPhieu;
                    rngCol2.Font.Italic = 0;

                    List<string> listTKCo = new List<string>();
                    foreach (var item in ListDataCT)
                        listTKCo.Add(item.MaTK);

                    TitleTable.Cell(1, 3).Range.ParagraphFormat.Alignment = WdParagraphAlignment.wdAlignParagraphLeft;
                    TitleTable.Cell(1, 3).Range.Text = "Nợ: " + selectedPhieuXuat.TKNo + "\vCó: " + Utils.Utils.FormatListTK(listTKCo);


                    Paragraph Date = document.Content.Paragraphs.Add(ref missing);
                    Date.Range.Font.Size = 11;
                    Date.Range.Font.Bold = 0;
                    Date.Range.Text = "\t- Họ và tên người nhận hàng: " + selectedPhieuXuat.TenNguoiNhan + "\t\tĐịa chỉ (bộ phận): " + selectedPhieuXuat.DiaChiCT +
                        "\v\t- Lý do xuất kho: " + selectedPhieuXuat.LyDo +
                        "\v\t- Xuất tại kho (ngăn lô): " + selectedPhieuXuat.TenKho + "\t\tđịa điểm: " + selectedPhieuXuat.DiaChiKho;
                    Date.Range.ParagraphFormat.Alignment = WdParagraphAlignment.wdAlignParagraphLeft;
                    Date.Range.ParagraphFormat.SpaceAfter = 6;
                    Date.Range.InsertParagraphAfter();


                    Table MainTable = document.Tables.Add(Date.Range, ListDataCT.Count + 4, 8);
                    MainTable.Range.ParagraphFormat.Alignment = WdParagraphAlignment.wdAlignParagraphCenter;
                    MainTable.Range.ParagraphFormat.SpaceAfter = 0;
                    MainTable.Borders.Enable = 1;
                    MainTable.Range.Font.Bold = 0;
                    MainTable.Range.Font.Size = 11;
                    MainTable.Rows[ListDataCT.Count + 4].Range.Font.Bold = 1;

                    MainTable.PreferredWidthType = WdPreferredWidthType.wdPreferredWidthPoints;

                    //STT
                    MainTable.Columns[1].Width = app.CentimetersToPoints(2);
                    MainTable.Cell(1, 1).Range.Text = "STT";
                    //Tên
                    MainTable.Columns[2].Width = app.CentimetersToPoints(7.5f);
                    MainTable.Cell(1, 2).Range.Text = "Tên, nhãn hiệu, quy cách, phẩm chất vật tư, dụng cụ sản phẩm, hàng hoá";
                    //Mã số
                    MainTable.Columns[3].Width = app.CentimetersToPoints(2.5f);
                    MainTable.Cell(1, 3).Range.Text = "Mã số";
                    //Đơn vị tính
                    MainTable.Columns[4].Width = app.CentimetersToPoints(2.5f);
                    MainTable.Cell(1, 4).Range.Text = "Đơn vị tính";
                    //Số lượng
                    MainTable.Columns[5].Width = app.CentimetersToPoints(3);
                    MainTable.Cell(1, 5).Range.Text = "Số lượng";
                    MainTable.Cell(2, 5).Range.Text = "Yêu cầu";
                    MainTable.Columns[6].Width = app.CentimetersToPoints(3);
                    MainTable.Cell(2, 6).Range.Text = "Thực nhập";

                    MainTable.Columns[7].Width = app.CentimetersToPoints(3);
                    MainTable.Cell(1, 7).Range.Text = "Đơn giá";

                    //Ghi chú
                    MainTable.Columns[8].Width = PageWidth - app.CentimetersToPoints(23.5f);
                    MainTable.Cell(1, 8).Range.Text = "Thành tiền";

                    MainTable.Cell(1, 1).Merge(MainTable.Cell(2, 1));
                    MainTable.Cell(1, 1).VerticalAlignment = WdCellVerticalAlignment.wdCellAlignVerticalCenter;

                    MainTable.Cell(1, 2).Merge(MainTable.Cell(2, 2));
                    MainTable.Cell(1, 2).VerticalAlignment = WdCellVerticalAlignment.wdCellAlignVerticalCenter;

                    MainTable.Cell(1, 3).Merge(MainTable.Cell(2, 3));
                    MainTable.Cell(1, 3).VerticalAlignment = WdCellVerticalAlignment.wdCellAlignVerticalCenter;

                    MainTable.Cell(1, 4).Merge(MainTable.Cell(2, 4));
                    MainTable.Cell(1, 4).VerticalAlignment = WdCellVerticalAlignment.wdCellAlignVerticalCenter;

                    MainTable.Cell(1, 7).Merge(MainTable.Cell(2, 7));
                    MainTable.Cell(1, 7).VerticalAlignment = WdCellVerticalAlignment.wdCellAlignVerticalCenter;

                    MainTable.Cell(1, 8).Merge(MainTable.Cell(2, 8));
                    MainTable.Cell(1, 8).VerticalAlignment = WdCellVerticalAlignment.wdCellAlignVerticalCenter;

                    MainTable.Cell(1, 5).Merge(MainTable.Cell(1, 6));


                    MainTable.Cell(3, 1).Range.Text = "A";
                    MainTable.Cell(3, 2).Range.Text = "B";
                    MainTable.Cell(3, 3).Range.Text = "C";
                    MainTable.Cell(3, 4).Range.Text = "D";
                    MainTable.Cell(3, 5).Range.Text = "1";
                    MainTable.Cell(3, 6).Range.Text = "2";
                    MainTable.Cell(3, 7).Range.Text = "3";
                    MainTable.Cell(3, 8).Range.Text = "4";

                    NumberFormatInfo nfi = new CultureInfo("en-US", false).NumberFormat;

                    nfi.CurrencyDecimalSeparator = ",";
                    nfi.CurrencyGroupSeparator = ".";
                    nfi.CurrencySymbol = "";
                    for (int i = 0; i < ListDataCT.Count; i++)
                    {
                        //STT
                        MainTable.Cell(i + 4, 1).Range.Text = (i + 1).ToString();
                        MainTable.Cell(i + 4, 1).VerticalAlignment = WdCellVerticalAlignment.wdCellAlignVerticalCenter;
                        //Tên
                        MainTable.Cell(i + 4, 2).Range.Text = ListDataCT[i].TenVT;
                        MainTable.Cell(i + 4, 2).VerticalAlignment = WdCellVerticalAlignment.wdCellAlignVerticalCenter;
                        MainTable.Cell(i + 4, 2).Range.ParagraphFormat.Alignment = WdParagraphAlignment.wdAlignParagraphLeft;
                        //MaVT
                        MainTable.Cell(i + 4, 3).Range.Text = ListDataCT[i].MaVT;
                        MainTable.Cell(i + 4, 3).VerticalAlignment = WdCellVerticalAlignment.wdCellAlignVerticalCenter;
                        //DVT
                        MainTable.Cell(i + 4, 4).Range.Text = ListDataCT[i].TenDVT;
                        MainTable.Cell(i + 4, 4).VerticalAlignment = WdCellVerticalAlignment.wdCellAlignVerticalCenter;
                        //SLSS
                        MainTable.Cell(i + 4, 5).Range.Text = ListDataCT[i].SLSoSach.ToString();
                        MainTable.Cell(i + 4, 5).VerticalAlignment = WdCellVerticalAlignment.wdCellAlignVerticalCenter;
                        //SLTT
                        MainTable.Cell(i + 4, 6).Range.Text = ListDataCT[i].SLThucTe.ToString();
                        MainTable.Cell(i + 4, 6).VerticalAlignment = WdCellVerticalAlignment.wdCellAlignVerticalCenter;
                        //DonGia
                        MainTable.Cell(i + 4, 7).Range.Text = ListDataCT[i].DonGia.ToString("C0", nfi);
                        MainTable.Cell(i + 4, 7).VerticalAlignment = WdCellVerticalAlignment.wdCellAlignVerticalCenter;
                        MainTable.Cell(i + 4, 7).Range.ParagraphFormat.Alignment = WdParagraphAlignment.wdAlignParagraphRight;
                        //ThanhTien
                        MainTable.Cell(i + 4, 8).Range.Text = ListDataCT[i].ThanhTien.ToString("C0", nfi);
                        MainTable.Cell(i + 4, 8).VerticalAlignment = WdCellVerticalAlignment.wdCellAlignVerticalCenter;
                        MainTable.Cell(i + 4, 8).Range.ParagraphFormat.Alignment = WdParagraphAlignment.wdAlignParagraphRight;
                    }
                    int n = ListDataCT.Count + 4;
                    //Cong
                    MainTable.Cell(n, 2).Range.Text = "Cộng";
                    MainTable.Cell(n, 2).VerticalAlignment = WdCellVerticalAlignment.wdCellAlignVerticalCenter;
                    //x
                    for (int col = 3; col <= 7; col++)
                    {
                        MainTable.Cell(n, col).Range.Text = "x";
                        MainTable.Cell(n, col).VerticalAlignment = WdCellVerticalAlignment.wdCellAlignVerticalCenter;
                    }
                    //Tong tien
                    MainTable.Cell(n, 8).Range.Text = selectedPhieuXuat.TongTien.ToString("C0", nfi);
                    MainTable.Cell(n, 8).VerticalAlignment = WdCellVerticalAlignment.wdCellAlignVerticalCenter;
                    MainTable.Cell(n, 8).Range.ParagraphFormat.Alignment = WdParagraphAlignment.wdAlignParagraphRight;

                    string money = Utils.Utils.NumberToText(selectedPhieuXuat.TongTien);
                    money = money[0].ToString().ToUpper() + money.Substring(1) + ".";
                    Paragraph TextMoney = document.Content.Paragraphs.Add(ref missing);
                    TextMoney.Alignment = WdParagraphAlignment.wdAlignParagraphLeft;
                    TextMoney.Range.Font.Bold = 0;
                    TextMoney.Range.Font.Size = 11;
                    TextMoney.Range.Text = "\n\t- Tổng số tiền (viết bằng chữ): " + money;
                    TextMoney.Range.ParagraphFormat.Alignment = WdParagraphAlignment.wdAlignParagraphLeft;
                    TextMoney.SpaceBefore = 6.0f;

                    object start = TextMoney.Range.Start + TextMoney.Range.Text.IndexOf("chữ):") + 5;
                    object end = TextMoney.Range.Start + TextMoney.Range.Text.IndexOf("chữ):") + 5 + money.Length;
                    var rngItalic = document.Range(ref start, ref end);
                    rngItalic.Italic = 1;
                    TextMoney.Range.InsertParagraphAfter();

                    Paragraph Last = document.Content.Paragraphs.Add(ref missing);
                    Last.Range.Font.Bold = 0;
                    Last.Range.Font.Size = 11;
                    Last.SpaceBefore = 6.0f;
                    Last.Range.Text = "\t- Số chứng từ gốc kèm theo: " + selectedPhieuXuat.ChungTuLQ;
                    Last.Range.InsertParagraphAfter();

                    Range wordRange2 = document.Bookmarks.get_Item(ref oEndOfDoc).Range;

                    Table SignTable = document.Tables.Add(wordRange2, 1, 5);
                    SignTable.Range.ParagraphFormat.Alignment = WdParagraphAlignment.wdAlignParagraphCenter;
                    SignTable.Range.Font.Italic = 0;
                    SignTable.Range.Font.Bold = 0;
                    SignTable.Range.Font.Size = 11;
                    Range col1 = SignTable.Cell(1, 1).Range;
                    col1.Collapse(ref oCollapseStart);
                    col1.Text = "\vNgười lập phiếu\v";
                    col1.Font.Bold = 1;
                    col1.Collapse(oCollapseEnd);
                    col1.Text = "(Ký, họ tên)";
                    col1.Font.Italic = 1;

                    Range col2 = SignTable.Cell(1, 2).Range;
                    col2.Collapse(ref oCollapseStart);
                    col2.Text = "\vNgười nhận hàng\v";
                    col2.Font.Bold = 1;
                    col2.Collapse(oCollapseEnd);
                    col2.Text = "(Ký, họ tên)";
                    col2.Font.Italic = 1;

                    Range col3 = SignTable.Cell(1, 3).Range;
                    col3.Collapse(ref oCollapseStart);
                    col3.Text = "\vThủ kho\v";
                    col3.Font.Bold = 1;
                    col3.Collapse(oCollapseEnd);
                    col3.Text = "(Ký, họ tên)";
                    col3.Font.Italic = 1;

                    Range col4 = SignTable.Cell(1, 4).Range;
                    col4.Collapse(ref oCollapseStart);
                    col4.Text = "\vKế toán trưởng\v(Hoặc bộ phận có nhu cầu nhập)\v";
                    col4.Font.Bold = 1;
                    col4.Collapse(oCollapseEnd);
                    col4.Text = "(Ký, họ tên)";
                    col4.Font.Italic = 1;

                    Range col5 = SignTable.Cell(1, 5).Range;
                    col5.Collapse(ref oCollapseStart);
                    col5.Text = "Ngày " + Ngay + " tháng " + Thang + " năm " + Nam + "\v";
                    col5.Font.Italic = 1;
                    col5.Collapse(oCollapseEnd);
                    col5.Text = "Giám đốc\v";
                    col5.Font.Bold = 1;
                    col5.Collapse(oCollapseEnd);
                    col5.Text = "(Ký, họ tên)";
                    col5.Font.Italic = 1;

                    document.SaveAs2(sfd.FileName);
                    document.Close(ref missing, ref missing, ref missing);
                    document = null;
                    app.Quit(ref missing, ref missing, ref missing);
                    app = null;
                    Process.Start(sfd.FileName);
                }
            }
        }  
    }
}
