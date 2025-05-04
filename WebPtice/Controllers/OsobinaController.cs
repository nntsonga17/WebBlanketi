using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Models;

namespace Ptice.Controllers;

[ApiController]
[Route("[controller]")]
public class OsobinaController : ControllerBase
{
    public Context Context { get; set; }

    public OsobinaController(Context context)
    {
        Context = context;
    }
    [HttpGet("Pro")]
    public async Task<ActionResult> Pro()
    {
        return Ok(await Context
            .Osobine
            .Include(p => p.Ptica)
            .ToListAsync());
    }
     [HttpGet("PreuzmiOsobine")]
    public async Task<ActionResult> PreuzmiOsobine()
    {
        var podaci = await Context
            .Osobine
            .ToListAsync();
        var grupisano = podaci
            .GroupBy(p => p.Naziv);

        return Ok(grupisano
            .ToDictionary(p => p.Key,
                        q => q.
                            Select(r => new 
                            {
                                 r.ID,
                                 r.Naziv,
                                 r.Vrednost,
                                 Tip = r.ViseVrednosti
                            }).ToList()));
    }

    [HttpPost("DodajOsobinu")]
    public async Task<ActionResult> DodajOsobinu([FromBody]Osobine osobina)
    {
        try
        {
            await Context.Osobine.AddAsync(osobina);
            await Context.SaveChangesAsync();
            return Ok(osobina.ID);
        }
        catch (Exception e)
        {
            return BadRequest(e.Message);
        }
    }

    [HttpPut("IzmeniOsobinu/{id}")]
    public async Task<ActionResult> IzmeniOsobinu(int id, [FromBody]Osobine osobina)
    {
        var stara = await Context.Osobine.FindAsync(id);

        if (stara != null &&
            !string.IsNullOrWhiteSpace(osobina.Naziv) &&
            !string.IsNullOrWhiteSpace(osobina.Vrednost))
        {
            stara.Naziv = osobina.Naziv;
            stara.Vrednost = osobina.Vrednost;

            Context.Osobine.Update(stara);
            await Context.SaveChangesAsync();
            return Ok($"Promenjena osobina ID={stara.ID}");
        }
        else
        {
            return BadRequest("Neuspelo!");
        }
    }

    [HttpDelete("IzbrisiOsobinu/{id}")]
    public async Task<ActionResult> Izbrisati(int id)
    {
        var stara = await Context.Osobine.FindAsync(id);

        if (stara != null)
        {
            string vrednost = stara.Vrednost;
            Context.Osobine.Remove(stara);
            await Context.SaveChangesAsync();
            return Ok($"Izbrisana osobina sa Vrednost={vrednost}");
        }
        else
        {
            return BadRequest("Neuspelo!");
        }
    }
}