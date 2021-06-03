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
    public class CongTrinhViewModel : BaseViewModel
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

        private string _txtMaCongTrinh;
        public string txtMaCongTrinh
        {
            get => _txtMaCongTrinh;
            set => SetProperty(ref _txtMaCongTrinh, value);
        }

        private string _txtTenCongTrinh;
        public string txtTenCongTrinh
        {
            get => _txtTenCongTrinh;
            set => SetProperty(ref _txtTenCongTrinh, value);
        }
        private string _txtDiaChi;
        public string txtDiaChi
        {
            get => _txtDiaChi;
            set => SetProperty(ref _txtDiaChi, value);
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

        private ObservableCollection<CongTrinh> _listData;
        public ObservableCollection<CongTrinh> ListData
        {
            get => _listData;
            set => SetProperty(ref _listData, value);
        }
        public void ClearTextboxValue()
        {
            txtMaCongTrinh = string.Empty;
            txtTenCongTrinh = string.Empty;
            txtDiaChi = string.Empty;
            txtMoTa = string.Empty;
        }
        public CongTrinhViewModel()
        {
            AddCommand = new RelayCommand<object>((p) => true, (p) =>
            {
                CongTrinhDialog dialog = new CongTrinhDialog();
                TitleDialog = "Thêm công trình";
                BtnContent = "Thêm";
                tbVisibility = "Collapsed";
                ClearTextboxValue();
                dialog.ShowDialog();
            });

            EditCommand = new RelayCommand<object>((p) => true, (p) =>
            { 
                CongTrinhDialog dialog = new CongTrinhDialog();
                TitleDialog = "Cập nhật công trình";
                BtnContent = "Lưu";
                tbVisibility = "Visible";
                var itemData = p as CongTrinh;
                txtMaCongTrinh = itemData.MaCongTrinh;
                txtTenCongTrinh = itemData.TenCongTrinh;
                txtDiaChi = itemData.DiaChi;
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
                    CongTrinh ct = new CongTrinh
                    {
                        MaCongTrinh = ListData.Count() == 0 ? "CT001" : CRUD.GeneratePrimaryKey(ListData[ListData.Count() - 1].MaCongTrinh),
                        TenCongTrinh = txtTenCongTrinh,
                        DiaChi = txtDiaChi,
                        MoTa = txtMoTa,
                    };
                    if (CRUD.InsertData("CongTrinh", ct))
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
                    CongTrinh ct = new CongTrinh
                    {
                        MaCongTrinh = txtMaCongTrinh,
                        TenCongTrinh = txtTenCongTrinh,
                        DiaChi = txtDiaChi,
                        MoTa = txtMoTa,
                    };
                    if (CRUD.UpdateData("CongTrinh", ct))
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
                var itemData = p as CongTrinh;
                if (CRUD.DeleteData("CongTrinh", itemData.MaCongTrinh))
                {
                    Console.WriteLine("Success");
                    LoadTableData();
                }

            });
            LoadTableData();
        }
        public void LoadTableData()
        {
            string JsonData = CRUD.GetJsonData("CongTrinh");
            ListData = JsonConvert.DeserializeObject<ObservableCollection<CongTrinh>>(JsonData);
        }
    }
}
