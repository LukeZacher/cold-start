namespace ColdStart.Api.Models.DTOs.Auth;

public class ResendEmailConfirmationResponse
{
    public required int CooldownSeconds { get; set; }
}
