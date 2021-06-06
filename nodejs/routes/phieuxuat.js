const express = require('express')
const Router = express.Router()
const connection = require("../connection")

Router.get('/join', (req, res) => {
  connection.query(
    "SELECT SoPhieu, NgayXuat, phieuXuat.MaCongTrinh, TenCongTrinh, phieuXuat.MaNguoiNhan, TenNguoiNhan, " + 
    "phieuXuat.MaKho, TenKho, LyDo, TKNo, TKCo, TongTien, ChungTuLQ " +
    "FROM phieuXuat LEFT JOIN congtrinh ON phieuXuat.MaCongTrinh = congtrinh.MaCongTrinh " +
    "LEFT JOIN nguoiNhan ON phieuXuat.MaNguoiNhan = nguoiNhan.MaNguoiNhan " +
    "LEFT JOIN kho ON phieuXuat.MaKho = kho.MaKho",
    (err, rows) => {
      if (!err) {
        res.send(rows)
      } else {
        console.log(err); res.status(400).send({ message: err })
      }
    })
})

Router.get('/', (req, res) => {
  connection.query("SELECT * FROM phieuxuat", (err, rows) => {
    if (!err) {
      res.send(rows)
    } else {
      console.log(err); res.status(400).send({ message: err })
    }
  })
})

Router.delete('/:SoPhieu', (req, res) => {
  connection.query("DELETE FROM phieuxuat WHERE SoPhieu = ?", [req.params.SoPhieu], (err, rows) => {
    if (!err) {
      res.send(`phieuxuat with SoPhieu: ${[req.params.SoPhieu]} has been removed`)
    } else {
      console.log(err); res.status(400).send({ message: err })
    }
  })
})

Router.post('/', (req, res) => {
  const params = req.body;
  connection.query("INSERT INTO phieuxuat SET ?", params, (err, rows) => {
    if (!err) {
      res.send(`phieuxuat with SoPhieu: ${params.SoPhieu} has been added`)
    } else {
      console.log(err); res.status(400).send({ message: err })
    }
  })
})

Router.put('/', (req, res) => {
  const { SoPhieu } = req.body
  connection.query('UPDATE phieuxuat SET ? WHERE SoPhieu = ?', [req.body, SoPhieu], (err, rows) => {
    if (!err) {
      res.send(`phieuxuat with SoPhieu: ${SoPhieu} has been updated`)
    } else {
      console.log(err); res.status(400).send({ message: err })
    }
  })
})  

Router.put('/:SoPhieu', (req, res) => {
  const {SoPhieu} = req.params;
  connection.query('UPDATE phieuxuat SET TongTien = ? WHERE SoPhieu = ?', [req.body.TongTien, SoPhieu], (err, rows) => {
    if (!err) {
      res.send(`phieunhap with SoPhieu: ${SoPhieu} has been updated`)
    } else {
      console.log(err); res.status(400).send({ message: err })
    }
  })
})  


module.exports = Router;