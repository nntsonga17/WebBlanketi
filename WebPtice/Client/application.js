import { Ptica } from "./ptica.js";

export class Application{
    constructor(podrucja,osobine){
        this.podrucja=podrucja;
        this.osobine=osobine;
    }

    crtajAplikaciju(container){

        let glavni = document.createElement("div");
        glavni.classList.add("Glavni");
        container.prepend(glavni);

        let levi = document.createElement("div");
        levi.classList.add("levi");
        glavni.appendChild(levi);

        this.leviContainer = levi;

        let desni = document.createElement("div");
        desni.classList.add("desni");
        glavni.appendChild(desni);

        this.desniContainer = desni;

        let divPod = document.createElement("div");
        divPod.classList.add("divPodrucje");
        levi.appendChild(divPod);

        const pLab = document.createElement("label");
        pLab.innerText = "Podrucje: ";
        pLab.setAttribute("for", "podrucja");
        divPod.appendChild(pLab);

        const pSel = document.createElement("select");
        pSel.id="podrucja";

        let oSel;
        this.podrucja.forEach(p=>{
            oSel = document.createElement("option");
            oSel.value=p.idPodrucja;
            oSel.innerText=p.nazivPodrucja;
            pSel.appendChild(oSel);
        })
        divPod.appendChild(pSel);

        let opcijeLab = document.createElement("label");
        opcijeLab.innerText="Osobine: ";
        levi.appendChild(opcijeLab);

        const tDiv = document.createElement("div");
        tDiv.classList.add("tableDiv");
        levi.appendChild(tDiv);

        for(let ok in this.osobine){
            let pKljuc = document.createElement("p");
            pKljuc.classList.add("nazivOsobine", "border1");
            pKljuc.innerText=ok;
            tDiv.appendChild(pKljuc);

            const elDiv = document.createElement("div");
            elDiv.classList.add("border2");
            tDiv.appendChild(elDiv);

            this.osobine[ok].forEach(os => { 
                const dir = document.createElement("div");
                dir.classList.add("vrednostOsobine");
                elDiv.appendChild(dir);
            
                const ir = document.createElement("input");
                ir.type = "radio";
                ir.name = ok;
                ir.value = os.vrednost;
                dir.appendChild(ir);
            
                console.log("Osobina:", os); // Provera u konzoli
            
                const lir = document.createElement("label");
                lir.innerText = os.vrednost;
                lir.setAttribute("for", os.vrednost);
                dir.appendChild(lir);
            });
        };

        let btnPronadji = document.createElement("button");
        btnPronadji.innerHTML="Pronadji";
        levi.appendChild(btnPronadji);

        btnPronadji.onclick=()=>{
            this.pretrazi();
        }
    }
    async pretrazi() {

        while (this.desniContainer.firstChild) {
            this.desniContainer.removeChild(this.desniContainer.lastChild);
        }

        const podrucjeID = this.leviContainer.querySelector("select").value;
        const osobineIDs = this.leviContainer.querySelectorAll("input:checked");
        const osobineList = Array.from(osobineIDs).map((p) => `osobina=${p.value}`).join("&");
        const p = await fetch(`http://localhost:5223/controller/PreuzmiPtice/${podrucjeID}?${osobineList}`);
        const ptice = await p.json();
        console.log(ptice);

        for (let p of ptice) {
            const ptica = new Ptica(p.id, p.naziv, p.slika, p.brojVidjenja, podrucjeID);
            ptica.crtajPticu(this.desniContainer);
            
        }
    }
}