using Biljke.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Biljke.Controllers;

[ApiController]
[Route("[controller]")]
public class BiljkeController : ControllerBase
{
    public Context Context{ get; set; }

    public BiljkeController(Context context)
    {
        Context = context;
    }
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [HttpPost("UpisiVidjenje/{idBiljke}/{idPodrucja}")]
    public async Task<ActionResult> UpisiVidjenje([FromBody]Vidjenja vidjenje, int idBiljke, int idPodrucja)
    {
        if(vidjenje.Latitude >= -90 && vidjenje.Latitude <= 90 &&
        vidjenje.Longitude>= -180 && vidjenje.Longitude<= 180)
        {
            var biljka = await Context.Biljke.Include(p => p.Vidjenja).Where(p => p.ID == idBiljke).FirstOrDefaultAsync();
            var podrucje = await Context.Podrucja.Include(p => p.Vidjenja).Where(p => p.ID == idPodrucja).FirstOrDefaultAsync();

            if(biljka != null && podrucje != null)
            {
                vidjenje.Biljka = biljka;
                vidjenje.Podrucje = podrucje;
                biljka?.Vidjenja?.Add(vidjenje);
                podrucje?.Vidjenja?.Add(vidjenje);

                await Context.Vidjenja.AddAsync(vidjenje);
                await Context.SaveChangesAsync();

                return Ok($"Vidjenje sa ID: {vidjenje.ID} je upisano");
            }
            else
            {
                return BadRequest("Nisu pronađene biljka i područje.");
            }
        }
        else
        {
            return BadRequest("Nije uspelo");  
        }
    }
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [HttpPost("UpisiBiljku")]
    public async Task<ActionResult> UpisiBiljku([FromBody]Models.Biljke biljka, [FromQuery]int[] osobine)
    {
        try
        {
            biljka.Osobine = new List<Osobine>();

            foreach(int o in osobine)
            {
                var osobina = await Context.Osobine.Include(p => p.Biljke).Where(p => p.ID == o).FirstOrDefaultAsync();

                if(osobina != null && osobina.Biljke != null)
                {
                    osobina.Biljke.Add(biljka);
                    biljka.Osobine.Add(osobina);
                }
            }
            await Context.Biljke.AddAsync(biljka);
            await Context.SaveChangesAsync();

            return Ok($"Dodata je biljka sa ID: {biljka.ID}.");
        }    
        catch(Exception e)
        {
            return BadRequest(e.Message);
        }
    }
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [HttpGet("PreuzmiBiljke/{podrucjeID}")]
    public async Task<ActionResult> PreuzmiBiljke(int podrucjeID, [FromQuery] int[] osobineIDs)
    {
        try
        {
            var biljke = await Context.Biljke
                .Include(p => p.Osobine)
                .Include(p => p.Vidjenja!)
                .ThenInclude(p => p.Podrucje)
                .Where(p => p.Vidjenja!.Any(q => q.Podrucje!.ID == podrucjeID))
                .Where(p => p.Osobine!.Count(q => osobineIDs.Contains(q.ID))== osobineIDs.Length)
                .Select(p => new
                {
                    p.ID,
                    p.Naziv,
                    p.Slika,
                    BrojVidjenja = p.Vidjenja!.Count
                }).ToListAsync();
                return Ok(biljke);
        }
        catch(Exception e)
        {
            return BadRequest(e.Message);
        }
    }
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status403Forbidden)]
    [HttpPost("DodajNepronadjenu")]
    public async Task<ActionResult> DodajNepronadjenu([FromQuery]int[] osobineIDs)
    {
        try
        {
            var nepoznataBiljka = new NepoznataBiljka
            {
                Osobine = new List<Osobine>()
            };

            bool uspelo = true;

            foreach (int osobinaID in osobineIDs)
            {
                var o = await Context.Osobine
                    .Include(p => p.NepoznateBiljke)
                    .Where(p => p.ID == osobinaID).FirstOrDefaultAsync();

                if (nepoznataBiljka.Osobine != null &&
                    o != null &&
                    o.NepoznateBiljke != null)
                {
                    nepoznataBiljka.Osobine.Add(o);
                    o.NepoznateBiljke.Add(nepoznataBiljka);
                }
                else
                {
                    uspelo = false;
                }
            }

            if (uspelo)
            {
                await Context.NepoznateBiljke.AddAsync(nepoznataBiljka);
                await Context.SaveChangesAsync();
                return Ok($"Uspešno upisana nepoznata biljka.");
            }
            else
            {
                return StatusCode(StatusCodes.Status403Forbidden, "Nije uspelo upisivanje nepoznate biljke.");
            }
        }
        catch (Exception e)
        {
            return BadRequest(e.Message);
        }
    }
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [Produces("text/xml")]
    [Route("PreuzmiBiljkeXML")]
    [HttpGet]
    public async Task<ActionResult> PreuzmiBiljkeXML([FromQuery]int[] ids)
    {
        try
        {
            var xml = await Context.Biljke
                .Where(p => ids.Contains(p.ID))
                .ToListAsync();
            return Ok(xml);
        }
        catch (Exception e)
        {
            return BadRequest(e.Message);
        }
    }

}