import { Application } from "./application.js";


const promPodrucja = await fetch("http://localhost:5223/Podrucje/PreuzmiPodrucja");
const podrucja = await promPodrucja.json();

const promOsobine = await fetch("http://localhost:5223/Osobina/PreuzmiOsobine");
const osobine = await promOsobine.json();

console.log(podrucja);
console.log(osobine);

const app = new Application(podrucja, osobine);
app.crtajAplikaciju(document.body);