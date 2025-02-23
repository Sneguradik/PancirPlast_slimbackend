namespace PrimitiveApi.Models;

public class Application
{
    public int Id { get; set; }
    public string Name { get; set; } = String.Empty;
    public string Surname { get; set; } = String.Empty;
    public string Email { get; set; } = String.Empty;
    public string Phone { get; set; } = String.Empty;
    public string Telegram { get; set; } = String.Empty;
    public string Content { get; set; } = String.Empty;
    public DateTime DateCreated { get; set; } = DateTime.UtcNow;
}