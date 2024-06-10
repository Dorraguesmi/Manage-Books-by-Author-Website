using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Project.ViewModels;

namespace Project.Controllers
{
  [AllowAnonymous]
  [Route("[controller]")]
  public class AccountController : Controller
  {
    private readonly UserManager<IdentityUser> _userManager;
    private readonly SignInManager<IdentityUser> _signInManager;

    public AccountController(UserManager<IdentityUser> userManager, SignInManager<IdentityUser> signInManager)
    {
      _userManager = userManager;
      _signInManager = signInManager;
    }

    [HttpGet("Register", Name = "Register")]
    public IActionResult Register()
    {
      return View("~/Views/Home/Register.cshtml");
    }
    [HttpPost("Register")]
    public async Task<IActionResult> Register(RegisterViewModel model)
    {
      if (ModelState.IsValid)
      {
        var user = new IdentityUser { UserName = model.Email, Email = model.Email };
        var result = await _userManager.CreateAsync(user, model.Password);

        if (result.Succeeded)
        {
          await _signInManager.SignInAsync(user, isPersistent: false);
          return RedirectToAction("Index", "Home");
        }

        foreach (var error in result.Errors)
        {
          ModelState.AddModelError(string.Empty, error.Description);
        }
      }

      return View("~/Views/Home/Register.cshtml", model);
    }

    [HttpGet("Login", Name = "Login")]
    public IActionResult Login(string returnUrl = null)
    {
      ViewData["ReturnUrl"] = returnUrl;
      return View("~/Views/Home/Login.cshtml");
    }

    [HttpPost("Login")]
    public async Task<IActionResult> Login(Models.ViewModels.LoginViewModel model, string returnUrl = null)
    {
      ViewData["ReturnUrl"] = returnUrl;

      if (ModelState.IsValid)
      {
        var result = await _signInManager.PasswordSignInAsync(model.Email, model.Password, model.RememberMe, lockoutOnFailure: false);

        if (result.Succeeded)
        {
          if (!string.IsNullOrEmpty(returnUrl) && Url.IsLocalUrl(returnUrl))
          {
            return Redirect(returnUrl);
          }
          else
          {
            return RedirectToAction("Index", "Home");
          }
        }

        ModelState.AddModelError(string.Empty, "Invalid login attempt.");
      }

      return View("~/Views/Home/Login.cshtml", model);
    }

    [HttpPost("Logout", Name = "Logout")]
    public async Task<IActionResult> Logout()
    {
      await _signInManager.SignOutAsync();
      return RedirectToAction("Index", "Home");
    }
  }
}
