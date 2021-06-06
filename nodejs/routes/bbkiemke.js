const express = require('express')
const Router = express.Router()
const connection = require("../connection")

Router.get('/join', (req, res) => {
  connection.query(
    "SELECT SoBienBan, NgayLap, bbkiemke.MaKho, TenKho, TruongBan, UyVien1, UyVien2, " + 
    "tb.TenNV AS TenTruongBan, uv1.TenNV AS TenUyVien1, uv2.TenNV AS TenUyVien2 " + 
    "FROM bbkiemke LEFT JOIN kho ON bbkiemke.MaKho = kho.MaKho " +
    "LEFT JOIN nhanvien tb ON bbkiemke.TruongBan = tb.MaNV " +
    "LEFT JOIN nhanvien uv1 ON bbkiemke.UyVien1 = uv1.MaNV " +
    "LEFT JOIN nhanvien uv2 ON bbkiemke.UyVien2 = uv2.MaNV " ,
    (err, rows) => {
      if (!err) {
        res.send(rows)
      } else {
        console.log(err); res.status(400).send({ message: err })
      }
    })
})
Router.get('/', (req, res) => {
  connection.query("SELECT * FROM bbkiemke", (err, rows) => {
    if (!err) {
      res.send(rows)
    } else {
      console.log(err); res.status(400).send({ message: err })
    }
  })
})

Router.delete('/:SoBienBan', (req, res) => {
  connection.query("DELETE FROM bbkiemke WHERE SoBienBan = ?", [req.params.SoBienBan], (err, rows) => {
    if (!err) {
      res.send(`bbkiemke with SoBienBan: ${[req.params.SoBienBan]} has been removed`)
    } else {
      console.log(err); res.status(400).send({ message: err })
    }
  })
})

Router.post('/', (req, res) => {
  const params = req.body;
  connection.query("INSERT INTO bbkiemke SET ?", params, (err, rows) => {
    if (!err) {
      res.send(`bbkiemke with SoBienBan: ${params.SoBienBan} has been added`)
    } else {
      console.log(err); res.status(400).send({ message: err })
    }
  })
})

Router.put('/', (req, res) => {
  const { SoBienBan } = req.body
  connection.query('UPDATE bbkiemke SET ? WHERE SoBienBan = ?', [req.body, SoBienBan], (err, rows) => {
    if (!err) {
      res.send(`bbkiemke with SoBienBan: ${SoBienBan} has been updated`)
    } else {
      console.log(err); res.status(400).send({ message: err })
    }
  })
})  


module.exports = Router;