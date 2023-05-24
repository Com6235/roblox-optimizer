const { log } = require('console')
const fs = require('fs')
const path = require('path')
const folderPath = '/Users/User'

fs.readdirSync(folderPath).map(fileName => {
    log(path.join(folderPath, fileName))
    return path.join(folderPath, fileName)
  })