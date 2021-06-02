const express = require('express')
const Router = express.Router()
const connection = require("../connection")

Router.get('/', (req, res) => {
  connection.query("SELECT * FROM phieuxuat", (err, rows) => {
    if (!err) {
      res.send(rows)
    } else {
      console.log(err)
    }
  })
})

Router.delete('/:SoPhieu', (req, res) => {
  connection.query("DELETE FROM phieuxuat WHERE SoPhieu = ?", [req.params.SoPhieu], (err, rows) => {
    if (!err) {
      res.send(`phieuxuat with SoPhieu: ${[req.params.SoPhieu]} has been removed`)
    } else {
      console.log(err)
    }
  })
})

Router.post('/', (req, res) => {
  const params = req.body;
  connection.query("INSERT INTO phieuxuat SET ?", params, (err, rows) => {
    if (!err) {
      res.send(`phieuxuat with SoPhieu: ${params.SoPhieu} has been added`)
    } else {
      console.log(err)
    }
  })
})

Router.put('/', (req, res) => {
  const { SoPhieu } = req.body
  connection.query('UPDATE phieuxuat SET ? WHERE SoPhieu = ?', [req.body, SoPhieu], (err, rows) => {
    if (!err) {
      res.send(`phieuxuat with SoPhieu: ${SoPhieu} has been updated`)
    } else {
      console.log(err)
    }
  })
})  


module.exports = Router;