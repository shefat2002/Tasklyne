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
    public async Task<IActionResult> Create(Project project)
    {
        if (!ModelState.IsValid)
        {
            return View(project);
        }

        try
        {
            _context.Projects.Add(project);
            var result = await _context.SaveChangesAsync();
            if (result > 0)
            {
                TempData["SuccessMsg"] = "Project created successfully!";
            }
            else
            {
                TempData["ErrorMsg"] = "Failed to create project.";
            }
        }
        catch (Exception)
        {
            TempData["ErrorMsg"] = "An unexpected error occurred while creating the project.";
        }

        return RedirectToAction("Index");
    }

    [HttpGet]
    public IActionResult Edit(int id)
    {
        var project = _context.Projects.Find(id);
        if(project == null)
        {
            return NotFound();
        }
        return View(project);
    }
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Edit(Project project) 
    {
        var existingProject = _context.Projects.Find(project.Id);
        if (existingProject == null)
        {
            return NotFound();
        }
        try
        {
            existingProject.Name = project.Name;
            existingProject.Description = project.Description;
            var result = await _context.SaveChangesAsync();
            if (result > 0)
            {
                TempData["SuccessMsg"] = "Project updated successfully!";
            }
            else
            {
                TempData["ErrorMsg"] = "Failed to update project.";
            }
        }
        catch (Exception)
        {
            TempData["ErrorMsg"] = "An unexpected error occurred while updating the project.";
        }
        return RedirectToAction("Index");
    }

    public IActionResult Delete(int id)
    {
        var project = _context.Projects.Find(id);
        if (project == null)
        {
            return NotFound();
        }
        try
        {
            _context.Projects.Remove(project);
            var result = _context.SaveChanges();
            if (result > 0)
            {
                TempData["SuccessMsg"] = "Project deleted successfully!";
            }
            else
            {
                TempData["ErrorMsg"] = "Failed to delete project.";
            }
        }
        catch (Exception)
        {
            TempData["ErrorMsg"] = "An unexpected error occurred while deleting the project.";
        }
        return RedirectToAction("Index");
    }
}
