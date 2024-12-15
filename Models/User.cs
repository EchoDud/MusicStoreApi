using System.ComponentModel.DataAnnotations;

public class User
{
    public int Id { get; set; }

    [Required]
    [MaxLength(100)]
    public string Email { get; set; } = null!;

    [Required]
    public string PasswordHash { get; set; } = null!;

    [Required]
    [MaxLength(50)]
    public string Role { get; set; } = "Client"; // Роль по умолчанию: клиент
}
