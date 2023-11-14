using System.ComponentModel.DataAnnotations;

namespace TaskManagementApi.Models;

public class RegisterModel
{
    public RegisterModel(string userName, string emailAddress, string password)
    {
        UserName = userName;
        EmailAddress = emailAddress;
        Password = password;
    }

    [Required(ErrorMessage = "Username is Required")]
    public string UserName { get; set; }
    [Required(ErrorMessage = "Email is Required")]
    [EmailAddress(ErrorMessage = "Invalid Email Address")]
    public string EmailAddress { get; set; }
    [Required(ErrorMessage = "Password is Required")]
    public string Password { get; set; }
}