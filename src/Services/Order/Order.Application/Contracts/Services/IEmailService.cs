using Order.Application.Models;

namespace Order.Application.Contracts.Services;

public interface IEmailService
{
    Task<bool> SendEmailAsync(EmailRequest emailRequest);
}