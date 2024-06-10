using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Project.Models
{
  public class Livre
  {
    public int Id { get; set; }
    public string Titre { get; set; } = string.Empty;
    public string? Maisondédition { get; set; }
    public DateTime Datedédition { get; set; }
    public string? Résumé { get; set; }
    public int AuteurId { get; set; }
    public Auteur? Auteur { get; set; }
    public string? ImagePath { get; set; }

  }
}