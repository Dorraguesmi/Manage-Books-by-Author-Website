using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Project.Models.ViewModels
{
  public class LoginViewModel
  {
    [Required]
    [EmailAddress]
    public required string Email { get; set; }
    [Required]
    [DataType(DataType.Password)]
    public required string Password { get; set; }
    [Display(Name = "Remember me")]
    public bool RememberMe { get; set; }
  }
}