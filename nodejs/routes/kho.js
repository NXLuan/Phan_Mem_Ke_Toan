const express = require('express')
const Router = express.Router()
const connection = require("../connection")

Router.get('/join', (req, res) => {
  connection.query(
    "SELECT MaKho, TenKho, DiaChi, SDT, kho.MaThuKho, TenNV " +
    "FROM kho LEFT JOIN nhanvien ON kho.MaThuKho = nhanvien.MaNV",
    (err, rows) => {
      if (!err) {
        res.send(rows)
      } else {
        console.log(err); res.status(400).send({ message: err })
      }
    })
})

Router.get('/', (req, res) => {
  connection.query("SELECT * FROM kho", (err, rows) => {
    if (!err) {
      res.send(rows)
    } else {
      console.log(err); res.status(400).send({ message: err })
    }
  })
})

Router.delete('/:MaKho', (req, res) => {
  connection.query("DELETE FROM kho WHERE MaKho = ?", [req.params.MaKho], (err, rows) => {
    if (!err) {
      res.send(`Kho with MaKho: ${[req.params.MaKho]} has been removed`)
    } else {
      console.log(err); res.status(400).send({ message: err })
    }
  })
})

Router.post('/', (req, res) => {
  const params = req.body;
  connection.query("INSERT INTO kho SET ?", params, (err, rows) => {
    if (!err) {
      res.send(`Kho with MaKho: ${params.MaKho} has been added`)
    } else {
      console.log(err); res.status(400).send({ message: err })
    }
  })
})

Router.put('/', (req, res) => {
  const { MaKho } = req.body
  connection.query('UPDATE kho SET ? WHERE MaKho = ?', [req.body, MaKho], (err, rows) => {
    if (!err) {
      res.send(`Kho with MaKho: ${MaKho} has been updated`)
    } else {
      console.log(err); res.status(400).send({ message: err })
    }
  })
})  


module.exports = Router;