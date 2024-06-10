using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;


namespace Project.Models
{
  public class Auteur
  {
    public int ID { get; set; }

    [Required]
    [StringLength(100, MinimumLength = 1)]
    public string? Nom { get; set; }

    [Required]
    [StringLength(100, MinimumLength = 1)]
    public string? Prenom { get; set; }

    [DataType(DataType.Date)]
    public DateTime? DatedeNaissance { get; set; }

    public List<Livre> Livres { get; set; } = new List<Livre>();
  }
}