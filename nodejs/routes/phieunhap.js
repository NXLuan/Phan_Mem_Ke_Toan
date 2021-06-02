const express = require('express')
const Router = express.Router()
const connection = require("../connection")

Router.get('/', (req, res) => {
  connection.query("SELECT * FROM phieunhap", (err, rows) => {
    if (!err) {
      res.send(rows)
    } else {
      console.log(err)
    }
  })
})

Router.delete('/:SoPhieu', (req, res) => {
  connection.query("DELETE FROM phieunhap WHERE SoPhieu = ?", [req.params.SoPhieu], (err, rows) => {
    if (!err) {
      res.send(`phieunhap with SoPhieu: ${[req.params.SoPhieu]} has been removed`)
    } else {
      console.log(err)
    }
  })
})

Router.post('/', (req, res) => {
  const params = req.body;
  connection.query("INSERT INTO phieunhap SET ?", params, (err, rows) => {
    if (!err) {
      res.send(`phieunhap with SoPhieu: ${params.SoPhieu} has been added`)
    } else {
      console.log(err)
    }
  })
})

Router.put('/', (req, res) => {
  const { SoPhieu } = req.body
  connection.query('UPDATE phieunhap SET ? WHERE SoPhieu = ?', [req.body, SoPhieu], (err, rows) => {
    if (!err) {
      res.send(`phieunhap with SoPhieu: ${SoPhieu} has been updated`)
    } else {
      console.log(err)
    }
  })
})  


module.exports = Router;