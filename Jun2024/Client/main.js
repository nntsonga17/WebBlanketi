import { Automobil } from "./Automobil.js";

var host = document.body;

var glavniDiv = document.createElement("div");
glavniDiv.className="glavniDiv";
host.appendChild(glavniDiv);

var divPretraga = document.createElement("div");
divPretraga.className="pretraga";
glavniDiv.appendChild(divPretraga);

var divRezultat = document.createElement("div");
divRezultat.className="Rezultat";
glavniDiv.appendChild(divRezultat);

var crtajred = (h) => 
    {
        var d = document.createElement("div");
        d.className = "red";
        h.appendChild(d);
    
        return d;
    }

var divKorisnik = document.createElement("div");
divKorisnik.className="korisnik";
divPretraga.appendChild(divKorisnik);

var red = crtajred(divKorisnik);
var l = document.createElement("label");
l.innerHTML="Ime";
red.appendChild(l);
var tb = document.createElement("input");
tb.type="text";
tb.className="ime";
red.appendChild(tb);

red = crtajred(divKorisnik);
var l = document.createElement("label");
l.innerHTML="Prezime";
red.appendChild(l);
var tb = document.createElement("input");
tb.type="text";
tb.className="prezime";
red.appendChild(tb);

red = crtajred(divKorisnik);
var l = document.createElement("label");
l.innerHTML="JMBG";
red.appendChild(l);
var tb = document.createElement("input");
tb.className="jmbg";
tb.type="text";
red.appendChild(tb);

red = crtajred(divKorisnik);
var l = document.createElement("label");
l.innerHTML="Broj Vozacke";
red.appendChild(l);
var tb = document.createElement("input");
tb.type="text";
tb.className="vozacka";
red.appendChild(tb);

red = crtajred(divKorisnik);
var l = document.createElement("label");
l.innerHTML="Broj dana";
red.appendChild(l);
var tb = document.createElement("input");
tb.className="dani";
tb.type="number";
red.appendChild(tb);

var divAutomobili = document.createElement("div");
divAutomobili.className="Automobili";
divPretraga.appendChild(divAutomobili);

red = crtajred(divAutomobili);
var l = document.createElement("label");
l.innerHTML="Predjena kilometraza";
red.appendChild(l);
var tb = document.createElement("input");
tb.className="km";
tb.type="number";
red.appendChild(tb);

red = crtajred(divAutomobili);
var l = document.createElement("label");
l.innerHTML="Broj sedista";
red.appendChild(l);
var tb = document.createElement("input");
tb.className="brSedista";
tb.type="number";
red.appendChild(tb);

red = crtajred(divAutomobili);
var l = document.createElement("label");
l.innerHTML="Cena";
red.appendChild(l);
var tb = document.createElement("input");
tb.className="cena";
tb.type="number";
red.appendChild(tb);

var listaAutomobila=[];

fetch("http://localhost:5237/Automobil/VratiAutomobile")
.then(p => p.json())
.then(q => {
    q.forEach(a => {
        var auto = new Automobil(a.id, a.model, a.predjeniKM, a.godiste, a.brojSedista, a.cenaPoDanu, a.iznajmljen);
        listaAutomobila.push(auto);
    });

    // Kreiraj red, label i select POSLE što se napuni lista
    var red = crtajred(divAutomobili);
    var l = document.createElement("label");
    l.innerHTML = "Model";
    red.appendChild(l);

    var select = document.createElement("select");
    red.appendChild(select);

    let modeliSet = new Set();

    listaAutomobila.forEach(p => {
        if (!modeliSet.has(p.model)) {
            modeliSet.add(p.model);

            var option = document.createElement("option");
            option.innerHTML = p.model;
            option.value = p.model;
            select.appendChild(option);
        }
    });
    red = crtajred(divAutomobili);
    var buttonFiltriraj = document.createElement("button");
    buttonFiltriraj.className="buttonFiltriraj";
    buttonFiltriraj.innerHTML="Filtriraj Prikaz";
    buttonFiltriraj.onclick=(ev)=>filtriraj();
    red.appendChild(buttonFiltriraj);

    function filtriraj()
    {
        var filtriranaLista=[];
        var select = document.querySelector("select");
        var model = select.value;
        
        var predjKM = document.querySelector(".km").value;

        var brSed = document.querySelector(".brSedista").value;
        var cena = document.querySelector(".cena").value;

        var queryFaktori = "?";
        var listaOdabrano=[model, cena, brSed, predjKM];
        var listaZaPretragu=["model", "cena", "brojSedista", "predjeno" ];
        var prvi = true;

        listaZaPretragu.forEach((p,index)=>{
            if(prvi){
                prvi=false;
                queryFaktori+=`${listaZaPretragu[index]}=${listaOdabrano[index]}`;
            }else{
                queryFaktori+=`&${listaZaPretragu[index]}=${listaOdabrano[index]}`;
            }
        })
        fetch("http://localhost:5237/Automobil/VratiAutomobileFiltrirano" + queryFaktori)
        .then(p=>p.json())
        .then(q=> {
            q.forEach(a=>{
                var auto = new Automobil(a.id, a.model, a.predjeniKM, a.godiste, a.brojSedista, a.cenaPoDanu, a.iznajmljen);
                filtriranaLista.push(auto);
            })
            console.log(filtriranaLista);
            var child = divRezultat.lastElementChild;
            while(child){
                divRezultat.removeChild(child);
                child=divRezultat.lastElementChild;
            }
            filtriranaLista.forEach(autic=>{
                autic.crtaj(divRezultat);
            })
        })
        
    }
});






