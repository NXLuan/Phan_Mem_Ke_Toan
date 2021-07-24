const express = require('express')
const Router = express.Router()
const connection = require('../connection')

Router.get('/:SoBienBan', (req, res) => {
  connection.query(
    'SELECT MaSo, SoBienBan, ct_bbkiemke.MaVT, TenVT, TenDVT, DonGia, SLSoSach,	SLThucTe,	SLThua,	SLThieu, SLPhamChatTot, SLPhamChatKem, SLMatPhamChat ' +
      'FROM ct_bbkiemke LEFT JOIN vattu ON ct_bbkiemke.MaVT = vattu.MaVT ' +
      'LEFT JOIN donvitinh ON vattu.MaDVT = donvitinh.MaDVT ' +
      'WHERE SoBienBan = ?',
    [req.params.SoBienBan],
    (err, rows) => {
      if (!err) {
        res.send(rows)
      } else {
        console.log(err)
        res.status(400).send({ message: err })
      }
    }
  )
})
Router.get('/', (req, res) => {
  connection.query('SELECT * FROM ct_bbkiemke', (err, rows) => {
    if (!err) {
      res.send(rows)
    } else {
      console.log(err)
      res.status(400).send({ message: err })
    }
  })
})

Router.delete('/:SoBienBan', (req, res) => {
  connection.query(
    'DELETE FROM ct_bbkiemke WHERE SoBienBan = ?',
    [req.params.SoBienBan],
    (err, rows) => {
      if (!err) {
        res.send(
          `ct_bbkiemke with SoBienBan: ${[
            req.params.SoBienBan,
          ]} has been removed`
        )
      } else {
        console.log(err)
        res.status(400).send({ message: err })
      }
    }
  )
})

Router.post('/', (req, res) => {
  const params = req.body
  connection.query('INSERT INTO ct_bbkiemke SET ?', params, (err, rows) => {
    if (!err) {
      res.send(`ct_bbkiemke with MaSo: ${params.MaSo} has been added`)
    } else {
      console.log(err)
      res.status(400).send({ message: err })
    }
  })
})

Router.put('/', (req, res) => {
  const { MaSo } = req.body
  connection.query(
    'UPDATE ct_bbkiemke SET ? WHERE MaSo = ?',
    [req.body, MaSo],
    (err, rows) => {
      if (!err) {
        res.send(`ct_bbkiemke with MaSo: ${MaSo} has been updated`)
      } else {
        console.log(err)
        res.status(400).send({ message: err })
      }
    }
  )
})

module.exports = Router
