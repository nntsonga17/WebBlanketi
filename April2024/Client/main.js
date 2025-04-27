import { Projekcija } from "./Projekcija.js";

var listaProjekcija=[];

fetch("http://localhost:5027/Projekcija/VratiProjekcije")
.then(p=>p.json())
.then(data=>{
    data.forEach(q=> {
        var film = new Projekcija(q.id, q.naziv, q.vremePrikazivanja, q.sifra, q.brojSale);
        listaProjekcija.push(film);
        film.crtaj(document.body);
    })
})
console.log(listaProjekcija);