using System.ComponentModel.DataAnnotations;

namespace ColdStart.Api.Options;

public class ResendOptions
{
    [Required(AllowEmptyStrings = false)]
    public string FromName { get; set; } = string.Empty;

    [Required(AllowEmptyStrings = false)]
    public string FromEmail { get; set; } = string.Empty;

    [Required(AllowEmptyStrings = false)]
    public string ApiKey { get; set; } = string.Empty;

    [Required(AllowEmptyStrings = false)]
    public string RegistrationConfirmationTemplateId { get; set; } = string.Empty;
}
