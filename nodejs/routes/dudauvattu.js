const express = require('express')
const Router = express.Router()
const connection = require("../connection")

Router.get('/tonkhothang', (req, res) => {
  const {MaVT, MaKho, Thang, Nam} = req.query
  let sql = `SELECT * FROM dudauvattu WHERE MaVT = ? AND MaKho = ? AND MONTH(Ngay) = ? AND YEAR(Ngay) = ? ` +
  `ORDER BY Ngay ASC LIMIT 1`
  connection.query(sql, [MaVT, MaKho, Thang, Nam], (err, rows) => {
    if (!err) {
      res.send(rows)
    } else {
      console.log(err); res.status(400).send({ message: err })
    }
  })
})


Router.get('/tonkho', (req, res) => {
  const {MaVT, MaKho, NgayLap} = req.query
  let sql = `SELECT * FROM dudauvattu WHERE MaVT = ? AND MaKho = ? AND Ngay <= ? ` +
  `ORDER BY Ngay DESC LIMIT 1`
  connection.query(sql, [MaVT, MaKho, NgayLap], (err, rows) => {
    if (!err) {
      res.send(rows)
    } else {
      console.log(err); res.status(400).send({ message: err })
    }
  })
})


Router.get('/', (req, res) => {
  connection.query("SELECT * FROM dudauvattu", (err, rows) => {
    if (!err) {
      res.send(rows)
    } else {
      console.log(err); res.status(400).send({ message: err })
    }
  })
})

Router.delete('/:MaSo', (req, res) => {
  connection.query("DELETE FROM dudauvattu WHERE MaSo = ?", [req.params.MaSo], (err, rows) => {
    if (!err) {
      res.send(`dudauvattu with MaSo: ${[req.params.MaSo]} has been removed`)
    } else {
      console.log(err); res.status(400).send({ message: err })
    }
  })
})

Router.post('/', (req, res) => {
  const params = req.body;
  connection.query("INSERT INTO dudauvattu SET ?", params, (err, rows) => {
    if (!err) {
      res.send(`dudauvattu with MaSo: ${params.MaSo} has been added`)
    } else {
      console.log(err); res.status(400).send({ message: err })
    }
  })
})

Router.put('/', (req, res) => {
  const { MaSo } = req.body
  connection.query('UPDATE dudauvattu SET ? WHERE MaSo = ?', [req.body, MaSo], (err, rows) => {
    if (!err) {
      res.send(`dudauvattu with MaSo: ${MaSo} has been updated`)
    } else {
      console.log(err); res.status(400).send({ message: err })
    }
  })
})
module.exports = Router;