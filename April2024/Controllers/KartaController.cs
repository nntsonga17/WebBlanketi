using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Models
{
    [ApiController]
    [Route("[controller]")]
    public class KartaController : ControllerBase
    {
        public BioskopContext Context { get; set; }

        public KartaController(BioskopContext context)
        {
            Context = context;
        }
        [HttpPost("DodajKartu/{projekcijaID}")]
        public async Task<ActionResult> DodajKartu([FromBody]Karta karta, int projekcijaID)
        {

            var projekcija = await Context.Projekcije.FindAsync(projekcijaID);
            if (projekcija == null)
                return BadRequest("Projekcija ne postoji!");
            karta.Projekcija = projekcija;
            Context.Karte.Add(karta);
            await Context.SaveChangesAsync();
            return Ok($"Uspesno dodata karta sa IDjem {karta.ID}");
        }

        [HttpPut("IzmeniKartu")]
        public async Task<ActionResult> IzmeniKartu([FromBody]Karta karta)
        {
            Context.Karte.Update(karta);
            await Context.SaveChangesAsync();
            return Ok($"Uspesno dodata karta sa IDjem {karta.ID}");
        }
        [HttpGet("VratiKarte")]
        public async Task<ActionResult> VratiKarte()
        {
            var karte = await Context.Karte
                                    .Include(k => k.Projekcija) // Ako želiš i šifru projekcije da šalješ
                                    .ToListAsync();

            var rezultat = karte.Select(karta => new
            {
                ID = karta.ID,
                Red = karta.Red,
                BrSedista = karta.BrSedista,
                Cena = Math.Round(karta.Cena * Math.Pow(0.97, karta.Red-1), 2), // Cena uz umanjenje
                Kupljena = karta.Kupljena,
                Sifra = karta.Projekcija?.Sifra // stavio sam "?" da ne pukne ako nije povezana
            });

            return Ok(rezultat);
        }

        

        [HttpGet("VratiKartu/{id}")]
        public async Task<ActionResult> VratiKartu(int id)
         {
            var karta = await Context.Karte.Include(p=>p.Projekcija).Where(p=>p.ID==id).FirstOrDefaultAsync();
            if(karta == null)
            {
                return BadRequest("Odabrana karta ne postoji");
            }
            double OsnovnaCena = karta.Cena;

            double NovaCena = OsnovnaCena * Math.Pow(0.97, karta.Red-1);
            return Ok(new{
                ID = karta.ID,
                Red = karta.Red,
                BrSedista = karta.BrSedista,
                Cena = Math.Round(NovaCena, 2),
                Kupljena = karta.Kupljena,
                Sifra = karta.Projekcija!.Sifra
            });

         }
        [HttpDelete("ObrisiKartu/{id}")]
        public async Task<ActionResult> IzbrisiKartu(int id)
        {
            if(id <= 0)
            {
                return BadRequest("Pogresan ID");
            }
            try
            {
                var karta = await Context.Karte.FindAsync(id);
                Context.Karte.Remove(karta!);
                await Context.SaveChangesAsync();
                return Ok($"Uspesno Izbrisan student sa ID: {karta!.ID}");
            }
            catch(Exception e)
            {
                return BadRequest(e.Message);
            }
        }
        [HttpPost("KupiKartu/{idKarte}")]
        public async Task<ActionResult> KupiKartu(int idKarte)
        {
            var karta = await Context.Karte.FindAsync(idKarte);
            karta!.Kupljena = true;
            Context.Karte.Update(karta);
            await Context.SaveChangesAsync();
            return Ok($"Uspesno kupljena karta u redu - {karta.Red}, sediste - {karta.BrSedista}");
        }
        

    }
}