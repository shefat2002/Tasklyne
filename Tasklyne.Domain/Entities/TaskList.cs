using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Tasklyne.Domain.Entities;

public class TaskList
{
    public int Id { get; set; }
    [Required]
    [StringLength(20, ErrorMessage = "Name cannot be longer than 20 characters.")]
    public string Name { get; set; } = string.Empty;
    [StringLength(30, ErrorMessage = "Description cannot be longer than 30 characters.")]
    public string? Description { get; set; }
    public DateTime CreatedAt { get; set; } = DateTime.Now;
    public DateTime? DueDate { get; set; } = DateTime.Now.AddDays(7);
    [StringLength(20, ErrorMessage = "Name cannot be longer than 20 characters.")]
    public string? CreatedBy { get; set; }
    public int? ProjectId{ get; set; }

    // Navigation Properties
    [NotMapped]
    public ICollection<AssignedTask>? AssignedTasks { get; set; } = new List<AssignedTask>();
    [NotMapped]
    public ICollection<Project>? Projects { get; set; } = new List<Project>();



}
