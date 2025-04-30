using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Models;

namespace Controller
{
    [ApiController]
    [Route("[controller]")]
    public class TrkaController : ControllerBase
    {
        public Context Context { get; set; }

        public TrkaController(Context context)
        {
            Context = context;
        }
        [HttpPost("DodajTrku")]
        public async Task<ActionResult> DodajTrku([FromBody]Trka trka)
        {
            try
            {
                
                Context.Trke.Add(trka);
                await Context.SaveChangesAsync();
                return Ok($"Trka dodata! ID je {trka.ID}");
            }
            catch(Exception e)
            {
                return BadRequest(e.Message);
            }
        }
        [HttpGet("VratiTrke")]
        public async Task<ActionResult> VratiTrke()
        {
            var trke = await Context.Trke.ToListAsync();

            

            return Ok(trke);
        }
        [HttpGet("VratiTrkeSaMaratoncima")]
        public async Task<ActionResult> VratiTrkeSaMaratoncima()
        {
            var trke = await Context.Trke.Include(p=> p.UcescaT!)
                                         .ThenInclude(p=>p.Maratonac)
                                         .ToListAsync();

            return Ok(trke);
        }
        [HttpPut("IzmeniTrku")]
        public async Task<ActionResult> IzmeniTrku([FromBody]Trka trka)
        {
            try
            {
                Context.Trke.Update(trka);
                await Context.SaveChangesAsync();
                return Ok($"Trka izmenjen! ID je {trka.ID}");
            }
            catch(Exception e)
            {
                return BadRequest(e.Message);
            }
        }
        [HttpPut("IzmeniTrku/{id}/{duzina}")]
        public async Task<ActionResult> IzmeniTrku(int id, double duzina)
        {
            try
            {
                var trkaZaMenjane = await Context.Trke.FindAsync(id);
                trkaZaMenjane!.DuzinaStaze = duzina;
                Context.Trke.Update(trkaZaMenjane);
                await Context.SaveChangesAsync();
                return Ok($"Trka izmenjen! ID je {trkaZaMenjane.ID}");
            }
            catch(Exception e)
            {
                return BadRequest(e.Message);
            }
        }
        [HttpDelete("IzbrisiTrku/{id}")]
        public async Task<ActionResult> Izbrisi(int id)
        {
            if (id <= 0)
            {
                return BadRequest("Pogresan ID");
            }
            try
            {
                var trka = await Context.Trke
                    .Include(t => t.UcescaT)  // Uključujemo zavisne entitete
                    .FirstOrDefaultAsync(t => t.ID == id);

                if (trka == null)
                {
                    return NotFound($"Trka sa ID: {id} nije pronađena.");
                }

                // Brisanje povezanih ucesca
                if (trka.UcescaT != null)
                {
                    Context.Ucesca.RemoveRange(trka.UcescaT);
                }

                // Brisanje trke
                Context.Trke.Remove(trka);
                await Context.SaveChangesAsync();

                return Ok($"Uspesno Izbrisana trka sa ID: {id}");
            }
            catch (Exception e)
            {
                return BadRequest($"Greška prilikom brisanja trke: {e.Message}");
            }
        }

    }
    
}