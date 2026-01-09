using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Tasklyne.Controllers;

public class RolesController(ILogger<RolesController> logger, RoleManager<IdentityRole> roleManager)
    : Controller
{
    private readonly ILogger<RolesController> _logger = logger;
    private readonly RoleManager<IdentityRole> _roleManager = roleManager;

    public async Task<IActionResult> Index()
    {
        var roles = await _roleManager.Roles.ToListAsync();
        return View(roles);
    }
    
    public async Task<IActionResult> AddOrEdit(string id)
    {
        if (string.IsNullOrEmpty(id))
        {
            return PartialView(new IdentityRole());
        }
        else
        {
            var role = await _roleManager.FindByIdAsync(id);
            if (role == null) return NotFound();
            return PartialView(role);
        }
    }

    // POST: Create or Edit Role
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> AddOrEdit(string id, [Bind("Id,Name")] IdentityRole role)
    {
        if (ModelState.IsValid)
        {
            // Check if role already exists
            bool roleExists = await _roleManager.RoleExistsAsync(role.Name);
            if (roleExists && string.IsNullOrEmpty(id)) // Only if creating new
            {
                ModelState.AddModelError("Name", "Role already exists");
                return PartialView(role);
            }

            if (string.IsNullOrEmpty(id)) // Create
            {
                await _roleManager.CreateAsync(new IdentityRole(role.Name));
            }
            else // Update (Re-fetch to avoid concurrency issues)
            {
                var existingRole = await _roleManager.FindByIdAsync(id);
                if (existingRole != null)
                {
                    existingRole.Name = role.Name;
                    existingRole.NormalizedName = role.Name.ToUpper();
                    await _roleManager.UpdateAsync(existingRole);
                }
            }
            return Json(new { isValid = true });
        }
        return PartialView(role);
    }

    // POST: Delete Role
    public async Task<IActionResult> Delete(string id)
    {
        var role = await _roleManager.FindByIdAsync(id);
        if (role != null)
        {
            await _roleManager.DeleteAsync(role);
        }
        return RedirectToAction(nameof(Index));
    }
    
}