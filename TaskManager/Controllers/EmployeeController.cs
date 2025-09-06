using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Tasklyne.Data;
using Tasklyne.Models;

namespace Tasklyne.Controllers;

//[Authorize(Roles = "Admin")]
public class EmployeeController : Controller
{
    private readonly TaskDbContext _dbContext;
    private readonly UserManager<IdentityUser> _userManager;

    public EmployeeController(TaskDbContext dbContext, UserManager<IdentityUser> userManager)
    {
        _dbContext = dbContext;
        _userManager = userManager;
    }
    
    public IActionResult Index()
    {
        var users = _dbContext.Employees.ToList();
        return View(users);
    }
    public IActionResult Create()
    {
        return View();
    }
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Create(Employee user)
    {
        if (ModelState.IsValid)
        {
            if (user.ProfileImage != null)
            {
                string fileName = Path.GetFileName(user.ProfileImage.FileName);
                string extension = Path.GetExtension(fileName);
                string isExist = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "Pictures");
                if (!Directory.Exists(isExist))
                {
                    Directory.CreateDirectory(isExist);
                }
                string filePath = Path.Combine(isExist, user.Id + extension);
                if (extension != ".jpg" && extension != ".png" && extension != ".jpeg")
                {
                    ModelState.AddModelError("Picture", "Only .jpg, .png, and .jpeg files are allowed.");
                    return View(user);
                }
                using (var stream = new FileStream(filePath, FileMode.Create))
                {
                    user.ProfileImage.CopyTo(stream);
                }
                user.ProfileImagePath = "/Pictures/" + user.Id + extension;
            }
            await _dbContext.Employees.AddAsync(user);
            var result = await _dbContext.SaveChangesAsync();
            if (result > 0)
            {
                string defaultPassword = $"{user.Name}@123Aa"; // Set a default password
                var identityUser = new IdentityUser
                {
                    UserName = user.Email,
                    Email = user.Email,
                    PhoneNumber = user.Phone                    
                    
                };
                var identityResult = await _userManager.CreateAsync(identityUser, defaultPassword);
                if (identityResult.Succeeded)
                {
                    await _userManager.AddToRoleAsync(identityUser, "Employee");
                }
                else
                {
                    ModelState.AddModelError(string.Empty, "Failed to create user in identity system.");
                    return View(user);
                }
                return RedirectToAction("Index");
            }
            ModelState.AddModelError(string.Empty, "Failed to create employee.");
        }
        else
        {
            ModelState.AddModelError(string.Empty, "Please correct the errors and try again.");
        }
        return View(user);
    }
    [HttpGet]
    public IActionResult edit(int id)
    {
        if (id == null || id == 0)
        {
            return NotFound();
        }
        var user = _dbContext.Employees.Find(id);
        if (user == null)
        {
            return NotFound();
        }
        return View(user);
    }
    //edit
    [HttpPost]
    public async Task<IActionResult> Edit(Employee user)
    {
        
        if(ModelState.IsValid)
        {
            var existingUser = await _dbContext.Employees.FindAsync(user.Id);
            if (existingUser != null)
            {
                existingUser.Name = user.Name;
                existingUser.Email = user.Email;
                existingUser.Phone = user.Phone;

                if (user.ProfileImage != null)
                {
                    string fileName = Path.GetFileName(user.ProfileImage.FileName);
                    string extension = Path.GetExtension(fileName);
                    string isExist = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "Pictures");
                    if (!Directory.Exists(isExist))
                    {
                        Directory.CreateDirectory(isExist);
                    }
                    string filePath = Path.Combine(isExist, user.Id + extension);
                    if (extension != ".jpg" && extension != ".png" && extension != ".jpeg")
                    {
                        ModelState.AddModelError("Picture", "Only .jpg, .png, and .jpeg files are allowed.");
                        return View(user);
                    }
                    using (var stream = new FileStream(filePath, FileMode.Create))
                    {
                        user.ProfileImage.CopyTo(stream);
                    }
                    existingUser.ProfileImagePath = "/Pictures/" + user.Id + extension;
                }
                _dbContext.Employees.Update(existingUser);
                var result = await _dbContext.SaveChangesAsync();
                if(result > 0)
                {
                    return RedirectToAction("Index");
                }
                ModelState.AddModelError(string.Empty, "Failed to update employee."); 
                return View(user);
            }
        }
        return View(user);
    }

    //delete
    public async Task<IActionResult> Delete(int? id)
    {
        if (id == null || id == 0)
        {
            return NotFound();
        }
        var user = await _dbContext.Employees.FindAsync(id);
        if (user == null)
        {
            return NotFound();
        }
        _dbContext.Employees.Remove(user);
        await _dbContext.SaveChangesAsync();
        return RedirectToAction("Index");
    }
    //details
    public async Task<IActionResult> Details(int? id)
    {
        if (id == null || id == 0)
        {
            return NotFound();
        }
        var user = await _dbContext.Employees.FindAsync(id);
        if (user == null)
        {
            return NotFound();
        }
        return View(user);
    }

}