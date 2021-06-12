const express = require('express')
const Router = express.Router()
const connection = require("../connection")

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