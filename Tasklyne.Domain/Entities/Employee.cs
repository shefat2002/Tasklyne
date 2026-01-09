using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.AspNetCore.Identity;

namespace Tasklyne.Domain.Entities
{   
    public class Employee : IdentityUser
    {
        [Required]
        [StringLength(20, ErrorMessage = "Full Name cannot be longer than 20 characters.")]
        public string FullName { get; set; } = string.Empty;
        [Required]
        [StringLength(10, ErrorMessage = "Nickname cannot be longer than 10 characters.")]
        public string Nickname { get; set; } = string.Empty; 


        [ForeignKey("Department")]
        public int DepartmentId { get; set; }

        // Navigation property
        
        // An Employee belongs to one Department
        public Department? Department { get; set; }
        // Employee can be assigned multiple ProjectTasks
        public ICollection<AssignedTask>? AssignedTasks { get; set; } = new List<AssignedTask>();
        // Employee can create multiple ProjectTasks
        public ICollection<TaskList>? CreatedTasks { get; set; } = new List<TaskList>();

    }
}
