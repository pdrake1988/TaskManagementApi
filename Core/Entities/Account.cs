using Microsoft.AspNetCore.Identity;

namespace Core.Entities;

public class Account : IdentityUser
{
    public List<Task> Tasks { get; set; }
}