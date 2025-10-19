using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Tasklyne.Models;

public enum TaskStatus
{
    NotStarted,
    InProgress,
    Completed,
    OnHold,
    Cancelled
}
public enum PriorityLevel
{
    Low,
    Medium,
    High
}
public enum ReviewStatus
{
    NotSubmitted,
    Pending,
    Approved,
    Rejected
}
public class AssignedTask
{
    public int Id { get; set; }
    [ForeignKey("TaskList")]
    public int TaskListId { get; set; }
    [ForeignKey("Employee")]
    public int EmployeeId { get; set; }
    public DateTime AssignedDate { get; set; } = DateTime.Now;
    public DateTime? DueDate { get; set; }
    public PriorityLevel Priority { get; set; } = PriorityLevel.Medium;
    public TaskStatus Status { get; set; } = TaskStatus.NotStarted;
    public DateTime? SubmittedDate { get; set; }
    public ReviewStatus ReviewStatus { get; set; } = ReviewStatus.NotSubmitted;
    [MaxLength(255, ErrorMessage = "The maximum length is 255 characters.")]
    public string? ReviewerComments { get; set; }
    public DateTime? ReviewedDate { get; set; }
    public string? Reviewer { get; set; }



    // Navigation Properties
    [ValidateNever]
    public TaskList? TaskList { get; set; }
    [ValidateNever]
    public Employee? Employee { get; set; }
    [NotMapped]
    public ICollection<TaskList> TaskLists { get; set; } = new List<TaskList>();
    [NotMapped]
    public ICollection<Employee> Employees { get; set; } = new List<Employee>();

}
