using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace BlogApp.Models;

public class LoginViewModel
{
    [Required]
    [EmailAddress]
    [DisplayName("E-Posta Adresi")]
    public string? Email { get; set; }
    
    [Required]
    [StringLength(10, ErrorMessage = "{0} alanı en az {2} karakter uzunluğunda olmalıdır", MinimumLength = 6)]
    [DisplayName("Şifre")]
    [DataType(DataType.Password)]
    public string? Password { get; set; }
}