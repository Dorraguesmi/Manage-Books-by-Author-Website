using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Project.Models.ViewModels
{
  public class UserRoleViewModel
  {
    public required string UserId { get; set; }
    public required string UserName { get; set; }
    public bool IsSelected { get; set; }
  }
}