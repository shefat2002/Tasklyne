using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Tasklyne.Models
{   
    public class Employee : IdentityUser
    {
        [Required]
        [StringLength(20, ErrorMessage = "Full Name cannot be longer than 20 characters.")]
        public string FullName { get; set; }
        [Required]
        [StringLength(10, ErrorMessage = "Nickname cannot be longer than 10 characters.")]
        public string Nickname { get; set; }


        [ForeignKey("Department")]
        public int DepartmentId { get; set; }

        // Navigation property
        [ValidateNever]
        public Department Department { get; set; }
        [NotMapped]
        public ICollection<AssignedTask>? AssignedTasks { get; set; } = new List<AssignedTask>();
        [NotMapped]
        public ICollection<Department>? Departments { get; set; } = new List<Department>();
        [NotMapped]
        public ICollection<TaskList>? TaskLists { get; set; } = new List<TaskList>();

    }
}
