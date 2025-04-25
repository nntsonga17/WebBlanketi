export class Automobil{
    constructor(id, model, predjeniKM, godiste, brojSedista, cenaPoDanu, iznajmljen){
        this.id =id;
        this.model=model;
        this.predjeniKM=predjeniKM;
        this.godiste=godiste;
        this.brojSedista=brojSedista;
        this.cenaPoDanu=cenaPoDanu;
        this.iznajmljen=iznajmljen;
    }
    crtaj(host){
        var div = document.createElement("div");
        div.classList.add("auto");
        host.appendChild(div);
        if(this.iznajmljen==true){
            div.classList.add("crveno");
        }else{
            div.classList.add("zeleno");
        }

        var l = document.createElement("label");
        l.innerHTML=`Model: ${this.model}`;
        div.appendChild(l);

        l = document.createElement("label");
        l.innerHTML=`Kilometraza: ${this.predjeniKM}`;
        div.appendChild(l);

        l = document.createElement("label");
        l.innerHTML=`Godiste: ${this.godiste}`;
        div.appendChild(l);

        l = document.createElement("label");
        l.innerHTML=`Cena po danu: ${this.cenaPoDanu}`;
        div.appendChild(l);

        l = document.createElement("label");
        l.innerHTML=`Broj Sedista: ${this.brojSedista}`;
        div.appendChild(l);

        l = document.createElement("label");
        l.innerHTML=`Iznajmljen: ${this.iznajmljen}`;
        div.appendChild(l);

        var buttonIznajmi = document.createElement("button");
        buttonIznajmi.innerHTML="Iznajmi";
        buttonIznajmi.className="iznajmi";
        if(this.iznajmljen){
            buttonIznajmi.disabled=true;
            buttonIznajmi.style.cursor="not allowed";
        }else{
            buttonIznajmi.onclick=()=>this.iznajmi();
        }
        div.appendChild(buttonIznajmi);

    }
    iznajmi() {
        var ime = document.querySelector(".ime").value;
        var prezime = document.querySelector(".prezime").value;
        var JMBG = document.querySelector(".jmbg").value;
        var brojVozacke = document.querySelector(".vozacka").value;
        var brojDana = document.querySelector(".dani").value;
    
        // Proveri da li korisnik već postoji na osnovu JMBG-a ili broja vozačke
        fetch("http://localhost:5237/Korisnik/VratiKorisnike")
            .then(p => p.json())
            .then(korisnici => {
                // Pronađi korisnika u lokalnom nizu
                let korisnik = korisnici.find(k => k.jmbg === JMBG || k.brojVozacke === brojVozacke);
                
                if (korisnik) {
                    // Postoji - direktno pozovi DodajIznajmljivanje
                    this.iznajmiAutomobil(korisnik.id, brojDana);
                } else {
                    // Ne postoji - prvo ga kreiraj
                    fetch("http://localhost:5237/Korisnik/DodajKorisnika", {
                        method: "POST",
                        headers: {
                            "Content-Type": "application/json"
                        },
                        body: JSON.stringify({
                            ime: ime,
                            prezime: prezime,
                            jmbg: JMBG,
                            brojVozacke: brojVozacke
                        })
                    })
                    .then(r => r.text()) // jer vraćaš plain tekst
                    .then(data => {
                        if (data.startsWith("Korisnik je dodat!")) {
                            var korisnikID = data.match(/ID je (\d+)/)[1];
                            this.iznajmiAutomobil(korisnikID, brojDana);
                        } else {
                            alert("Greška prilikom dodavanja korisnika: " + data);
                        }
                    });
                }
            });
    }
    iznajmiAutomobil(korisnikID, brojDana){
        var automobilID = this.id;
        fetch(`http://localhost:5237/Korisnik/DodajIznajmljivanje/${korisnikID}/${automobilID}/${brojDana}`, {
            method: "POST"
        })
        .then(r=>r.text())
        .then(data => {
            if(data.startsWith("Korisnik")){
                alert("Automobil je uspesno iznajmljen!");
            }else{
                alert(data);
            }
        })
    }
}