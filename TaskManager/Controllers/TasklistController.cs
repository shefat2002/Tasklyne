using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

using Tasklyne.Data;
using Tasklyne.Models;

namespace Tasklyne.Controllers;

public class TasklistController : Controller
{
    private readonly TaskDbContext _context;
    public TasklistController(TaskDbContext context)
    {
        _context = context;
    }
    public IActionResult Index()
    {
        var tasklists = _context.Tasklists.ToList();
        return View(tasklists);
    }
    public IActionResult Create()
    {
        var model = new Tasklist
        {
            CreatedAt = DateTime.Now,
            IsCompleted = false,
            ProjectList = _context.Projects.OrderBy(o => o.Name).ToList()

        };

        return View(model);
    }
    [HttpPost]
    [ValidateAntiForgeryToken]
    public IActionResult Create(Tasklist tasklist, List<int> Projectlist)
    {
        var result = 0;
        try
        {
            if (ModelState.IsValid)
            {
                if (Projectlist != null && Projectlist.Count > 0)
                {
                    foreach (var projectId in Projectlist)
                    {
                        var addnew = new Tasklist
                        {
                            CreatedAt = tasklist.CreatedAt,
                        };
                        var project = _context.Projects.Find(projectId);
                        if (project != null)
                        {
                            addnew.Project = project;
                            addnew.ProjectId = project.Id;
                        }
                        _context.Tasklists.Add(addnew);
                    }
                }
                result = _context.SaveChanges();
                if (result > 0)
                {
                    TempData["SuccessMsg"] = "Task created successfully!";
                }
                else
                {
                    TempData["ErrorMsg"] = "Failed to create task.";
                }
                return RedirectToAction("Index");
            }
        }
        catch (Exception ex)
        {
            TempData["ErrorMsg"] = $"An error occurred: {ex.Message}";
        }
        return View(tasklist);
    }

    [HttpGet]
    public IActionResult Edit(int id)
    {
        var tasklist = _context.Tasklists.Include(t => t.Project).FirstOrDefault(t => t.Id == id);
        if (tasklist == null)
        {
            return NotFound();
        }
        tasklist.ProjectList = _context.Projects.OrderBy(o => o.Name).ToList();
        return View(tasklist);
    }
    [HttpPost]
    [ValidateAntiForgeryToken]
    public IActionResult Edit(Tasklist tasklist)
    {
        var result = 0;
        try
        {
            if (ModelState.IsValid)
            {
                var existingTasklist = _context.Tasklists.Find(tasklist.Id);
                if (existingTasklist != null)
                {
                    existingTasklist.IsCompleted = tasklist.IsCompleted;
                    existingTasklist.CreatedAt = tasklist.CreatedAt;
                    existingTasklist.ProjectId = tasklist.ProjectId;
                    _context.Tasklists.Update(existingTasklist);
                    result = _context.SaveChanges();
                    if (result > 0)
                    {
                        TempData["SuccessMsg"] = "Task updated successfully!";
                    }
                    else
                    {
                        TempData["ErrorMsg"] = "Failed to update task.";
                    }
                }
                return RedirectToAction("Index");
            }
        }
        catch (Exception ex)
        {
            TempData["ErrorMsg"] = $"An error occurred: {ex.Message}";
        }
        tasklist.ProjectList = _context.Projects.OrderBy(o => o.Name).ToList();
        return View(tasklist);
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public IActionResult Delete(int id)
    {
        var result = 0;
        try
        {
            var tasklist = _context.Tasklists.Find(id);
            if (tasklist != null)
            {
                _context.Tasklists.Remove(tasklist);
                result = _context.SaveChanges();
                if (result > 0)
                {
                    TempData["SuccessMsg"] = "Task deleted successfully!";
                }
                else
                {
                    TempData["ErrorMsg"] = "Failed to delete task.";
                }
            }
            else
            {
                TempData["ErrorMsg"] = "Task not found.";
            }
        }
        catch (Exception ex)
        {
            TempData["ErrorMsg"] = $"An error occurred: {ex.Message}";
        }
        return RedirectToAction("Index");
    }

}
