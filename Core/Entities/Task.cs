﻿using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Core.Entities;

public class Task
{
    public Task(string name, string description, int categoryId, DateTime dueDate, string userId)
    {
        Name = name;
        Description = description;
        CategoryId = categoryId;
        DueDate = dueDate;
        UserId = userId;
    }

    public int Id { get; set; }
    [Required]
    [Column(TypeName = "VARCHAR(20)")]
    public string Name { get; set; }
    [Required]
    [Column(TypeName = "Text")]
    public string Description { get; set; }
    [Required]
    public int CategoryId { get; set; }
    public Category Category { get; set; }
    [Required]
    [Column(TypeName = "Date")]
    public DateTime DueDate { get; set; }
    [Required]
    [ForeignKey("Account")]
    public string UserId { get; set; }
    public virtual Account Account { get; set; }
}