using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations.Schema;

namespace Tasklyne.Models;

[PrimaryKey(nameof(ProjectId), nameof(DepartmentId))]
public class ProjectDepartment
{
    //public int Id { get; set; }
    [ForeignKey("Project")]
    public int ProjectId { get; set; }
    [ValidateNever]
    public Project Project { get; set; } = null!;
    [ForeignKey("Department")]
    public int DepartmentId { get; set; }
    [ValidateNever]
    public Department Department { get; set; } = null!;

}
