using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using static Tasklyne.Models.Enums;

namespace Tasklyne.Models;

[PrimaryKey(nameof(TaskListId), nameof(EmployeeId))]
public class AssignedTask
{
    //public int Id { get; set; }
    [ForeignKey("TaskList")]
    public int TaskListId { get; set; }
    [ForeignKey("Employee")]
    public string EmployeeId { get; set; } = string.Empty;
    public DateTime AssignedDate { get; set; } = DateTime.Now;
    public DateTime? DueDate { get; set; }
    public PriorityLevel Priority { get; set; } = PriorityLevel.Medium;
    public Enums.TaskStatus Status { get; set; } = Enums.TaskStatus.NotStarted;
    public DateTime? SubmittedDate { get; set; }
    public ReviewStatus ReviewStatus { get; set; } = ReviewStatus.NotSubmitted;
    [MaxLength(255, ErrorMessage = "The maximum length is 255 characters.")]
    public string? ReviewerComments { get; set; }
    public DateTime? ReviewedDate { get; set; }
    [ForeignKey("Reviewer")]
    public string? ReviewerId { get; set; }




    // Navigation Properties
    [ValidateNever]
    [InverseProperty("ReviewedTasks")]
    public Employee? Reviewer { get; set; }
    [ValidateNever]
    public ProjectTask? ProjectTask { get; set; }
    [ValidateNever]
    public Employee? Employee { get; set; }

}
