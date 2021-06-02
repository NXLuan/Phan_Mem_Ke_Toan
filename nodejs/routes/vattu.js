const express = require('express')
const Router = express.Router()
const connection = require("../connection")

Router.get('/', (req, res) => {
  connection.query("SELECT * FROM vattu", (err, rows) => {
    if (!err) {
      res.send(rows)
    } else {
      console.log(err)
    }
  })
})

Router.delete('/:MaVT', (req, res) => {
  connection.query("DELETE FROM vattu WHERE MaVT = ?", [req.params.MaVT], (err, rows) => {
    if (!err) {
      res.send(`vattu with MaVT: ${[req.params.MaVT]} has been removed`)
    } else {
      console.log(err)
    }
  })
})

Router.post('/', (req, res) => {
  const params = req.body;
  connection.query("INSERT INTO vattu SET ?", params, (err, rows) => {
    if (!err) {
      res.send(`vattu with MaVT: ${params.MaVT} has been added`)
    } else {
      console.log(err)
    }
  })
})

Router.put('/', (req, res) => {
  const { MaVT } = req.body
  connection.query('UPDATE vattu SET ? WHERE MaVT = ?', [req.body, MaVT], (err, rows) => {
    if (!err) {
      res.send(`vattu with MaVT: ${MaVT} has been updated`)
    } else {
      console.log(err)
    }
  })
})  


module.exports = Router;