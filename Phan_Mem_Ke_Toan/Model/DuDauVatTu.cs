﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Phan_Mem_Ke_Toan.Model
{
    public class DuDauVatTu
    {
        public int MaSo { get; set; }
        public string MaVT { get; set; }
        public string MaKho { get; set; }
        public DateTime Ngay { get; set; }
        public double SoLuong { get; set; }
        public decimal DonGia { get; set; }
        public decimal ThanhTien { get; set; }
    }
}
