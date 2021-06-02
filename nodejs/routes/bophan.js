const express = require('express')
const Router = express.Router()
const connection = require("../connection")

Router.get('/', (req, res) => {
  connection.query("SELECT * FROM bophan", (err, rows) => {
    if (!err) {
      res.send(rows)
    } else {
      console.log(err)
    }
  })
})

Router.delete('/:MaBoPhan', (req, res) => {
  connection.query("DELETE FROM bophan WHERE MaBoPhan = ?", [req.params.MaBoPhan], (err, rows) => {
    if (!err) {
      res.send(`BoPhan with MaBoPhan: ${[req.params.MaBoPhan]} has been removed`)
    } else {
      console.log(err)
    }
  })
})

Router.post('/', (req, res) => {
  const params = req.body;
  connection.query("INSERT INTO bophan SET ?", params, (err, rows) => {
    if (!err) {
      res.send(`BoPhan with MaBoPhan: ${params.MaBoPhan} has been added`)
    } else {
      console.log(err)
    }
  })
})

Router.put('/', (req, res) => {
  const { MaBoPhan } = req.body
  connection.query('UPDATE bophan SET ? WHERE MaBoPhan = ?', [req.body, MaBoPhan], (err, rows) => {
    if (!err) {
      res.send(`BoPhan with MaBoPhan: ${MaBoPhan} has been updated`)
    } else {
      console.log(err)
    }
  })
})  


module.exports = Router;