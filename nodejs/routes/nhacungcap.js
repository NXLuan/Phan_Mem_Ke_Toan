const express = require('express')
const Router = express.Router()
const connection = require("../connection")

Router.get('/', (req, res) => {
  connection.query("SELECT * FROM nhacungcap", (err, rows) => {
    if (!err) {
      res.send(rows)
    } else {
      console.log(err); res.status(400).send({ message: err })
    }
  })
})

Router.delete('/:MaNCC', (req, res) => {
  connection.query("DELETE FROM nhacungcap WHERE MaNCC = ?", [req.params.MaNCC], (err, rows) => {
    if (!err) {
      res.send(`nhacungcap with MaNCC: ${[req.params.MaNCC]} has been removed`)
    } else {
      console.log(err); res.status(400).send({ message: err })
    }
  })
})

Router.post('/', (req, res) => {
  const params = req.body;
  connection.query("INSERT INTO nhacungcap SET ?", params, (err, rows) => {
    if (!err) {
      res.send(`nhacungcap with MaNCC: ${params.MaNCC} has been added`)
    } else {
      console.log(err); res.status(400).send({ message: err })
    }
  })
})

Router.put('/', (req, res) => {
  const { MaNCC } = req.body
  connection.query('UPDATE nhacungcap SET ? WHERE MaNCC = ?', [req.body, MaNCC], (err, rows) => {
    if (!err) {
      res.send(`nhacungcap with MaNCC: ${MaNCC} has been updated`)
    } else {
      console.log(err); res.status(400).send({ message: err })
    }
  })
})  


module.exports = Router;