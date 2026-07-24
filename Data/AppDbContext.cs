using Microsoft.EntityFrameworkCore;
using dvd_rental.Models;

namespace dvd_rental.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) {}

        public DbSet<Cliente> Clienti { get; set; }
        public DbSet<Dvd> Dvds { get; set; }
        public DbSet<Noleggio> Noleggi { get; set; }
    }
}