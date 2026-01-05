using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Tasklyne.Models;

namespace Tasklyne.Data;

public class ApplicationDbContext : IdentityDbContext
{
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
        : base(options)
    {
    }
    public DbSet<Employee> Employees { get; set; }
    public DbSet<ProjectTask> ProjectTasks { get; set; }
    public DbSet<AssignedTask> AssignedTasks { get; set; }
    public DbSet<Project> Projects { get; set; }
    public DbSet<ProjectDepartment> ProjectDepartments { get; set; }
    public DbSet<Department> Departments { get; set; }

}
