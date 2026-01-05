using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Tasklyne.Models;

public class Employee : IdentityUser
{
    [Required]
    [StringLength(20, ErrorMessage = "Full Name cannot be longer than 20 characters.")]
    public string FullName { get; set; } = string.Empty;
    [Required]
    [StringLength(10, ErrorMessage = "Nickname cannot be longer than 10 characters.")]
    public string Nickname { get; set; } = string.Empty;


    [ForeignKey("Department")]
    public int? DepartmentId { get; set; }

    // Navigation property
    [ValidateNever]
    public Department? Department { get; set; }
    public ICollection<AssignedTask>? AssignedTasks { get; set; } = new List<AssignedTask>();
    public ICollection<AssignedTask> ReviewedTasks { get; set; } = new List<AssignedTask>();
    public ICollection<ProjectTask>? TaskLists { get; set; } = new List<ProjectTask>();

}
