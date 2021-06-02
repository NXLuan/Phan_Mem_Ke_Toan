-- phpMyAdmin SQL Dump
-- version 4.9.0.1
-- https://www.phpmyadmin.net/
--
-- Host: 127.0.0.1
-- Generation Time: May 19, 2021 at 09:16 AM
-- Server version: 10.3.16-MariaDB
-- PHP Version: 7.3.6

SET SQL_MODE = "NO_AUTO_VALUE_ON_ZERO";
SET AUTOCOMMIT = 0;
START TRANSACTION;
SET time_zone = "+00:00";


/*!40101 SET @OLD_CHARACTER_SET_CLIENT=@@CHARACTER_SET_CLIENT */;
/*!40101 SET @OLD_CHARACTER_SET_RESULTS=@@CHARACTER_SET_RESULTS */;
/*!40101 SET @OLD_COLLATION_CONNECTION=@@COLLATION_CONNECTION */;
/*!40101 SET NAMES utf8mb4 */;

--
-- Database: `ketoan`
--

-- --------------------------------------------------------

--
-- Table structure for table `bbkiemke`
--

CREATE TABLE `bbkiemke` (
  `SoBienBan` varchar(50) COLLATE utf8_vietnamese_ci NOT NULL,
  `NgayLap` date NOT NULL,
  `MaKho` varchar(50) COLLATE utf8_vietnamese_ci NOT NULL,
  `TruongBan` varchar(50) COLLATE utf8_vietnamese_ci NOT NULL,
  `UyVien1` varchar(50) COLLATE utf8_vietnamese_ci NOT NULL,
  `UyVien2` varchar(50) COLLATE utf8_vietnamese_ci NOT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8 COLLATE=utf8_vietnamese_ci;

-- --------------------------------------------------------

--
-- Table structure for table `bophan`
--

CREATE TABLE `bophan` (
  `MaBoPhan` varchar(50) COLLATE utf8_vietnamese_ci NOT NULL,
  `TenBoPhan` varchar(50) COLLATE utf8_vietnamese_ci NOT NULL,
  `MoTa` varchar(100) COLLATE utf8_vietnamese_ci NOT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8 COLLATE=utf8_vietnamese_ci;

--
-- Dumping data for table `bophan`
--

INSERT INTO `bophan` (`MaBoPhan`, `TenBoPhan`, `MoTa`) VALUES
('BP001', 'Kiểm toán', '');

-- --------------------------------------------------------

--
-- Table structure for table `congtrinh`
--

CREATE TABLE `congtrinh` (
  `MaCongTrinh` varchar(50) COLLATE utf8_vietnamese_ci NOT NULL,
  `TenCongTrinh` varchar(50) COLLATE utf8_vietnamese_ci NOT NULL,
  `DiaChi` varchar(100) COLLATE utf8_vietnamese_ci NOT NULL,
  `MoTa` varchar(100) COLLATE utf8_vietnamese_ci NOT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8 COLLATE=utf8_vietnamese_ci;

--
-- Dumping data for table `congtrinh`
--

INSERT INTO `congtrinh` (`MaCongTrinh`, `TenCongTrinh`, `DiaChi`, `MoTa`) VALUES
('CT001', 'Công trình 1', '', '');

-- --------------------------------------------------------

--
-- Table structure for table `ct_bbkiemke`
--

CREATE TABLE `ct_bbkiemke` (
  `MaSo` int(11) NOT NULL,
  `SoBienBan` varchar(50) COLLATE utf8_vietnamese_ci NOT NULL,
  `MaVT` varchar(50) COLLATE utf8_vietnamese_ci NOT NULL,
  `SLSoSach` double NOT NULL,
  `SLThucTe` double NOT NULL,
  `SLThua` double NOT NULL,
  `SLThieu` double NOT NULL,
  `SLPhamChatTot` double NOT NULL,
  `SLPhamChatKem` double NOT NULL,
  `SLMatPhamChat` double NOT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8 COLLATE=utf8_vietnamese_ci;

-- --------------------------------------------------------

--
-- Table structure for table `ct_phieunhap`
--

CREATE TABLE `ct_phieunhap` (
  `MaSo` int(11) NOT NULL,
  `SoPhieu` varchar(50) COLLATE utf8_vietnamese_ci NOT NULL,
  `MaVT` varchar(50) COLLATE utf8_vietnamese_ci NOT NULL,
  `TKNo` varchar(50) COLLATE utf8_vietnamese_ci NOT NULL,
  `TKCo` varchar(50) COLLATE utf8_vietnamese_ci NOT NULL,
  `SoLuong` double NOT NULL,
  `DonGia` decimal(10,0) NOT NULL,
  `ThanhTien` decimal(10,0) NOT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8 COLLATE=utf8_vietnamese_ci;

-- --------------------------------------------------------

--
-- Table structure for table `ct_phieuxuat`
--

CREATE TABLE `ct_phieuxuat` (
  `MaSo` int(11) NOT NULL,
  `SoPhieu` varchar(50) COLLATE utf8_vietnamese_ci NOT NULL,
  `MaVT` varchar(50) COLLATE utf8_vietnamese_ci NOT NULL,
  `TKNo` varchar(50) COLLATE utf8_vietnamese_ci NOT NULL,
  `TKCo` varchar(50) COLLATE utf8_vietnamese_ci NOT NULL,
  `SoLuong` double NOT NULL,
  `DonGia` decimal(10,0) NOT NULL,
  `ThanhTien` decimal(10,0) NOT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8 COLLATE=utf8_vietnamese_ci;

-- --------------------------------------------------------

--
-- Table structure for table `donvitinh`
--

CREATE TABLE `donvitinh` (
  `MaDVT` varchar(50) COLLATE utf8_vietnamese_ci NOT NULL,
  `TenDVT` varchar(50) COLLATE utf8_vietnamese_ci NOT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8 COLLATE=utf8_vietnamese_ci;

--
-- Dumping data for table `donvitinh`
--

INSERT INTO `donvitinh` (`MaDVT`, `TenDVT`) VALUES
('DVT001', 'tấn');

-- --------------------------------------------------------

--
-- Table structure for table `dudauvattu`
--

CREATE TABLE `dudauvattu` (
  `MaSo` int(11) NOT NULL,
  `MaVT` varchar(50) COLLATE utf8_vietnamese_ci NOT NULL,
  `MaKho` varchar(50) COLLATE utf8_vietnamese_ci NOT NULL,
  `Ngay` date NOT NULL,
  `SoLuong` double NOT NULL,
  `DonGia` decimal(10,0) NOT NULL,
  `ThanhTien` decimal(10,0) NOT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8 COLLATE=utf8_vietnamese_ci;

--
-- Dumping data for table `dudauvattu`
--

INSERT INTO `dudauvattu` (`MaSo`, `MaVT`, `MaKho`, `Ngay`, `SoLuong`, `DonGia`, `ThanhTien`) VALUES
(2, 'sat001', 'K001', '2021-05-19', 10, '1000000', '10000000');

-- --------------------------------------------------------

--
-- Table structure for table `kho`
--

CREATE TABLE `kho` (
  `MaKho` varchar(50) COLLATE utf8_vietnamese_ci NOT NULL,
  `TenKho` varchar(50) COLLATE utf8_vietnamese_ci NOT NULL,
  `DiaChi` varchar(100) COLLATE utf8_vietnamese_ci NOT NULL,
  `SDT` varchar(10) COLLATE utf8_vietnamese_ci NOT NULL,
  `MaThuKho` varchar(50) COLLATE utf8_vietnamese_ci NOT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8 COLLATE=utf8_vietnamese_ci;

--
-- Dumping data for table `kho`
--

INSERT INTO `kho` (`MaKho`, `TenKho`, `DiaChi`, `SDT`, `MaThuKho`) VALUES
('K001', 'Kho 1', '01 Hồng Chương', '', 'NV001');

-- --------------------------------------------------------

--
-- Table structure for table `loaivattu`
--

CREATE TABLE `loaivattu` (
  `MaLoai` varchar(50) COLLATE utf8_vietnamese_ci NOT NULL,
  `TenLoai` varchar(50) COLLATE utf8_vietnamese_ci NOT NULL,
  `MoTa` varchar(100) COLLATE utf8_vietnamese_ci NOT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8 COLLATE=utf8_vietnamese_ci;

--
-- Dumping data for table `loaivattu`
--

INSERT INTO `loaivattu` (`MaLoai`, `TenLoai`, `MoTa`) VALUES
('NVL', 'Nguyên vật liệu', '');

-- --------------------------------------------------------

--
-- Table structure for table `nguoigiao`
--

CREATE TABLE `nguoigiao` (
  `MaNguoiGiao` varchar(50) COLLATE utf8_vietnamese_ci NOT NULL,
  `TenNguoiGiao` varchar(50) COLLATE utf8_vietnamese_ci NOT NULL,
  `DiaChi` varchar(100) COLLATE utf8_vietnamese_ci NOT NULL,
  `MaNCC` varchar(50) COLLATE utf8_vietnamese_ci NOT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8 COLLATE=utf8_vietnamese_ci;

--
-- Dumping data for table `nguoigiao`
--

INSERT INTO `nguoigiao` (`MaNguoiGiao`, `TenNguoiGiao`, `DiaChi`, `MaNCC`) VALUES
('NG001', 'Luân', '', 'NCC001');

-- --------------------------------------------------------

--
-- Table structure for table `nguoinhan`
--

CREATE TABLE `nguoinhan` (
  `MaNguoiNhan` varchar(50) COLLATE utf8_vietnamese_ci NOT NULL,
  `TenNguoiNhan` varchar(50) COLLATE utf8_vietnamese_ci NOT NULL,
  `DiaChi` varchar(100) COLLATE utf8_vietnamese_ci NOT NULL,
  `MaCongTrinh` varchar(50) COLLATE utf8_vietnamese_ci NOT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8 COLLATE=utf8_vietnamese_ci;

--
-- Dumping data for table `nguoinhan`
--

INSERT INTO `nguoinhan` (`MaNguoiNhan`, `TenNguoiNhan`, `DiaChi`, `MaCongTrinh`) VALUES
('NN001', 'Tiến', '', 'CT001');

-- --------------------------------------------------------

--
-- Table structure for table `nhacungcap`
--

CREATE TABLE `nhacungcap` (
  `MaNCC` varchar(50) COLLATE utf8_vietnamese_ci NOT NULL,
  `TenNCC` varchar(50) COLLATE utf8_vietnamese_ci NOT NULL,
  `DiaChi` varchar(100) COLLATE utf8_vietnamese_ci NOT NULL,
  `MaSoThue` varchar(50) COLLATE utf8_vietnamese_ci NOT NULL,
  `SDT` varchar(10) COLLATE utf8_vietnamese_ci NOT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8 COLLATE=utf8_vietnamese_ci;

--
-- Dumping data for table `nhacungcap`
--

INSERT INTO `nhacungcap` (`MaNCC`, `TenNCC`, `DiaChi`, `MaSoThue`, `SDT`) VALUES
('NCC001', 'Nhà cung cấp 1', '', '', '');

-- --------------------------------------------------------

--
-- Table structure for table `nhanvien`
--

CREATE TABLE `nhanvien` (
  `MaNV` varchar(50) COLLATE utf8_vietnamese_ci NOT NULL,
  `TenNV` varchar(50) COLLATE utf8_vietnamese_ci NOT NULL,
  `MaBoPhan` varchar(50) COLLATE utf8_vietnamese_ci NOT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8 COLLATE=utf8_vietnamese_ci;

--
-- Dumping data for table `nhanvien`
--

INSERT INTO `nhanvien` (`MaNV`, `TenNV`, `MaBoPhan`) VALUES
('NV001', 'Cường', 'BP001');

-- --------------------------------------------------------

--
-- Table structure for table `phieunhap`
--

CREATE TABLE `phieunhap` (
  `SoPhieu` varchar(50) COLLATE utf8_vietnamese_ci NOT NULL,
  `NgayNhap` date NOT NULL,
  `MaNCC` varchar(50) COLLATE utf8_vietnamese_ci NOT NULL,
  `MaNguoiGiao` varchar(50) COLLATE utf8_vietnamese_ci NOT NULL,
  `MaKho` varchar(50) COLLATE utf8_vietnamese_ci NOT NULL,
  `LyDo` varchar(100) COLLATE utf8_vietnamese_ci NOT NULL,
  `TongTien` decimal(10,0) NOT NULL,
  `ChungTuLQ` varchar(100) COLLATE utf8_vietnamese_ci NOT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8 COLLATE=utf8_vietnamese_ci;

-- --------------------------------------------------------

--
-- Table structure for table `phieuxuat`
--

CREATE TABLE `phieuxuat` (
  `SoPhieu` varchar(50) COLLATE utf8_vietnamese_ci NOT NULL,
  `NgayXuat` date NOT NULL,
  `MaCongTrinh` varchar(50) COLLATE utf8_vietnamese_ci NOT NULL,
  `MaNguoiNhan` varchar(50) COLLATE utf8_vietnamese_ci NOT NULL,
  `MaKho` varchar(50) COLLATE utf8_vietnamese_ci NOT NULL,
  `LyDo` varchar(100) COLLATE utf8_vietnamese_ci NOT NULL,
  `TongTien` decimal(10,0) NOT NULL,
  `ChungTuLQ` varchar(100) COLLATE utf8_vietnamese_ci NOT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8 COLLATE=utf8_vietnamese_ci;

-- --------------------------------------------------------

--
-- Table structure for table `taikhoan`
--

CREATE TABLE `taikhoan` (
  `MaTK` varchar(50) COLLATE utf8_vietnamese_ci NOT NULL,
  `TenTK` varchar(50) COLLATE utf8_vietnamese_ci NOT NULL,
  `CapTK` int(11) NOT NULL,
  `TKMe` varchar(50) COLLATE utf8_vietnamese_ci NOT NULL,
  `LoaiTK` varchar(50) COLLATE utf8_vietnamese_ci NOT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8 COLLATE=utf8_vietnamese_ci;

--
-- Dumping data for table `taikhoan`
--

INSERT INTO `taikhoan` (`MaTK`, `TenTK`, `CapTK`, `TKMe`, `LoaiTK`) VALUES
('1111', 'Cường', 2, '123', 'VIP');

-- --------------------------------------------------------

--
-- Table structure for table `vattu`
--

CREATE TABLE `vattu` (
  `MaVT` varchar(50) COLLATE utf8_vietnamese_ci NOT NULL,
  `TenVT` varchar(50) COLLATE utf8_vietnamese_ci NOT NULL,
  `MaLoai` varchar(50) COLLATE utf8_vietnamese_ci NOT NULL,
  `MaDVT` varchar(50) COLLATE utf8_vietnamese_ci NOT NULL,
  `MaTK` varchar(50) COLLATE utf8_vietnamese_ci NOT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8 COLLATE=utf8_vietnamese_ci;

--
-- Dumping data for table `vattu`
--

INSERT INTO `vattu` (`MaVT`, `TenVT`, `MaLoai`, `MaDVT`, `MaTK`) VALUES
('sat001', 'Sắt', 'NVL', 'DVT001', '1111');

--
-- Indexes for dumped tables
--

--
-- Indexes for table `bbkiemke`
--
ALTER TABLE `bbkiemke`
  ADD PRIMARY KEY (`SoBienBan`),
  ADD KEY `FK_BB_Kho` (`MaKho`),
  ADD KEY `FK_BB_UyVien1` (`UyVien1`),
  ADD KEY `FK_BB_UyVien2` (`UyVien2`);

--
-- Indexes for table `bophan`
--
ALTER TABLE `bophan`
  ADD PRIMARY KEY (`MaBoPhan`);

--
-- Indexes for table `congtrinh`
--
ALTER TABLE `congtrinh`
  ADD PRIMARY KEY (`MaCongTrinh`);

--
-- Indexes for table `ct_bbkiemke`
--
ALTER TABLE `ct_bbkiemke`
  ADD PRIMARY KEY (`MaSo`),
  ADD KEY `FK_CTKiemKe_BB` (`SoBienBan`),
  ADD KEY `FK_CTKiemKe_VT` (`MaVT`);

--
-- Indexes for table `ct_phieunhap`
--
ALTER TABLE `ct_phieunhap`
  ADD PRIMARY KEY (`MaSo`),
  ADD KEY `FK_CTPN_SoPhieu` (`SoPhieu`),
  ADD KEY `FK_CTPN_VT` (`MaVT`),
  ADD KEY `FK_CTPN_TKNo` (`TKNo`),
  ADD KEY `FK_CTPN_TKCo` (`TKCo`);

--
-- Indexes for table `ct_phieuxuat`
--
ALTER TABLE `ct_phieuxuat`
  ADD PRIMARY KEY (`MaSo`),
  ADD KEY `FK_CTPX_PX` (`SoPhieu`),
  ADD KEY `FK_CTPX_VT` (`MaVT`),
  ADD KEY `FK_CTPX_TKNo` (`TKNo`),
  ADD KEY `FK_CTPX_TKCo` (`TKCo`);

--
-- Indexes for table `donvitinh`
--
ALTER TABLE `donvitinh`
  ADD PRIMARY KEY (`MaDVT`);

--
-- Indexes for table `dudauvattu`
--
ALTER TABLE `dudauvattu`
  ADD PRIMARY KEY (`MaSo`),
  ADD KEY `FK_Ton_VT` (`MaVT`),
  ADD KEY `FK_Ton_Kho` (`MaKho`);

--
-- Indexes for table `kho`
--
ALTER TABLE `kho`
  ADD PRIMARY KEY (`MaKho`),
  ADD KEY `FK_Kho_NhanVien` (`MaThuKho`);

--
-- Indexes for table `loaivattu`
--
ALTER TABLE `loaivattu`
  ADD PRIMARY KEY (`MaLoai`);

--
-- Indexes for table `nguoigiao`
--
ALTER TABLE `nguoigiao`
  ADD PRIMARY KEY (`MaNguoiGiao`),
  ADD KEY `FK_NguoiGiao_NCC` (`MaNCC`);

--
-- Indexes for table `nguoinhan`
--
ALTER TABLE `nguoinhan`
  ADD PRIMARY KEY (`MaNguoiNhan`),
  ADD KEY `FK_NguoiNhan_CongTrinh` (`MaCongTrinh`);

--
-- Indexes for table `nhacungcap`
--
ALTER TABLE `nhacungcap`
  ADD PRIMARY KEY (`MaNCC`);

--
-- Indexes for table `nhanvien`
--
ALTER TABLE `nhanvien`
  ADD PRIMARY KEY (`MaNV`),
  ADD KEY `FK_NhanVien_BoPhan` (`MaBoPhan`);

--
-- Indexes for table `phieunhap`
--
ALTER TABLE `phieunhap`
  ADD PRIMARY KEY (`SoPhieu`),
  ADD KEY `FK_PhieuNhap_NG` (`MaNguoiGiao`),
  ADD KEY `FK_PhieuNhap_Kho` (`MaKho`),
  ADD KEY `FK_PhieuNhap_NCC` (`MaNCC`);

--
-- Indexes for table `phieuxuat`
--
ALTER TABLE `phieuxuat`
  ADD PRIMARY KEY (`SoPhieu`),
  ADD KEY `FK_PhieuXuat_CongTrinh` (`MaCongTrinh`),
  ADD KEY `FK_PX_NN` (`MaNguoiNhan`),
  ADD KEY `FK_PX_Kho` (`MaKho`);

--
-- Indexes for table `taikhoan`
--
ALTER TABLE `taikhoan`
  ADD PRIMARY KEY (`MaTK`);

--
-- Indexes for table `vattu`
--
ALTER TABLE `vattu`
  ADD PRIMARY KEY (`MaVT`),
  ADD KEY `FK_VT_LoaiVT` (`MaLoai`),
  ADD KEY `FK_VT_DVT` (`MaDVT`),
  ADD KEY `FK_VT_TK` (`MaTK`);

--
-- AUTO_INCREMENT for dumped tables
--

--
-- AUTO_INCREMENT for table `ct_bbkiemke`
--
ALTER TABLE `ct_bbkiemke`
  MODIFY `MaSo` int(11) NOT NULL AUTO_INCREMENT;

--
-- AUTO_INCREMENT for table `ct_phieunhap`
--
ALTER TABLE `ct_phieunhap`
  MODIFY `MaSo` int(11) NOT NULL AUTO_INCREMENT;

--
-- AUTO_INCREMENT for table `ct_phieuxuat`
--
ALTER TABLE `ct_phieuxuat`
  MODIFY `MaSo` int(11) NOT NULL AUTO_INCREMENT;

--
-- AUTO_INCREMENT for table `dudauvattu`
--
ALTER TABLE `dudauvattu`
  MODIFY `MaSo` int(11) NOT NULL AUTO_INCREMENT, AUTO_INCREMENT=3;

--
-- Constraints for dumped tables
--

--
-- Constraints for table `bbkiemke`
--
ALTER TABLE `bbkiemke`
  ADD CONSTRAINT `FK_BB_Kho` FOREIGN KEY (`MaKho`) REFERENCES `kho` (`MaKho`),
  ADD CONSTRAINT `FK_BB_UyVien1` FOREIGN KEY (`UyVien1`) REFERENCES `nhanvien` (`MaNV`),
  ADD CONSTRAINT `FK_BB_UyVien2` FOREIGN KEY (`UyVien2`) REFERENCES `nhanvien` (`MaNV`);

--
-- Constraints for table `ct_bbkiemke`
--
ALTER TABLE `ct_bbkiemke`
  ADD CONSTRAINT `FK_CTKiemKe_BB` FOREIGN KEY (`SoBienBan`) REFERENCES `bbkiemke` (`SoBienBan`),
  ADD CONSTRAINT `FK_CTKiemKe_VT` FOREIGN KEY (`MaVT`) REFERENCES `vattu` (`MaVT`);

--
-- Constraints for table `ct_phieunhap`
--
ALTER TABLE `ct_phieunhap`
  ADD CONSTRAINT `FK_CTPN_SoPhieu` FOREIGN KEY (`SoPhieu`) REFERENCES `phieunhap` (`SoPhieu`),
  ADD CONSTRAINT `FK_CTPN_TKCo` FOREIGN KEY (`TKCo`) REFERENCES `taikhoan` (`MaTK`),
  ADD CONSTRAINT `FK_CTPN_TKNo` FOREIGN KEY (`TKNo`) REFERENCES `taikhoan` (`MaTK`),
  ADD CONSTRAINT `FK_CTPN_VT` FOREIGN KEY (`MaVT`) REFERENCES `vattu` (`MaVT`);

--
-- Constraints for table `ct_phieuxuat`
--
ALTER TABLE `ct_phieuxuat`
  ADD CONSTRAINT `FK_CTPX_PX` FOREIGN KEY (`SoPhieu`) REFERENCES `phieuxuat` (`SoPhieu`),
  ADD CONSTRAINT `FK_CTPX_TKCo` FOREIGN KEY (`TKCo`) REFERENCES `taikhoan` (`MaTK`),
  ADD CONSTRAINT `FK_CTPX_TKNo` FOREIGN KEY (`TKNo`) REFERENCES `taikhoan` (`MaTK`),
  ADD CONSTRAINT `FK_CTPX_VT` FOREIGN KEY (`MaVT`) REFERENCES `vattu` (`MaVT`);

--
-- Constraints for table `dudauvattu`
--
ALTER TABLE `dudauvattu`
  ADD CONSTRAINT `FK_Ton_Kho` FOREIGN KEY (`MaKho`) REFERENCES `kho` (`MaKho`),
  ADD CONSTRAINT `FK_Ton_VT` FOREIGN KEY (`MaVT`) REFERENCES `vattu` (`MaVT`);

--
-- Constraints for table `kho`
--
ALTER TABLE `kho`
  ADD CONSTRAINT `FK_Kho_NhanVien` FOREIGN KEY (`MaThuKho`) REFERENCES `nhanvien` (`MaNV`);

--
-- Constraints for table `nguoigiao`
--
ALTER TABLE `nguoigiao`
  ADD CONSTRAINT `FK_NguoiGiao_NCC` FOREIGN KEY (`MaNCC`) REFERENCES `nhacungcap` (`MaNCC`);

--
-- Constraints for table `nguoinhan`
--
ALTER TABLE `nguoinhan`
  ADD CONSTRAINT `FK_NguoiNhan_CongTrinh` FOREIGN KEY (`MaCongTrinh`) REFERENCES `congtrinh` (`MaCongTrinh`);

--
-- Constraints for table `nhanvien`
--
ALTER TABLE `nhanvien`
  ADD CONSTRAINT `FK_NhanVien_BoPhan` FOREIGN KEY (`MaBoPhan`) REFERENCES `bophan` (`MaBoPhan`);

--
-- Constraints for table `phieunhap`
--
ALTER TABLE `phieunhap`
  ADD CONSTRAINT `FK_PhieuNhap_Kho` FOREIGN KEY (`MaKho`) REFERENCES `kho` (`MaKho`),
  ADD CONSTRAINT `FK_PhieuNhap_NCC` FOREIGN KEY (`MaNCC`) REFERENCES `nhacungcap` (`MaNCC`),
  ADD CONSTRAINT `FK_PhieuNhap_NG` FOREIGN KEY (`MaNguoiGiao`) REFERENCES `nguoigiao` (`MaNguoiGiao`);

--
-- Constraints for table `phieuxuat`
--
ALTER TABLE `phieuxuat`
  ADD CONSTRAINT `FK_PX_Kho` FOREIGN KEY (`MaKho`) REFERENCES `kho` (`MaKho`),
  ADD CONSTRAINT `FK_PX_NN` FOREIGN KEY (`MaNguoiNhan`) REFERENCES `nguoinhan` (`MaNguoiNhan`),
  ADD CONSTRAINT `FK_PhieuXuat_CongTrinh` FOREIGN KEY (`MaCongTrinh`) REFERENCES `congtrinh` (`MaCongTrinh`);

--
-- Constraints for table `vattu`
--
ALTER TABLE `vattu`
  ADD CONSTRAINT `FK_VT_DVT` FOREIGN KEY (`MaDVT`) REFERENCES `donvitinh` (`MaDVT`),
  ADD CONSTRAINT `FK_VT_LoaiVT` FOREIGN KEY (`MaLoai`) REFERENCES `loaivattu` (`MaLoai`),
  ADD CONSTRAINT `FK_VT_TK` FOREIGN KEY (`MaTK`) REFERENCES `taikhoan` (`MaTK`);
COMMIT;

/*!40101 SET CHARACTER_SET_CLIENT=@OLD_CHARACTER_SET_CLIENT */;
/*!40101 SET CHARACTER_SET_RESULTS=@OLD_CHARACTER_SET_RESULTS */;
/*!40101 SET COLLATION_CONNECTION=@OLD_COLLATION_CONNECTION */;
