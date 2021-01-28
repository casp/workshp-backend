using System.ComponentModel.DataAnnotations;

namespace WorkShop.Models
{
  public class User
  {
    [Key]
    public int Id { get; set; }

    [Required]
    [EmailAddress]

    public string email { get; set; }

    [Required]
    [MaxLength(20)]
    [MinLength(4)]
    public string first_name { get; set; }

    [Required]
    [MaxLength(20)]
    [MinLength(4)]
    public string last_name { get; set; }

    [Url]
    public string avatar { get; set; }
  }
}
