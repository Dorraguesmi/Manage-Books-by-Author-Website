using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Project.Models.Repositories
{
  public interface ILivreRepository
  {
    IList<Livre> GetAll();
    Livre GetById(int id);
    void Add(Livre l);
    void Edit(Livre l);
    void Delete(Livre l);
    IList<Livre> GetLivresByAuteurID(int? AuteurId);
    IList<Livre> FindByName(string name);
  }
}