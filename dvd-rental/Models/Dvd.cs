namespace dvd_rental.Models;

public class Dvd
{
    public int Id { get; set; }

    public string Titolo { get; set; } = "";

    public DateOnly DataUscita { get; set; }
    
    public string Categoria { get; set; } = "";

    public int DurataMinuti { get; set; }

    public int QuantitaTotale { get; set; }

    // un dvd può essere stato noleggiato tante volte
    public ICollection<Noleggio> Noleggi { get; set; } = [];
}