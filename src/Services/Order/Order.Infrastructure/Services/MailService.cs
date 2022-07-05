using Microsoft.Extensions.Logging;
using Order.Application.Contracts.Services;
using Order.Application.Models;

namespace Order.Infrastructure.Services;

public class MailService : IEmailService
{
	private readonly ILogger<MailService> _logger;

	public MailService(ILogger<MailService> logger)
	{
		_logger = logger;
	}

	public Task<bool> SendEmailAsync(EmailRequest emailRequest)
	{
		// Fake mail service

		_logger.LogInformation("Sending email to {To}, {Subject}", emailRequest.To, emailRequest.Subject);

		Thread.Sleep(2000);
		return Task.FromResult(true);
	}
}