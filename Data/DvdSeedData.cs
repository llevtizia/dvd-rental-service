using dvd_rental.Models;


namespace dvd_rental.Data
{
    public class DvdSeedData
    {
        public static List<Dvd> GetDvdSeedData() =>
        [
            // commedie
            new Dvd() { Titolo = "Barbie", DataUscita = new DateOnly(2023, 7, 20), Categoria = "Commedia", DurataMinuti = 114, QuantitaTotale = 8 },

            new Dvd() { Titolo = "Little Miss Sunshine", DataUscita = new DateOnly(2006, 9, 22), Categoria = "Commedia", DurataMinuti = 101, QuantitaTotale = 5 },

            // drammatico
            new Dvd() { Titolo = "Il Padrino", DataUscita = new DateOnly(1972, 3, 24), Categoria = "Drammatico", DurataMinuti = 175, QuantitaTotale = 3 },

            new Dvd() { Titolo = "Mommy", DataUscita = new DateOnly(2014, 12, 4), Categoria = "Drammatico", DurataMinuti = 139, QuantitaTotale = 2 },

            // crime
            new Dvd() { Titolo = "Pulp Fiction", DataUscita = new DateOnly(1994, 10, 14), Categoria = "Crime", DurataMinuti = 154, QuantitaTotale = 5 },

            new Dvd() { Titolo = "Seven", DataUscita = new DateOnly(1995, 12, 10), Categoria = "Crime", DurataMinuti = 127, QuantitaTotale = 6 },

            // fantascienza
            new Dvd() { Titolo = "Inception", DataUscita = new DateOnly(2010, 7, 16), Categoria = "Fantascienza", DurataMinuti = 148, QuantitaTotale = 4 },

            new Dvd() { Titolo = "Blade Runner", DataUscita = new DateOnly(1982, 10, 14), Categoria = "Fantascienza", DurataMinuti = 118, QuantitaTotale = 2 },

            // animazione
            new Dvd() { Titolo = "Spider-Man - Un nuovo universo", DataUscita = new DateOnly(2018, 12, 25), Categoria = "Animazione", DurataMinuti = 117, QuantitaTotale = 8 },

            new Dvd() { Titolo = "Your Name", DataUscita = new DateOnly(2017, 1, 25), Categoria = "Animazione", DurataMinuti = 107, QuantitaTotale = 8 },

            // horror
            new Dvd() { Titolo = "The Witch", DataUscita = new DateOnly(2016, 8, 18), Categoria = "Horror", DurataMinuti = 93, QuantitaTotale = 1 },

            new Dvd() { Titolo = "La cosa", DataUscita = new DateOnly(1982, 11, 25), Categoria = "Horror", DurataMinuti = 110, QuantitaTotale = 7 },
        ];
        
           
    }
}
