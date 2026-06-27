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
            HtmlBody =
                $@"
                <h2>Welcome to Cold Start!</h2>
                <p>Thanks for signing up. Please confirm your email address by clicking the link below.</p>
                <a href='{confirmationLink}'>Confirm Email</a>
                <p>If you didn't create an account at coldstart.fun, you can safely ignore this email.</p>
            ",
        };

        await _resend.EmailSendAsync(message);
    }
}
