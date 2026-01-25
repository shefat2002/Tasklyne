using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;

namespace Tasklyne.Domain.Entities;

public class AssignedTask
{
    public int Id { get; set; }
    [ForeignKey("TaskList")]
    public int TaskListId { get; set; }
    [ForeignKey("Employee")]
    public string EmployeeId { get; set; } = string.Empty;
    public DateTime AssignedDate { get; set; } = DateTime.Now;
    public DateTime? DueDate { get; set; }
    public Enums.PriorityLevel Priority { get; set; } = Enums.PriorityLevel.Medium;
    public Enums.TaskStatus Status { get; set; } = Enums.TaskStatus.NotStarted;
    public DateTime? SubmittedDate { get; set; }
    public Enums.ReviewStatus ReviewStatus { get; set; } = Enums.ReviewStatus.NotSubmitted;
    [MaxLength(255, ErrorMessage = "The maximum length is 255 characters.")]
    public string? ReviewerComments { get; set; }
    public DateTime? ReviewedDate { get; set; }
    public string? Reviewer { get; set; }



    // Navigation Properties
    [ValidateNever]
    public ProjectTask? ProjectTask { get; set; } 
    [ValidateNever]
    public Employee? Employee { get; set; }

}
