const express = require('express')
const Router = express.Router()
const connection = require("../connection")

Router.get('/', (req, res) => {
  connection.query("SELECT * FROM ct_phieunhap", (err, rows) => {
    if (!err) {
      res.send(rows)
    } else {
      console.log(err)
    }
  })
})

Router.delete('/:MaSo', (req, res) => {
  connection.query("DELETE FROM ct_phieunhap WHERE MaSo = ?", [req.params.MaSo], (err, rows) => {
    if (!err) {
      res.send(`ct_phieunhap with MaSo: ${[req.params.MaSo]} has been removed`)
    } else {
      console.log(err)
    }
  })
})

Router.post('/', (req, res) => {
  const params = req.body;
  connection.query("INSERT INTO ct_phieunhap SET ?", params, (err, rows) => {
    if (!err) {
      res.send(`ct_phieunhap with MaSo: ${params.MaSo} has been added`)
    } else {
      console.log(err)
    }
  })
})

Router.put('/', (req, res) => {
  const { MaSo } = req.body
  connection.query('UPDATE ct_phieunhap SET ? WHERE MaSo = ?', [req.body, MaSo], (err, rows) => {
    if (!err) {
      res.send(`ct_phieunhap with MaSo: ${MaSo} has been updated`)
    } else {
      console.log(err)
    }
  })
})  


module.exports = Router;