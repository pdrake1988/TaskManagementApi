using System.ComponentModel.DataAnnotations;

namespace TaskManagementApi.Models;

public class AccountModel
{
    public AccountModel(string userName, string password)
    {
        UserName = userName;
        Password = password;
    }

    public string UserName { get; set; }
    [Required(ErrorMessage = "Email is Required")]
    [EmailAddress(ErrorMessage = "Invalid Email Address")]
    public string EmailAddress { get; set; }
    [Required(ErrorMessage = "Password is Required")]
    public string Password { get; set; }
}