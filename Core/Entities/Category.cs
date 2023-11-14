using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Core.Entities;

public class Category
{
    public Category(string name)
    {
        Name = name;
    }

    public int Id { get; set; }
    [Required]
    [Column(TypeName = "varchar(20)")]
    public string Name { get; set; }
    public List<Task> Tasks { get; set; }
}