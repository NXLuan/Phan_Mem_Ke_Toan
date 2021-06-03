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
    public class LoaiVatTuViewModel : BaseViewModel
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

        private string _txtMaLoai;
        public string txtMaLoai
        {
            get => _txtMaLoai;
            set => SetProperty(ref _txtMaLoai, value);
        }

        private string _txtTenLoai;
        public string txtTenLoai
        {
            get => _txtTenLoai;
            set => SetProperty(ref _txtTenLoai, value);
        }
        private string _txtMoTa;
        public string txtMoTa
        {
            get => _txtMoTa;
            set => SetProperty(ref _txtMoTa, value);
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

        private ObservableCollection<LoaiVatTu> _listData;
        public ObservableCollection<LoaiVatTu> ListData
        {
            get => _listData;
            set => SetProperty(ref _listData, value);
        }
        public void ClearTextboxValue()
        {
            txtMaLoai = string.Empty;
            txtTenLoai = string.Empty;
            txtMoTa = string.Empty;
        }
        public LoaiVatTuViewModel()
        {
            AddCommand = new RelayCommand<object>((p) => true, (p) =>
            {
                LoaiVatTuDialog dialog = new LoaiVatTuDialog();
                TitleDialog = "Thêm loại vật tư";
                BtnContent = "Thêm";
                tbVisibility = "Collapsed";
                ClearTextboxValue();
                dialog.ShowDialog();
            });

            EditCommand = new RelayCommand<object>((p) => true, (p) =>
            {
                LoaiVatTuDialog dialog = new LoaiVatTuDialog();
                TitleDialog = "Cập nhật loại vật tư";
                BtnContent = "Lưu";
                tbVisibility = "Visible";
                var itemData = p as LoaiVatTu;
                txtMaLoai = itemData.MaLoai;
                txtTenLoai = itemData.TenLoai;
                txtMoTa = itemData.MoTa;
                dialog.ShowDialog();
            });
            BtnCommand = new RelayCommand<object>((p) =>
            {
                return Valid.IsValid(p as DependencyObject);
            }, (p) =>
            {
                if (BtnContent == "Thêm")
                {
                    LoaiVatTu lvt = new LoaiVatTu
                    {
                        MaLoai = ListData.Count() == 0 ? "LVT001" : CRUD.GeneratePrimaryKey(ListData[ListData.Count() - 1].MaLoai),
                        TenLoai = txtTenLoai,
                        MoTa = txtMoTa,
                    };
                    if (CRUD.InsertData("loaivattu", lvt))
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
                    LoaiVatTu lvt = new LoaiVatTu
                    {
                        MaLoai = txtMaLoai,
                        TenLoai = txtTenLoai,
                        MoTa = txtMoTa,
                    };
                    if (CRUD.UpdateData("loaivattu", lvt))
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
                var itemData = p as LoaiVatTu;
                if (CRUD.DeleteData("loaivattu", itemData.MaLoai))
                {
                    Console.WriteLine("Success");
                    LoadTableData();
                }

            });
            LoadTableData();
        }
        public void LoadTableData()
        {
            string JsonData = CRUD.GetJsonData("loaivattu");
            ListData = JsonConvert.DeserializeObject<ObservableCollection<LoaiVatTu>>(JsonData);
        }
    }
}
