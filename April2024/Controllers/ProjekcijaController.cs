using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Models
{
    [ApiController]
    [Route("[controller]")]
    public class ProjekcijaController : ControllerBase
    {
        public BioskopContext Context { get; set; }

        public ProjekcijaController(BioskopContext context)
        {
            Context = context;
        }
        [HttpPost("DodajProjekciju")]
        public async Task<ActionResult> DodajProjekciju([FromBody]Projekcija projekcija)
        {
            Context.Projekcije.Add(projekcija);
            await Context.SaveChangesAsync();
            return Ok($"Uspesno dodata projekcija sa IDjem {projekcija.ID}");
        }

        [HttpPut("IzmeniProjekciju")]
        public async Task<ActionResult> IzmeniProjekciju([FromBody]Projekcija projekcija)
        {
            Context.Projekcije.Update(projekcija);
            await Context.SaveChangesAsync();
            return Ok($"Uspesno dodata projekcija sa IDjem {projekcija.ID}");
        }
        [HttpGet("VratiProjekcije")]
        public async Task<ActionResult> VratiProjekcije()
        {
            var projekcije = await Context.Projekcije.ToListAsync();
            return Ok(projekcije);
        }

        [HttpGet("VratiProjekcijeSaKartama")]
        public async Task<ActionResult> VratiProjekcijeSaKartama()
        {
            var projekcije = await Context.Projekcije.Include(p=>p.Karte).ToListAsync();
            return Ok(projekcije);
        }

        [HttpGet("VratiProjekciju/{id}")]
        public async Task<ActionResult> VratiProjekciju(int id)
         {
            var projekcija = await Context.Projekcije.Include(p=>p.Karte).Where(p=>p.ID==id).FirstOrDefaultAsync();
            if(projekcija == null)
            {
                return BadRequest("Odabrana projekcija ne postoji");
            }
            return Ok(projekcija);

         }
        [HttpDelete("ObrisiProjekciju/{id}")]
        public async Task<ActionResult> Izbrisi(int id)
        {
            if(id <= 0)
            {
                return BadRequest("Pogresan ID");
            }
            try
            {
                var projekcija = await Context.Projekcije.FindAsync(id);
                Context.Projekcije.Remove(projekcija!);
                await Context.SaveChangesAsync();
                return Ok($"Uspesno Izbrisan student sa ID: {projekcija!.ID}");
            }
            catch(Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        

    }
}