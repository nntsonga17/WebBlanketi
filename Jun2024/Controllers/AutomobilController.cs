using Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Jun2024.Controllers
{
    [ApiController]
    [Route("[controller]")]

    public class AutomobilController : ControllerBase
    {
        public Context Context { get; set; }

        public AutomobilController(Context context)
        {
            Context = context;
        }
        [HttpPost("DodajAutomobil")]
        public async Task<ActionResult> DodajAutomobil([FromBody] Automobil auto)
        {
            try
            {
                Context.Automobili.Add(auto);
                await Context.SaveChangesAsync();
                return Ok($"Automobil dodat! ID je {auto.ID}");
            }
            catch(Exception e)
            {
                return BadRequest(e.Message);
            }
        }
        [HttpGet("VratiAutomobile")]
        public async Task<ActionResult> VratiAutomobile()
        {
            var automobili =await Context.Automobili.ToListAsync();

            return Ok(automobili);
        }

        [HttpPut("IzmeniAutomobil")]
        public async Task<ActionResult> IzmeniAutomobil([FromBody] Automobil auto)
        {
            try
            {
                Context.Automobili.Update(auto);
                await Context.SaveChangesAsync();
                return Ok($"Automobil izmenjen! ID je {auto.ID}");
            }
            catch(Exception e)
            {
                return BadRequest(e.Message);
            }
        }
        [HttpGet("VratiAutomobileFiltrirano")]
        public async Task<ActionResult> VratiFiltrirano(
        [FromQuery] string? model,
        [FromQuery] int? cena,
        [FromQuery] int? brojSedista,
        [FromQuery] int? predjeno)
        {
            var query = Context.Automobili.AsQueryable();

            if (!string.IsNullOrEmpty(model))
                query = query.Where(a => a.Model == model);

            if (cena.HasValue)
                query = query.Where(a => a.CenaPoDanu <= cena.Value);

            if (brojSedista.HasValue)
                query = query.Where(a => a.BrojSedista == brojSedista.Value);

            if (predjeno.HasValue)
                query = query.Where(a => a.PredjeniKM <= predjeno.Value);

            var rezultat = await query.ToListAsync();
            return Ok(rezultat);
        }
        [HttpDelete("IzbrisiAutomobil/{id}")]
        public async Task<ActionResult> Izbrisi(int id)
        {
            if(id <= 0)
            {
                return BadRequest("Pogresan ID");
            }
            try
            {
                var auto = await Context.Automobili.FindAsync(id);
                Context.Automobili.Remove(auto!);
                await Context.SaveChangesAsync();
                return Ok($"Uspesno Izbrisan automobil sa ID: {id}");
            }
            catch(Exception e)
            {
                return BadRequest(e.Message);
            }
        }
    }

}