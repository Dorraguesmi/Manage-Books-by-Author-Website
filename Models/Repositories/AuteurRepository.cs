using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace Project.Models.Repositories
{
  public class AuteurRepository : IAuteurRepository
  {
    readonly LivreContext context;
    public AuteurRepository(LivreContext context)
    {
      this.context = context;

    }
    public IList<Auteur> GetAll()
    {
      return context.Auteurs.OrderBy(a => a.Nom).ToList();
    }
    public Auteur GetById(int id)
    {
#pragma warning disable CS8603 
      return context.Auteurs.Find(id);
#pragma warning restore CS8603 
    }
    public void Add(Auteur auteur)
    {
      context.Auteurs.Add(auteur);
      var result = context.SaveChanges();
      if (result == 0)
      {
        throw new InvalidOperationException("No records were added to the database.");
      }
    }


    public void Edit(Auteur a)
    {
      Auteur a1 = context.Auteurs.Find(a.ID);
      if (a1 != null)
      {
        a1.Nom = a.Nom;
        a1.Prenom = a.Prenom;
        context.SaveChanges();
      }
    }
    public void Delete(Auteur a)
    {
      Auteur a1 = context.Auteurs.Find(a.ID);
      if (a1 != null)
      {
        context.Auteurs.Remove(a1);
        context.SaveChanges();
      }
    }
    public int LivreCount(int auteurId)
    {
      return context.Auteurs.Where(a => a.ID ==
      auteurId).Count();
    }
    public IEnumerable<Auteur> GetAllWithLivres()
    {
      return context.Auteurs.Include(a => a.Livres).ToList();
    }
  }
}