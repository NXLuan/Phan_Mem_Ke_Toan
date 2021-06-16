const express = require('express')
const Router = express.Router()
const connection = require("../connection")
Router.get('/chart', (req, res) => {
  const { Thang, Nam } = req.query;
  let sql = `SELECT TenVT, SUM(ThanhTien) AS TongTT ` +
    `FROM ct_phieunhap LEFT JOIN phieunhap ON ct_phieunhap.SoPhieu = phieunhap.SoPhieu ` +
    `LEFT JOIN vattu ON ct_phieunhap.MaVT = vattu.MaVT ` +
    `WHERE MONTH(NgayNhap) = ? AND YEAR(NgayNhap) = ? ` +
    `GROUP BY ct_phieunhap.MaVT`
  connection.query(sql, [Thang, Nam], (err, rows) => {
    if (!err) {
      res.send(rows)
    } else {
      console.log(err); res.status(400).send({ message: err })
    }
  })
})
Router.get('/ctpnngay', (req, res) => {
  const { MaKho, Ngay } = req.query;
  let sql = `SELECT MaVT, SUM(SLThucTe) AS TongSL, SUM(ThanhTien) AS TongTT ` +
    `FROM ct_phieunhap LEFT JOIN phieunhap ON ct_phieunhap.SoPhieu = phieunhap.SoPhieu ` +
    `WHERE MaKho = ? AND NgayNhap <= ? ` +
    `GROUP BY MaVT`
  connection.query(sql, [MaKho, Ngay], (err, rows) => {
    if (!err) {
      res.send(rows)
    } else {
      console.log(err); res.status(400).send({ message: err })
    }
  })
})
Router.get('/allctpnthang', (req, res) => {
  const { MaKho, Thang, Nam } = req.query;
  let sql = `SELECT MaVT, SUM(SLThucTe) AS TongSL, SUM(ThanhTien) AS TongTT ` +
    `FROM ct_phieunhap LEFT JOIN phieunhap ON ct_phieunhap.SoPhieu = phieunhap.SoPhieu ` +
    `WHERE MaKho = ? AND MONTH(NgayNhap) = ? AND YEAR(NgayNhap) = ? ` +
    `GROUP BY MaVT`
  connection.query(sql, [MaKho, Thang, Nam], (err, rows) => {
    if (!err) {
      res.send(rows)
    } else {
      console.log(err); res.status(400).send({ message: err })
    }
  })
})
Router.get('/ctpnthang', (req, res) => {
  const { MaVT, MaKho, NgayBD, Thang, Nam } = req.query;
  let sql = `SELECT NgayNhap as Ngay, ct_phieunhap.SoPhieu, SLThucTe, DonGia, ThanhTien, LyDo, TKCo as MaTK ` +
    `FROM ct_phieunhap LEFT JOIN phieunhap ON ct_phieunhap.SoPhieu = phieunhap.SoPhieu ` +
    `WHERE MaVT = ? AND MaKho = ? AND NgayNhap >= ? AND MONTH(NgayNhap) = ? AND YEAR(NgayNhap) = ?`
  connection.query(sql, [MaVT, MaKho, NgayBD, Thang, Nam], (err, rows) => {
    if (!err) {
      res.send(rows)
    } else {
      console.log(err); res.status(400).send({ message: err })
    }
  })
})

Router.get('/ctpn', (req, res) => {
  const { MaVT, MaKho, NgayBD, NgayKT } = req.query;
  let sql = `SELECT NgayNhap as Ngay, ct_phieunhap.SoPhieu, SLThucTe, DonGia, ThanhTien, LyDo ` +
    `FROM ct_phieunhap LEFT JOIN phieunhap ON ct_phieunhap.SoPhieu = phieunhap.SoPhieu ` +
    `WHERE MaVT = ? AND MaKho = ? AND NgayNhap >= ? AND NgayNhap <= ?`
  connection.query(sql, [MaVT, MaKho, NgayBD, NgayKT], (err, rows) => {
    if (!err) {
      res.send(rows)
    } else {
      console.log(err); res.status(400).send({ message: err })
    }
  })
})
Router.get('/:SoPhieu', (req, res) => {
  connection.query(
    "SELECT MaSo, SoPhieu, ct_phieunhap.MaVT, TenVT, TenDVT, MaTK, SLSoSach, SLThucTe, DonGia, ThanhTien " +
    "FROM ct_phieunhap LEFT JOIN vattu ON ct_phieunhap.MaVT = vattu.MaVT " +
    "LEFT JOIN donvitinh ON vattu.MaDVT = donvitinh.MaDVT " +
    "WHERE SoPhieu = ?", [req.params.SoPhieu], (err, rows) => {
      if (!err) {
        res.send(rows)
      } else {
        console.log(err); res.status(400).send({ message: err })
      }
    })
})
Router.get('/', (req, res) => {
  connection.query("SELECT * FROM ct_phieunhap", (err, rows) => {
    if (!err) {
      res.send(rows)
    } else {
      console.log(err); res.status(400).send({ message: err })
    }
  })
})

Router.delete('/:SoPhieu', (req, res) => {
  connection.query("DELETE FROM ct_phieunhap WHERE SoPhieu = ?", [req.params.SoPhieu], (err, rows) => {
    if (!err) {
      res.send(`ct_phieunhap with MaSo: ${[req.params.SoPhieu]} has been removed`)
    } else {
      console.log(err); res.status(400).send({ message: err })
    }
  })
})

Router.post('/', (req, res) => {
  const params = req.body;
  connection.query("INSERT INTO ct_phieunhap SET ?", params, (err, rows) => {
    if (!err) {
      res.send(`ct_phieunhap with MaSo: ${params.MaSo} has been added`)
    } else {
      console.log(err); res.status(400).send({ message: err })
    }
  })
})

Router.put('/', (req, res) => {
  const { MaSo } = req.body
  connection.query('UPDATE ct_phieunhap SET ? WHERE MaSo = ?', [req.body, MaSo], (err, rows) => {
    if (!err) {
      res.send(`ct_phieunhap with MaSo: ${MaSo} has been updated`)
    } else {
      console.log(err); res.status(400).send({ message: err })
    }
  })
})


module.exports = Router;