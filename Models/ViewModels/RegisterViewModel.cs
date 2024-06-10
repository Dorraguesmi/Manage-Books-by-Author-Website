using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Project.ViewModels
{
  public class RegisterViewModel
  {
    // Existing properties
    public string Email { get; set; }
    public string Password { get; set; }
    public string ConfirmPassword { get; set; }

    // New property for role selection
    // public string SelectedRole { get; set; }
    // public List<string> Roles { get; set; }
  }
}
