using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace Tasklyne.Controllers;

//[Authorize(Roles = "Admin")]
public class ManageRoleController : Controller
{
    private RoleManager<IdentityRole> _roleManager;
    public ManageRoleController(RoleManager<IdentityRole> roleManager)
    {
        _roleManager = roleManager;
    }
    public IActionResult Index()
    {
        // This method will return a view that lists all roles
        var roles = _roleManager.Roles.OrderBy(r => r.Name).ToList();
        return View(roles);
    }
    public IActionResult Create()
    {
        // This method will return a view to create a new role
        return View();
    }
    [HttpPost]
    public async Task<IActionResult> Create(string roleName)
    {
        // This method will return a view to create a new role
        var result = await _roleManager.CreateAsync(new IdentityRole(roleName));
        if (result.Succeeded)
        {
            return RedirectToAction("Index");
        }
        else
        {
            string msg = "";
            foreach (var error in result.Errors)
            {
                //  msg += error.Code +  error.Description+ "\n";
                msg += $"{error.Code} - {error.Description} \n";
            }
            ViewBag.Msg = msg;
        }
        return View();
    }

    [HttpGet]
    public IActionResult Edit(string id)
    {
        var role = _roleManager.FindByIdAsync(id).Result;
        if (role != null)
        {
            return View(role);
        }
        return RedirectToAction("Index");
    }
    [HttpPost]
    public async Task<IActionResult> Edit(string id, string roleName)
    {
        var role = await _roleManager.FindByIdAsync(id);
        if (role != null)
        {
            role.Name = roleName;
            var result = await _roleManager.UpdateAsync(role);
            if (result.Succeeded)
            {
                return RedirectToAction("Index");
            }
            else
            {
                string msg = "";
                foreach (var error in result.Errors)
                {
                    //  msg += error.Code +  error.Description+ "\n";
                    msg += $"{error.Code} - {error.Description} \n";
                }
                ViewBag.Msg = msg;
            }
            return View(role);
        }
        return RedirectToAction("Index");
    }
    [HttpPost]
    public async Task<IActionResult> Delete(string id)
    {
        var role = await _roleManager.FindByIdAsync(id);
        if (role != null)
        {
            var result = await _roleManager.DeleteAsync(role);
            if (result.Succeeded)
            {
                return RedirectToAction("Index");
            }
            else
            {
                string msg = "";
                foreach (var error in result.Errors)
                {
                    //  msg += error.Code +  error.Description+ "\n";
                    msg += $"{error.Code} - {error.Description} \n";
                }
                ViewBag.Msg = msg;
                return RedirectToAction("Index");
            }
        }
        return RedirectToAction("Index");
    }

}
