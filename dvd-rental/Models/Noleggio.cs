namespace dvd_rental.Models;

public class Noleggio( )
{
    public int Id { get; set; } 

    public int ClienteId { get; set; } 
    public Cliente Cliente { get; set; } = null!; // non è mai null

    public int DvdId { get; set; } 
    public Dvd Dvd { get; set; } = null!;

    public DateTime DataNoleggio { get; set; } 

    public DateTime RestituzionePrevista { get; set; } 
    public DateTime? RestituzioneEffettiva { get; set; } // può essere null se il dvd non è stato ancora restituito

}