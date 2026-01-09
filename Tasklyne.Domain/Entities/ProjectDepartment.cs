using System.ComponentModel.DataAnnotations.Schema;

namespace Tasklyne.Domain.Entities;

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
    public Department Department { get; set; } = new Department();

    
}
