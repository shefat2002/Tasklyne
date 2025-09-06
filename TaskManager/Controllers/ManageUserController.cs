using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Tasklyne.ViewModels;

namespace Tasklyne.Controllers;

//[Authorize(Roles = "Admin")]
public class ManageUserController : Controller
{
    private UserManager<IdentityUser> _userManager;
    private RoleManager<IdentityRole> _roleManager;
    public ManageUserController(UserManager<IdentityUser> userManager, RoleManager<IdentityRole> roleManager)
    {
        _userManager = userManager;
        _roleManager = roleManager;
    }
    public async Task<IActionResult> Index()
    {
        var userList = _userManager.Users.OrderBy(u => u.UserName).ToList();
        var userVM = new List<UserVM>();
        foreach (var user in userList)
        {
            var roles = await _userManager.GetRolesAsync(user);
            userVM.Add(new UserVM
            {
                UserId = user.Id,
                UserName = user.UserName,
                Email = user.Email,
                RoleName = string.Join(",", roles.ToList())
            });
        }

        return View(userVM);
    }
    public async Task<IActionResult> Create()
    {
        return View();
    }
    public async Task<IActionResult> AssignRole(string userId)
    {
        // This method will return a view to create a new user
        var user = await _userManager.FindByIdAsync(userId);
        ViewBag.Username = user.UserName;
        var role = _roleManager.Roles.OrderBy(r => r.Name).ToList();
        ViewBag.RoleList = role;
        return View();
    }
    [HttpPost]
    public async Task<IActionResult> AssignRole(string userId, List<string> roleName)
    {
        // This method will return a view to create a new user
        var user = await _userManager.FindByEmailAsync(userId);
        if (user != null)
        {
            try
            {
                var result = await _userManager.AddToRolesAsync(user, roleName);
                if (result.Succeeded)
                {
                    return RedirectToAction("Index");
                }
                else
                {
                    string msg = "";
                    foreach (var error in result.Errors)
                    {
                        msg += $"{error.Code} - {error.Description} \n";
                    }
                    ViewBag.Msg = msg;
                }
                // return View();
            }
            catch (Exception ex)
            {
                ViewBag.Msg = "An error occurred while assigning roles: " + ex.Message;
                //return View();
            }

        }
        else
        {

            ViewBag.Msg = "User not found.";

        }

        ViewBag.Username = user.UserName;
        var role = _roleManager.Roles.OrderBy(r => r.Name).ToList();
        ViewBag.RoleList = role;
        return View();
    }
}
