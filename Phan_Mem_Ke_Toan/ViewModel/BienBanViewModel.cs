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
        public ICommand ExportCommand { get; set; }
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
            ExportCommand = new RelayCommand<object>((p) => selectedBienBan != null && ListDataCT.Count > 0, (p) =>
            {
                ExportBienBanKiemKe();
            });
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
        public void ExportBienBanKiemKe()
        {
            string Ngay = selectedBienBan.NgayLap.Day.ToString();
            string Thang = selectedBienBan.NgayLap.Month.ToString();
            string Nam = selectedBienBan.NgayLap.Year.ToString();
            using (SaveFileDialog sfd = new SaveFileDialog() { FileName = "Biên bản kiểm kê ngày " + Ngay + "-" + Thang + "-" + Nam, Filter = "Word Document | *.docx", ValidateNames = true })
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
                    col2Range.Text = "Mẫu số 05 - VT\v";
                    col2Range.Font.Bold = 1;
                    col2Range.Collapse(oCollapseEnd);
                    col2Range.Text = "(Ban hành theo Thông tư số 200/2014/TT-BTC\vngày 22/12/2014 của Bộ Tài chính)";

                    Paragraph Title = document.Content.Paragraphs.Add(ref missing);
                    Title.Range.Text = "BIÊN BẢN KIỂM KÊ VẬT TƯ, CÔNG CỤ, SẢN PHẨM, HÀNG HÓA";
                    Title.Range.ParagraphFormat.Alignment = WdParagraphAlignment.wdAlignParagraphCenter;
                    Title.Range.Font.Size = 13;
                    Title.Range.Font.Bold = 1;
                    Title.SpaceAfter = 12;
                    Title.Range.InsertParagraphAfter();

                    Paragraph Date = document.Content.Paragraphs.Add(ref missing);
                    Date.Range.Font.Size = 11;
                    Date.Range.Font.Bold = 0;
                    Date.Range.Text = "Thời điểm kiểm kê:.......giờ.......ngày " + Ngay + " tháng " + Thang + " năm " + Nam +
                        "\vBan kiểm kê gồm:" +
                        "\v- Ông/Bà: " + selectedBienBan.TenTruongBan + "\t chức vụ..............Đại diện..............Trưởng ban" +
                        "\v- Ông/Bà: " + selectedBienBan.TenUyVien1 + "\t chức vụ..............Đại diện..............Ủy viên" +
                        "\v- Ông/Bà: " + selectedBienBan.TenUyVien2 + "\t chức vụ..............Đại diện..............Ủy viên" +
                        "\v\vĐã kiểm kê kho có những mặt hàng dưới đây:";
                    Date.Range.ParagraphFormat.Alignment = WdParagraphAlignment.wdAlignParagraphLeft;
                    Date.Range.ParagraphFormat.SpaceAfter = 6;
                    Date.Range.InsertParagraphAfter();


                    Table MainTable = document.Tables.Add(Date.Range, ListDataCT.Count + 4, 16);
                    MainTable.Range.ParagraphFormat.Alignment = WdParagraphAlignment.wdAlignParagraphCenter;
                    MainTable.Range.ParagraphFormat.SpaceAfter = 0;
                    MainTable.Borders.Enable = 1;
                    MainTable.Range.Font.Bold = 0;
                    MainTable.Range.Font.Size = 11;
                    MainTable.Rows[ListDataCT.Count + 4].Range.Font.Bold = 1;

                    MainTable.PreferredWidthType = WdPreferredWidthType.wdPreferredWidthPoints;

       
                    MainTable.Columns[1].Width = app.CentimetersToPoints(1.2f);
                    MainTable.Cell(1, 1).Range.Text = "STT";
                    
                    MainTable.Columns[3].Width = app.CentimetersToPoints(1.5f);
                    MainTable.Cell(1, 3).Range.Text = "Mã số";
           
                    MainTable.Columns[4].Width = app.CentimetersToPoints(1.5f);
                    MainTable.Cell(1, 4).Range.Text = "Đơn vị tính";
    
                    MainTable.Columns[5].Width = app.CentimetersToPoints(2);
                    MainTable.Cell(1, 5).Range.Text = "Đơn giá";

                    MainTable.Columns[6].Width = app.CentimetersToPoints(1.5f);
                    MainTable.Cell(1, 6).Range.Text = "Theo sổ kế toán";
                    MainTable.Cell(3, 6).Range.Text = "Số lượng";

                    MainTable.Columns[7].Width = app.CentimetersToPoints(1.5f);
                    MainTable.Cell(3, 7).Range.Text = "Thành tiền";

                    MainTable.Columns[8].Width = app.CentimetersToPoints(1.5f);
                    MainTable.Cell(1, 8).Range.Text = "Theo kiểm kê";
                    MainTable.Cell(3, 8).Range.Text = "Số lượng";

                    MainTable.Columns[9].Width = app.CentimetersToPoints(1.5f);
                    MainTable.Cell(3, 9).Range.Text = "Thành tiền";

                    MainTable.Columns[10].Width = app.CentimetersToPoints(1.5f);
                    MainTable.Cell(1, 10).Range.Text = "Chênh lệch";
                    MainTable.Cell(2, 10).Range.Text = "Thừa";
                    MainTable.Cell(3, 10).Range.Text = "Số lượng";

                    MainTable.Columns[11].Width = app.CentimetersToPoints(1.5f);
                    MainTable.Cell(3, 11).Range.Text = "Thành tiền";

                    MainTable.Columns[12].Width = app.CentimetersToPoints(1.5f);
                    MainTable.Cell(2, 12).Range.Text = "Thiếu";
                    MainTable.Cell(3, 12).Range.Text = "Số lượng";

                    MainTable.Columns[13].Width = app.CentimetersToPoints(1.5f);
                    MainTable.Cell(3, 13).Range.Text = "Thành tiền";

                    MainTable.Columns[14].Width = app.CentimetersToPoints(1.5f);
                    MainTable.Cell(1, 14).Range.Text = "Phẩm chất";
                    MainTable.Cell(2, 14).Range.Text = "Còn tốt 100%";

                    MainTable.Columns[15].Width = app.CentimetersToPoints(1.5f);
                    MainTable.Cell(2, 15).Range.Text = "Kém phẩm chất";

                    MainTable.Columns[16].Width = app.CentimetersToPoints(1.5f);
                    MainTable.Cell(2, 16).Range.Text = "Mất phẩm chất";

                    MainTable.Columns[2].Width = PageWidth - app.CentimetersToPoints(22.7f);
                    MainTable.Cell(1, 2).Range.Text = "Tên, nhãn hiệu, quy cách, phẩm chất vật tư, dụng cụ sản phẩm, hàng hoá";

                    MainTable.Cell(1, 1).Merge(MainTable.Cell(2, 1));
                    MainTable.Cell(1, 1).Merge(MainTable.Cell(3, 1));
                    MainTable.Cell(1, 1).VerticalAlignment = WdCellVerticalAlignment.wdCellAlignVerticalCenter;

                    MainTable.Cell(1, 2).Merge(MainTable.Cell(2, 2));
                    MainTable.Cell(1, 2).Merge(MainTable.Cell(3, 2));
                    MainTable.Cell(1, 2).VerticalAlignment = WdCellVerticalAlignment.wdCellAlignVerticalCenter;

                    MainTable.Cell(1, 3).Merge(MainTable.Cell(2, 3));
                    MainTable.Cell(1, 3).Merge(MainTable.Cell(3, 3));
                    MainTable.Cell(1, 3).VerticalAlignment = WdCellVerticalAlignment.wdCellAlignVerticalCenter;

                    MainTable.Cell(1, 4).Merge(MainTable.Cell(2, 4));
                    MainTable.Cell(1, 4).Merge(MainTable.Cell(3, 4));
                    MainTable.Cell(1, 4).VerticalAlignment = WdCellVerticalAlignment.wdCellAlignVerticalCenter;

                    MainTable.Cell(1, 5).Merge(MainTable.Cell(2, 5));
                    MainTable.Cell(1, 5).Merge(MainTable.Cell(3, 5));
                    MainTable.Cell(1, 5).VerticalAlignment = WdCellVerticalAlignment.wdCellAlignVerticalCenter;

                    MainTable.Cell(1, 6).Merge(MainTable.Cell(2, 6));
                    MainTable.Cell(1, 6).VerticalAlignment = WdCellVerticalAlignment.wdCellAlignVerticalCenter;

                    MainTable.Cell(1, 7).Merge(MainTable.Cell(2, 7));
                    MainTable.Cell(1, 7).VerticalAlignment = WdCellVerticalAlignment.wdCellAlignVerticalCenter;

                    MainTable.Cell(1, 8).Merge(MainTable.Cell(2, 8));
                    MainTable.Cell(1, 8).VerticalAlignment = WdCellVerticalAlignment.wdCellAlignVerticalCenter;

                    MainTable.Cell(1, 9).Merge(MainTable.Cell(2, 9));
                    MainTable.Cell(1, 9).VerticalAlignment = WdCellVerticalAlignment.wdCellAlignVerticalCenter;

                    MainTable.Cell(2, 14).Merge(MainTable.Cell(3, 14));
                    MainTable.Cell(2, 14).VerticalAlignment = WdCellVerticalAlignment.wdCellAlignVerticalCenter;

                    MainTable.Cell(2, 15).Merge(MainTable.Cell(3, 15));
                    MainTable.Cell(2, 15).VerticalAlignment = WdCellVerticalAlignment.wdCellAlignVerticalCenter;

                    MainTable.Cell(2, 16).Merge(MainTable.Cell(3, 16));
                    MainTable.Cell(2, 16).VerticalAlignment = WdCellVerticalAlignment.wdCellAlignVerticalCenter;

                    MainTable.Cell(1, 6).Merge(MainTable.Cell(1, 7));
                    MainTable.Cell(1, 6).VerticalAlignment = WdCellVerticalAlignment.wdCellAlignVerticalCenter;

                    MainTable.Cell(1, 7).Merge(MainTable.Cell(1, 8));
                    MainTable.Cell(1, 7).VerticalAlignment = WdCellVerticalAlignment.wdCellAlignVerticalCenter;

                    MainTable.Cell(1, 8).Merge(MainTable.Cell(1, 9));
                    MainTable.Cell(1, 8).Merge(MainTable.Cell(1, 9));
                    MainTable.Cell(1, 8).Merge(MainTable.Cell(1, 9));
                    MainTable.Cell(1, 8).VerticalAlignment = WdCellVerticalAlignment.wdCellAlignVerticalCenter;

                    MainTable.Cell(1, 9).Merge(MainTable.Cell(1, 10));
                    MainTable.Cell(1, 9).Merge(MainTable.Cell(1, 10));
                    MainTable.Cell(1, 9).VerticalAlignment = WdCellVerticalAlignment.wdCellAlignVerticalCenter;


                    MainTable.Cell(2, 8).Merge(MainTable.Cell(2, 9));
                    MainTable.Cell(2, 8).VerticalAlignment = WdCellVerticalAlignment.wdCellAlignVerticalCenter;

                    MainTable.Cell(2, 9).Merge(MainTable.Cell(2, 10));
                    MainTable.Cell(2, 9).VerticalAlignment = WdCellVerticalAlignment.wdCellAlignVerticalCenter;


                    MainTable.Cell(4, 1).Range.Text = "A";
                    MainTable.Cell(4, 2).Range.Text = "B";
                    MainTable.Cell(4, 3).Range.Text = "C";
                    MainTable.Cell(4, 4).Range.Text = "D";
                    for (int i = 5; i <= 16; i++)
                        MainTable.Cell(4, i).Range.Text = (i - 4).ToString();

                    NumberFormatInfo nfi = new CultureInfo("en-US", false).NumberFormat;
                    nfi.CurrencyDecimalSeparator = ",";
                    nfi.CurrencyGroupSeparator = ".";
                    nfi.CurrencySymbol = "";
                    //for (int i = 0; i < ListDataCT.Count; i++)
                    //{
                    //    //STT
                    //    MainTable.Cell(i + 5, 1).Range.Text = (i + 1).ToString();
                    //    MainTable.Cell(i + 5, 1).VerticalAlignment = WdCellVerticalAlignment.wdCellAlignVerticalCenter;
                    //    //Tên
                    //    MainTable.Cell(i + 5, 2).Range.Text = ListDataCT[i].TenVT;
                    //    MainTable.Cell(i + 5, 2).VerticalAlignment = WdCellVerticalAlignment.wdCellAlignVerticalCenter;
                    //    MainTable.Cell(i + 5, 2).Range.ParagraphFormat.Alignment = WdParagraphAlignment.wdAlignParagraphLeft;
                    //    //MaVT
                    //    MainTable.Cell(i + 5, 3).Range.Text = ListDataCT[i].MaVT;
                    //    MainTable.Cell(i + 5, 3).VerticalAlignment = WdCellVerticalAlignment.wdCellAlignVerticalCenter;
                    //    //DVT
                    //    MainTable.Cell(i + 5, 4).Range.Text = ListDataCT[i].TenDVT;
                    //    MainTable.Cell(i + 5, 4).VerticalAlignment = WdCellVerticalAlignment.wdCellAlignVerticalCenter;
                    //    //SLSS
                    //    MainTable.Cell(i + 4, 5).Range.Text = ListDataCT[i].SLSoSach.ToString();
                    //    MainTable.Cell(i + 4, 5).VerticalAlignment = WdCellVerticalAlignment.wdCellAlignVerticalCenter;
                    //    //SLTT
                    //    MainTable.Cell(i + 4, 6).Range.Text = ListDataCT[i].SLThucTe.ToString();
                    //    MainTable.Cell(i + 4, 6).VerticalAlignment = WdCellVerticalAlignment.wdCellAlignVerticalCenter;
                    //    //DonGia
                    //    MainTable.Cell(i + 4, 7).Range.Text = ListDataCT[i].DonGia.ToString("C0", nfi);
                    //    MainTable.Cell(i + 4, 7).VerticalAlignment = WdCellVerticalAlignment.wdCellAlignVerticalCenter;
                    //    MainTable.Cell(i + 4, 7).Range.ParagraphFormat.Alignment = WdParagraphAlignment.wdAlignParagraphRight;
                    //    //ThanhTien
                    //    MainTable.Cell(i + 4, 8).Range.Text = ListDataCT[i].ThanhTien.ToString("C0", nfi);
                    //    MainTable.Cell(i + 4, 8).VerticalAlignment = WdCellVerticalAlignment.wdCellAlignVerticalCenter;
                    //    MainTable.Cell(i + 4, 8).Range.ParagraphFormat.Alignment = WdParagraphAlignment.wdAlignParagraphRight;
                    //}
                    //int n = ListDataCT.Count + 4;
                    ////Cong
                    //MainTable.Cell(n, 2).Range.Text = "Cộng";
                    //MainTable.Cell(n, 2).VerticalAlignment = WdCellVerticalAlignment.wdCellAlignVerticalCenter;
                    ////x
                    //for (int col = 3; col <= 7; col++)
                    //{
                    //    MainTable.Cell(n, col).Range.Text = "x";
                    //    MainTable.Cell(n, col).VerticalAlignment = WdCellVerticalAlignment.wdCellAlignVerticalCenter;
                    //}
                    ////Tong tien
                    //MainTable.Cell(n, 8).Range.Text = selectedBienBan.TongTien.ToString("C0", nfi);
                    //MainTable.Cell(n, 8).VerticalAlignment = WdCellVerticalAlignment.wdCellAlignVerticalCenter;
                    //MainTable.Cell(n, 8).Range.ParagraphFormat.Alignment = WdParagraphAlignment.wdAlignParagraphRight;

                    //string money = Utils.Utils.NumberToText(selectedBienBan.TongTien);
                    //money = money[0].ToString().ToUpper() + money.Substring(1) + ".";
                    //Paragraph TextMoney = document.Content.Paragraphs.Add(ref missing);
                    //TextMoney.Alignment = WdParagraphAlignment.wdAlignParagraphLeft;
                    //TextMoney.Range.Font.Bold = 0;
                    //TextMoney.Range.Font.Size = 11;
                    //TextMoney.Range.Text = "\n\t- Tổng số tiền (viết bằng chữ): " + money;
                    //TextMoney.Range.ParagraphFormat.Alignment = WdParagraphAlignment.wdAlignParagraphLeft;
                    //TextMoney.SpaceBefore = 6.0f;

                    //object start = TextMoney.Range.Start + TextMoney.Range.Text.IndexOf("chữ):") + 5;
                    //object end = TextMoney.Range.Start + TextMoney.Range.Text.IndexOf("chữ):") + 5 + money.Length;
                    //var rngItalic = document.Range(ref start, ref end);
                    //rngItalic.Italic = 1;
                    //TextMoney.Range.InsertParagraphAfter();

                    //Paragraph Last = document.Content.Paragraphs.Add(ref missing);
                    //Last.Range.Font.Bold = 0;
                    //Last.Range.Font.Size = 11;
                    //Last.SpaceBefore = 6.0f;
                    //Last.Range.Text = "\t- Số chứng từ gốc kèm theo: " + selectedBienBan.ChungTuLQ;
                    //Last.Range.InsertParagraphAfter();

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
