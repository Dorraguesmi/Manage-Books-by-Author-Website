using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Project.Models;
using Project.Models.Repositories;
using System.Linq;

namespace Project.Controllers
{
  [Route("[controller]")]
  [Authorize]
  public class LivreController : Controller
  {
    private readonly ILivreRepository _livreRepository;
    private readonly IAuteurRepository _auteurRepository;

    public LivreController(ILivreRepository livreRepository, IAuteurRepository auteurRepository)
    {
      _livreRepository = livreRepository;
      _auteurRepository = auteurRepository;
    }

    [HttpGet("")]
    [AllowAnonymous]
    public IActionResult Index(string search)
    {
      var livres = _livreRepository.GetAll();
      if (!string.IsNullOrEmpty(search))
      {
        livres = livres.Where(l => l.Titre.Contains(search, StringComparison.OrdinalIgnoreCase)).ToList();
      }
      return View(livres);
    }

    [HttpGet("Details/{id}")]
    [AllowAnonymous]
    public IActionResult Details(int id)
    {
      var livre = _livreRepository.GetById(id);
      if (livre == null)
      {
        return NotFound();
      }
      return View(livre);
    }

    [HttpGet("Create")]
    public IActionResult Create()
    {
      PopulateAuteursDropdown();
      return View();
    }

    [HttpPost("Create")]
    [ValidateAntiForgeryToken]
    public IActionResult Create(Livre livre, string AuteurId)
    {
      if (AuteurId == "others")
      {
        return RedirectToAction("Create", "Auteur");
      }

      if (!string.IsNullOrEmpty(AuteurId) && int.TryParse(AuteurId, out int auteurIdParsed))
      {
        livre.AuteurId = auteurIdParsed;
      }
      else
      {
        ModelState.AddModelError("AuteurId", "Invalid author selected.");
      }

      if (ModelState.IsValid)
      {
        _livreRepository.Add(livre);
        return RedirectToAction(nameof(Index));
      }

      PopulateAuteursDropdown(livre.AuteurId);
      return View(livre);
    }

    [HttpGet("Edit/{id}")]
    public IActionResult Edit(int id)
    {
      var livre = _livreRepository.GetById(id);
      if (livre == null)
      {
        return NotFound();
      }

      PopulateAuteursDropdown(livre.AuteurId);
      return View(livre);
    }

    [HttpPost("Edit/{id}")]
    [ValidateAntiForgeryToken]
    public IActionResult Edit(int id, Livre livre, string AuteurId)
    {
      if (id != livre.Id)
      {
        return BadRequest();
      }

      if (AuteurId == "others")
      {
        return RedirectToAction("Create", "Auteur");
      }

      if (!string.IsNullOrEmpty(AuteurId) && int.TryParse(AuteurId, out int auteurIdParsed))
      {
        livre.AuteurId = auteurIdParsed;
      }
      else
      {
        ModelState.AddModelError("AuteurId", "Invalid author selected.");
      }

      if (ModelState.IsValid)
      {
        try
        {
          _livreRepository.Edit(livre);
          return RedirectToAction(nameof(Index));
        }
        catch (ArgumentException ex)
        {
          ModelState.AddModelError(string.Empty, ex.Message);
        }
      }

      PopulateAuteursDropdown(livre.AuteurId);
      return View(livre);
    }

    [HttpGet("Delete/{id}")]
    public IActionResult Delete(int id)
    {
      var livre = _livreRepository.GetById(id);
      if (livre == null)
      {
        return NotFound();
      }
      return View(livre);
    }

    [HttpPost("Delete/{id}"), ActionName("Delete")]
    [ValidateAntiForgeryToken]
    public IActionResult DeleteConfirmed(int id)
    {
      var livre = _livreRepository.GetById(id);
      _livreRepository.Delete(livre);
      return RedirectToAction(nameof(Index));
    }

    private void PopulateAuteursDropdown(int? selectedAuteurId = null)
    {
      var auteurs = _auteurRepository.GetAll()
          .Select(a => new SelectListItem
          {
            Value = a.ID.ToString(),
            Text = $"{a.Prenom} {a.Nom}"
          }).ToList();

      auteurs.Insert(0, new SelectListItem { Value = "", Text = "-- Select Auteur --" });
      auteurs.Add(new SelectListItem { Value = "others", Text = "Others" });

      ViewBag.Auteurs = new SelectList(auteurs, "Value", "Text", selectedAuteurId);
    }
  }
}
