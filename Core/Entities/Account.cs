using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.AspNetCore.Identity;

namespace Core.Entities;

public class Account : IdentityUser
{
    [ForeignKey("UserId")]
    public virtual List<Task> Tasks { get; set; }
}