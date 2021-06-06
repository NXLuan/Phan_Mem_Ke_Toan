const mysql = require('mysql')

const connection = mysql.createConnection({
  host: "localhost",
  user: "root",
  password: "",
  database: "ketoan",
  multipleStatements: true,
  dateStrings: true,
})

connection.connect((err) => {
  if (!err) {
    console.log("Connected")
  } else {
    console.log("Connection failed")
  }
})

module.exports = connection