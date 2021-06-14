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
using System.Threading;

namespace Phan_Mem_Ke_Toan.ViewModel
{
    class BienBanViewModel : TableViewModel<BienBanDetail>
    {
        private string _btnContent;
        public string BtnContent
        {
            get => _btnContent;
            set => SetProperty(ref _btnContent, value);
        }
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
                if (value != _selectedTruongBan)
                {
                    _selectedTruongBan = value;
                    OnPropertyChanged("selectedTruongBan");
                    OnPropertyChanged("ListUyVien1");
                    OnPropertyChanged("ListUyVien2");
                }
            }
        }
        private string _selectedUyVien1;
        public string selectedUyVien1
        {
            get => _selectedUyVien1;
            set
            {
                if (value != _selectedUyVien1)
                {
                    _selectedUyVien1 = value;
                    OnPropertyChanged("selectedUyVien1");
                    OnPropertyChanged("ListTruongBan");
                    OnPropertyChanged("ListUyVien2");
                }
            }
        }

        private string _selectedUyVien2;
        public string selectedUyVien2
        {
            get => _selectedUyVien2;
            set
            {
                if (value != _selectedUyVien2)
                {
                    _selectedUyVien2 = value;
                    OnPropertyChanged("selectedUyVien2");
                    OnPropertyChanged("ListTruongBan");
                    OnPropertyChanged("ListUyVien1");
                }
            }
        }
        private ObservableCollection<Kho> _ListKho;
        public ObservableCollection<Kho> ListKho
        {
            get => _ListKho;
            set => SetProperty(ref _ListKho, value);
        }
        private ObservableCollection<NhanVienDetail> _listKeToan;
        public ObservableCollection<NhanVienDetail> ListKeToan
        {
            get => _listKeToan;
            set
            {
                if (value != _listKeToan)
                {
                    _listKeToan = value;
                    OnPropertyChanged("ListKeToan");
                    OnPropertyChanged("ListTruongBan");
                    OnPropertyChanged("ListUyVien1");
                    OnPropertyChanged("ListUyVien2");
                }
            }
        }

        public IEnumerable<NhanVienDetail> ListTruongBan
        {
            get
            {
                if (ListKeToan == null) return null;
                return ListKeToan.Where(x => x.MaNV != selectedUyVien1 && x.MaNV != selectedUyVien2);
            }
        }

        public IEnumerable<NhanVienDetail> ListUyVien1
        {
            get
            {
                if (ListKeToan == null) return null;
                return ListKeToan.Where(x => x.MaNV != selectedTruongBan && x.MaNV != selectedUyVien2);
            }
        }

        public IEnumerable<NhanVienDetail> ListUyVien2
        {
            get
            {
                if (ListKeToan == null) return null;
                return ListKeToan.Where(x => x.MaNV != selectedUyVien1 && x.MaNV != selectedTruongBan);
            }
        }

        public ICommand ExportCommand { get; set; }
        public ICommand ShowDetailCommand { get; set; }
        public ICommand DeleteItemCommandCT { get; set; }
        public ICommand AddCommandCT { get; set; }

        private string _search;
        public string Search
        {
            get => _search;
            set
            {
                SetProperty(ref _search, value);
                string text = value.Trim().ToLower();
                filter.AddFilter("Search", element =>
                {
                    BienBanDetail item = element as BienBanDetail;
                    return item.SoBienBan.ToLower().Contains(text);
                });
            }
        }
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

        public BienBanViewModel() : base("BBKiemKe")
        {
            ListDataCT = new ObservableCollection<CT_BienBanDetail>();
        }

        public override void Event()
        {
            base.Event();

            LoadedCommand = new RelayCommand<object>((p) => true, (p) =>
            {
                LoadNewData();
            });

            ExportCommand = new RelayCommand<object>((p) => p != null, (p) =>
            {
                var selectedBienBan = p as BienBanDetail;
                GetListCT(selectedBienBan.SoBienBan);
                if (ListDataCT.Count == 0)
                {
                    notify.updateDataFail("Chưa có dữ liệu chi tiết, không thể xuất file");
                    return;
                }
                ExportBienBanKiemKe(selectedBienBan);

            });
            AddCommand = new RelayCommand<object>((p) => true, (p) =>
            {
                LoadNewData();
                txtSoBienBan = ListData.Count() == 0 ? "BB001" : CRUD.GeneratePrimaryKey(ListData[ListData.Count() - 1].SoBienBan);
                BtnContent = "Xong";
                ClearTextboxValue();
                BienBanDialog dialog = new BienBanDialog();
                dialog.ShowDialog();
            });

            ShowDetailCommand = new RelayCommand<object>((p) => true, (p) =>
            {
                var selectedBienBan = p as BienBanDetail;
                txtSoBienBan = selectedBienBan.SoBienBan;
                GetListCT(selectedBienBan.SoBienBan);
                CT_BienBanDialog dialog = new CT_BienBanDialog();
                dialog.ShowDialog();
            });

            EditCommand = new RelayCommand<object>((p) => true, (p) =>
            {
                var itemData = p as BienBanDetail;
                GetListKeToan();
                GetListCT(itemData.SoBienBan);
                BtnContent = "Lưu";
                txtSoBienBan = itemData.SoBienBan;
                selectedMaKho = itemData.MaKho;
                selectedNgayLap = itemData.NgayLap;
                selectedTruongBan = itemData.TruongBan;
                selectedUyVien1 = itemData.UyVien1;
                selectedUyVien2 = itemData.UyVien2;
                BienBanDialog dialog = new BienBanDialog();
                dialog.ShowDialog();
            });
            BtnCommand = new RelayCommand<object>((p) =>
            {
                return Valid.IsValid(p as DependencyObject);
            }, (p) =>
            {
                bool isSuccess = true;
                if (BtnContent == "Xong")
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

                    if (CRUD.InsertData("BBKiemKe", bb))
                    {
                        foreach (var item in ListDataCT)
                        {
                            CT_BienBan ctbb = new CT_BienBan
                            {
                                SoBienBan = item.SoBienBan,
                                MaVT = item.MaVT,
                                SLSoSach = item.SLSoSach,
                                SLThucTe = item.SLThucTe,
                                SLThua = item.SLThucTe > item.SLSoSach ? item.SLThucTe - item.SLSoSach : 0,
                                SLThieu = item.SLSoSach > item.SLThucTe ? item.SLSoSach - item.SLThucTe : 0,
                                SLPhamChatTot = item.SLPhamChatTot,
                                SLPhamChatKem = item.SLPhamChatKem,
                                SLMatPhamChat = item.SLMatPhamChat,
                            };
                            isSuccess = CRUD.InsertData("CT_BBKiemKe", ctbb);

                            if (!isSuccess) break;
                        }
                    }
                    else isSuccess = false;

                    if (isSuccess)
                    {
                        LoadTableData();
                        notify.updateDataSuccess("Thêm biên bản thành công");
                    }
                    else notify.updateDataFail();
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
                        if (CRUD.DeleteData("CT_BBKiemKe", txtSoBienBan))
                        {
                            foreach (var item in ListDataCT)
                            {
                                CT_BienBan ctbb = new CT_BienBan
                                {
                                    SoBienBan = item.SoBienBan,
                                    MaVT = item.MaVT,
                                    SLSoSach = item.SLSoSach,
                                    SLThucTe = item.SLThucTe,
                                    SLThua = item.SLThucTe > item.SLSoSach ? item.SLThucTe - item.SLSoSach : 0,
                                    SLThieu = item.SLSoSach > item.SLThucTe ? item.SLSoSach - item.SLThucTe : 0,
                                    SLPhamChatTot = item.SLPhamChatTot,
                                    SLPhamChatKem = item.SLPhamChatKem,
                                    SLMatPhamChat = item.SLMatPhamChat,
                                };
                                isSuccess = CRUD.InsertData("CT_BBKiemKe", ctbb);

                                if (!isSuccess) break;
                            }
                        }
                        else isSuccess = false;
                    }
                    else isSuccess = false;


                    if (isSuccess)
                    {
                        LoadTableData();
                        notify.updateDataSuccess("Cập nhật biên bản thành công");
                    }
                    else notify.updateDataFail();
                }
                ((Window)p).Close();
            });
            DeleteItemCommand = new RelayCommand<object>((p) => true, (p) =>
            {
                var itemData = p as BienBanDetail;
                DeleteData(itemData.SoBienBan);
            });

            AddCommandCT = new RelayCommand<object>((p) => selectedMaKho != "" && selectedNgayLap != null, (p) =>
             {
                 //ListDataCT = getlist
             });

            DeleteItemCommandCT = new RelayCommand<object>((p) => true, (p) =>
            {
                ListDataCT.Remove(p as CT_BienBanDetail);
            });
        }

        public void LoadNewData()
        {
            LoadTableData();
            GetListKho();
            GetListVatTu();
            GetListKeToan();
            notify.init();
        }

        public override void InitFilter()
        {
            Search = "";
        }

        public override void ClearTextboxValue()
        {
            selectedMaKho = string.Empty;
            selectedNgayLap = DateTime.Now;
            selectedTruongBan = string.Empty;
            selectedUyVien1 = string.Empty;
            selectedUyVien2 = string.Empty;
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
        public void ExportBienBanKiemKe(BienBanDetail selectedBienBan)
        {
            string Ngay = selectedBienBan.NgayLap.Day.ToString();
            string Thang = selectedBienBan.NgayLap.Month.ToString();
            string Nam = selectedBienBan.NgayLap.Year.ToString();
            using (SaveFileDialog sfd = new SaveFileDialog() { FileName = "Biên bản kiểm kê ngày " + Ngay + "-" + Thang + "-" + Nam, Filter = "Word Document | *.docx", ValidateNames = true })
            {
                if (sfd.ShowDialog() == DialogResult.OK)
                {
                    notify.IsProcessing = true;
                    Thread thread = new Thread(new ThreadStart(() =>
                    {
                        try
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
                        catch
                        {
                            notify.IsProcessing = false;
                            notify.updateDataFail("Xuất file thất bại");
                        }
                        notify.IsProcessing = false;
                    }));
                    thread.IsBackground = true;
                    thread.Start();
                }
            }
        }
    }
}
