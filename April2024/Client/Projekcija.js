import { Karta } from "./Karta.js";

export class Projekcija{
    constructor(id, naziv, vremePrikazivanja, sifra, brojSale) {
        this.id = id;
        this.naziv = naziv;
        this.vremePrikazivanja = vremePrikazivanja;
        this.sifra = sifra;
        this.brojSale = brojSale;
        this.listaKarata=[];
        this.kont=null
    }
    crtaj(host){
        var glavniDiv = document.createElement("div");
        glavniDiv.className="glavniDiv";
        host.appendChild(glavniDiv);

        this.kont=glavniDiv;

        let datum = new Date(this.vremePrikazivanja);

        let dan = datum.getDate().toString().padStart(2, '0');
        let mesec = (datum.getMonth() + 1).toString().padStart(2, '0'); // meseci su 0-indexirani
        let godina = datum.getFullYear();
        let sati = datum.getHours().toString().padStart(2, '0');
        let minuti = datum.getMinutes().toString().padStart(2, '0');

        let formatiranDatum = `${dan}.${mesec}.${godina}. ${sati}:${minuti}`;

        var naslov = document.createElement("h3");
        naslov.className="naslov";
        naslov.innerHTML=`${this.naziv}:  ${formatiranDatum} - Sala${this.brojSale}`;
        glavniDiv.appendChild(naslov);

        var sadrzaj = document.createElement("div");
        sadrzaj.className="sadrzaj";
        glavniDiv.appendChild(sadrzaj);

        var kupovinaDiv = document.createElement("div");
        kupovinaDiv.className="kupovinaDiv";
        sadrzaj.appendChild(kupovinaDiv);

        var sedistaDiv = document.createElement("div");
        sedistaDiv.className="sedistaDiv";
        sadrzaj.appendChild(sedistaDiv);

        this.kupovinaKarte(kupovinaDiv);
        this.sedistaPrikaz(sedistaDiv);

    }

    crtajRed(host){
        var red = document.createElement("div");
        red.className="red";
        host.appendChild(red);

        return red
    }

    kupovinaKarte(host){
        var naslov2 = document.createElement("h3");
        naslov2.className="naslov2";
        naslov2.innerHTML=`Kupi kartu`;
        host.appendChild(naslov2);

        var red = this.crtajRed(host);
        var labela = document.createElement("label");
        labela.innerHTML = "Red: ";
        red.appendChild(labela);
        var tb = document.createElement("input");
        tb.type = "number";
        tb.readOnly = true;
        tb.className="redtb";
        red.appendChild(tb);

        red = this.crtajRed(host);
        labela = document.createElement("label");
        labela.innerHTML = "Broj sedista: ";
        red.appendChild(labela);
        tb = document.createElement("input");
        tb.type = "number";
        tb.readOnly = true;
        tb.className="brSedista";
        red.appendChild(tb);

        red = this.crtajRed(host);
        labela = document.createElement("label");
        labela.innerHTML = "Cena: ";
        red.appendChild(labela);
        tb = document.createElement("input");
        tb.type = "number";
        tb.readOnly = true;
        tb.className="cena";
        red.appendChild(tb);

        red = this.crtajRed(host);
        labela = document.createElement("label");
        labela.innerHTML = "Sifra: ";
        red.appendChild(labela);
        tb = document.createElement("input");
        tb.type = "text";
        tb.readOnly = true;
        tb.className="sifra";
        red.appendChild(tb);

        red = this.crtajRed(host);
        var buttonKupi = document.createElement("button");
        buttonKupi.innerHTML="Kupi kartu";
        buttonKupi.className="buttonKupi";
        buttonKupi.onclick=()=>this.kupiKartu();
        red.appendChild(buttonKupi);
    }

    kupiKartu(){
        var kartaID = null;
        var redKarte = parseInt(this.kont.querySelector(".redtb").value);
        var sedisteKarte = parseInt(this.kont.querySelector(".brSedista").value);
        this.listaKarata.forEach(k=> {
            if(k.red===redKarte && k.brSedista===sedisteKarte){
                kartaID = k.id;
            }
        })
        var karta = this.listaKarata.find(k=> k.id === kartaID);
        console.log(kartaID);
        fetch("http://localhost:5027/Karta/KupiKartu/" + kartaID, {
            method: "POST"
        })
        .then(p=>{
            if(p.ok){
                karta.kupljena = true;
            }
            this.osveziPrikaz()
        })
        
        
    }
    osveziPrikaz(){
        var sedistaDiv = this.kont.querySelector(".sedistaDiv");
        var child = sedistaDiv.lastElementChild;
        while(child){
            sedistaDiv.removeChild(child);
            child = sedistaDiv.lastElementChild;
        }
        this.sedistaPrikaz(sedistaDiv);
    }

    sedistaPrikaz(host){
        this.listaKarata=[];
        fetch("http://localhost:5027/Karta/VratiKarte")
        .then(p=>p.json())
        .then(q=> {
            q.forEach(k=> {
                var karta = new Karta(k.id, k.red, k.brSedista, k.cena, k.kupljena, k.sifra);
                this.listaKarata.push(karta);
            })
            let brojac = 0;
            let redDiv = document.createElement("div");
            redDiv.className = "sedistaRed";
            host.appendChild(redDiv);
            console.log(this.listaKarata);
            this.listaKarata.forEach(k=>{
                if (brojac === 5) { // kad dodjes do 5 sedista, novi red
                    redDiv = document.createElement("div");
                    redDiv.className = "sedistaRed";
                    host.appendChild(redDiv);
                    brojac = 0;
                }
                
                var tb = document.createElement("input");
                tb.type = "text";
                tb.readOnly = true;
                tb.classList.add("sedista");
                if(k.kupljena==false){
                    tb.classList.add("zeleno");
                }else{
                    tb.classList.add("crveno");
                    tb.disabled=true;
                }
                tb.value=`Red: ${k.red}; Broj: ${k.brSedista}`;
                tb.onclick=()=>this.popuni(k.id);
                redDiv.appendChild(tb);

                brojac++;
            })
        })
        
        
    }
    popuni(id){
        console.log(id);
        var karta = this.listaKarata.find(k=> k.id===id);

        var poljeRed = this.kont.querySelector(".redtb");
        var poljeBrSedista = this.kont.querySelector(".brSedista");
        var poljeCena = this.kont.querySelector(".cena");
        var poljeSifra = this.kont.querySelector(".sifra");
        poljeRed.value = karta.red;
        poljeBrSedista.value = karta.brSedista;
        poljeCena.value = karta.cena;
        poljeSifra.value = karta.sifra;
        
    }
}