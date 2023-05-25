const fs = require('fs')
const path = require('path')
const os = require('os');

const vers = path.join(os.homedir(), "AppData", "Local", "Roblox", "Versions")

function copyFile(sourcePath, destinationPath, cb) {
  fs.copyFile(sourcePath, destinationPath, (err) => {
    if (err) return cb(err);
    cb(null);
  });
}

function getLastModifiedSubDir(dirPath) {
  let lastModified = new Date(0);
  let lastModifiedDir = null;

  const files = fs.readdirSync(dirPath);

  files.forEach(file => {
    const filePath = path.join(dirPath, file);
    const stat = fs.statSync(filePath);
    if (stat.isDirectory() && stat.mtime > lastModified) {
      lastModified = stat.mtime;
      lastModifiedDir = filePath;
    }
  });

  return lastModifiedDir;
}

const lastModifiedSubDir = getLastModifiedSubDir(vers);
console.log(`Last modified subdirectory: ${lastModifiedSubDir}`);

if (lastModifiedSubDir && fs.existsSync(path.join(lastModifiedSubDir, 'ClientSettings'))) {
  console.log('ClientSettings directory found in last modified subdirectory, checking for fflags');
  if (lastModifiedSubDir && fs.existsSync(path.join(lastModifiedSubDir, 'ClientSettings', "ClientAppSettings.json"))) {
    console.log("Roblox Client already optimized")
  } else {
    const sourceFilePath = path.join(__dirname, 'ClientAppSettings.json');
    const destinationFilePath = path.join(lastModifiedSubDir, 'ClientSettings', 'ClientAppSettings.json');
    console.log(sourceFilePath)
    copyFile(sourceFilePath, destinationFilePath, (err) => {
      if (err) throw err;
      console.log('File was copied successfully!');
    });
  }
} else {
  console.log('ClientSettings directory not found in last modified subdirectory, creating');
  fs.mkdirSync(path.join(lastModifiedSubDir, 'ClientSettings'), (err) => {
    if (err) throw err;
    console.log('Directory was copied successfully!');
  })
  const sourceFilePath = path.join(__dirname, 'ClientAppSettings.json');
  console.log(sourceFilePath)
  const destinationFilePath = path.join(lastModifiedSubDir, 'ClientSettings', 'ClientAppSettings.json');
  copyFile(sourceFilePath, destinationFilePath, (err) => {
    if (err) throw err;
      console.log('File was copied successfully!');
  });
}