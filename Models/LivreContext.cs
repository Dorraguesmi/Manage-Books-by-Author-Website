using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace Project.Models
{
  public class LivreContext : IdentityDbContext
  {
    public LivreContext(DbContextOptions<LivreContext> options) : base(options)
    {
    }
    public DbSet<Livre> Livres { get; set; }
    public DbSet<Auteur> Auteurs { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
      base.OnModelCreating(modelBuilder);

      modelBuilder.Entity<Auteur>()
          .HasMany(a => a.Livres)
          .WithOne(l => l.Auteur)
          .HasForeignKey(l => l.AuteurId);
    }
  }
}
