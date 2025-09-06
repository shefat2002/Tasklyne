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
        assignedTask.Employees = _context.Employees.OrderBy(u => u.Name).ToList();
        assignedTask.Tasklists = _context.Tasklists.OrderBy(t => t.Title).ToList();
        return View(assignedTask);
    }

}
