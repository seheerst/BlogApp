using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using BlogApp.Entity;

namespace BlogApp.Models;

public class CreateViewModel
{
    public int PostId { get; set; }

    public bool IsActive { get; set; }
    
    [Required]
    [DisplayName("Başlık")]
    public string? Title { get; set; }
    
    [Required]
    [DisplayName("İçerik")]
    public string? Content { get; set; }
    
    [Required]
    [DisplayName("Açıklama")]
    public string? Description { get; set; }
    
    [Required]
    [DisplayName("Url")]
    public string? PostUrl { get; set; }

    public List<Tag> Tags { get; set; } = new();
}