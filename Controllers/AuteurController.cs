using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Project.Models;
using Project.Models.Repositories;

namespace Project.Controllers
{
  [Route("[controller]")]
  [Authorize]
  // [Authorize(Roles = "Admin,Manager")]
  public class AuteurController : Controller
  {
    private readonly IAuteurRepository _auteurRepository;

    public AuteurController(IAuteurRepository auteurRepository)
    {
      _auteurRepository = auteurRepository;
    }

    [HttpGet("")]
    [AllowAnonymous]
    public IActionResult Index(string search)
    {
      var auteurs = _auteurRepository.GetAllWithLivres();

      if (!string.IsNullOrEmpty(search))
      {
        auteurs = auteurs.Where(a => a.Nom.Contains(search, StringComparison.OrdinalIgnoreCase)).ToList();
      }

      return View(auteurs);
    }

    [HttpGet("Details/{id}")]
    public IActionResult Details(int id)
    {
      var auteur = _auteurRepository.GetById(id);
      if (auteur == null)
      {
        return NotFound();
      }
      return View(auteur);
    }

    [HttpGet("Create")]
    public IActionResult Create()
    {
      return View();
    }

    [HttpPost("Create")]
    [ValidateAntiForgeryToken]
    public IActionResult Create(Auteur auteur)
    {
      if (!ModelState.IsValid)
      {
        // Log each error to understand what might be causing ModelState to be invalid
        foreach (var error in ModelState.Values.SelectMany(m => m.Errors))
        {
          Console.WriteLine(error.ErrorMessage);
        }
        return View(auteur);
      }

      try
      {
        _auteurRepository.Add(auteur);
        return RedirectToAction(nameof(Index));
      }
      catch (Exception ex)
      {
        // Log the exception to understand what went wrong during the save operation
        Console.WriteLine($"Exception: {ex.Message}");
        ModelState.AddModelError("", "An error occurred saving the author");
        return View(auteur);
      }
    }



    [HttpGet("Edit/{id}")]
    public IActionResult Edit(int id)
    {
      var auteur = _auteurRepository.GetById(id);
      if (auteur == null)
      {
        return NotFound();
      }
      return View(auteur);
    }

    [HttpPost("Edit/{id}")]
    [ValidateAntiForgeryToken]
    public IActionResult Edit(int id, Auteur auteur)
    {
      if (id != auteur.ID)
      {
        return BadRequest();
      }
      if (ModelState.IsValid)
      {
        _auteurRepository.Edit(auteur);
        return RedirectToAction(nameof(Index));
      }
      return View(auteur);
    }

    [HttpGet("Delete/{id}")]
    public IActionResult Delete(int id)
    {
      var auteur = _auteurRepository.GetById(id);
      if (auteur == null)
      {
        return NotFound();
      }
      return View(auteur);
    }

    [HttpPost("Delete/{id}")]
    [ActionName("Delete")]
    [ValidateAntiForgeryToken]
    public IActionResult DeleteConfirmed(int id)
    {
      var auteur = _auteurRepository.GetById(id);
      _auteurRepository.Delete(auteur);
      return RedirectToAction(nameof(Index));
    }
  }
}
