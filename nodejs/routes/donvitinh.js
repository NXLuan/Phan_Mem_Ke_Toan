const express = require('express')
const Router = express.Router()
const connection = require("../connection")
Router.get('/', (req, res) => {
  connection.query("SELECT * FROM donvitinh", (err, rows) => {
    if (!err) {
      res.send(rows)
    } else {
      res.status(400).send({
        message: err
      })
    }
  })
})

Router.delete('/:MaDVT', (req, res) => {
  connection.query("DELETE FROM donvitinh WHERE MaDVT = ?", [req.params.MaDVT], (err, rows) => {
    if (!err) {
      res.send(`donvitinh with MaDVT: ${[req.params.MaDVT]} has been removed`)
    } else {
      console.log(err)
    }
  })
})

Router.post('/', (req, res) => {
  const params = req.body;
  connection.query("INSERT INTO donvitinh SET ?", params, (err, rows) => {
    if (!err) {
      res.send(`donvitinh with MaDVT: ${params.MaDVT} has been added`)
    } else {
      console.log(err)
    }
  })
})

Router.put('/', (req, res) => {
  const { MaDVT } = req.body
  connection.query('UPDATE donvitinh SET ? WHERE MaDVT = ?', [req.body, MaDVT], (err, rows) => {
    if (!err) {
      res.send(`donvitinh with MaDVT: ${MaDVT} has been updated`)
    } else {
      console.log(err)
    }
  })
})  


module.exports = Router;