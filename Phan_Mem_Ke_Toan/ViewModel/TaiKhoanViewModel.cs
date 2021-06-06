﻿using Phan_Mem_Ke_Toan.API;
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
    public class TaiKhoanViewModel : BaseViewModel
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

        private string _txtMaTK;
        public string txtMaTK
        {
            get => _txtMaTK;
            set => SetProperty(ref _txtMaTK, value);
        }

        private string _txtTenTK;
        public string txtTenTK
        {
            get => _txtTenTK;
            set => SetProperty(ref _txtTenTK, value);
        }
        private int _selectedCapTK;
        public int selectedCapTK
        {
            get => _selectedCapTK;
            set
            {
                SetProperty(ref _selectedCapTK, value);
                GetListTK(selectedCapTK - 1);
            }
        }
        private string _selectedTK;
        public string selectedTK
        {
            get => _selectedTK;
            set => SetProperty(ref _selectedTK, value);

        }
        private string _txtLoaiTK;
        public string txtLoaiTK
        {
            get => _txtLoaiTK;
            set => SetProperty(ref _txtLoaiTK, value);
        }
        private bool _MaTKEnable;
        public bool MaTKEnable 
        {
            get => _MaTKEnable;
            set => SetProperty(ref _MaTKEnable, value);
        }

        public ICommand AddCommand { get; set; }
        public ICommand EditCommand { get; set; }
        public ICommand BtnCommand { get; set; }
        public ICommand DeleteItemCommand { get; set; }

        private ObservableCollection<TaiKhoan> _listData;
        public ObservableCollection<TaiKhoan> ListData
        {
            get => _listData;
            set => SetProperty(ref _listData, value);
        }
        public ObservableCollection<int> ListCapTK { get; set; }
        private ObservableCollection<TaiKhoan> _listTKMe;
        public ObservableCollection<TaiKhoan> ListTKMe
        {
            get => _listTKMe;
            set => SetProperty(ref _listTKMe, value);
        }
        public void ClearTextboxValue()
        {
            txtTenTK = string.Empty;
            txtMaTK = string.Empty;
            selectedCapTK = 1;
            txtLoaiTK = string.Empty;
        }
        public TaiKhoanViewModel()
        {
            ListCapTK = new ObservableCollection<int>() { 1, 2, 3 };
            AddCommand = new RelayCommand<object>((p) => true, (p) =>
            {
                TaiKhoanDialog dialog = new TaiKhoanDialog();
                TitleDialog = "Thêm tài khoản";
                BtnContent = "Thêm";
                MaTKEnable = true;
                ClearTextboxValue();
                dialog.ShowDialog();
            });

            EditCommand = new RelayCommand<object>((p) => true, (p) =>
            {
                TaiKhoanDialog dialog = new TaiKhoanDialog();
                TitleDialog = "Cập nhật tài khoản";
                BtnContent = "Lưu";
                MaTKEnable = false;
                var itemData = p as TaiKhoan;
                txtMaTK = itemData.MaTK;
                txtTenTK = itemData.TenTK;
                selectedCapTK = itemData.CapTK;
                selectedTK = itemData.TKMe;
                txtLoaiTK = itemData.LoaiTK;
                dialog.ShowDialog();
            });
            BtnCommand = new RelayCommand<object>((p) =>
            {
                return Valid.IsValid(p as DependencyObject);
            }, (p) =>
            {
                TaiKhoan tk = new TaiKhoan
                {
                    MaTK = txtMaTK,
                    TenTK = txtTenTK,
                    CapTK = selectedCapTK,
                    TKMe = selectedTK,
                    LoaiTK = txtLoaiTK,
                };
                if (BtnContent == "Thêm")
                {
                    if (CRUD.InsertData("TaiKhoan", tk))
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
                    if (CRUD.UpdateData("TaiKhoan", tk))
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
                var itemData = p as TaiKhoan;
                if (CRUD.DeleteData("TaiKhoan", itemData.MaTK))
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
            string JsonData = CRUD.GetJsonData("TaiKhoan");
            ListData = JsonConvert.DeserializeObject<ObservableCollection<TaiKhoan>>(JsonData);
        }
        public void GetListTK(int CapTK)
        {
            ListTKMe = new ObservableCollection<TaiKhoan>();
            foreach (var item in ListData)
            {
                if (item.CapTK == CapTK)
                {
                    item.TenTK = item.MaTK.ToString() + " - " + item.TenTK;
                    ListTKMe.Add(item);
                }
            }
        }
    }
}
