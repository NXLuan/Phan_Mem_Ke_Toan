﻿using Phan_Mem_Ke_Toan.View;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Input;

namespace Phan_Mem_Ke_Toan.ViewModel
{
    class MainViewModel : BaseViewModel
    {
        public static MainViewModel Instance;

        private string _titleOption;
        public string TitleOption
        {
            get => _titleOption;
            set => SetProperty(ref _titleOption, value);
        }
        private int _selectedIndexMenu;
        public int SelectedIndexMenu
        {
            get => _selectedIndexMenu;
            set
            {
                SetProperty(ref _selectedIndexMenu, value);
                NhomChucNangVMs = Menu[_selectedIndexMenu].NhomChucNangVMs;
                TitleOption = Menu[_selectedIndexMenu].text;
            }
        }

        private int _selectedIndexWorking;
        public int SelectedIndexWorking
        {
            get => _selectedIndexWorking;
            set
            {
                SetProperty(ref _selectedIndexWorking, value);
                CurrentPage = PageWorkings[value].page;
            }
        }

        private UserControl _currentPage;
        public UserControl CurrentPage
        {
            get => _currentPage;
            set
            {
                SetProperty(ref _currentPage, value);
            }
        }
        public ObservableCollection<MenuViewModel> Menu { get; set; }
        public ObservableCollection<ChucNangViewModel> PageWorkings { get; set; }

        private ObservableCollection<NhomChucNangViewModel> _nhomChucNangVMs;
        public ObservableCollection<NhomChucNangViewModel> NhomChucNangVMs
        {
            get => _nhomChucNangVMs;
            set => SetProperty(ref _nhomChucNangVMs, value);
        }

        public MainViewModel()
        {
            Instance = this;

            Menu = new ObservableCollection<MenuViewModel>()
            {
                new MenuViewModel(){ icon="ViewDashboard", text="Bảng điều khiển", NhomChucNangVMs = new ObservableCollection<NhomChucNangViewModel>(){
                }},
                new MenuViewModel(){ icon="Cog", text="Hệ thống", NhomChucNangVMs = new ObservableCollection<NhomChucNangViewModel>(){
                    new NhomChucNangViewModel() { Title="Quản trị người dùng", ChucNangVMs=new ObservableCollection<ChucNangViewModel>(){
                        new ChucNangViewModel() { text="Đổi mật khẩu", icon="Key", iconColor="#FAAD14" },
                        new ChucNangViewModel() { text="Quản trị người dùng", icon="AccountDetails", iconColor="#3773E1", page = new UserControl2() },
                    }},
                    new NhomChucNangViewModel() { Title="Thoát", ChucNangVMs=new ObservableCollection<ChucNangViewModel>(){
                        new ChucNangViewModel() { text="Đăng xuất", icon="Logout", iconColor="#E80D00" },
                    }},
                }},
                new MenuViewModel(){ icon="Layers", text="Danh mục", NhomChucNangVMs = new ObservableCollection<NhomChucNangViewModel>(){
                    new NhomChucNangViewModel() { Title="Tài khoản", ChucNangVMs=new ObservableCollection<ChucNangViewModel>(){
                        new ChucNangViewModel() { text="Tài khoản", icon="CreditCardMultiple", iconColor="#4B32C3" },
                    }},
                    new NhomChucNangViewModel() { Title="Đối tượng", ChucNangVMs=new ObservableCollection<ChucNangViewModel>(){
                        new ChucNangViewModel() { text="Nhà cung cấp", icon="AccountTie", iconColor="#000000" },
                        new ChucNangViewModel() { text="Người giao", icon="AccountCowboyHat", iconColor="#F5DE19" },
                        new ChucNangViewModel() { text="Bộ phận", icon="AccountGroup", iconColor="#4630EB" },
                        new ChucNangViewModel() { text="Nhân viên", icon="HumanChild", iconColor="#01A5F4" },
                        new ChucNangViewModel() { text="Người nhận", icon="AccountHardHat", iconColor="#DD4C35", page= new UserControl1() },
                    }},
                    new NhomChucNangViewModel() { Title="Kho - Vật tư", ChucNangVMs=new ObservableCollection<ChucNangViewModel>(){
                        new ChucNangViewModel() { text="Kho vật tư", icon="Warehouse", iconColor="#06CC14" },
                        new ChucNangViewModel() { text="Loại vật tư", icon="Cube", iconColor="#B17CFF" },
                        new ChucNangViewModel() { text="Vật tư", icon="Wall", iconColor="#FB7604" },
                    }},
                    new NhomChucNangViewModel() { Title="Công trình", ChucNangVMs=new ObservableCollection<ChucNangViewModel>(){
                        new ChucNangViewModel() { text="Công trình", icon="OfficeBuilding", iconColor="#4DCC89" },
                    }},
                }},
                new MenuViewModel(){ icon="FileSwap", text="Chứng từ" },
                new MenuViewModel(){ icon="Finance", text="Báo cáo" },
                new MenuViewModel(){ icon="HelpCircle", text="Trợ giúp" },
            };

            PageWorkings = new ObservableCollection<ChucNangViewModel>();
            SelectedIndexMenu = 0;
        }
    }
}