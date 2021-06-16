const express = require('express')
const Router = express.Router()
const connection = require("../connection")
Router.get('/chart', (req, res) => {
  const { Thang, Nam } = req.query;
  let sql = `SELECT TenVT, SUM(ThanhTien) AS TongTT ` +
    `FROM ct_phieuxuat LEFT JOIN phieuxuat ON ct_phieuxuat.SoPhieu = phieuxuat.SoPhieu ` + 
    `LEFT JOIN vattu ON ct_phieuxuat.MaVT = vattu.MaVT ` +
    `WHERE MONTH(NgayXuat) = ? AND YEAR(NgayXuat) = ? ` + 
    `GROUP BY ct_phieuxuat.MaVT`
  connection.query(sql, [Thang, Nam], (err, rows) => {
    if (!err) {
      res.send(rows)
    } else {
      console.log(err); res.status(400).send({ message: err })
    }
  })
})
Router.get('/ctpxngay', (req, res) => {
  const { MaKho, Ngay } = req.query;
  let sql = `SELECT MaVT, SUM(SLThucTe) AS TongSL, SUM(ThanhTien) AS TongTT ` +
    `FROM ct_phieuxuat LEFT JOIN phieuxuat ON ct_phieuxuat.SoPhieu = phieuxuat.SoPhieu ` +
    `WHERE MaKho = ? AND NgayXuat <= ? ` +
    `GROUP BY MaVT`
  connection.query(sql, [MaKho, Ngay], (err, rows) => {
    if (!err) {
      res.send(rows)
    } else {
      console.log(err); res.status(400).send({ message: err })
    }
  })
})
Router.get('/allctpxthang', (req, res) => {
  const { MaKho, Thang, Nam } = req.query;
  let sql = `SELECT MaVT, SUM(SLThucTe) AS TongSL, SUM(ThanhTien) AS TongTT ` +
    `FROM ct_phieuxuat LEFT JOIN phieuxuat ON ct_phieuxuat.SoPhieu = phieuxuat.SoPhieu ` +
    `WHERE MaKho = ? AND MONTH(NgayXuat) = ? AND YEAR(NgayXuat) = ? ` + 
    `GROUP BY MaVT`
  connection.query(sql, [MaKho, Thang, Nam], (err, rows) => {
    if (!err) {
      res.send(rows)
    } else {
      console.log(err); res.status(400).send({ message: err })
    }
  })
})

Router.get('/ctpxthang', (req, res) => {
  const { MaVT, MaKho, NgayBD, Thang, Nam } = req.query;
  let sql = `SELECT NgayXuat as Ngay, ct_phieuxuat.SoPhieu, SLThucTe, DonGia, ThanhTien, LyDo, TKNo as MaTK ` +
    `FROM ct_phieuxuat LEFT JOIN phieuxuat ON ct_phieuxuat.SoPhieu = phieuxuat.SoPhieu ` +
    `WHERE MaVT = ? AND MaKho = ? AND NgayXuat >= ? AND MONTH(NgayXuat) = ? AND YEAR(NgayXuat) = ?`
  connection.query(sql, [MaVT, MaKho, NgayBD, Thang, Nam], (err, rows) => {
    if (!err) {
      res.send(rows)
    } else {
      console.log(err); res.status(400).send({ message: err })
    }
  })
})

Router.get('/ctpx', (req, res) => {
  const { MaVT, MaKho, NgayBD, NgayKT } = req.query;
  let sql = `SELECT NgayXuat as Ngay, ct_phieuxuat.SoPhieu, SLThucTe, DonGia, ThanhTien, LyDo ` +
    `FROM ct_phieuxuat LEFT JOIN phieuxuat ON ct_phieuxuat.SoPhieu = phieuxuat.SoPhieu ` +
    `WHERE MaVT = ? AND MaKho = ? AND NgayXuat >= ? AND NgayXuat <= ?`
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
    "SELECT MaSo, SoPhieu, ct_phieuxuat.MaVT, TenVT, TenDVT, MaTK, SLSoSach, SLThucTe, DonGia, ThanhTien " +
    "FROM ct_phieuxuat LEFT JOIN vattu ON ct_phieuxuat.MaVT = vattu.MaVT " +
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
  connection.query("SELECT * FROM ct_phieuxuat", (err, rows) => {
    if (!err) {
      res.send(rows)
    } else {
      console.log(err); res.status(400).send({ message: err })
    }
  })
})

Router.delete('/:SoPhieu', (req, res) => {
  connection.query("DELETE FROM ct_phieuxuat WHERE SoPhieu = ?", [req.params.SoPhieu], (err, rows) => {
    if (!err) {
      res.send(`ct_phieuxuat with MaSo: ${[req.params.SoPhieu]} has been removed`)
    } else {
      console.log(err); res.status(400).send({ message: err })
    }
  })
})

Router.post('/', (req, res) => {
  const params = req.body;
  connection.query("INSERT INTO ct_phieuxuat SET ?", params, (err, rows) => {
    if (!err) {
      res.send(`ct_phieuxuat with MaSo: ${params.MaSo} has been added`)
    } else {
      console.log(err); res.status(400).send({ message: err })
    }
  })
})

Router.put('/', (req, res) => {
  const { MaSo } = req.body
  connection.query('UPDATE ct_phieuxuat SET ? WHERE MaSo = ?', [req.body, MaSo], (err, rows) => {
    if (!err) {
      res.send(`ct_phieuxuat with MaSo: ${MaSo} has been updated`)
    } else {
      console.log(err); res.status(400).send({ message: err })
    }
  })
})  


module.exports = Router;