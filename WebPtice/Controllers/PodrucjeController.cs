using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Models;

namespace Ptice.Controllers;

[ApiController]
[Route("[controller]")]
public class PodrucjeController : ControllerBase
{
    public Context Context { get; set; }

    public PodrucjeController(Context context)
    {
        Context = context;
    }
    [HttpGet("PreuzmiPodrucja")]
    public async Task<ActionResult> PreuzmiPodrucja()
    {
        
        return Ok(await Context.Podrucja
            .Include(p=>p.Vidjena)
            .Select(p => new
            {
                IDPodrucja = p.ID,
                NazivPodrucja = p.Naziv,
                BrojVidjenja = p.Vidjena
                     .Count()
            })
            .ToListAsync());
    }
    [HttpPost("UpisiPodrucje/{nazivPodrucja}")]
    public async Task<ActionResult> UpisiPodrucje(string nazivPodrucja)
    {
        try
        {
            var podrucje = new Podrucje
            {
                Naziv = nazivPodrucja
            };
            await Context.Podrucja.AddAsync(podrucje);
            await Context.SaveChangesAsync();
            return Ok($"Podrucje sa ID: {podrucje.ID} je upisano.");
        }
        catch (Exception e)
        {
            return BadRequest(e.Message);
        }
    }
}