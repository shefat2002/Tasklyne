using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Tasklyne.Domain.Entities;

namespace Tasklyne.Infrastructure.Data;

public class ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : IdentityDbContext<Employee>(options)
{
    public DbSet<AssignedTask> AssignedTasks { get; set; }
    public DbSet<Department> Departments { get; set; }
    public DbSet<Project> Projects { get; set; }
    public DbSet<ProjectDepartment> ProjectDepartments { get; set; }
    public DbSet<ProjectTask> ProjectTasks { get; set;  }
    
}
