using Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Jun2024.Controllers
{
    [ApiController]
    [Route("[controller]")]

    public class KorisnikController : ControllerBase
    {
        public Context Context { get; set; }

        public KorisnikController(Context context)
        {
            Context = context;
        }
        [HttpPost("DodajIznajmljivanje/{idKorisnika}/{idAutomobila}/{brojDana}")]
        public async Task<ActionResult> DodajIznajmljivanje(int idKorisnika, int idAutomobila, int brojDana)
        {
            var korisnik = await Context.Korisnici.FindAsync(idKorisnika);
            if(korisnik==null)
                 return NotFound("Korisnik nije pronadjen!");
            var automobil = await Context.Automobili.FindAsync(idAutomobila);
            if(automobil==null)
                 return NotFound("Automobil nije pronadjen!");
            if(automobil.Iznajmljen)
            {
                return BadRequest("Automobil je vec iznajmljen");
            }

            Iznajmljivanje i = new Iznajmljivanje
            {
                Automobil = automobil,
                Korisnik = korisnik,
                BrojDana = brojDana
            };
            automobil!.Iznajmljen = true;
            await Context.Iznajmljivanja.AddAsync(i);
            await Context.SaveChangesAsync();
            return Ok($"Korisnik {korisnik.Ime}{korisnik.Prezime} je iznajmio je Automobil {automobil!.Model}");
        }
        [HttpPost("DodajKorisnika")]
        public async Task<ActionResult> DodajKorisnika([FromBody] Korisnik korisnik)
        {
            try
            {
                var postoji = await Context.Korisnici
                                    .AnyAsync(k=>k.JMBG == korisnik.JMBG || k.BrojVozacke==korisnik.BrojVozacke);
                
                if(postoji)
                {
                    return BadRequest("Korisnik sa ovim JMBG i brojem vozacke vec postoji");
                }

                Context.Korisnici.Add(korisnik);
                await Context.SaveChangesAsync();
                return Ok($"Korisnik je dodat! ID je {korisnik.ID}");
            }
            catch(Exception e)
            {
                return BadRequest(e.Message);
            }
        }
        [HttpGet("VratiKorisnike")]
        public async Task<ActionResult> VratiKorisnike()
        {
            var korisnici =await Context.Korisnici.ToListAsync();

            return Ok(korisnici);
        }
        [HttpDelete("IzbrisiKorisnika/{id}")]
        public async Task<ActionResult> Izbrisi(int id)
        {
            if(id <= 0)
            {
                return BadRequest("Pogresan ID");
            }
            try
            {
                var korisnik = await Context.Korisnici.FindAsync(id);
                Context.Korisnici.Remove(korisnik!);
                await Context.SaveChangesAsync();
                return Ok($"Uspesno Izbrisan korisnik sa ID: {id}");
            }
            catch(Exception e)
            {
                return BadRequest(e.Message);
            }
        }
        [HttpGet("VratiKorisnikeSaIznajmljivanjima")]
        public async Task<ActionResult> VratiSaIznajmljivanjima()
        {
            var korisnici = await Context.Korisnici
                                        .Include(p=>p.IznajmljivanjaK)
                                        .ThenInclude(p=>p.Automobil)
                                        .ToListAsync();

            return Ok(korisnici);
        }
    }

}