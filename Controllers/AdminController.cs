using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Project.Models.ViewModels;

namespace Project.Controllers
{
  // [Authorize(Roles = "Admin")]
  [Route("[controller]")]
  public class AdminController : Controller
  {
    private readonly RoleManager<IdentityRole> _roleManager;
    private readonly UserManager<IdentityUser> _userManager;

    public AdminController(RoleManager<IdentityRole> roleManager, UserManager<IdentityUser> userManager)
    {
      _roleManager = roleManager;
      _userManager = userManager;
    }

    [HttpGet("CreateRole")]
    public IActionResult CreateRole()
    {
      return View();
    }

    [HttpPost("CreateRole")]
    public async Task<IActionResult> CreateRole(CreateRoleViewModel model)
    {
      if (ModelState.IsValid)
      {
        IdentityRole role = new IdentityRole { Name = model.RoleName };
        IdentityResult result = await _roleManager.CreateAsync(role);
        if (result.Succeeded)
        {
          return RedirectToAction("Index", "Home");
        }
        foreach (IdentityError error in result.Errors)
        {
          ModelState.AddModelError(string.Empty, error.Description);
        }
      }
      return View(model);
    }

    [HttpGet("ListRoles")]
    public IActionResult ListRoles()
    {
      var roles = _roleManager.Roles;
      return View(roles);
    }

    [HttpGet("EditRole/{id}")]
    public async Task<IActionResult> EditRole(string id)
    {
      var role = await _roleManager.FindByIdAsync(id);
      if (role == null)
      {
        ViewBag.ErrorMessage = $"Role with Id = {id} cannot be found";
        return View("NotFound");
      }
      var model = new EditRoleViewModel
      {
        Id = role.Id,
        RoleName = role.Name
      };
      foreach (var user in _userManager.Users.ToList())
      {
        if (await _userManager.IsInRoleAsync(user, role.Name))
        {
          model.Users.Add(user.UserName);
        }
      }
      return View(model);
    }

    [HttpPost("EditRole/{id}")]
    public async Task<IActionResult> EditRole(EditRoleViewModel model)
    {
      var role = await _roleManager.FindByIdAsync(model.Id);
      if (role == null)
      {
        ViewBag.ErrorMessage = $"Role with Id = {model.Id} cannot be found";
        return View("NotFound");
      }
      role.Name = model.RoleName;
      var result = await _roleManager.UpdateAsync(role);
      if (result.Succeeded)
      {
        return RedirectToAction("ListRoles");
      }
      foreach (var error in result.Errors)
      {
        ModelState.AddModelError("", error.Description);
      }
      return View(model);
    }

    [HttpPost("DeleteRole/{id}")]
    public async Task<IActionResult> DeleteRole(string id)
    {
      var role = await _roleManager.FindByIdAsync(id);
      if (role == null)
      {
        ViewBag.ErrorMessage = $"Role with Id = {id} cannot be found";
        return View("NotFound");
      }
      var result = await _roleManager.DeleteAsync(role);
      if (result.Succeeded)
      {
        return RedirectToAction("ListRoles");
      }
      foreach (var error in result.Errors)
      {
        ModelState.AddModelError("", error.Description);
      }
      return View("ListRoles");
    }

    [HttpGet("EditUsersInRole/{roleId}")]
    public async Task<IActionResult> EditUsersInRole(string roleId)
    {
      ViewBag.roleId = roleId;
      var role = await _roleManager.FindByIdAsync(roleId);
      if (role == null)
      {
        ViewBag.ErrorMessage = $"Role with Id = {roleId} cannot be found";
        return View("NotFound");
      }
      var model = new List<UserRoleViewModel>();
      foreach (var user in _userManager.Users.ToList())
      {
        var userRoleViewModel = new UserRoleViewModel
        {
          UserId = user.Id,
          UserName = user.UserName
        };
        if (await _userManager.IsInRoleAsync(user, role.Name))
        {
          userRoleViewModel.IsSelected = true;
        }
        else
        {
          userRoleViewModel.IsSelected = false;
        }
        model.Add(userRoleViewModel);
      }
      return View(model);
    }

    [HttpPost("EditUsersInRole/{roleId}")]
    public async Task<IActionResult> EditUsersInRole(List<UserRoleViewModel> model, string roleId)
    {
      var role = await _roleManager.FindByIdAsync(roleId);
      if (role == null)
      {
        ViewBag.ErrorMessage = $"Role with Id = {roleId} cannot be found";
        return View("NotFound");
      }
      foreach (var userRole in model)
      {
        var user = await _userManager.FindByIdAsync(userRole.UserId);
        if (user == null)
        {
          return NotFound();
        }

        if (userRole.IsSelected && !(await _userManager.IsInRoleAsync(user, role.Name)))
        {
          await _userManager.AddToRoleAsync(user, role.Name);
        }
        else if (!userRole.IsSelected && await _userManager.IsInRoleAsync(user, role.Name))
        {
          await _userManager.RemoveFromRoleAsync(user, role.Name);
        }
        else
        {
          continue;
        }
      }
      return RedirectToAction("EditRole", new { roleId });
    }
  }
}
