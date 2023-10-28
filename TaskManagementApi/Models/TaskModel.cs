using System.ComponentModel.DataAnnotations;

namespace TaskManagementApi.Models;

public class TaskModel
{
    public TaskModel(string name, string description, int categoryId, DateTime dueDate, int userId)
    {
        Name = name;
        Description = description;
        CategoryId = categoryId;
        DueDate = dueDate;
        UserId = userId;
    }

    [Required]
    public string Name { get; set; }
    [Required]
    public string Description { get; set; }
    [Required]
    public int CategoryId { get; set; }
    [Required]
    [DisplayFormat(DataFormatString = "{0:MM/dd/yyyy}")]
    public DateTime DueDate { get; set; }
    [Required]
    public int UserId { get; set; }
}