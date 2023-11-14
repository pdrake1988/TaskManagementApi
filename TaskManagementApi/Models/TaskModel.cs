using System.ComponentModel.DataAnnotations;

namespace TaskManagementApi.Models;

public class TaskModel
{
    public TaskModel(string name, string description, int categoryId, DateTime dueDate)
    {
        Name = name;
        Description = description;
        CategoryId = categoryId;
        DueDate = dueDate;
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
}