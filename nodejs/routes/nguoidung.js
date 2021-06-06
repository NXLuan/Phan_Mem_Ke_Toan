const express = require("express");
const { config } = require("../connection");
const Router = express.Router();
const connection = require("../connection");

Router.get("/", (req, res) => {
  connection.query("SELECT * FROM nguoidung", (err, rows) => {
    if (!err) {
      res.send(rows);
    } else {
      console.log(err);
    }
  });
});

Router.get("/token", (req, res) => {
  const {TenDangNhap, MatKhau} = req.query;
  connection.query(
    "SELECT * FROM nguoidung WHERE TenDangNhap = ? AND MatKhau = ?",
    [TenDangNhap, Buffer.from(MatKhau, "utf8").toString("base64")],
    (err, rows) => {
      if (!err) {
        res.send(rows);
      } else {
        console.log(err);
      }
    }
  );
});

Router.delete("/:TenDangNhap", (req, res) => {
  connection.query(
    "DELETE FROM nguoidung WHERE TenDangNhap = ?",
    [req.params.TenDangNhap],
    (err, rows) => {
      if (!err) {
        res.send(
          `nguoidung with TenDangNhap: ${[
            req.params.TenDangNhap,
          ]} has been removed`
        );
      } else {
        console.log(err);
      }
    }
  );
});

Router.post("/", (req, res) => {
  const params = req.body;
  params.MatKhau = Buffer.from(params.MatKhau, "utf8").toString("base64");
  connection.query("INSERT INTO nguoidung SET ?", params, (err, rows) => {
    if (!err) {
      res.send(
        `nguoidung with TenDangNhap: ${params.TenDangNhap} has been added`
      );
    } else {
      res.status(412).send(err.message);
    }
  });
});

Router.put("/", (req, res) => {
  const { TenDangNhap } = req.body;
  connection.query(
    "UPDATE nguoidung SET ? WHERE TenDangNhap = ?",
    [req.body, TenDangNhap],
    (err, rows) => {
      if (!err) {
        res.send(`nguoidung with TenDangNhap: ${TenDangNhap} has been updated`);
      } else {
        console.log(err);
      }
    }
  );
});

module.exports = Router;
