using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Diagnostics;
using Microsoft.Extensions.Logging.Abstractions;
using Models;

namespace Ptice.Controllers;

[ApiController]
[Route("controller")]
public class PticaController : ControllerBase 
{
    public Context Context { get; set; }
    public PticaController(Context context)
    {
        Context = context;
    }
    [HttpPost("DodajVidjenje/{idPtice}/{idPodrucja}")]
    public async Task<ActionResult> DodajVidjenje(
        int idPtice,
        int idPodrucja,
        [FromBody]Vidjena vidjenje)
    {

        
        var ptica = await Context.Ptice.FindAsync(idPtice);
        var podrucje = await Context.Podrucja.FindAsync(idPodrucja);

        if(ptica != null &&
           podrucje != null &&
           vidjenje.Latitude <=90 &&
           vidjenje.Latitude >=-90 &&
           vidjenje.Longitude >=-180 &&
           vidjenje.Longitude <=180)
        {
            vidjenje.Ptica = ptica;
            vidjenje.Podrucje = podrucje;
            ptica?.Vidjena?.Add(vidjenje);
            podrucje?.Vidjena?.Add(vidjenje);

            await Context.Vidjenja.AddAsync(vidjenje);
            await Context.SaveChangesAsync();
            return Ok("Uspesno upisano vidjenje");
        }
        else
        {
            return BadRequest("Podaci su nevalidni!");
        }
        
    }
    [HttpPost("UpisiPticu")]
    public async Task<ActionResult> UpisiPticu([FromBody] Ptica ptica, [FromQuery]int[] osobine)
    {
        try
        {
            ptica.Osobine = new List<Osobine>();
            
            foreach(int o in osobine)
            {
                var osobina = Context.Osobine.Include(p => p.Ptica).Where(p => p.ID == o).FirstOrDefault();

                if(osobina != null && osobina.Ptica != null)
                {
                    ptica.Osobine.Add(osobina);
                    osobina.Ptica.Add(ptica);
                    Context.Osobine.Update(osobina);
                }
            }

            await Context.Ptice.AddAsync(ptica);
            await Context.SaveChangesAsync();
            return Ok($"Uneta je ptica sa ID: {ptica.ID}.");
        }
        catch (Exception e)
        {
            return BadRequest(e.Message);
        }
    }
    [HttpGet("PreuzmiPtice/{podrucjeID}")]
    public async Task<ActionResult> PreuzmiPtice(int podrucjeID, [FromQuery]int[] osobineIDs)
    {
        try
        {
            var ptice = Context.Ptice
                    .Include(p => p.Osobine!)
                    .Include(p => p.Vidjena!)
                    .ThenInclude(p => p.Podrucje);

            // Ima više načina da se ovaj upit napiše.
            // Jedan od najjednostavnijih je da se proveri da li je broj osobina koje imaju ID
            // koji se nalazi u listi osobineIDs, isti broju elemenata tog niza
            var pSaOsob = ptice
                .Where(p => p.Osobine!.Count(p => osobineIDs.Contains(p.ID)) == osobineIDs.Count());

            // Proverava da li je ptica viđena na području koje je prosleđeno bar jednom
            var osobIPodr = await pSaOsob
                    .Where(p => p.Vidjena!
                            .Any(p => p.Podrucje!.ID == podrucjeID))
                    .ToListAsync();

            return Ok(osobIPodr.Select(p => new
            {
                p.ID,
                p.Naziv,
                p.Slika,
                BrojVidjenja = p.Vidjena!.Where(p => p.Podrucje!.ID == podrucjeID).Count()
            }).ToList());
        }
        catch (Exception e)
        {
            return BadRequest(e.Message);
        }
        
    }
    [HttpPost("DodajNepronadjenu")]
    public async Task<ActionResult> DodajNepronadjenu([FromQuery]int[] osobineIDs)
    {
        try
        {
            var nepoznata = new NepoznataPtica();
            nepoznata.Osobine = new List<Osobine>();

            foreach (int o in osobineIDs)
            {
                var osobina = await Context.Osobine.FindAsync(o);

                if (osobina != null)
                {
                    nepoznata.Osobine.Add(osobina);
                }
            }

            await Context.Nepoznata.AddAsync(nepoznata);
            await Context.SaveChangesAsync();

            return Ok($"Dodata nepoznata ptica: {nepoznata.ID}");
        }
        catch (Exception e)
        {
            return BadRequest(e.Message);
        }
    }
    [HttpGet("PreuzmiPticeSaVidjenjima")]
    public async Task<ActionResult> PreuzmiSaVidjenjima()
    {
        try
        {
            return Ok(await Context.Ptice.Include(p => p.Vidjena).ToListAsync());
        }
        catch (Exception e)
        {
            return BadRequest(e.Message);
        }
    }

    [HttpGet("PreuzmiPticeSaOsobinama")]
    public async Task<ActionResult> PreuzmiSaOsobinama()
    {
        try
        {
            return Ok(await Context.Ptice.Include(p => p.Osobine).ToListAsync());
        }
        catch (Exception e)
        {
            return BadRequest(e.Message);
        }
    }
}