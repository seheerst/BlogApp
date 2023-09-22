using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace BlogApp.Models;

public class RegisterViewModel
{
    [Required]
    [DisplayName("Kullanıcı Adı")]
    public string? UserName { get; set; }
    
    [Required]
    [DisplayName("Ad Soyad")]
    public string? Name { get; set; }
    
    [Required]
    [EmailAddress]
    [DisplayName("E-Posta Adresi")]
    public string? Email { get; set; }
    
    [Required]
    [StringLength(10, ErrorMessage = "{0} alanı en az {2} karakter uzunluğunda olmalıdır", MinimumLength = 6)]
    [DisplayName("Şifre")]
    [DataType(DataType.Password)]
    public string? Password { get; set; }
    
    [Required]
    [Compare("Password", ErrorMessage = "Şifreler eşleşmiyor")]
    [DisplayName("Şifre Tekrar")]
    [DataType(DataType.Password)]
    public string? ConfirmPassword { get; set; }
}