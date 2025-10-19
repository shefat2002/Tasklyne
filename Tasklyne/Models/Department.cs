using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Tasklyne.Models;

public class Department
{
    public int Id { get; set; }
    [Required]
    [StringLength(15, ErrorMessage = "Name cannot be longer than 15 characters.")]
    public string Name { get; set; } 
    [StringLength(50, ErrorMessage = "Description cannot be longer than 50 characters.")]
    public string? Description { get; set; }


    // Navigation property
    [NotMapped]
    public ICollection<Employee>? Employees { get; set; } = new List<Employee>();
    [NotMapped]
    public ICollection<TaskList>? TaskLists { get; set; } = new List<TaskList>();
    [NotMapped]
    public ICollection<Project>? Projects { get; set; } = new List<Project>();

}
