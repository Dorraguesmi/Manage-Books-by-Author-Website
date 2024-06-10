using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Project.Models.Repositories
{
  public interface IAuteurRepository
  {
    IList<Auteur> GetAll();
    Auteur GetById(int id);
    void Add(Auteur a);
    void Edit(Auteur a);
    void Delete(Auteur a);
    int LivreCount(int auteurId);
    IEnumerable<Auteur> GetAllWithLivres();
  }
}