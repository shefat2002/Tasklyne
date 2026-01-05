using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Tasklyne.Models;

public class ProjectTask
{
    public int Id { get; set; }
    [Required]
    [StringLength(20, ErrorMessage = "Name cannot be longer than 20 characters.")]
    public string Name { get; set; } = string.Empty;
    [StringLength(30, ErrorMessage = "Description cannot be longer than 30 characters.")]
    public string? Description { get; set; }
    public DateTime CreatedAt { get; set; } = DateTime.Now;
    public DateTime? DueDate { get; set; } = DateTime.Now.AddDays(7);
    [ForeignKey("Creator")]
    public string CreatedById { get; set; } = string.Empty;
    
    [ForeignKey("Project")]
    public int? ProjectId{ get; set; }



    // Navigation Properties
    [ValidateNever]
    public Project? Project { get; set; }
    [ValidateNever]
    public Employee? CreatedBy { get; set; }
    public ICollection<AssignedTask>? AssignedTasks { get; set; } = new List<AssignedTask>();



}
