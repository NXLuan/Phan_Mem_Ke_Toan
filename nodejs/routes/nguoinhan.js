const express = require('express')
const Router = express.Router()
const connection = require("../connection")

Router.get('/join', (req, res) => {
  connection.query(
    "SELECT MaNguoiNhan, TenNguoiNhan, nguoinhan.DiaChi, nguoiNhan.MaCongTrinh, TenCongTrinh FROM nguoiNhan LEFT JOIN congtrinh ON nguoiNhan.MaCongTrinh = congtrinh.MaCongTrinh",
    (err, rows) => {
      if (!err) {
        res.send(rows)
      } else {
        console.log(err); res.status(400).send({ message: err })
      }
    })
})
Router.get('/', (req, res) => {
  connection.query("SELECT * FROM nguoinhan", (err, rows) => {
    if (!err) {
      res.send(rows)
    } else {
      console.log(err); res.status(400).send({ message: err })
    }
  })
})

Router.delete('/:MaNguoiNhan', (req, res) => {
  connection.query("DELETE FROM nguoinhan WHERE MaNguoiNhan = ?", [req.params.MaNguoiNhan], (err, rows) => {
    if (!err) {
      res.send(`nguoinhan with MaNguoiNhan: ${[req.params.MaNguoiNhan]} has been removed`)
    } else {
      console.log(err); res.status(400).send({ message: err })
    }
  })
})

Router.post('/', (req, res) => {
  const params = req.body;
  connection.query("INSERT INTO nguoinhan SET ?", params, (err, rows) => {
    if (!err) {
      res.send(`nguoinhan with MaNguoiNhan: ${params.MaNguoiNhan} has been added`)
    } else {
      console.log(err); res.status(400).send({ message: err })
    }
  })
})

Router.put('/', (req, res) => {
  const { MaNguoiNhan } = req.body
  connection.query('UPDATE nguoinhan SET ? WHERE MaNguoiNhan = ?', [req.body, MaNguoiNhan], (err, rows) => {
    if (!err) {
      res.send(`nguoinhan with MaNguoiNhan: ${MaNguoiNhan} has been updated`)
    } else {
      console.log(err); res.status(400).send({ message: err })
    }
  })
})  


module.exports = Router;