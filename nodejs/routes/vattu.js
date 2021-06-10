const express = require('express')
const Router = express.Router()
const connection = require("../connection")
const async = require("async")
const { resolve } = require('path')

Router.get('/join', (req, res) => {
  connection.query(
    "SELECT MaVT, TenVT, vattu.MaLoai, TenLoai, vattu.MaDVT, TenDVT, MaTK " +
    "FROM vattu LEFT JOIN loaivattu ON vattu.MaLoai = loaivattu.MaLoai " +
    "LEFT JOIN donvitinh ON vattu.MaDVT = donvitinh.MaDVT",
    (err, rows) => {
      if (!err) {
        res.send(rows)
      } else {
        console.log(err); res.status(400).send({ message: err })
      }
    })
})
Router.get('/', (req, res) => {
  connection.query("SELECT * FROM vattu", (err, rows) => {
    if (!err) {
      res.send(rows)
    } else {
      console.log(err); res.status(400).send({ message: err })
    }
  })
})

Router.delete('/:MaVT', (req, res) => {
  connection.query("DELETE FROM vattu WHERE MaVT = ?", [req.params.MaVT], (err, rows) => {
    if (!err) {
      res.send(`vattu with MaVT: ${[req.params.MaVT]} has been removed`)
    } else {
      console.log(err); res.status(400).send({ message: err })
    }
  })
})

Router.post('/', (req, res) => {
  const params = req.body;
  connection.query("INSERT INTO vattu SET ?", params, (err, rows) => {
    if (!err) {
      res.send(`vattu with MaVT: ${params.MaVT} has been added`)
    } else {
      console.log(err); res.status(400).send({ message: err })
    }
  })
})

Router.put('/', (req, res) => {
  const { MaVT } = req.body
  connection.query('UPDATE vattu SET ? WHERE MaVT = ?', [req.body, MaVT], (err, rows) => {
    if (!err) {
      res.send(`vattu with MaVT: ${MaVT} has been updated`)
    } else {
      console.log(err); res.status(400).send({ message: err })
    }
  })
})
function getMaVT(connection) {
  return new Promise((resolve, reject) => {
    let sql = `SELECT MaVT FROM vattu`
    connection.query(sql, (err, rows) => {
      if (err) reject(err)
      resolve(rows)
    })
  })
}
function getSoLuong(connection, MaVT, data) {
  return new Promise((resolve, reject) => {
    let sql = `SELECT SLThucTe, ThanhTien FROM ct_phieunhap ` +
      `LEFT JOIN phieunhap ON ct_phieunhap.SoPhieu = phieunhap.SoPhieu ` +
      `WHERE MaVT = ? AND NgayNhap >= ? AND NgayNhap <= ?`
    connection.query(sql, [MaVT, data.NgayBD, data.NgayKT], (err, rows) => {
      if (err) reject(err)
      resolve(rows)
    })
  })
}
function getTonKho(connection, MaVT, data) {
  return new Promise((resolve, reject) => {
    let sql = `SELECT SoLuong, ThanhTien FROM dudauvattu WHERE MaVT = ? AND Ngay >= ? AND Ngay <= ?`
    connection.query(sql, [MaVT, data.NgayBD, data.NgayKT], (err, rows) => {
      if (err) reject(err)
      resolve(rows)
    })
  })
}

Router.put('/tinhgiaxuat', (req, res) => {
  const { NgayBD, NgayKT } = req.body
  getMaVT(connection).then(listVT => {
    async.each(listVT, (item, callback) => {
      getSoLuong(connection, item.MaVT, req.body).then(data => {
        console.log(data)
        let TongSL = 0;
        let TongThanhTien = 0;
        if (data.length > 0) {
          for (let i = 0; i < data.length; i++) {
            TongSL += data[i].SLThucTe;
            TongThanhTien += data[i].ThanhTien;
          }
        }
        console.log(TongSL + " " + item.MaVT)
        console.log(TongThanhTien + " " + item.MaVT)
        getTonKho(connection, item.MaVT, req.body).then(list => {
          console.log(list);
          if (list.length > 0) {
            for (let i = 0; i < list.length; i++) {
              TongSL += list[i].SoLuong;
              TongThanhTien += list[i].ThanhTien;
            }
          }
          let DonGiaXuat = TongSL > 0 ? TongThanhTien / TongSL : 0;
          console.log(DonGiaXuat + " " + item.MaVT)
          connection.query(`UPDATE ct_phieuxuat SET DonGia = ?, ThanhTien = SLThucTe * DonGia ` +
            `WHERE MaVT = ?`, [DonGiaXuat, item.MaVT], (err, rows) => {
              if (err) {
                callback(err);
              }
              callback(null);
            })
        })
      })
    }, (err) => {
      if (err) {
        res.status(400).send(err)
      } else {
        res.send("Success")
      }
    })
  })
})


module.exports = Router;