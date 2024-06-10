using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace Project.Models.Repositories
{
  public class LivreRepository : ILivreRepository
  {
    readonly LivreContext context;
    public LivreRepository(LivreContext context)
    {
      this.context = context;
    }
    public IList<Livre> GetAll()
    {
      return context.Livres.OrderBy(x => x.Titre).Include(x => x.Auteur).ToList();
    }
    public Livre GetById(int id)
    {
      return context.Livres.Where(x => x.Id == id).Include(x => x.Auteur).SingleOrDefault();
    }
    public void Add(Livre l)
    {
      context.Livres.Add(l);
      context.SaveChanges();
    }
    public void Edit(Livre l)
{
    Livre l1 = context.Livres.Include(x => x.Auteur).SingleOrDefault(x => x.Id == l.Id);
    if (l1 != null)
    {
        l1.Titre = l.Titre;
        l1.Maisondédition = l.Maisondédition;
        l1.Datedédition = l.Datedédition;
        l1.Résumé = l.Résumé;

        if (l1.Auteur != null && l.Auteur != null && l1.Auteur.ID != l.Auteur.ID)
        {
            var newAuteur = context.Auteurs.Find(l.Auteur.ID);
            if (newAuteur != null)
            {
                l1.Auteur = newAuteur;
            }
        }

        context.SaveChanges();
    }
}


    public void Delete(Livre l)
    {
      Livre l1 = context.Livres.Find(l.Id);
      if (l1 != null)
      {
        context.Livres.Remove(l1);
        context.SaveChanges();
      }
    }
    public IList<Livre> GetLivresByAuteurID(int? auteurId)
    {
      return context.Livres.Where(s =>
      s.Auteur.ID.Equals(auteurId))
      .OrderBy(l => l.Titre)
      .Include(std => std.Auteur).ToList();

    }
    public IList<Livre> FindByName(string titre)
    {
      return context.Livres.Where(s =>
      s.Titre.Contains(titre)).Include(std =>
      std.Auteur).ToList();

    }

  }
}