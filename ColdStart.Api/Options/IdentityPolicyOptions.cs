using System.ComponentModel.DataAnnotations;

namespace ColdStart.Api.Options;

public class IdentityPolicyOptions
{
    [Range(1, int.MaxValue)]
    public int PasswordRequiredLength { get; set; }

    [Range(1, int.MaxValue)]
    public int MaxFailedAccessAttempts { get; set; }

    [Range(1, int.MaxValue)]
    public int LockoutMinutes { get; set; }
}
