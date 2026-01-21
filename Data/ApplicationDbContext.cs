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

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Reaktion>()
                .HasOne(r => r.Benutzer)
                .WithMany(x=>x.Reaktionen)
                .HasForeignKey(r => r.BenutzerId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<Beitrag>()
                .HasMany(b => b.Reaktionen)
                .WithOne(r => r.Beitrag)
                .HasForeignKey(r => r.BeitragId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<Beitrag>()
                .HasOne(b => b.Autor)
                .WithMany(x=>x.Beitraege)
                .HasForeignKey(b => b.AutorId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<Beitrag>()
                .HasOne(b => b.Thema)
                .WithMany(x=>x.Beitraege)
                .HasForeignKey(b => b.ThemaId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<Thema>()
                .HasOne(t => t.Ersteller)
                .WithMany(x=>x.Themen)
                .HasForeignKey(t => t.ErstellerId)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<Termin>()
                .HasOne(t => t.Ersteller)
                .WithMany(x=>x.Termine)
                .HasForeignKey(t => t.ErstellerId)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<Benutzer>()
                .HasMany(b => b.Reaktionen)
                .WithOne(r => r.Benutzer)
                .HasForeignKey(r => r.BenutzerId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<Reaktion>()
                .HasOne(r => r.Beitrag)
                .WithMany(x=>x.Reaktionen)  
                .HasForeignKey(r => r.BeitragId)
                .OnDelete(DeleteBehavior.Restrict);
        }
    }
}