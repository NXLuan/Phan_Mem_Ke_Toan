const express = require('express')
const Router = express.Router()
const connection = require("../connection")

Router.get('/', (req, res) => {
  connection.query("SELECT * FROM congtrinh", (err, rows) => {
    if (!err) {
      res.send(rows)
    } else {
      console.log(err)
    }
  })
})

Router.delete('/:MaCongTrinh', (req, res) => {
  connection.query("DELETE FROM congtrinh WHERE MaCongTrinh = ?", [req.params.MaCongTrinh], (err, rows) => {
    if (!err) {
      res.send(`congtrinh with MaCongTrinh: ${[req.params.MaCongTrinh]} has been removed`)
    } else {
      console.log(err)
    }
  })
})

Router.post('/', (req, res) => {
  const params = req.body;
  connection.query("INSERT INTO congtrinh SET ?", params, (err, rows) => {
    if (!err) {
      res.send(`congtrinh with MaCongTrinh: ${params.MaCongTrinh} has been added`)
    } else {
      console.log(err)
    }
  })
})

Router.put('/', (req, res) => {
  const { MaCongTrinh } = req.body
  connection.query('UPDATE congtrinh SET ? WHERE MaCongTrinh = ?', [req.body, MaCongTrinh], (err, rows) => {
    if (!err) {
      res.send(`congtrinh with MaCongTrinh: ${MaCongTrinh} has been updated`)
    } else {
      console.log(err)
    }
  })
})  


module.exports = Router;