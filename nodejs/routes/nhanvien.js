const express = require('express')
const Router = express.Router()
const connection = require("../connection")

Router.get('/join', (req, res) => {
  connection.query(
    "SELECT MaNV, TenNV, nhanvien.MaBoPhan, TenBoPhan FROM nhanvien LEFT JOIN bophan ON nhanvien.MaBoPhan = bophan.MaBoPhan",
    (err, rows) => {
      if (!err) {
        res.send(rows)
      } else {
        console.log(err)
      }
    })
})
Router.get('/', (req, res) => {
  connection.query("SELECT * FROM nhanvien", (err, rows) => {
    if (!err) {
      res.send(rows)
    } else {
      console.log(err)
    }
  })
})
Router.delete('/:MaNV', (req, res) => {
  connection.query("DELETE FROM nhanvien WHERE MaNV = ?", [req.params.MaNV], (err, rows) => {
    if (!err) {
      res.send(`NhanVien with MaNV: ${[req.params.MaNV]} has been removed`)
    } else {
      console.log(err)
    }
  })
})

Router.post('/', (req, res) => {
  const params = req.body;
  connection.query("INSERT INTO nhanvien SET ?", params, (err, rows) => {
    if (!err) {
      res.send(`NhanVien with MaNV: ${params.MaNV} has been added`)
    } else {
      console.log(err)
    }
  })
})

Router.put('/', (req, res) => {
  const { MaNV } = req.body
  connection.query('UPDATE nhanvien SET ? WHERE MaNV = ?', [req.body, MaNV], (err, rows) => {
    if (!err) {
      res.send(`NhanVien with MaNV: ${MaNV} has been updated`)
    } else {
      console.log(err)
    }
  })
})


module.exports = Router;