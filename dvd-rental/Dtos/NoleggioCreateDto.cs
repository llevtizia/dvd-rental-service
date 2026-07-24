namespace dvd_rental.Dtos;

public class NoleggioCreateDto
{
    public int ClienteId { get; set; }
    public int DvdId { get; set; }
    public DateTime RestituzionePrevista { get; set; }
}