using Microsoft.AspNetCore.Mvc;
using Tasklyne.Infrastructure.Data;

namespace Tasklyne.Controllers;

public class DepartmentController : Controller
{
    private readonly ILogger<DepartmentController> _logger;
    private readonly ApplicationDbContext _context;
    
    public DepartmentController(ILogger<DepartmentController> logger, ApplicationDbContext context)
    {
        _logger = logger;
        _context = context;
    }

    public IActionResult Index()
    {
        var departments = _context.Departments.ToList();
        ViewData["Departments"] = departments;
        return View();
    }
    
    
}