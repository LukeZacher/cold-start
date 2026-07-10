using System.ComponentModel.DataAnnotations;

namespace ColdStart.Api.Options;

public class JwtOptions
{
    [Required(AllowEmptyStrings = false)]
    public string Key { get; set; } = string.Empty;

    [Required(AllowEmptyStrings = false)]
    public string Issuer { get; set; } = string.Empty;

    [Required(AllowEmptyStrings = false)]
    public string Audience { get; set; } = string.Empty;
}
