using ColdStart.Api.Services.Interfaces;
using Resend;

namespace ColdStart.Api.Services;

public class EmailService : IEmailService
{
    private readonly IResend _resend;
    private readonly IConfiguration _configuration;

    public EmailService(IResend resend, IConfiguration configuration)
    {
        _resend = resend;
        _configuration = configuration;
    }

    public async Task SendConfirmationEmailAsync(string toEmail, string confirmationLink)
    {
        EmailMessage message = new EmailMessage
        {
            From = $"{_configuration["Resend:FromName"]} <{_configuration["Resend:FromEmail"]}>",
            To = { toEmail },
            Subject = "Confirm your account - coldstart.fun",
            Template = new EmailMessageTemplate
            {
                TemplateId = Guid.Parse(
                    _configuration["Resend:RegistrationConfirmationTemplateId"]!
                ),
                Variables = new Dictionary<string, object>
                {
                    { "CONFIRMATION_LINK", confirmationLink },
                },
            },
        };

        await _resend.EmailSendAsync(message);
    }
}
