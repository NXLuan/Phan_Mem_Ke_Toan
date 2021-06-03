-- phpMyAdmin SQL Dump
-- version 4.9.0.1
-- https://www.phpmyadmin.net/
--
-- Máy chủ: 127.0.0.1
-- Thời gian đã tạo: Th6 03, 2021 lúc 06:36 AM
-- Phiên bản máy phục vụ: 10.3.16-MariaDB
-- Phiên bản PHP: 7.3.6

SET SQL_MODE = "NO_AUTO_VALUE_ON_ZERO";
SET AUTOCOMMIT = 0;
START TRANSACTION;
SET time_zone = "+00:00";


/*!40101 SET @OLD_CHARACTER_SET_CLIENT=@@CHARACTER_SET_CLIENT */;
/*!40101 SET @OLD_CHARACTER_SET_RESULTS=@@CHARACTER_SET_RESULTS */;
/*!40101 SET @OLD_COLLATION_CONNECTION=@@COLLATION_CONNECTION */;
/*!40101 SET NAMES utf8mb4 */;

--
-- Cơ sở dữ liệu: `ketoan`
--

-- --------------------------------------------------------

--
-- Cấu trúc bảng cho bảng `bbkiemke`
--

CREATE TABLE `bbkiemke` (
  `SoBienBan` varchar(50) COLLATE utf8_vietnamese_ci NOT NULL,
  `NgayLap` date NOT NULL,
  `MaKho` varchar(50) COLLATE utf8_vietnamese_ci DEFAULT NULL,
  `TruongBan` varchar(50) COLLATE utf8_vietnamese_ci DEFAULT NULL,
  `UyVien1` varchar(50) COLLATE utf8_vietnamese_ci DEFAULT NULL,
  `UyVien2` varchar(50) COLLATE utf8_vietnamese_ci DEFAULT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8 COLLATE=utf8_vietnamese_ci;

-- --------------------------------------------------------

--
-- Cấu trúc bảng cho bảng `bophan`
--

CREATE TABLE `bophan` (
  `MaBoPhan` varchar(50) COLLATE utf8_vietnamese_ci NOT NULL,
  `TenBoPhan` varchar(50) COLLATE utf8_vietnamese_ci NOT NULL,
  `MoTa` varchar(100) COLLATE utf8_vietnamese_ci NOT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8 COLLATE=utf8_vietnamese_ci;

--
-- Đang đổ dữ liệu cho bảng `bophan`
--

INSERT INTO `bophan` (`MaBoPhan`, `TenBoPhan`, `MoTa`) VALUES
('BP001', 'Bán hàng', ''),
('BP002', 'Mua hàng', ''),
('BP003', 'Thi công', 'Xây dựng công trình'),
('BP004', 'Kế toán', ''),
('BP005', 'Giám đốc', ''),
('BP006', 'Kế toán vật tư', 'Quản lý kho');

-- --------------------------------------------------------

--
-- Cấu trúc bảng cho bảng `congtrinh`
--

CREATE TABLE `congtrinh` (
  `MaCongTrinh` varchar(50) COLLATE utf8_vietnamese_ci NOT NULL,
  `TenCongTrinh` varchar(50) COLLATE utf8_vietnamese_ci NOT NULL,
  `DiaChi` varchar(100) COLLATE utf8_vietnamese_ci NOT NULL,
  `MoTa` varchar(100) COLLATE utf8_vietnamese_ci NOT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8 COLLATE=utf8_vietnamese_ci;

--
-- Đang đổ dữ liệu cho bảng `congtrinh`
--

INSERT INTO `congtrinh` (`MaCongTrinh`, `TenCongTrinh`, `DiaChi`, `MoTa`) VALUES
('CT001', 'Công trình 1', '', ''),
('CT002', 'Công trình 2', '01 Hồng Chương', 'xây trường học');

-- --------------------------------------------------------

--
-- Cấu trúc bảng cho bảng `ct_bbkiemke`
--

CREATE TABLE `ct_bbkiemke` (
  `MaSo` int(11) NOT NULL,
  `SoBienBan` varchar(50) COLLATE utf8_vietnamese_ci DEFAULT NULL,
  `MaVT` varchar(50) COLLATE utf8_vietnamese_ci DEFAULT NULL,
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
-- Cấu trúc bảng cho bảng `ct_phieunhap`
--

CREATE TABLE `ct_phieunhap` (
  `MaSo` int(11) NOT NULL,
  `SoPhieu` varchar(50) COLLATE utf8_vietnamese_ci DEFAULT NULL,
  `MaVT` varchar(50) COLLATE utf8_vietnamese_ci DEFAULT NULL,
  `TKNo` varchar(50) COLLATE utf8_vietnamese_ci DEFAULT NULL,
  `TKCo` varchar(50) COLLATE utf8_vietnamese_ci DEFAULT NULL,
  `SoLuong` double NOT NULL,
  `DonGia` decimal(10,0) NOT NULL,
  `ThanhTien` decimal(10,0) NOT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8 COLLATE=utf8_vietnamese_ci;

-- --------------------------------------------------------

--
-- Cấu trúc bảng cho bảng `ct_phieuxuat`
--

CREATE TABLE `ct_phieuxuat` (
  `MaSo` int(11) NOT NULL,
  `SoPhieu` varchar(50) COLLATE utf8_vietnamese_ci DEFAULT NULL,
  `MaVT` varchar(50) COLLATE utf8_vietnamese_ci DEFAULT NULL,
  `TKNo` varchar(50) COLLATE utf8_vietnamese_ci DEFAULT NULL,
  `TKCo` varchar(50) COLLATE utf8_vietnamese_ci DEFAULT NULL,
  `SoLuong` double NOT NULL,
  `DonGia` decimal(10,0) NOT NULL,
  `ThanhTien` decimal(10,0) NOT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8 COLLATE=utf8_vietnamese_ci;

-- --------------------------------------------------------

--
-- Cấu trúc bảng cho bảng `donvitinh`
--

CREATE TABLE `donvitinh` (
  `MaDVT` varchar(50) COLLATE utf8_vietnamese_ci NOT NULL,
  `TenDVT` varchar(50) COLLATE utf8_vietnamese_ci NOT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8 COLLATE=utf8_vietnamese_ci;

--
-- Đang đổ dữ liệu cho bảng `donvitinh`
--

INSERT INTO `donvitinh` (`MaDVT`, `TenDVT`) VALUES
('DVT001', 'tấn'),
('DVT002', 'tạ'),
('DVT003', 'bao');

-- --------------------------------------------------------

--
-- Cấu trúc bảng cho bảng `dudauvattu`
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

-- --------------------------------------------------------

--
-- Cấu trúc bảng cho bảng `kho`
--

CREATE TABLE `kho` (
  `MaKho` varchar(50) COLLATE utf8_vietnamese_ci NOT NULL,
  `TenKho` varchar(50) COLLATE utf8_vietnamese_ci NOT NULL,
  `DiaChi` varchar(100) COLLATE utf8_vietnamese_ci NOT NULL,
  `SDT` varchar(10) COLLATE utf8_vietnamese_ci NOT NULL,
  `MaThuKho` varchar(50) COLLATE utf8_vietnamese_ci DEFAULT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8 COLLATE=utf8_vietnamese_ci;

--
-- Đang đổ dữ liệu cho bảng `kho`
--

INSERT INTO `kho` (`MaKho`, `TenKho`, `DiaChi`, `SDT`, `MaThuKho`) VALUES
('K001', 'Kho 1', '01 Hồng Chương', '', 'NV004'),
('K002', '2323', '', '', NULL);

-- --------------------------------------------------------

--
-- Cấu trúc bảng cho bảng `loaivattu`
--

CREATE TABLE `loaivattu` (
  `MaLoai` varchar(50) COLLATE utf8_vietnamese_ci NOT NULL,
  `TenLoai` varchar(50) COLLATE utf8_vietnamese_ci NOT NULL,
  `MoTa` varchar(100) COLLATE utf8_vietnamese_ci NOT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8 COLLATE=utf8_vietnamese_ci;

--
-- Đang đổ dữ liệu cho bảng `loaivattu`
--

INSERT INTO `loaivattu` (`MaLoai`, `TenLoai`, `MoTa`) VALUES
('LVT001', 'Nguyên vật liệu', ''),
('LVT002', 'Nhiên liệu', ''),
('LVT003', 'Phụ tùng thay thế', ''),
('LVT004', 'Thiết bị xây dựng', ''),
('LVT005', 'Vật liệu khác', ''),
('LVT006', 'Công cụ, dụng cụ', '');

-- --------------------------------------------------------

--
-- Cấu trúc bảng cho bảng `nguoigiao`
--

CREATE TABLE `nguoigiao` (
  `MaNguoiGiao` varchar(50) COLLATE utf8_vietnamese_ci NOT NULL,
  `TenNguoiGiao` varchar(50) COLLATE utf8_vietnamese_ci NOT NULL,
  `DiaChi` varchar(100) COLLATE utf8_vietnamese_ci NOT NULL,
  `MaNCC` varchar(50) COLLATE utf8_vietnamese_ci DEFAULT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8 COLLATE=utf8_vietnamese_ci;

--
-- Đang đổ dữ liệu cho bảng `nguoigiao`
--

INSERT INTO `nguoigiao` (`MaNguoiGiao`, `TenNguoiGiao`, `DiaChi`, `MaNCC`) VALUES
('NG001', 'Luân', '', 'NCC001');

-- --------------------------------------------------------

--
-- Cấu trúc bảng cho bảng `nguoinhan`
--

CREATE TABLE `nguoinhan` (
  `MaNguoiNhan` varchar(50) COLLATE utf8_vietnamese_ci NOT NULL,
  `TenNguoiNhan` varchar(50) COLLATE utf8_vietnamese_ci NOT NULL,
  `DiaChi` varchar(100) COLLATE utf8_vietnamese_ci NOT NULL,
  `MaCongTrinh` varchar(50) COLLATE utf8_vietnamese_ci DEFAULT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8 COLLATE=utf8_vietnamese_ci;

--
-- Đang đổ dữ liệu cho bảng `nguoinhan`
--

INSERT INTO `nguoinhan` (`MaNguoiNhan`, `TenNguoiNhan`, `DiaChi`, `MaCongTrinh`) VALUES
('NN001', 'Tiến', '', 'CT001');

-- --------------------------------------------------------

--
-- Cấu trúc bảng cho bảng `nhacungcap`
--

CREATE TABLE `nhacungcap` (
  `MaNCC` varchar(50) COLLATE utf8_vietnamese_ci NOT NULL,
  `TenNCC` varchar(50) COLLATE utf8_vietnamese_ci NOT NULL,
  `DiaChi` varchar(100) COLLATE utf8_vietnamese_ci NOT NULL,
  `MaSoThue` varchar(50) COLLATE utf8_vietnamese_ci NOT NULL,
  `SDT` varchar(10) COLLATE utf8_vietnamese_ci NOT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8 COLLATE=utf8_vietnamese_ci;

--
-- Đang đổ dữ liệu cho bảng `nhacungcap`
--

INSERT INTO `nhacungcap` (`MaNCC`, `TenNCC`, `DiaChi`, `MaSoThue`, `SDT`) VALUES
('NCC001', 'Nhà cung cấp 1', '', '74 12345678-123', '0832799425');

-- --------------------------------------------------------

--
-- Cấu trúc bảng cho bảng `nhanvien`
--

CREATE TABLE `nhanvien` (
  `MaNV` varchar(50) COLLATE utf8_vietnamese_ci NOT NULL,
  `TenNV` varchar(50) COLLATE utf8_vietnamese_ci NOT NULL,
  `MaBoPhan` varchar(50) COLLATE utf8_vietnamese_ci DEFAULT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8 COLLATE=utf8_vietnamese_ci;

--
-- Đang đổ dữ liệu cho bảng `nhanvien`
--

INSERT INTO `nhanvien` (`MaNV`, `TenNV`, `MaBoPhan`) VALUES
('NV001', 'Cường', 'BP005'),
('NV002', 'Luân', 'BP006'),
('NV003', 'Tiến', 'BP002'),
('NV004', 'Hiếu', 'BP006'),
('NV005', 'Quân', 'BP006');

-- --------------------------------------------------------

--
-- Cấu trúc bảng cho bảng `phieunhap`
--

CREATE TABLE `phieunhap` (
  `SoPhieu` varchar(50) COLLATE utf8_vietnamese_ci NOT NULL,
  `NgayNhap` date NOT NULL,
  `MaNCC` varchar(50) COLLATE utf8_vietnamese_ci DEFAULT NULL,
  `MaNguoiGiao` varchar(50) COLLATE utf8_vietnamese_ci DEFAULT NULL,
  `MaKho` varchar(50) COLLATE utf8_vietnamese_ci DEFAULT NULL,
  `LyDo` varchar(100) COLLATE utf8_vietnamese_ci NOT NULL,
  `TongTien` decimal(10,0) NOT NULL,
  `ChungTuLQ` varchar(100) COLLATE utf8_vietnamese_ci NOT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8 COLLATE=utf8_vietnamese_ci;

-- --------------------------------------------------------

--
-- Cấu trúc bảng cho bảng `phieuxuat`
--

CREATE TABLE `phieuxuat` (
  `SoPhieu` varchar(50) COLLATE utf8_vietnamese_ci NOT NULL,
  `NgayXuat` date NOT NULL,
  `MaCongTrinh` varchar(50) COLLATE utf8_vietnamese_ci DEFAULT NULL,
  `MaNguoiNhan` varchar(50) COLLATE utf8_vietnamese_ci DEFAULT NULL,
  `MaKho` varchar(50) COLLATE utf8_vietnamese_ci DEFAULT NULL,
  `LyDo` varchar(100) COLLATE utf8_vietnamese_ci NOT NULL,
  `TongTien` decimal(10,0) NOT NULL,
  `ChungTuLQ` varchar(100) COLLATE utf8_vietnamese_ci NOT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8 COLLATE=utf8_vietnamese_ci;

-- --------------------------------------------------------

--
-- Cấu trúc bảng cho bảng `taikhoan`
--

CREATE TABLE `taikhoan` (
  `MaTK` varchar(50) COLLATE utf8_vietnamese_ci NOT NULL,
  `TenTK` varchar(50) COLLATE utf8_vietnamese_ci NOT NULL,
  `CapTK` int(11) NOT NULL,
  `TKMe` varchar(50) COLLATE utf8_vietnamese_ci DEFAULT NULL,
  `LoaiTK` varchar(50) COLLATE utf8_vietnamese_ci NOT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8 COLLATE=utf8_vietnamese_ci;

--
-- Đang đổ dữ liệu cho bảng `taikhoan`
--

INSERT INTO `taikhoan` (`MaTK`, `TenTK`, `CapTK`, `TKMe`, `LoaiTK`) VALUES
('111', 'Tiền mặt', 1, NULL, ''),
('1111', 'Tiền Việt Nam', 2, '111', ''),
('1112', 'Tiền ngoại tệ', 2, '111', ''),
('1113', 'Đá quý', 2, '111', ''),
('131', 'Phải thu khách hàng', 1, NULL, ''),
('152', 'Nguyên vật liệu', 1, NULL, ''),
('153', 'Công cụ dụng cụ', 1, NULL, ''),
('331', 'Phải trả người bán', 1, NULL, ''),
('333', 'Thuế và các khoản phải nộp', 1, NULL, '');

-- --------------------------------------------------------

--
-- Cấu trúc bảng cho bảng `vattu`
--

CREATE TABLE `vattu` (
  `MaVT` varchar(50) COLLATE utf8_vietnamese_ci NOT NULL,
  `TenVT` varchar(50) COLLATE utf8_vietnamese_ci NOT NULL,
  `MaLoai` varchar(50) COLLATE utf8_vietnamese_ci DEFAULT NULL,
  `MaDVT` varchar(50) COLLATE utf8_vietnamese_ci DEFAULT NULL,
  `MaTK` varchar(50) COLLATE utf8_vietnamese_ci DEFAULT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8 COLLATE=utf8_vietnamese_ci;

--
-- Đang đổ dữ liệu cho bảng `vattu`
--

INSERT INTO `vattu` (`MaVT`, `TenVT`, `MaLoai`, `MaDVT`, `MaTK`) VALUES
('VT001', 'Sắt', 'LVT001', 'DVT001', '152');

--
-- Chỉ mục cho các bảng đã đổ
--

--
-- Chỉ mục cho bảng `bbkiemke`
--
ALTER TABLE `bbkiemke`
  ADD PRIMARY KEY (`SoBienBan`),
  ADD KEY `FK_BB_Kho` (`MaKho`),
  ADD KEY `FK_BB_TruongBan` (`TruongBan`),
  ADD KEY `FK_BB_UyVien1` (`UyVien1`),
  ADD KEY `FK_BB_UyVien2` (`UyVien2`);

--
-- Chỉ mục cho bảng `bophan`
--
ALTER TABLE `bophan`
  ADD PRIMARY KEY (`MaBoPhan`);

--
-- Chỉ mục cho bảng `congtrinh`
--
ALTER TABLE `congtrinh`
  ADD PRIMARY KEY (`MaCongTrinh`);

--
-- Chỉ mục cho bảng `ct_bbkiemke`
--
ALTER TABLE `ct_bbkiemke`
  ADD PRIMARY KEY (`MaSo`),
  ADD KEY `FK_CTKiemKe_BB` (`SoBienBan`),
  ADD KEY `FK_CTKiemKe_VT` (`MaVT`);

--
-- Chỉ mục cho bảng `ct_phieunhap`
--
ALTER TABLE `ct_phieunhap`
  ADD PRIMARY KEY (`MaSo`),
  ADD KEY `FK_CTPN_SoPhieu` (`SoPhieu`),
  ADD KEY `FK_CTPN_TKCo` (`TKCo`),
  ADD KEY `FK_CTPN_TKNo` (`TKNo`),
  ADD KEY `FK_CTPN_VT` (`MaVT`);

--
-- Chỉ mục cho bảng `ct_phieuxuat`
--
ALTER TABLE `ct_phieuxuat`
  ADD PRIMARY KEY (`MaSo`),
  ADD KEY `FK_CTPX_PX` (`SoPhieu`),
  ADD KEY `FK_CTPX_TKCo` (`TKCo`),
  ADD KEY `FK_CTPX_TKNo` (`TKNo`),
  ADD KEY `FK_CTPX_VT` (`MaVT`);

--
-- Chỉ mục cho bảng `donvitinh`
--
ALTER TABLE `donvitinh`
  ADD PRIMARY KEY (`MaDVT`);

--
-- Chỉ mục cho bảng `dudauvattu`
--
ALTER TABLE `dudauvattu`
  ADD PRIMARY KEY (`MaSo`),
  ADD KEY `FK_Ton_VT` (`MaVT`),
  ADD KEY `FK_Ton_Kho` (`MaKho`);

--
-- Chỉ mục cho bảng `kho`
--
ALTER TABLE `kho`
  ADD PRIMARY KEY (`MaKho`),
  ADD KEY `FK_Kho_NhanVien` (`MaThuKho`);

--
-- Chỉ mục cho bảng `loaivattu`
--
ALTER TABLE `loaivattu`
  ADD PRIMARY KEY (`MaLoai`);

--
-- Chỉ mục cho bảng `nguoigiao`
--
ALTER TABLE `nguoigiao`
  ADD PRIMARY KEY (`MaNguoiGiao`),
  ADD KEY `FK_NguoiGiao_NCC` (`MaNCC`);

--
-- Chỉ mục cho bảng `nguoinhan`
--
ALTER TABLE `nguoinhan`
  ADD PRIMARY KEY (`MaNguoiNhan`),
  ADD KEY `FK_NguoiNhan_CongTrinh` (`MaCongTrinh`);

--
-- Chỉ mục cho bảng `nhacungcap`
--
ALTER TABLE `nhacungcap`
  ADD PRIMARY KEY (`MaNCC`);

--
-- Chỉ mục cho bảng `nhanvien`
--
ALTER TABLE `nhanvien`
  ADD PRIMARY KEY (`MaNV`),
  ADD KEY `FK_NhanVien_BoPhan` (`MaBoPhan`);

--
-- Chỉ mục cho bảng `phieunhap`
--
ALTER TABLE `phieunhap`
  ADD PRIMARY KEY (`SoPhieu`),
  ADD KEY `FK_PhieuNhap_Kho` (`MaKho`),
  ADD KEY `FK_PhieuNhap_NCC` (`MaNCC`),
  ADD KEY `FK_PhieuNhap_NG` (`MaNguoiGiao`);

--
-- Chỉ mục cho bảng `phieuxuat`
--
ALTER TABLE `phieuxuat`
  ADD PRIMARY KEY (`SoPhieu`),
  ADD KEY `FK_PX_Kho` (`MaKho`),
  ADD KEY `FK_PX_NN` (`MaNguoiNhan`),
  ADD KEY `FK_PhieuXuat_CongTrinh` (`MaCongTrinh`);

--
-- Chỉ mục cho bảng `taikhoan`
--
ALTER TABLE `taikhoan`
  ADD PRIMARY KEY (`MaTK`),
  ADD KEY `FK_TK_TKMe` (`TKMe`);

--
-- Chỉ mục cho bảng `vattu`
--
ALTER TABLE `vattu`
  ADD PRIMARY KEY (`MaVT`),
  ADD KEY `FK_VT_DVT` (`MaDVT`),
  ADD KEY `FK_VT_LoaiVT` (`MaLoai`),
  ADD KEY `FK_VT_TK` (`MaTK`);

--
-- AUTO_INCREMENT cho các bảng đã đổ
--

--
-- AUTO_INCREMENT cho bảng `ct_bbkiemke`
--
ALTER TABLE `ct_bbkiemke`
  MODIFY `MaSo` int(11) NOT NULL AUTO_INCREMENT;

--
-- AUTO_INCREMENT cho bảng `ct_phieunhap`
--
ALTER TABLE `ct_phieunhap`
  MODIFY `MaSo` int(11) NOT NULL AUTO_INCREMENT;

--
-- AUTO_INCREMENT cho bảng `ct_phieuxuat`
--
ALTER TABLE `ct_phieuxuat`
  MODIFY `MaSo` int(11) NOT NULL AUTO_INCREMENT;

--
-- AUTO_INCREMENT cho bảng `dudauvattu`
--
ALTER TABLE `dudauvattu`
  MODIFY `MaSo` int(11) NOT NULL AUTO_INCREMENT, AUTO_INCREMENT=3;

--
-- Các ràng buộc cho các bảng đã đổ
--

--
-- Các ràng buộc cho bảng `bbkiemke`
--
ALTER TABLE `bbkiemke`
  ADD CONSTRAINT `FK_BB_Kho` FOREIGN KEY (`MaKho`) REFERENCES `kho` (`MaKho`),
  ADD CONSTRAINT `FK_BB_TruongBan` FOREIGN KEY (`TruongBan`) REFERENCES `nhanvien` (`MaNV`),
  ADD CONSTRAINT `FK_BB_UyVien1` FOREIGN KEY (`UyVien1`) REFERENCES `nhanvien` (`MaNV`),
  ADD CONSTRAINT `FK_BB_UyVien2` FOREIGN KEY (`UyVien2`) REFERENCES `nhanvien` (`MaNV`);

--
-- Các ràng buộc cho bảng `ct_bbkiemke`
--
ALTER TABLE `ct_bbkiemke`
  ADD CONSTRAINT `FK_CTKiemKe_BB` FOREIGN KEY (`SoBienBan`) REFERENCES `bbkiemke` (`SoBienBan`),
  ADD CONSTRAINT `FK_CTKiemKe_VT` FOREIGN KEY (`MaVT`) REFERENCES `vattu` (`MaVT`);

--
-- Các ràng buộc cho bảng `ct_phieunhap`
--
ALTER TABLE `ct_phieunhap`
  ADD CONSTRAINT `FK_CTPN_SoPhieu` FOREIGN KEY (`SoPhieu`) REFERENCES `phieunhap` (`SoPhieu`),
  ADD CONSTRAINT `FK_CTPN_TKCo` FOREIGN KEY (`TKCo`) REFERENCES `taikhoan` (`MaTK`),
  ADD CONSTRAINT `FK_CTPN_TKNo` FOREIGN KEY (`TKNo`) REFERENCES `taikhoan` (`MaTK`),
  ADD CONSTRAINT `FK_CTPN_VT` FOREIGN KEY (`MaVT`) REFERENCES `vattu` (`MaVT`);

--
-- Các ràng buộc cho bảng `ct_phieuxuat`
--
ALTER TABLE `ct_phieuxuat`
  ADD CONSTRAINT `FK_CTPX_PX` FOREIGN KEY (`SoPhieu`) REFERENCES `phieuxuat` (`SoPhieu`),
  ADD CONSTRAINT `FK_CTPX_TKCo` FOREIGN KEY (`TKCo`) REFERENCES `taikhoan` (`MaTK`),
  ADD CONSTRAINT `FK_CTPX_TKNo` FOREIGN KEY (`TKNo`) REFERENCES `taikhoan` (`MaTK`),
  ADD CONSTRAINT `FK_CTPX_VT` FOREIGN KEY (`MaVT`) REFERENCES `vattu` (`MaVT`);

--
-- Các ràng buộc cho bảng `dudauvattu`
--
ALTER TABLE `dudauvattu`
  ADD CONSTRAINT `FK_Ton_Kho` FOREIGN KEY (`MaKho`) REFERENCES `kho` (`MaKho`),
  ADD CONSTRAINT `FK_Ton_VT` FOREIGN KEY (`MaVT`) REFERENCES `vattu` (`MaVT`);

--
-- Các ràng buộc cho bảng `kho`
--
ALTER TABLE `kho`
  ADD CONSTRAINT `FK_Kho_NhanVien` FOREIGN KEY (`MaThuKho`) REFERENCES `nhanvien` (`MaNV`);

--
-- Các ràng buộc cho bảng `nguoigiao`
--
ALTER TABLE `nguoigiao`
  ADD CONSTRAINT `FK_NguoiGiao_NCC` FOREIGN KEY (`MaNCC`) REFERENCES `nhacungcap` (`MaNCC`);

--
-- Các ràng buộc cho bảng `nguoinhan`
--
ALTER TABLE `nguoinhan`
  ADD CONSTRAINT `FK_NguoiNhan_CongTrinh` FOREIGN KEY (`MaCongTrinh`) REFERENCES `congtrinh` (`MaCongTrinh`);

--
-- Các ràng buộc cho bảng `nhanvien`
--
ALTER TABLE `nhanvien`
  ADD CONSTRAINT `FK_NhanVien_BoPhan` FOREIGN KEY (`MaBoPhan`) REFERENCES `bophan` (`MaBoPhan`);

--
-- Các ràng buộc cho bảng `phieunhap`
--
ALTER TABLE `phieunhap`
  ADD CONSTRAINT `FK_PhieuNhap_Kho` FOREIGN KEY (`MaKho`) REFERENCES `kho` (`MaKho`),
  ADD CONSTRAINT `FK_PhieuNhap_NCC` FOREIGN KEY (`MaNCC`) REFERENCES `nhacungcap` (`MaNCC`),
  ADD CONSTRAINT `FK_PhieuNhap_NG` FOREIGN KEY (`MaNguoiGiao`) REFERENCES `nguoigiao` (`MaNguoiGiao`);

--
-- Các ràng buộc cho bảng `phieuxuat`
--
ALTER TABLE `phieuxuat`
  ADD CONSTRAINT `FK_PX_Kho` FOREIGN KEY (`MaKho`) REFERENCES `kho` (`MaKho`),
  ADD CONSTRAINT `FK_PX_NN` FOREIGN KEY (`MaNguoiNhan`) REFERENCES `nguoinhan` (`MaNguoiNhan`),
  ADD CONSTRAINT `FK_PhieuXuat_CongTrinh` FOREIGN KEY (`MaCongTrinh`) REFERENCES `congtrinh` (`MaCongTrinh`);

--
-- Các ràng buộc cho bảng `taikhoan`
--
ALTER TABLE `taikhoan`
  ADD CONSTRAINT `FK_TK_TKMe` FOREIGN KEY (`TKMe`) REFERENCES `taikhoan` (`MaTK`);

--
-- Các ràng buộc cho bảng `vattu`
--
ALTER TABLE `vattu`
  ADD CONSTRAINT `FK_VT_DVT` FOREIGN KEY (`MaDVT`) REFERENCES `donvitinh` (`MaDVT`),
  ADD CONSTRAINT `FK_VT_LoaiVT` FOREIGN KEY (`MaLoai`) REFERENCES `loaivattu` (`MaLoai`),
  ADD CONSTRAINT `FK_VT_TK` FOREIGN KEY (`MaTK`) REFERENCES `taikhoan` (`MaTK`);
COMMIT;

/*!40101 SET CHARACTER_SET_CLIENT=@OLD_CHARACTER_SET_CLIENT */;
/*!40101 SET CHARACTER_SET_RESULTS=@OLD_CHARACTER_SET_RESULTS */;
/*!40101 SET COLLATION_CONNECTION=@OLD_COLLATION_CONNECTION */;
