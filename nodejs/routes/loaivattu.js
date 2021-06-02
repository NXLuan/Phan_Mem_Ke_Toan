const express = require('express')
const Router = express.Router()
const connection = require("../connection")

Router.get('/', (req, res) => {
  connection.query("SELECT * FROM loaivattu", (err, rows) => {
    if (!err) {
      res.send(rows)
    } else {
      console.log(err)
    }
  })
})

Router.delete('/:MaLoai', (req, res) => {
  connection.query("DELETE FROM loaivattu WHERE MaLoai = ?", [req.params.MaLoai], (err, rows) => {
    if (!err) {
      res.send(`loaivattu with MaLoai: ${[req.params.MaLoai]} has been removed`)
    } else {
      console.log(err)
    }
  })
})

Router.post('/', (req, res) => {
  const params = req.body;
  connection.query("INSERT INTO loaivattu SET ?", params, (err, rows) => {
    if (!err) {
      res.send(`loaivattu with MaLoai: ${params.MaLoai} has been added`)
    } else {
      console.log(err)
    }
  })
})

Router.put('/', (req, res) => {
  const { MaLoai } = req.body
  connection.query('UPDATE loaivattu SET ? WHERE MaLoai = ?', [req.body, MaLoai], (err, rows) => {
    if (!err) {
      res.send(`loaivattu with MaLoai: ${MaLoai} has been updated`)
    } else {
      console.log(err)
    }
  })
})  


module.exports = Router;