using Microsoft.AspNetCore.Mvc;
using Tasklyne.Data;

namespace Tasklyne.Controllers;

public class DepartmentController : Controller
{
    private readonly TaskDbContext _context;
    public DepartmentController(TaskDbContext context)
    {
        _context = context;
    }
    public IActionResult Index()
    {
        var list = _context.Departments.ToList();
        return View(list);
    }
    public IActionResult Create()
    {
        return View();
    }
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Create(Models.Department department)
    {
        if (!ModelState.IsValid)
        {
            return View(department);
        }
        try
        {
            _context.Departments.Add(department);
            var result = await _context.SaveChangesAsync();
            if (result > 0)
            {
                return RedirectToAction("Index");
            }
            else
            {
                ModelState.AddModelError(string.Empty, "Failed to create department.");
                return View(department);
            }
        }
        catch (Exception)
        {
            ModelState.AddModelError(string.Empty, "An unexpected error occurred while creating the department.");
            return View(department);
        }
    }
    public IActionResult Edit(int id)
    {
        var department = _context.Departments.Find(id);
        if (department == null)
        {
            return NotFound();
        }
        return View(department);
    }
    
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Edit(Models.Department department)
    {
        if (!ModelState.IsValid)
        {
            return View(department);
        }
        try
        {
            _context.Departments.Update(department);
            var result = await _context.SaveChangesAsync();
            if (result > 0)
            {
                return RedirectToAction("Index");
            }
            else
            {
                ModelState.AddModelError(string.Empty, "Failed to update department.");
                return View(department);
            }
        }
        catch (Exception)
        {
            ModelState.AddModelError(string.Empty, "An unexpected error occurred while updating the department.");
            return View(department);
        }
    }

    public IActionResult Delete(int id)
    {
        var department = _context.Departments.Find(id);
        if (department == null)
        {
            return NotFound();
        }
        _context.Departments.Remove(department);
        _context.SaveChanges();
        return RedirectToAction("Index");
    }

}
