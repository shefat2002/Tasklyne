using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Tasklyne.Data;
using Tasklyne.Models;

namespace Tasklyne.Controllers;

public class AssignTaskController : Controller
{
    private readonly TaskDbContext _context;

    public AssignTaskController(TaskDbContext context)
    {
        _context = context;
    }
    public IActionResult Index()
    {
        var assignedTasks = _context.AssignTasks.ToList();
        return View(assignedTasks);
    }

    public IActionResult Create()
    {
        var model = new AssignTask
        {
            AssignDate = DateTime.Now,
            DueDate = DateTime.Now.AddDays(7), // Default due date is 7 days from now
            Employees = _context.Employees.OrderBy(u => u.Name).ToList(),
            Tasklists = _context.Tasklists.OrderBy(t => t.Title).ToList()
        };
        return View(model);
    }
    [HttpPost]
    [ValidateAntiForgeryToken]
    public IActionResult Create(AssignTask assignedTask, List<int> Tasklist)
    {
        try
        {
            if (ModelState.IsValid)
            {
                int result = 0;
                foreach (var taskId in Tasklist)
                {
                    var addnew = new AssignTask
                    {
                        EmployeeId = assignedTask.EmployeeId,
                        AssignDate = assignedTask.AssignDate,
                        DueDate = assignedTask.DueDate,
                        SubmitDate = assignedTask.SubmitDate,
                        Status = assignedTask.Status,
                        Remarks = assignedTask.Remarks
                    };
                    var task = _context.Tasklists.Find(taskId);
                    if (task != null)
                    {
                        addnew.Tasklist = task;
                        addnew.TaskId = task.Id;
                    }
                    _context.AssignTasks.Add(addnew);
                }
                result = _context.SaveChanges();
                if (result > 0)
                {
                    return RedirectToAction("Index");
                }
                ModelState.AddModelError("", "Failed to create assigned task. Please try again.");
            }
            else
            {
                var message = string.Join(" | ", ModelState.Values
                    .SelectMany(v => v.Errors)
                    .Select(e => e.ErrorMessage));
                ModelState.AddModelError(" ", message);
            }
        }
        catch (Exception ex)
        {
            ModelState.AddModelError("", $"An error occurred: {ex.Message}");
        }
        assignedTask.Employees = _context.Employees.OrderBy(u => u.Name).ToList();
        assignedTask.Tasklists = _context.Tasklists.OrderBy(t => t.Title).ToList();
        return View(assignedTask);
    }
    [HttpGet]
    public IActionResult Edit(int id)
    {
        var assignedTask = _context.AssignTasks.Find(id);
        if (assignedTask == null)
        {
            return NotFound();
        }
        assignedTask.Employees = _context.Employees.OrderBy(u => u.Name).ToList();
        assignedTask.Tasklists = _context.Tasklists.OrderBy(t => t.Title).ToList();
        return View(assignedTask);
    }
    [HttpPost]
    [ValidateAntiForgeryToken]
    public IActionResult Edit(AssignTask assignedTask)
    {
        try
        {
            if (ModelState.IsValid)
            {
                var existingTask = _context.AssignTasks.Find(assignedTask.Id);
                if (existingTask == null)
                {
                    return NotFound();
                }
                existingTask.EmployeeId = assignedTask.EmployeeId;
                existingTask.TaskId = assignedTask.TaskId;
                existingTask.AssignDate = assignedTask.AssignDate;
                existingTask.DueDate = assignedTask.DueDate;
                existingTask.SubmitDate = assignedTask.SubmitDate;
                existingTask.Status = assignedTask.Status;
                existingTask.Remarks = assignedTask.Remarks;
                _context.AssignTasks.Update(existingTask);
                var result = _context.SaveChanges();
                if (result > 0)
                {
                    return RedirectToAction("Index");
                }
                ModelState.AddModelError("", "Failed to update assigned task. Please try again.");
            }
            else
            {
                var message = string.Join(" | ", ModelState.Values
                    .SelectMany(v => v.Errors)
                    .Select(e => e.ErrorMessage));
                ModelState.AddModelError(" ", message);
            }
        }
        catch (Exception ex)
        {
            ModelState.AddModelError("", $"An error occurred: {ex.Message}");
        }
        assignedTask.Employees = _context.Employees.OrderBy(u => u.Name).ToList();
        assignedTask.Tasklists = _context.Tasklists.OrderBy(t => t.Title).ToList();
        return View(assignedTask);
    }
    public IActionResult Delete(int id)
    {
        var assignedTask = _context.AssignTasks.Find(id);
        if (assignedTask == null)
        {
            return NotFound();
        }
        _context.AssignTasks.Remove(assignedTask);
        var result = _context.SaveChanges();
        if (result > 0)
        {
            return RedirectToAction("Index");
        }
        ModelState.AddModelError("", "Failed to delete assigned task. Please try again.");
        return RedirectToAction("Index");
    }

}
