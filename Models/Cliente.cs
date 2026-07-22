namespace dvd_rental.Models;

public class Cliente
{
    public int Id { get; set; }

    public string Nome { get; set; } = "";
    public string Cognome { get; set; } = "";
 
    public string Email { get; set; } = "";

    // un cliente può avere tanti noleggi
    public ICollection<Noleggio> Noleggi { get; set; } = [];
}