using Api.Models;

namespace Api.Dto;

public class ApplicationDto
{
    public string Name { get; set; } = String.Empty;
    public string Surname { get; set; } = String.Empty;
    public string Email { get; set; } = String.Empty;
    public string Phone { get; set; } = String.Empty;
    public string Telegram { get; set; } = String.Empty;
    public string Content { get; set; } = String.Empty;
    public ContactFormType PreferredContact { get; set; } = ContactFormType.Telegram;
}