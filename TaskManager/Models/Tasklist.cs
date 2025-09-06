using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Tasklyne.Models;

public class Tasklist
{
    public int Id { get; set; }
    [Required(ErrorMessage = "You must enter the Task Title!")]
    [StringLength(25, ErrorMessage = "Title must be less than or equal to 25 characters.")]
    public string Title { get; set; }
    [StringLength(255, ErrorMessage = "Description must be less than or equal to 255 characters.")]
    public string? Description { get; set; }
    public bool IsCompleted { get; set; } = false;
    [ForeignKey("Project")]
    public int ProjectId { get; set; }
    public Project Project { get; set; }
    public DateTime CreatedAt { get; set; } = DateTime.Now;
    [NotMapped]
    [ValidateNever]
    public List<Project> ProjectList { get; set; }
    [NotMapped]
    public ICollection<AssignTask> AssignTasks { get; set; } = new List<AssignTask>();
    

}
