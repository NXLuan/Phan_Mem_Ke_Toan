const express = require('express')
const Router = express.Router()
const connection = require("../connection")

Router.get('/', (req, res) => {
  connection.query("SELECT * FROM taikhoan", (err, rows) => {
    if (!err) {
      res.send(rows)
    } else {
      console.log(err); res.status(400).send({ message: err })
    }
  })
})

Router.delete('/:MaTK', (req, res) => {
  connection.query("DELETE FROM taikhoan WHERE MaTK = ?", [req.params.MaTK], (err, rows) => {
    if (!err) {
      res.send(`TaiKhoan with MaTK: ${[req.params.MaTK]} has been removed`)
    } else {
      console.log(err); res.status(400).send({ message: err })
    }
  })
})

Router.post('/', (req, res) => {
  const params = req.body;
  connection.query("INSERT INTO taikhoan SET ?", params, (err, rows) => {
    if (!err) {
      res.send(`TaiKhoan with MaTK: ${params.MaTK} has been added`)
    } else {
      console.log(err); res.status(400).send({ message: err })
      res.status(400).send({
        message: err
      })
    }
  })
})
Router.put('/', (req, res) => {
  const { MaTK } = req.body
  connection.query('UPDATE taikhoan SET ? WHERE MaTK = ?', [req.body, MaTK], (err, rows) => {
    if (!err) {
      res.send(`TaiKhoan with MaTK: ${MaTK} has been updated`)
    } else {
      console.log(err); res.status(400).send({ message: err })
    }
  })
})


  module.exports = Router;