const express = require('express')
const bodyParser = require('body-parser')

const TaiKhoanRoutes = require('./routes/taikhoan')
const BoPhanRoutes = require('./routes/bophan')
const NhanVienRoutes = require('./routes/nhanvien')
const KhoRoutes = require('./routes/kho')
const LoaiVatTuRoutes = require('./routes/loaivattu')
const DonViTinhRoutes = require('./routes/donvitinh')
const VatTuRoutes = require('./routes/vattu')
const NhaCungCapRoutes = require('./routes/nhacungcap')
const NguoiGiaoRoutes = require('./routes/nguoigiao')
const CongTrinhRoutes = require('./routes/congtrinh')
const NguoiNhanRoutes = require('./routes/nguoinhan')
const DuDauVatTuRoutes = require('./routes/dudauvattu')
const PhieuNhapRoutes = require('./routes/phieunhap')
const CT_PhieuNhapRoutes = require('./routes/ct_phieunhap')
const PhieuXuatRoutes = require('./routes/phieuxuat')
const CT_PhieuXuatRoutes = require('./routes/ct_phieuxuat')
const BBKiemKeRoutes = require('./routes/bbkiemke')
const CT_BBKiemKeRoutes = require('./routes/ct_bbkiemke')
const NguoiDung = require('./routes/nguoidung')

const app = express()

const port = process.env.port || 5000

app.use(express.urlencoded({ extended: true }))

app.use(express.json())

app.use('/taikhoan', TaiKhoanRoutes)
app.use('/bophan', BoPhanRoutes)
app.use('/nhanvien', NhanVienRoutes)
app.use('/kho', KhoRoutes)
app.use('/loaivattu', LoaiVatTuRoutes)
app.use('/donvitinh', DonViTinhRoutes)
app.use('/vattu', VatTuRoutes)
app.use('/nhacungcap', NhaCungCapRoutes)
app.use('/nguoigiao', NguoiGiaoRoutes)
app.use('/congtrinh', CongTrinhRoutes)
app.use('/nguoinhan', NguoiNhanRoutes)
app.use('/dudauvattu', DuDauVatTuRoutes)
app.use('/phieunhap', PhieuNhapRoutes)
app.use('/ct_phieunhap', CT_PhieuNhapRoutes)
app.use('/phieuxuat', PhieuXuatRoutes)
app.use('/ct_phieuxuat', CT_PhieuXuatRoutes)
app.use('/bbkiemke', BBKiemKeRoutes)
app.use('/ct_bbkiemke', CT_BBKiemKeRoutes)
app.use('/nguoidung', NguoiDung)


app.listen(port, () => console.log(`Listen on port ${port}`))

