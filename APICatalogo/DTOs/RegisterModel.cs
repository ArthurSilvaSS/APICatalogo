using System.ComponentModel.DataAnnotations;

namespace APICatalogo.DTOs;
    public class RegisterModel
    {
    [Required(ErrorMessage = "User name is required")]
    public string? UserName { get; set; }

    [EmailAddress]
    [Required(ErrorMessage = "Email is required")]
    public string? Email { get; set; }

    [Required(ErrorMessage = "PassWord is required")]
    public string? PassWord { get; set; }
}

