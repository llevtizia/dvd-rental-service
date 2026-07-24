using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

using dvd_rental.Data;
using dvd_rental.Models;
using dvd_rental.Dtos;

namespace dvd_rental.Controllers;

[ApiController]
[Route("api/[controller]")]
public class NoleggiController : ControllerBase
{
    private readonly AppDbContext _db;

    public NoleggiController(AppDbContext db) => _db = db;

    // leggo un noleggio dato l'id (http get id)
    [HttpGet("{id}")]
    public async Task<ActionResult<object>> GetById(int id)
    {
        var noleggio = await _db.Noleggi.FindAsync(id);
        if ( noleggio is null )
            return NotFound("Noleggio non trovato");
        
        var risultato = new
        {
            noleggio.Id, noleggio.ClienteId, noleggio.DvdId, noleggio.DataNoleggio,
            noleggio.RestituzionePrevista, noleggio.RestituzioneEffettiva
        };

        return Ok(risultato);
    }

    // apro un nuovo noleggio (http post)
    [HttpPost]
    public async Task<ActionResult<object>> Create(NoleggioCreateDto dto)
    {
        var dvd = await _db.Dvds
            .Include( d => d.Noleggi ) // carico i noleggi di ogni dvd
            .FirstOrDefaultAsync( d => d.Id == dto.DvdId );
        if ( dvd is null )
            return NotFound("Dvd non trovato!");

        var cliente = await _db.Clienti.FindAsync(dto.ClienteId);
        if ( cliente is null )
            return NotFound("Cliente non trovato!");

        var copieDisponibili = dvd.Noleggi.Count( n => n.RestituzioneEffettiva == null);
        if ( copieDisponibili >= dvd.QuantitaTotale )
            return BadRequest("Nessuna copia attualmente disponibile per questo DVD!");
        
        var noleggio = new Noleggio // creo il noleggio da aggiungere
        {
            ClienteId = dto.ClienteId,
            DvdId = dto.DvdId,
            DataNoleggio = DateTime.UtcNow,
            RestituzionePrevista = dto.RestituzionePrevista
        };

        _db.Noleggi.Add(noleggio);
        await _db.SaveChangesAsync();

        var risultato = new // senza la lista dei noleggi per cliente che fa un object cycle
        { 
            noleggio.Id, noleggio.ClienteId, noleggio.DvdId,
            noleggio.DataNoleggio, noleggio.RestituzionePrevista, noleggio.RestituzioneEffettiva

        };
        // coem in clienticontroller.cs
        return CreatedAtAction(
            nameof(GetById),
            new { id = noleggio.Id },
            risultato );
    }

    // chiudo il noleggio (modifico la risorsa)
    [HttpPut("{id}/restituzione")]
    public async Task<IActionResult> Restituisci(int id) // IActionResult è più generico di ActionResult<T> -> nessun percorso restituisce dei dati
    {
        var noleggio = await _db.Noleggi.FindAsync(id);
        if ( noleggio is null )
            return NotFound("Noleggio non trovato");

        if ( noleggio.RestituzioneEffettiva is not null )
            return BadRequest("Il Dvd è già stato restituito");
        
        noleggio.RestituzioneEffettiva = DateTime.UtcNow;
        await _db.SaveChangesAsync();
        return NoContent(); // 204 -> l'operazione è andata
    }

    // leggo tutto lo storico (http get)
    [HttpGet]
    public async Task<ActionResult<IEnumerable<object>>> GetStorico(int? clienteId, string? titolo)
    {
        var query = _db.Noleggi // query base che prende cliente e dvd dal noleggio
            .Include( n => n.Cliente )
            .Include( n => n.Dvd)
            .AsQueryable(); // Queryable = query non ancora eseguita  

        if ( clienteId is not null ) // aggiungo un filtro alla volta sulla query e la salvo
            query = query.Where( n => n.ClienteId == clienteId);

        if ( !string.IsNullOrEmpty(titolo) )
            query = query.Where( n => n.Dvd.Titolo.Contains(titolo));

        var risultato = await query.Select( n => new
        {
           n.Id,
           Cliente = n.Cliente.Nome + " " + n.Cliente.Cognome,
           Dvd = n.Dvd.Titolo, n.DataNoleggio, n.RestituzionePrevista, n.RestituzioneEffettiva
        }).ToListAsync();
        
        return Ok(risultato);
    }

}