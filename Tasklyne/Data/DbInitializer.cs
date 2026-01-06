using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Tasklyne.Models;

namespace Tasklyne.Data;

public class DbInitializer
{
    public static async Task Initialize(IServiceProvider serviceProvider)
    {
        var context = serviceProvider.GetRequiredService<ApplicationDbContext>();
        var userManager = serviceProvider.GetRequiredService<UserManager<Employee>>();
        var roleManager = serviceProvider.GetRequiredService<RoleManager<IdentityRole>>();
        
        
        string[] roleNames = ["Admin", "Manager", "Employee"];

        foreach (var roleName in roleNames)
        {
            var roleExist = await roleManager.RoleExistsAsync(roleName);
            if(!roleExist)
            {
                await roleManager.CreateAsync(new IdentityRole(roleName));
            }
        }
        
        var adminDept = await context.Departments.FirstOrDefaultAsync(d => d.Name == "Administration");
        if (adminDept == null)
        {
            adminDept = new Department
            {
                Name = "Administration",
                Description = "Default department for Admins"
            };
            context.Departments.Add(adminDept);
            await context.SaveChangesAsync(); // Save to generate the ID
        }

        var adminEmail = "admin@email.com";
        var adminUser = await userManager.FindByEmailAsync(adminEmail);

        if (adminUser == null)
        {
            var newAdmin = new Employee
            {
                UserName = adminEmail,
                Email = adminEmail,
                FullName = "Admin User",
                Nickname = "admin",
                EmailConfirmed = true,
                DepartmentId = adminDept.Id
            };
            var createAdmin = await userManager.CreateAsync(newAdmin, "Admin@123");
            if (createAdmin.Succeeded)
            {
                await userManager.AddToRoleAsync(newAdmin, "Admin");
            }
        }
    }
}