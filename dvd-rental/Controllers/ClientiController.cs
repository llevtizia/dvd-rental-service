using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

using dvd_rental.Data;
using dvd_rental.Models;

namespace dvd_rental.Controllers;

[ApiController]
[Route("api/[controller]")]
public class ClientiController : ControllerBase
{
    private readonly AppDbContext _db;

    public ClientiController(AppDbContext db) => _db = db; // dependency injection 

    // leggo tutti i clienti (http get)
    [HttpGet]
    public async Task<ActionResult<IEnumerable<object>>> GetAll()
    {
        var clienti = await _db.Clienti.Select( c => new { c.Id, c.Nome, c.Cognome, c.Email }).ToListAsync(); 
        return Ok(clienti);
    }

    // leggo un singolo cliente dato l'id (http get id)
    [HttpGet("{id}")]
    public async Task<ActionResult<Cliente>> GetById(int id)
    {
        var cliente = await _db.Clienti.FindAsync(id);
        return cliente is null ? NotFound("Cliente non trovato") : Ok(cliente);
    }

    // aggiungo un nuovo cliente (http post)
    [HttpPost]
    public async Task<ActionResult<Cliente>> Create( Cliente cliente )
    {
        _db.Clienti.Add(cliente); // non scrive nel database (tipo preparare una query)
        await _db.SaveChangesAsync(); // genera istruzione SQL -> viene generato un id del cliente
        return CreatedAtAction( // ritorna 201 created
            nameof(GetById), // azione dove punta il link nell'header in location
            new { id = cliente.Id }, // oggetto anonimo che riempie {id} nell'url del link -> es. api/Clienti/{id}
            cliente ); // oggetto nel body della risposta 
    }
}