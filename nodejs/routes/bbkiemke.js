const express = require('express')
const Router = express.Router()
const connection = require("../connection")

Router.get('/', (req, res) => {
  connection.query("SELECT * FROM bbkiemke", (err, rows) => {
    if (!err) {
      res.send(rows)
    } else {
      console.log(err)
    }
  })
})

Router.delete('/:SoBienBan', (req, res) => {
  connection.query("DELETE FROM bbkiemke WHERE SoBienBan = ?", [req.params.SoBienBan], (err, rows) => {
    if (!err) {
      res.send(`bbkiemke with SoBienBan: ${[req.params.SoBienBan]} has been removed`)
    } else {
      console.log(err)
    }
  })
})

Router.post('/', (req, res) => {
  const params = req.body;
  connection.query("INSERT INTO bbkiemke SET ?", params, (err, rows) => {
    if (!err) {
      res.send(`bbkiemke with SoBienBan: ${params.SoBienBan} has been added`)
    } else {
      console.log(err)
    }
  })
})

Router.put('/', (req, res) => {
  const { SoBienBan } = req.body
  connection.query('UPDATE bbkiemke SET ? WHERE SoBienBan = ?', [req.body, SoBienBan], (err, rows) => {
    if (!err) {
      res.send(`bbkiemke with SoBienBan: ${SoBienBan} has been updated`)
    } else {
      console.log(err)
    }
  })
})  


module.exports = Router;