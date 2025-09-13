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
                return RedirectToAction("Index");
            }
            else
            {
                ModelState.AddModelError(string.Empty, "Failed to create project.");
                return View(project);
            }
        }
        catch (Exception)
        {
            ModelState.AddModelError(string.Empty, "An unexpected error occurred while creating the project.");
            return View(project);
        }
        return RedirectToAction("Index");
    }

    [HttpGet]
    public IActionResult Get()
    {
        var projects = _context.Projects.ToList();
        return Json(projects);
    }
    public IActionResult GetbyId(int id)
    {
        var project = _context.Projects.Find(id);
        if (project == null)
        {
            return NotFound();
        }
        return Json(project);
    }

    public async Task<IActionResult> Save(Project project)
    {
        try
        {
            await _context.Projects.AddAsync(project);
            var result = await _context.SaveChangesAsync();
            if (result > 0)
            {
                return Json(new { success = true, message = "Project created successfully!" });
            }
            else
            {
                return Json(new { success = false, message = "Failed to create project." });
            }
        }
        catch (Exception)
        {
            return Json(new { success = false, message = "An unexpected error occurred while creating the project." });
        }
    }

    public IActionResult Update(Project project)
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
            var result = _context.SaveChanges();
            if (result > 0)
            {
                return Json(new { success = true, message = "Project updated successfully!" });
            }
            else
            {
                return Json(new { success = false, message = "Failed to update project." });
            }
        }
        catch (Exception)
        {
            return Json(new { success = false, message = "An unexpected error occurred while updating the project." });
        }
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
                return Json(new { success = true, message = "Project deleted successfully!" });
            }
            else
            {
                return Json(new { success = false, message = "Failed to delete project." });
            }
        }
        catch (Exception)
        {
            return Json(new { success = false, message = "An unexpected error occurred while deleting the project." });
        }
    }

    //[HttpGet]
    //public IActionResult Edit(int id)
    //{
    //    var project = _context.Projects.Find(id);
    //    if(project == null)
    //    {
    //        return NotFound();
    //    }
    //    return View(project);
    //}
    //[HttpPost]
    //[ValidateAntiForgeryToken]
    //public async Task<IActionResult> Edit(Project project) 
    //{
    //    var existingProject = _context.Projects.Find(project.Id);
    //    if (existingProject == null)
    //    {
    //        return NotFound();
    //    }
    //    try
    //    {
    //        existingProject.Name = project.Name;
    //        existingProject.Description = project.Description;
    //        var result = await _context.SaveChangesAsync();
    //        if (result > 0)
    //        {
    //            ModelState.AddModelError(string.Empty, "Project updated successfully!");
    //        }
    //        else
    //        {
    //            ModelState.AddModelError(string.Empty, "Failed to update project.");
    //        }
    //    }
    //    catch (Exception)
    //    {
    //        ModelState.AddModelError(string.Empty, "An unexpected error occurred while updating the project.");
    //    }
    //    return RedirectToAction("Index");
    //}

    //[HttpPost]
    //[ValidateAntiForgeryToken]
    //public IActionResult Delete(int id)
    //{
    //    var project = _context.Projects.Find(id);
    //    if (project == null)
    //    {
    //        return NotFound();
    //    }
    //    try
    //    {
    //        _context.Projects.Remove(project);
    //        var result = _context.SaveChanges();
    //        if (result > 0)
    //        {
    //            ModelState.AddModelError(string.Empty, "Project deleted successfully!");
    //        }
    //        else
    //        {
    //            ModelState.AddModelError(string.Empty, "Failed to delete project.");
    //        }
    //    }
    //    catch (Exception)
    //    {
    //        ModelState.AddModelError(string.Empty, "An unexpected error occurred while deleting the project.");
    //    }
    //    return RedirectToAction("Index");
    //}
}
