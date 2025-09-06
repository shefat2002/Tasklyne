using System.ComponentModel.DataAnnotations;

namespace Tasklyne.Models;

public class Department
{
    public int Id { get; set; }
    [Required]
    [StringLength(10, ErrorMessage = "Name cannot be longer than 10 characters.")]
    public string Name { get; set; }
}
