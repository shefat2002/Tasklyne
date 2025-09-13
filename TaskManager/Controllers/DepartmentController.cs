using Microsoft.AspNetCore.Mvc;
using Tasklyne.Data;
using Tasklyne.Models;

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
        var model = _context.Departments.ToList();
        return View(model);
    }
    public IActionResult Get()
    {
        var departments = _context.Departments.ToList();
        return Json(departments);
    }
    public IActionResult GetbyId(int id)
    {
        var department = _context.Departments.Find(id);
        if (department == null)
        {
            return NotFound();
        }
        return Json(department);
    }
    public async Task<IActionResult> Save(Department department)
    {
        try
        {

            await _context.Departments.AddAsync(department);
            var result = await _context.SaveChangesAsync();
            if (result > 0)
            {
                return Json(new { success = true, message = "Department saved successfully." });
            }
            else
            {
                return Json(new { success = false, message = "Failed to save department." });
            }
        }
        catch (Exception)
        {
            return Json(new { success = false, message = "An unexpected error occurred while saving the department." });
        }
    }

    [HttpGet]
    public async Task<IActionResult> Update(int id)
    {
        var department = await _context.Departments.FindAsync(id);
        if (department == null)
        {
            return Json(new { success = false, message = "Department not found." });
        }
        return Json(department);
    }

    [HttpPost]
    public async Task<IActionResult> Update(Department department)
    {
        var existingDepartment = await _context.Departments.FindAsync(department.Id);
        if (existingDepartment == null)
        {
            return Json(new { success = false, message = "Department not found." });
        }
        try
        {
            existingDepartment.Name = department.Name;
            var result = await _context.SaveChangesAsync();
            if (result > 0)
            {
                return Json(new { success = true, message = "Department updated successfully." });
            }
            else
            {
                return Json(new { success = false, message = "Failed to update department." });
            }
        }
        catch (Exception)
        {
            return Json(new { success = false, message = "An unexpected error occurred while updating the department." });
        }
    }
    public async Task<IActionResult> Delete(int id)
    {
        var department = await _context.Departments.FindAsync(id);
        if (department == null)
        {
            return Json(new { success = false, message = "Department not found." });
        }
        try
        {
            _context.Departments.Remove(department);
            var result = await _context.SaveChangesAsync();
            if (result > 0)
            {
                return Json(new { success = true, message = "Department deleted successfully." });
            }
            else
            {
                return Json(new { success = false, message = "Failed to delete department." });
            }
        }
        catch (Exception)
        {
            return Json(new { success = false, message = "An unexpected error occurred while deleting the department." });
        }
    }
    
}
