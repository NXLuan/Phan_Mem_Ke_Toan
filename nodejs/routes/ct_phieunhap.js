const express = require('express')
const Router = express.Router()
const connection = require("../connection")

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

Router.delete('/:MaSo', (req, res) => {
  connection.query("DELETE FROM ct_phieunhap WHERE MaSo = ?", [req.params.MaSo], (err, rows) => {
    if (!err) {
      res.send(`ct_phieunhap with MaSo: ${[req.params.MaSo]} has been removed`)
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