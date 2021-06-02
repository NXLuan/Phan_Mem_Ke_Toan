const express = require('express')
const Router = express.Router()
const connection = require("../connection")

Router.get('/', (req, res) => {
  connection.query("SELECT * FROM ct_bbkiemke", (err, rows) => {
    if (!err) {
      res.send(rows)
    } else {
      console.log(err)
    }
  })
})

Router.delete('/:MaSo', (req, res) => {
  connection.query("DELETE FROM ct_bbkiemke WHERE MaSo = ?", [req.params.MaSo], (err, rows) => {
    if (!err) {
      res.send(`ct_bbkiemke with MaSo: ${[req.params.MaSo]} has been removed`)
    } else {
      console.log(err)
    }
  })
})

Router.post('/', (req, res) => {
  const params = req.body;
  connection.query("INSERT INTO ct_bbkiemke SET ?", params, (err, rows) => {
    if (!err) {
      res.send(`ct_bbkiemke with MaSo: ${params.MaSo} has been added`)
    } else {
      console.log(err)
    }
  })
})

Router.put('/', (req, res) => {
  const { MaSo } = req.body
  connection.query('UPDATE ct_bbkiemke SET ? WHERE MaSo = ?', [req.body, MaSo], (err, rows) => {
    if (!err) {
      res.send(`ct_bbkiemke with MaSo: ${MaSo} has been updated`)
    } else {
      console.log(err)
    }
  })
})  


module.exports = Router;