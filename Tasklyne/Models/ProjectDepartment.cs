using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using System.ComponentModel.DataAnnotations.Schema;

namespace Tasklyne.Models;

public class ProjectDepartment
{
    public int Id { get; set; }
    [ForeignKey("Project")]
    public int ProjectId { get; set; }
    [ValidateNever]
    public Project Project { get; set; } = new Project();
    [ForeignKey("Department")]
    public int DepartmentId { get; set; }
    [ValidateNever]
    public Department Department { get; set; }

    [NotMapped]
    public ICollection<Department>? Departments { get; set; }
    [NotMapped]
    public ICollection<Project>? Projects { get; set; }
}
