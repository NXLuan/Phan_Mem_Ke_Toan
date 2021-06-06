const express = require('express')
const Router = express.Router()
const connection = require("../connection")

Router.get('/join', (req, res) => {
  connection.query(
    "SELECT MaNguoiGiao, TenNguoiGiao, nguoigiao.DiaChi, nguoigiao.MaNCC, TenNCC FROM nguoigiao LEFT JOIN nhacungcap ON nguoigiao.MaNCC = nhacungcap.MaNCC",
    (err, rows) => {
      if (!err) {
        res.send(rows)
      } else {
        console.log(err); res.status(400).send({ message: err })
      }
    })
})
Router.get('/', (req, res) => {
  connection.query("SELECT * FROM nguoigiao", (err, rows) => {
    if (!err) {
      res.send(rows)
    } else {
      console.log(err); res.status(400).send({ message: err })
    }
  })
})

Router.delete('/:MaNguoiGiao', (req, res) => {
  connection.query("DELETE FROM nguoigiao WHERE MaNguoiGiao = ?", [req.params.MaNguoiGiao], (err, rows) => {
    if (!err) {
      res.send(`nguoigiao with MaNguoiGiao: ${[req.params.MaNguoiGiao]} has been removed`)
    } else {
      console.log(err); res.status(400).send({ message: err })
    }
  })
})

Router.post('/', (req, res) => {
  const params = req.body;
  connection.query("INSERT INTO nguoigiao SET ?", params, (err, rows) => {
    if (!err) {
      res.send(`nguoigiao with MaNguoiGiao: ${params.MaNguoiGiao} has been added`)
    } else {
      console.log(err); res.status(400).send({ message: err })
    }
  })
})

Router.put('/', (req, res) => {
  const { MaNguoiGiao } = req.body
  connection.query('UPDATE nguoigiao SET ? WHERE MaNguoiGiao = ?', [req.body, MaNguoiGiao], (err, rows) => {
    if (!err) {
      res.send(`nguoigiao with MaNguoiGiao: ${MaNguoiGiao} has been updated`)
    } else {
      console.log(err); res.status(400).send({ message: err })
    }
  })
})  


module.exports = Router;