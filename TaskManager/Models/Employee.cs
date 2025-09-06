using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Tasklyne.Models;

public class Employee
{
    public int Id { get; set; }
    [Required(ErrorMessage = "You must enter the Employee Name!")]
    [StringLength(20, ErrorMessage = "Employee Name must be less than or equal to 20 characters.")]
    public string Name { get; set; }
    [Required(ErrorMessage = "You must enter the Employee Email!")]
    [EmailAddress(ErrorMessage = "Invalid Email Address")]
    public string Email { get; set; }
    [Required(ErrorMessage = "You must enter the Employee Phone Number!")]
    [StringLength(15, ErrorMessage = "Phone number must be less than or equal to 15 characters.")]
    public string Phone { get; set; }
    public string? ProfileImagePath { get; set; }
    [NotMapped]
    public IFormFile? ProfileImage { get; set; }
    [ForeignKey("Department")]
    public int DepartmentId { get; set; }

    public Department Department { get; set; } = null!;
    [NotMapped]
    public ICollection<AssignTask> AssignTasks { get; set; } = new List<AssignTask>();
}
