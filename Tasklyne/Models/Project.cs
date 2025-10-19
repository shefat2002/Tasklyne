﻿using System.ComponentModel.DataAnnotations;

namespace Tasklyne.Models;

public class Project
{

    public int Id { get; set; }
    [Required]
    [StringLength(20, ErrorMessage = "Project Name cannot be longer than 20 characters.")]
    public string Name { get; set; }
    [StringLength(50, ErrorMessage = "Description cannot be longer than 50 characters.")]
    public string? Description { get; set; }
    public DateTime StartDate { get; set; } = DateTime.Now;

    // Navigation Properties
    public ICollection<TaskList>? TaskLists { get; set; } = new List<TaskList>();
    public ICollection<Department>? Departments { get; set; } = new List<Department>();

}
