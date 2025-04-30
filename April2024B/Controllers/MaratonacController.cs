using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Models;

namespace Controller
{
    [ApiController]
    [Route("[controller]")]
    public class MaratonacController : ControllerBase
    {
        public Context Context { get; set; }

        public MaratonacController(Context context)
        {
            Context = context;
        }
        [HttpPost("DodajMaratonca")]
        public async Task<ActionResult> DodajMaratonca([FromBody]Maratonac maratonac)
        {
            try
            {
                Context.Maratonci.Add(maratonac);
                await Context.SaveChangesAsync();
                return Ok($"Maratonac dodat! ID je {maratonac.ID}");
            }
            catch(Exception e)
            {
                return BadRequest(e.Message);
            }
        }
        [HttpGet("VratiMaratonce")]
        public async Task<ActionResult> VratiMaratonce()
        {
            var maratonci = await Context.Maratonci.ToListAsync();

            return Ok(maratonci);
        }
        [HttpPut("IzmeniMaratonca")]
        public async Task<ActionResult> IzmeniMaratonca([FromBody]Maratonac maratonac)
        {
            try
            {
                Context.Maratonci.Update(maratonac);
                await Context.SaveChangesAsync();
                return Ok($"Maratonac izmenjen! ID je {maratonac.ID}");
            }
            catch(Exception e)
            {
                return BadRequest(e.Message);
            }
        }
        [HttpDelete("IzbrisiMaratonca/{id}")]
        public async Task<ActionResult> Izbrisi(int id)
        {
            if(id <= 0)
            {
                return BadRequest("Pogresan ID");
            }
            try
            {
                var maratonac = await Context.Maratonci.FindAsync(id);
                Context.Maratonci.Remove(maratonac!);
                await Context.SaveChangesAsync();
                return Ok($"Uspesno Izbrisan maratonac sa ID: {id}");
            }
            catch(Exception e)
            {
                return BadRequest(e.Message);
            }
        }
        [HttpGet("VratiMaratonca/{id}")]
        public async Task<ActionResult> VratiMaratonca(int id)
        {
            var maratonac = await Context.Maratonci.FindAsync(id);

            var rez = new
            {
                ImePrezime = $"{maratonac!.Ime} {maratonac!.Prezime}",
                BrojNagrada = maratonac.BrojNagrada,
                SrednjaBrzina = maratonac.SrednjaBrzina
            };
            return Ok(rez);
        }
        [HttpPost("DodajUcesce/{idMaratonca}/{idTrke}/{startniBroj}/{vremeZavrsetka}")]
        public async Task<ActionResult> DodajUcesce(int idMaratonca, int idTrke, int startniBroj, TimeSpan vremeZavrsetka)
        {
            try
            {
                var maratonac = await Context.Maratonci.FindAsync(idMaratonca);
                var trka = await Context.Trke.FindAsync(idTrke);

                var postojiStartniBroj = await Context.Ucesca
                    .AnyAsync(u => u.StartniBroj == startniBroj && u.Trka!.ID == idTrke);

                if (postojiStartniBroj)
                {
                    return BadRequest($"Startni broj {startniBroj} već postoji za ovu trku.");
                }
                if(startniBroj>trka!.BrojTakmicara)
                {
                    return BadRequest("Dostignut je maksimalan broj takmicara");
                }
                if(vremeZavrsetka>trka!.TrajanjeTrke)
                {
                    return BadRequest("Prevazidjeno trajanje trke");
                }

                Ucesce u = new Ucesce
                {
                    Maratonac = maratonac,
                    Trka = trka,
                    StartniBroj = startniBroj,
                    VremeIstrcano = vremeZavrsetka
                };


                Context.Ucesca.Add(u);
                await Context.SaveChangesAsync();
                return Ok($"Dodato ucesce takmicara sa ID:{maratonac!.ID} na trku {trka!.ID}");
            }
            catch(Exception e)
            {
                return BadRequest(e.Message);
            }
        }
        [HttpGet("VratiInformacijeOTakmicaru/{idMaratonca}/{idTrke}/{vreme}")]
        public async Task<ActionResult> VratiInfoTakmicar(int idMaratonca, int idTrke, DateTime vreme)
        {
            var maratonac = await Context.Maratonci.FindAsync(idMaratonca);
            var trka = await Context.Trke
                             .Include(p=>p.UcescaT!)
                             .ThenInclude(p=>p.Maratonac)
                             .FirstOrDefaultAsync(p=>p.ID==idTrke);
            
            if (maratonac == null || trka == null)
            {
                return NotFound("Takmičar ili trka nisu pronađeni.");
            }
            if(vreme > trka.PocetakTrke + trka.TrajanjeTrke)
            {
                vreme = trka.PocetakTrke + trka.TrajanjeTrke;
            }
            if(vreme < trka.PocetakTrke)
            {
                return BadRequest("Trka jos nije pocela!");
            }
            
            var ucesce = trka.UcescaT!.FirstOrDefault(u=>u.Maratonac!.ID==idMaratonca);

            TimeSpan protekloVreme = vreme - trka.PocetakTrke;

            var prosloVreme = protekloVreme.TotalHours;

            var istrcaoSati = ucesce!.VremeIstrcano.TotalHours;

            double prosecnaBrzina =  trka!.DuzinaStaze /  istrcaoSati;

            var predjeno = prosecnaBrzina * prosloVreme;

            if(predjeno > trka.DuzinaStaze)
            {
                predjeno = trka.DuzinaStaze;
            }
            if(predjeno == trka.DuzinaStaze)
            {
                protekloVreme = ucesce.VremeIstrcano;
            }
            var takmicariPredjeno = trka.UcescaT!
                                    .Select(u=>{
                                        TimeSpan v = vreme - trka.PocetakTrke;
                                        if(u.VremeIstrcano<v)
                                        {
                                            v = u.VremeIstrcano;
                                        }

                                        double brz = trka.DuzinaStaze / u.VremeIstrcano.TotalHours;
                                        double p = brz * v.TotalHours; 
                                        return new{
                                            MaratonacID = u.Maratonac!.ID,
                                            Predjeno = p
                                        };
                                    })
                                    .OrderByDescending(p=>p.Predjeno)
                                    .ToList();
            ucesce.TrenutnaPozicija = takmicariPredjeno.FindIndex(p=> p.MaratonacID==idMaratonca) + 1;

            
            var info = new
            {
                PocetakTrke = trka.PocetakTrke.ToString("yyyy-MM-dd HH:mm:ss"),
                DuzinaStaze = trka.DuzinaStaze + " kilometara",
                StartniBroj = ucesce!.StartniBroj,
                TrenutnaPozicija = ucesce.TrenutnaPozicija,
                Predjeno = predjeno.ToString("0.00") + " kilometara",
                ProtekloVreme = protekloVreme.ToString(@"hh\:mm\:ss"),
                ProsecnaBrzina = prosecnaBrzina.ToString("0.00") + " km/h"
            };

            return Ok(info);
            

        }
    }
    
}