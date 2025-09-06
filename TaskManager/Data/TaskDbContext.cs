using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Tasklyne.Models;

namespace Tasklyne.Data;

public class TaskDbContext : IdentityDbContext
{
    public TaskDbContext(DbContextOptions<TaskDbContext> options)
        : base(options)
    {
        
    }

    public DbSet<Employee> Employees { get; set; }
    public DbSet<Project> Projects { get; set; }
    public DbSet<Tasklist> Tasklists { get; set; }
    public DbSet<AssignTask> AssignTasks { get; set; }
    public DbSet<Department> Departments { get; set; }
}
