// Script to generate missing boilerplate based on the config file
var fs = require("fs");
var config = require("../config.json");
var sessionCookie = process.argv[2];

if (!sessionCookie) {
  console.error("Error: Missing Session Cookie Argument");
  return;
}

config.years.forEach((year) => {
  // Create Folder
  let dir = `src/${year.year}`;
  if (!fs.existsSync(dir)) {
    fs.mkdirSync(dir);
  }

  // Create boilerplate files for each day
  for (let i = 1; i <= year.maxDay; i++) {
    let dayDir = `${dir}/${i}`;
    if (!fs.existsSync(dayDir)) {
      fs.mkdirSync(dayDir);
    } else continue;
  }
});
