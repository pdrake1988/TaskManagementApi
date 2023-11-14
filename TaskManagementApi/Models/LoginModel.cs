using System.ComponentModel.DataAnnotations;

namespace TaskManagementApi.Models;

public class LoginModel
{
    public LoginModel(string username, string password)
    {
        Username = username;
        Password = password;
    }

    [Required(ErrorMessage = "Username is Required")]
    public string Username { get; set; }
    [Required(ErrorMessage = "Password is Required")]
    public string Password { get; set; }
}