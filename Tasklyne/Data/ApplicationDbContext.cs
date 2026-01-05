using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Tasklyne.Models;

namespace Tasklyne.Data;

public class ApplicationDbContext : IdentityDbContext<Employee>
{
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
        : base(options)
    {
    }
    public DbSet<AssignedTask> AssignedTasks { get; set; }
    public DbSet<Department> Departments { get; set; }
    public DbSet<Project> Projects { get; set; }
    public DbSet<ProjectDepartment> ProjectDepartments { get; set; }
    public DbSet<ProjectTask> ProjectTasks { get; set;  }
    
}
