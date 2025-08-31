using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Tasklyne.Data;

namespace Tasklyne.Controllers;

public class DashboardController : Controller
{
    private readonly RoleManager<IdentityRole> _roleManager;
    private readonly UserManager<IdentityUser> _userManager;
    private readonly TaskDbContext _context;

    public DashboardController(RoleManager<IdentityRole> roleManager, UserManager<IdentityUser> userManager, TaskDbContext context)
    {
        _roleManager = roleManager;
        _userManager = userManager;
        _context = context;
    }
    public IActionResult Index()
    {
        return View();
    }

}
