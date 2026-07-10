using System.ComponentModel.DataAnnotations;

namespace ColdStart.Api.Options;

public class AuthOptions
{
    [Range(1, int.MaxValue)]
    public int TokenLifespanHours { get; set; }

    [Range(1, int.MaxValue)]
    public int ResendAccountConfirmationMailCooldownSeconds { get; set; }
}
