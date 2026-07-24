using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

using dvd_rental.Data;

namespace dvd_rental.Controllers;

[ApiController]
[Route("api/[controller]")]
public class DvdController : ControllerBase
{
    private readonly AppDbContext _db;

    public DvdController(AppDbContext db) => _db = db; // dependency injection (AppDbContext registrato come servizio in Program.cs)

    // leggo tutto il catalogo (http get)
    [HttpGet]
    public async Task<ActionResult<IEnumerable<object>>> GetCatalogo() // object perchè non è un DVD (calcolo anche disponibili -> non è un campo di DVD)
    {
        var dvds = await _db.Dvds
            .Select(d => new
            {
                d.Id,
                d.Titolo,
                d.DataUscita,
                d.Categoria,
                d.DurataMinuti,
                d.QuantitaTotale,
                Disponibili = d.QuantitaTotale - d.Noleggi.Count(n => n.RestituzioneEffettiva == null)
            }) // query
            .ToListAsync(); // genera istruzione SQL

        return Ok(dvds);
    }
}