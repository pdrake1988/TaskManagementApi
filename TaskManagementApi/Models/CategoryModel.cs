using System.ComponentModel.DataAnnotations;

namespace TaskManagementApi.Models;

public class CategoryModel
{
    public CategoryModel(string name)
    {
        Name = name;
    }

    [Required(ErrorMessage = "Name is Required")]
    public string Name { get; set; }
}