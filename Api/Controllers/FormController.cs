using System.Text;
using Api.Dto;
using Api.Models;
using Api.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Api.Controllers
{
    [Route("contact_application")]
    [ApiController]
    public class FormController(MainContext context, IBotService botService) : ControllerBase
    {
        [HttpPost("add")]
        public async Task<IActionResult> AddApplication([FromBody] ApplicationDto dto)
        {
            var res = context.Applications.Add(new Application()
            {
                Name = dto.Name,
                Surname = dto.Surname,
                Email = dto.Email,
                Phone = dto.Phone,
                Telegram = dto.Telegram,
                Content = dto.Content,
                PreferredContact = dto.PreferredContact
            });
            await context.SaveChangesAsync();
            var strBuilder = new StringBuilder();
            strBuilder.AppendLine("Новая заявка");
            strBuilder.AppendLine($"ID: {res.Entity.Id}");
            strBuilder.AppendLine($"Фамилия: {res.Entity.Surname}");
            strBuilder.AppendLine($"Имя: {res.Entity.Name}");
            strBuilder.AppendLine($"Предпочитаемый способ связи: {res.Entity.PreferredContact}");
            strBuilder.AppendLine($"Телефон: {res.Entity.Phone}");
            strBuilder.AppendLine($"Почта: {res.Entity.Email}");
            strBuilder.AppendLine($"Телеграм: {res.Entity.Telegram}");
            strBuilder.AppendLine($"Контент: {res.Entity.Content}");
            strBuilder.AppendLine($"Дата: {res.Entity.DateCreated}");
            await botService.SendAll(strBuilder.ToString());
            return Ok("Application added");
        }
    }
}
