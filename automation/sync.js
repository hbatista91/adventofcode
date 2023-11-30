// Script to generate missing boilerplate based on the config file
import got from "got";
import fs from "fs";
import jsdom from "jsdom";

import { createRequire } from "module";
const require = createRequire(import.meta.url);
const config = require("../config.json");

const { JSDOM } = jsdom;
const sessionCookie = process.argv[2];

if (!sessionCookie) {
  throw new Error("Missing Session Cookie Argument");
}
/*
const vgmUrl = "https://adventofcode.com/2022/day/1";

got(vgmUrl)
  .then((response) => {
    const dom = new JSDOM(response.body);
    let article = dom.window.document.querySelector(".day-desc");
    let title = article.firstChild.textContent;
    article.removeChild(article.firstChild);
  })
  .catch((err) => {
    console.log(err);
  });

//<article class="day-desc">
*/

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

    // Fetch content from web
    got(`https://adventofcode.com/${year.year}/day/${i}`)
      .then((response) => {
        const dom = new JSDOM(response.body);
        let article = dom.window.document.querySelector(".day-desc");
        let title = article.firstChild.textContent;
        article.removeChild(article.firstChild);

        console.log(`./src/${year.year}/${i}/README.md`);
        let writeStream = fs.createWriteStream(
          `./src/${year.year}/${i}/README.md`
        );
        // TODO
        writeStream.end();

        writeStream = fs.createWriteStream(
          `./src/${year.year}/${i}/part-one.js`
        );
        // TODO
        writeStream.end();

        writeStream = fs.createWriteStream(
          `./src/${year.year}/${i}/part-two.js`
        );
        // TODO
        writeStream.end();
      })
      .catch((err) => {
        console.log(err);
      });
  }
});
