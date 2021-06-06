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
    public class BoPhanViewModel : BaseViewModel
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

        private string _txtMaBoPhan;
        public string txtMaBoPhan
        {
            get => _txtMaBoPhan;
            set => SetProperty(ref _txtMaBoPhan, value);
        }

        private string _txtTenBoPhan;
        public string txtTenBoPhan
        {
            get => _txtTenBoPhan;
            set => SetProperty(ref _txtTenBoPhan, value);
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

        private ObservableCollection<BoPhan> _listData;
        public ObservableCollection<BoPhan> ListData
        {
            get => _listData;
            set => SetProperty(ref _listData, value);
        }
        public void ClearTextboxValue()
        {
            txtMaBoPhan = string.Empty;
            txtTenBoPhan = string.Empty;
            txtMoTa = string.Empty;
        }
        public BoPhanViewModel()
        {
            AddCommand = new RelayCommand<object>((p) => true, (p) =>
            {
                BoPhanDialog dialog = new BoPhanDialog();
                TitleDialog = "Thêm bộ phận";
                BtnContent = "Thêm";
                tbVisibility = "Collapsed";
                ClearTextboxValue();
                dialog.ShowDialog();
            });

            EditCommand = new RelayCommand<object>((p) => true, (p) =>
            {
                BoPhanDialog dialog = new BoPhanDialog();
                TitleDialog = "Cập nhật bộ phận";
                BtnContent = "Lưu";
                tbVisibility = "Visible";
                var itemData = p as BoPhan;
                txtMaBoPhan = itemData.MaBoPhan;
                txtTenBoPhan = itemData.TenBoPhan;
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
                    BoPhan bp = new BoPhan
                    {
                        MaBoPhan = ListData.Count() == 0 ? "BP001" : CRUD.GeneratePrimaryKey(ListData[ListData.Count() - 1].MaBoPhan),
                        TenBoPhan = txtTenBoPhan,
                        MoTa = txtMoTa,
                    };
                    if (CRUD.InsertData("bophan", bp))
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
                    BoPhan bp = new BoPhan
                    {
                        MaBoPhan = txtMaBoPhan,
                        TenBoPhan = txtTenBoPhan,
                        MoTa = txtMoTa,
                    };
                    if (CRUD.UpdateData("bophan", bp))
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
                var itemData = p as BoPhan;
                if (CRUD.DeleteData("bophan", itemData.MaBoPhan))
                {
                    MessageBox.Show("Thành công!", "Thông báo", MessageBoxButton.OK, MessageBoxImage.Information);
                    LoadTableData();
                }
                else
                {
                    MessageBox.Show("Đã có lỗi xảy ra, vui lòng thử lại sau", "Thông báo", MessageBoxButton.OK, MessageBoxImage.Error);
                }

            });
            LoadTableData();
        }
        public void LoadTableData()
        {
            string JsonData = CRUD.GetJsonData("bophan");
            ListData = JsonConvert.DeserializeObject<ObservableCollection<BoPhan>>(JsonData);
        }
    }
}
