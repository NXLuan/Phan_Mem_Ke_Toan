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
    public class NguoiGiaoViewModel : BaseViewModel
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
        private string _txtMaNguoiGiao;
        public string txtMaNguoiGiao
        {
            get => _txtMaNguoiGiao;
            set => SetProperty(ref _txtMaNguoiGiao, value);
        }
        private string _txtTenNguoiGiao;
        public string txtTenNguoiGiao
        {
            get => _txtTenNguoiGiao;
            set => SetProperty(ref _txtTenNguoiGiao, value);
        }
        private string _txtDiaChi;
        public string txtDiaChi 
        { 
            get => _txtDiaChi;
            set => SetProperty(ref _txtDiaChi, value);
        }
        private string _selectedMaNCC;
        public string selectedMaNCC
        {
            get => _selectedMaNCC;
            set => SetProperty(ref _selectedMaNCC, value);
        }

        public ICommand AddCommand { get; set; }
        public ICommand EditCommand { get; set; }
        public ICommand BtnCommand { get; set; }
        public ICommand DeleteItemCommand { get; set; }

        private ObservableCollection<NguoiGiaoDetail> _listData;
        public ObservableCollection<NguoiGiaoDetail> ListData
        {
            get => _listData;
            set => SetProperty(ref _listData, value);
        }
        private ObservableCollection<NhaCungCap> _ListNCC;
        public ObservableCollection<NhaCungCap> ListNCC
        {
            get => _ListNCC;
            set => SetProperty(ref _ListNCC, value);
        }
        public void ClearTextboxValue()
        {
            txtMaNguoiGiao = string.Empty;
            txtTenNguoiGiao = string.Empty;
            txtDiaChi = string.Empty;
            selectedMaNCC = string.Empty;
        }
        public NguoiGiaoViewModel()
        {
            AddCommand = new RelayCommand<object>((p) => true, (p) =>
            {
                NguoiGiaoDialog dialog = new NguoiGiaoDialog();
                TitleDialog = "Thêm người giao";
                BtnContent = "Thêm";
                tbVisibility = "Collapsed";
                ClearTextboxValue();
                dialog.ShowDialog();
            });

            EditCommand = new RelayCommand<object>((p) => true, (p) =>
            {
                NguoiGiaoDialog dialog = new NguoiGiaoDialog();
                TitleDialog = "Cập nhật người giao";
                BtnContent = "Lưu";
                tbVisibility = "Visible";
                var itemData = p as NguoiGiaoDetail;
                txtMaNguoiGiao = itemData.MaNguoiGiao;
                txtTenNguoiGiao = itemData.TenNguoiGiao;
                txtDiaChi = itemData.DiaChi;
                selectedMaNCC = itemData.MaNCC;
                dialog.ShowDialog();
            });
            BtnCommand = new RelayCommand<object>((p) =>
            {
                return Valid.IsValid(p as DependencyObject);
            }, (p) =>
            {
                if (BtnContent == "Thêm")
                {
                    NguoiGiao ng = new NguoiGiao
                    {
                        MaNguoiGiao = ListData.Count() == 0 ? "NG001" : CRUD.GeneratePrimaryKey(ListData[ListData.Count() - 1].MaNguoiGiao),
                        TenNguoiGiao = txtTenNguoiGiao,
                        DiaChi = txtDiaChi,
                        MaNCC = selectedMaNCC == "" ? null : selectedMaNCC,
                    };
                    if (CRUD.InsertData("NguoiGiao", ng))
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
                    NguoiGiao ng = new NguoiGiao
                    {
                        MaNguoiGiao = txtMaNguoiGiao,
                        TenNguoiGiao = txtTenNguoiGiao,
                        DiaChi = txtDiaChi,
                        MaNCC = selectedMaNCC == "" ? null : selectedMaNCC,
                    };
                    if (CRUD.UpdateData("NguoiGiao", ng))
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
                var itemData = p as NguoiGiaoDetail;
                if (CRUD.DeleteData("NguoiGiao", itemData.MaNguoiGiao))
                {
                    Console.WriteLine("Success");
                    LoadTableData();
                }

            });
            GetListNCC();
            LoadTableData();
        }
        public void LoadTableData()
        {
            string JsonData = CRUD.GetJoinTableData("NguoiGiao");
            ListData = JsonConvert.DeserializeObject<ObservableCollection<NguoiGiaoDetail>>(JsonData);
        }
        public void GetListNCC()
        {
            string data = CRUD.GetJsonData("NhaCungCap");
            ListNCC = JsonConvert.DeserializeObject<ObservableCollection<NhaCungCap>>(data);
        }
    }
}
