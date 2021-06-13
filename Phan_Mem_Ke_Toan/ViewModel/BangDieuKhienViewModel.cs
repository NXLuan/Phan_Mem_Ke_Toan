using Phan_Mem_Ke_Toan.API;
using Phan_Mem_Ke_Toan.View;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;

namespace Phan_Mem_Ke_Toan.ViewModel
{
    class BangDieuKhienViewModel : BaseViewModel
    {
        public ICommand CalculateCommand { get; set; }
        public ICommand DialogCalculateCommand { get; set; }
        private DateTime _selectedNgayBD;
        public DateTime selectedNgayBD
        {
            get => _selectedNgayBD;
            set => SetProperty(ref _selectedNgayBD, value);
        }
        private DateTime _selectedNgayKT;
        public DateTime selectedNgayKT
        {
            get => _selectedNgayKT;
            set => SetProperty(ref _selectedNgayKT, value);
        }

        public BangDieuKhienViewModel()
        {
            Event();
        }

        private void Event()
        {
            CalculateCommand = new RelayCommand<object>((p) => true, (p) =>
            {
                selectedNgayBD = DateTime.Now;
                selectedNgayKT = DateTime.Now;
                KhoCalculateDialog dialog = new KhoCalculateDialog();
                dialog.ShowDialog();
            });

            DialogCalculateCommand = new RelayCommand<object>((p) => true, (p) =>
            {
                if (CRUD.TinhGiaXuatKho(selectedNgayBD, selectedNgayKT))
                {
                    MessageBox.Show("Thành công!", "Thông báo", MessageBoxButton.OK, MessageBoxImage.Information);
                    ((Window)p).Close();
                }
                else
                {
                    MessageBox.Show("Đã có lỗi xảy ra, vui lòng thử lại sau", "Thông báo", MessageBoxButton.OK, MessageBoxImage.Error);
                }
            });
        }
    }
}
