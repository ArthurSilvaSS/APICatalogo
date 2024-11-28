using System.ComponentModel.DataAnnotations;

namespace APICatalogo.DTOs;
    public class LoginModel
    {
        [Required(ErrorMessage = "User name is required")]
        public string? UserName { get; set; }

        [Required(ErrorMessage = "PassWord is required")]
        public string? PassWord { get; set; }
}

