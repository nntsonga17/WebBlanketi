import { Trka } from "./Trka.js";

var listaTrka = [];
fetch("http://localhost:5141/Trka/VratiTrke")
.then(p=>p.json())
.then(q=> {
    q.forEach(t=>{
        var trka = new Trka(t.id, t.lokacija, t.duzinaStaze, t.brojTakmicara, t.trajanjeTrke, t.pocetakTrke);
        listaTrka.push(trka);
        trka.crtaj(document.body);
    })
})
console.log(listaTrka);