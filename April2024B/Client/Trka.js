import { Maratonac } from "./Maratonac.js"
export class Trka{
    constructor(id, lokacija, duzinaStaze, brojTakmicara, trajanjeTrke, pocetakTrke){
        this.id=id;
        this.lokacija=lokacija;
        this.duzinaStaze=duzinaStaze;
        this.brojTakmicara=brojTakmicara;
        this.trajanjeTrke=trajanjeTrke;
        this.pocetakTrke=pocetakTrke;
        this.listaMaratonaca=[];
    }
    crtaj(host){
        var glavniDiv = document.createElement("div");
        glavniDiv.className="glavniDiv";
        host.appendChild(glavniDiv);

        this.kont=glavniDiv;
        
        var kojaTrka = document.createElement("div");
        kojaTrka.className="kojaTrka";
        kojaTrka.innerHTML=this.lokacija;
        glavniDiv.appendChild(kojaTrka);
        
        var odabirMaratonca =document.createElement("div");
        odabirMaratonca.className = "odabirMaratonca";
        glavniDiv.appendChild(odabirMaratonca);
        var label = document.createElement("label");
        label.innerHTML="Odaberite maratonca: ";
        odabirMaratonca.appendChild(label);
        var select = document.createElement("select");
        select.className = "odabirMaratonca";
        odabirMaratonca.appendChild(select);

        fetch("http://localhost:5141/Maratonac/VratiMaratonce")
        .then(p=>p.json())
        .then(data =>{
            data.forEach(m=> {
                var maratonac = new Maratonac(m.id, m.ime, m.prezime, m.jmbg, m.srednjaBrzina, m.brojNagrada);
                this.listaMaratonaca.push(maratonac);
            })
            this.listaMaratonaca.forEach(q=> {
                var option = document.createElement("option");
                option.value = q.id;
                option.innerHTML=`${q.ime} ${q.prezime}`;
                select.appendChild(option);
            })
        })

        var sadrzaj = document.createElement("div");
        sadrzaj.className="sadrzaj";
        glavniDiv.appendChild(sadrzaj);

        var vremeOdabir = document.createElement("div");
        vremeOdabir.className="vremeOdabir";
        sadrzaj.appendChild(vremeOdabir);

        var prikazMaratonca = document.createElement("div");
        prikazMaratonca.className="prikazMaratonca";
        sadrzaj.appendChild(prikazMaratonca);

        this.crtajVreme(vremeOdabir);
        this.crtajMaratonca(prikazMaratonca);
    }
    crtajVreme(host){
        var label = document.createElement("label");
        label.innerHTML="Odaberite vreme: ";
        host.appendChild(label);

        var inputVreme = document.createElement("input");
        inputVreme.type = "time";
        inputVreme.step = 1;
        inputVreme.className = "inputVreme";
        host.appendChild(inputVreme);

        

        var buttonPrikazi = document.createElement("button");
        buttonPrikazi.innerHTML="Prikazi informacije";
        buttonPrikazi.onclick =()=>this.prikaziInformacije();
        host.appendChild(buttonPrikazi);
    }
    crtajRed(host){
        var red = document.createElement("div");
        red.className="red";
        host.appendChild(red);

        return red;
    }
    crtajMaratonca(host){

        var naslov1 = document.createElement("h3");
        naslov1.innerHTML="Opste informacije o maratoncu:";
        host.appendChild(naslov1);

        var red = this.crtajRed(host);
        var label = document.createElement("label");
        label.innerHTML="Ime i prezime: ";
        red.appendChild(label);
        var prikazImePrezime = document.createElement("span");
        prikazImePrezime.className="prikazImePrezime";
        red.appendChild(prikazImePrezime);

        red = this.crtajRed(host);
        label = document.createElement("label");
        label.innerHTML="Broj nagrada: ";
        red.appendChild(label);
        var prikazNagrada = document.createElement("span");
        prikazNagrada.className="prikazNagrada";
        red.appendChild(prikazNagrada);

        red = this.crtajRed(host);
        label = document.createElement("label");
        label.innerHTML="Srednja brzina: ";
        red.appendChild(label);
        var prikazBrzine = document.createElement("span");
        prikazBrzine.className="prikazBrzine";
        red.appendChild(prikazBrzine);

        var naslov2 = document.createElement("h3");
        naslov2.innerHTML="Trenutne informacije:";
        host.appendChild(naslov2);

        red = this.crtajRed(host);
        label = document.createElement("label");
        label.innerHTML="Pocetak trke: ";
        red.appendChild(label);
        var prikazPocetak = document.createElement("span");
        prikazPocetak.className="prikazPocetak";
        red.appendChild(prikazPocetak);

        red = this.crtajRed(host);
        label = document.createElement("label");
        label.innerHTML="Duzina staze: ";
        red.appendChild(label);
        var prikazDuzina = document.createElement("span");
        prikazDuzina.className="prikazDuzina";
        red.appendChild(prikazDuzina);

        red = this.crtajRed(host);
        label = document.createElement("label");
        label.innerHTML="Startni Broj: ";
        red.appendChild(label);
        var prikazStartniBroj = document.createElement("span");
        prikazStartniBroj.className="prikazStartniBroj";
        red.appendChild(prikazStartniBroj);

        red = this.crtajRed(host);
        label = document.createElement("label");
        label.innerHTML="Trenutna pozicija: ";
        red.appendChild(label);
        var prikazTrenutnaPozicija = document.createElement("span");
        prikazTrenutnaPozicija.className="prikazTrenutnaPozicija";
        red.appendChild(prikazTrenutnaPozicija);

        red = this.crtajRed(host);
        label = document.createElement("label");
        label.innerHTML="Predjeno: ";
        red.appendChild(label);
        var prikazPredjeno = document.createElement("span");
        prikazPredjeno.className="prikazPredjeno";
        red.appendChild(prikazPredjeno);

        red = this.crtajRed(host);
        label = document.createElement("label");
        label.innerHTML="Proteklo vreme: ";
        red.appendChild(label);
        var prikazProtekloVreme = document.createElement("span");
        prikazProtekloVreme.className="prikazProtekloVreme";
        red.appendChild(prikazProtekloVreme);

        red = this.crtajRed(host);
        label = document.createElement("label");
        label.innerHTML="Prosecna brzina: ";
        red.appendChild(label);
        var prikazProsecnaBrzina = document.createElement("span");
        prikazProsecnaBrzina.className="prikazProsecnaBrzina";
        red.appendChild(prikazProsecnaBrzina);

        red = this.crtajRed(host);
        label = document.createElement("label");
        label.innerHTML="Trenutni progres: ";
        red.appendChild(label);
        var prikazTrenutniProgres = document.createElement("div");
        prikazTrenutniProgres.className="prikazTrenutniProgres";
        red.appendChild(prikazTrenutniProgres);

    }
    prikaziInformacije(){
        var vreme = this.kont.querySelector(".inputVreme").value;
        var trkaID = this.id;
        var maratonacID = this.kont.querySelector("select").value;

        var ImePrezime = this.kont.querySelector(".prikazImePrezime");
        var BrojNagrada = this.kont.querySelector(".prikazNagrada");
        var SrBr = this.kont.querySelector(".prikazBrzine");
        var trkaStart = this.kont.querySelector(".prikazPocetak");
        var duzina = this.kont.querySelector(".prikazDuzina");
        var startniBr = this.kont.querySelector(".prikazStartniBroj");
        var trenutnaPoz = this.kont.querySelector(".prikazTrenutnaPozicija");
        var predjenoKM = this.kont.querySelector(".prikazPredjeno");
        var proteklo = this.kont.querySelector(".prikazProtekloVreme");
        var prosek = this.kont.querySelector(".prikazProsecnaBrzina");
        var progres = this.kont.querySelector(".prikazTrenutniProgres");



        var datumTrke = new Date(this.pocetakTrke);
        var [h,m,s] = vreme.split(":");
        var pocinje = new Date(this.pocetakTrke);
        var dan = pocinje.getDate().toString().padStart(2, '0');
        let mesec = (pocinje.getMonth() + 1).toString().padStart(2, '0'); // meseci su 0-indexirani
        let godina = pocinje.getFullYear();
        let sati = pocinje.getHours().toString().padStart(2, '0');
        let minuti = pocinje.getMinutes().toString().padStart(2, '0');
        let sekunde = pocinje.getSeconds().toString().padStart(2, '0');

        var formatiranPocetak = `${dan}.${mesec}.${godina}  ${sati}:${minuti}:${sekunde}`;
        

        datumTrke.setHours(h);
        datumTrke.setMinutes(m);
        datumTrke.setSeconds(s);
        console.log(datumTrke);

        var vremeZaUrl = new Date(datumTrke.getTime() - datumTrke.getTimezoneOffset() * 60000).toISOString().slice(0, 19);
          
        console.log(vremeZaUrl);

        fetch(`http://localhost:5141/Maratonac/VratiMaratonca/${maratonacID}`)
        .then(p=>p.json())
        .then(data => {
            ImePrezime.innerHTML = data.imePrezime;
            BrojNagrada.innerHTML = data.brojNagrada;
            SrBr.innerHTML = data.srednjaBrzina;
            fetch(`http://localhost:5141/Maratonac/VratiInformacijeOTakmicaru/${maratonacID}/${trkaID}/${vremeZaUrl}`)
            .then(p=>p.json())
            .then(p=>{
                trkaStart.innerHTML = formatiranPocetak;
                duzina.innerHTML = p.duzinaStaze;
                startniBr.innerHTML = p.startniBroj;
                trenutnaPoz.innerHTML = p.trenutnaPozicija;
                predjenoKM.innerHTML = p.predjeno;
                proteklo.innerHTML = p.protekloVreme;
                prosek.innerHTML = p.prosecnaBrzina;

                var presao = parseFloat(p.predjeno);
                var ukupnaDuzina = parseFloat(p.duzinaStaze);
                var procenat = Math.round((presao/ukupnaDuzina) * 100);

                progres.style.width = `${procenat}%`;
                progres.style.backgroundColor = "red";
                progres.innerHTML=`${procenat}%`;
        })
        })

        
        console.log(maratonacID);
    }
}