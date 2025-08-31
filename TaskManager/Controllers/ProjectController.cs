using Microsoft.AspNetCore.Mvc;
using Tasklyne.Data;
using Tasklyne.Models;

namespace Tasklyne.Controllers;

public class ProjectController : Controller
{
    private readonly TaskDbContext _context;
    public ProjectController(TaskDbContext context)
    {
        _context = context;
    }
    public IActionResult Index()
    {
        var projects = _context.Projects.ToList();
        return View(projects);
    }
    public IActionResult Create()
    {
        return View();
    }
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Create(Project project) // Changed to Task<IActionResult>
    {
        _context.Projects.Add(project);
        var result = await _context.SaveChangesAsync();
        if(result > 0)
        {
            TempData["SuccessMsg"] = "Project created successfully!";
        }
        else
        {
            TempData["ErrorMsg"] = "Failed to create project.";
        }
        return RedirectToAction("Index");
    }
}
