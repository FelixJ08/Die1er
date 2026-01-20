using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Die1Er_Projektarbeit.Models;

namespace Die1Er_Projektarbeit.Data
{
    public class ApplicationDbContext : IdentityDbContext
    {
        public DbSet<Beitrag> Beitrag { get; set; }
        public DbSet<Benutzer> Benutzer { get; set; }
        public DbSet<Berufsbereich> Berufsbereiches { get; set; }
        public DbSet<Reaktion> Reaktion { get; set; }
        public DbSet<Termin> Termin { get; set; }
        public DbSet<Thema> Thema { get; set; }

        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }
    }
}
