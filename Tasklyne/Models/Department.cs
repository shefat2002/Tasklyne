using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Tasklyne.Models;

public class Department
{
    public int Id { get; set; }
    [Required]
    [StringLength(15, ErrorMessage = "Name cannot be longer than 15 characters.")]
    public string Name { get; set; } = string.Empty;
    [StringLength(50, ErrorMessage = "Description cannot be longer than 50 characters.")]
    public string? Description { get; set; }


    // Navigation properties
    public ICollection<Employee>? Employees { get; set; } = new List<Employee>();
    public ICollection<ProjectDepartment>? ProjectDepartments { get; set; } = new List<ProjectDepartment>();

}
